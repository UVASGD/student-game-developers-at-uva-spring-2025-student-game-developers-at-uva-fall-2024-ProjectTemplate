using Unity.VisualScripting;
using UnityEngine;

public enum AbilityFireType
{
    TAP,
    HOLD
}
public enum AbilityHoldAction
{
    HOLD_START,
    HOLD_END
}
public abstract class AbilityBase
{
    public string abilityName { get; private set; }
    public KeyCode activationKey { get; private set; }
    public float cooldownTime { get; private set; }
    public int level { get; set; }
    public Sprite AbilityIcon { get; protected set; }
    public AbilityFireType abilityFireType { get; private set; }
    public float lastActivatedTime { get; protected set; }
    public float holdTime { get; private set; }

    private bool holding = false;
    private float holdRequirement = 2f;

    private Vector3? projectedHitPoint = null;

    protected LineRenderer LineRenderer;
    protected int LinePoints = 25;
    protected float TimeBetweenPoints = 0.1f;
    protected float LaunchForce = 15f;
    protected float SimulatedMass = 1f;
    protected LayerMask CollisionMask;

    public AbilityBase(string name, KeyCode key, float cTime, AbilityFireType fireType)
    {
        abilityName = name;
        activationKey = key;
        cooldownTime = cTime;
        level = 1;
        abilityFireType = fireType;

        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(0, i))
            {
                CollisionMask |= 1 << i;
            }
        }
    }

    public void SetLineRenderer(LineRenderer renderer)
    {
        LineRenderer = renderer;
    }

    public void Activate()
    {

        // Check if the current game phase allows ability activation
        if (RoundManager.Instance.GetCurrentRoundPhase() != RoundManager.RoundPhase.EnemiesSpawning &&
            RoundManager.Instance.GetCurrentRoundPhase() != RoundManager.RoundPhase.EnemiesNoLongerSpawning)
        {
            Debug.Log($"{abilityName} cannot be activated during the {RoundManager.Instance.GetCurrentRoundPhase()} phase.");
            return;
        }


        if (Time.time >= lastActivatedTime + cooldownTime)
        {
            lastActivatedTime = Time.time;
            Execute();
        }
        else
        {
            Debug.Log($"{abilityName} is on cooldown.");
        }
    }

    public void ToggleHold(AbilityHoldAction holdAction)
    {
        if (RoundManager.Instance.GetCurrentRoundPhase() != RoundManager.RoundPhase.EnemiesSpawning &&
            RoundManager.Instance.GetCurrentRoundPhase() != RoundManager.RoundPhase.EnemiesNoLongerSpawning)
        {
            Debug.Log($"{abilityName} cannot be activated during the {RoundManager.Instance.GetCurrentRoundPhase()} phase.");
            return;
        }

        if (Time.time >= lastActivatedTime + cooldownTime)
        {

            if (holdAction == AbilityHoldAction.HOLD_START && !holding)
            {
                holdTime = Time.time;
                holding = true;
                EnableTrajectoryLine();
            }
            else if (holdAction == AbilityHoldAction.HOLD_END)
            {
                lastActivatedTime = Time.time;
                holding = false;
                DisableTrajectoryLine();

                if (Time.time >= holdTime + holdRequirement && LineRenderer != null && projectedHitPoint.HasValue)
                {
                    float timeHeld = Time.time - holdTime;
                    HoldExecute(timeHeld, projectedHitPoint.Value);
                }
                else
                {
                    Debug.LogWarning($"[Ability: {abilityName}] Called off because:");

                    if (Time.time < holdTime + holdRequirement)
                        Debug.LogWarning("- Not held long enough");

                    if (LineRenderer == null)
                        Debug.LogWarning("- LineRenderer is NULL");

                    if (!projectedHitPoint.HasValue)
                        Debug.LogWarning("- No valid projected hit point");

                    lastActivatedTime = 0;
                }

                holdTime = 0;
            }
            else if (holding)
            {
                UpdateTrajectory();
            }
        }
        else
        {
            Debug.Log($"{abilityName} is on cooldown.");
        }
    }

    public void UpdateTrajectory()
    {
        if (holding && LineRenderer != null)
        {
            DrawTrajectory();
        }
    }

    private void DrawTrajectory()
    {
        LineRenderer.enabled = true;
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;

        Vector3 startPosition = GameObject.FindFirstObjectByType<Player>().transform.position + Vector3.up * 1f;
        Vector3 startVelocity = LaunchForce * Camera.main.transform.forward / SimulatedMass;

        int i = 0;
        LineRenderer.SetPosition(i, startPosition);
        projectedHitPoint = null;

        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
            LineRenderer.SetPosition(i, point);

            Vector3 lastPosition = LineRenderer.GetPosition(i - 1);
            if (Physics.Raycast(lastPosition, (point - lastPosition).normalized,
                                out RaycastHit hit,
                                (point - lastPosition).magnitude,
                                CollisionMask))
            {
                LineRenderer.SetPosition(i, hit.point);
                LineRenderer.positionCount = i + 1;
                projectedHitPoint = hit.point;
                return;
            }
        }
    }

    private void EnableTrajectoryLine()
    {
        if (LineRenderer != null)
        {
            LineRenderer.enabled = true;
        }
    }

    private void DisableTrajectoryLine()
    {
        if (LineRenderer != null)
        {
            LineRenderer.enabled = false;
        }
    }

    protected abstract void Execute();
    public abstract void UpgradeAbility();
    protected abstract void HoldExecute(float timeHeld, Vector3 targetPos);
}