using UnityEngine;

public class ufoAI : MonoBehaviour
{
    public enum State { Searching, Chasing, SearchingLostArea, Returning, Attacking }
    public State currentState;

    public Transform player;
    public float sightRange = 20f;
    public float attackRange = 5f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 2f;
    public float searchDuration = 3f;

    private Vector3 homePosition;
    private Vector3 lastKnownPlayerPos;
    private float searchTimer;
    private bool canDetectPlayer = true;

    public GameObject laserBeam; 
    private bool beamActive = false;
    public float laserGrowSpeed = 0.5f;
    public float maxLaserScale = 1.5f;
    private Vector3 initialLaserScale;
    private bool isGrowing = true;

    // For random movement during Searching
    private Vector3 searchTarget;
    private float searchTargetReachThreshold = 0.5f;
    public float searchRadius = 5f; // How far from home it can randomly move
    private bool hasSearchTarget = false;

    // Death/respawn
    public Transform respawnPoint;
    public float laserHitRadius = 1.5f;
    private bool laserCanDamage = false;
    public float laserChargeTime = 0.5f; // Time before laser can damage
    private float laserTimer = 0f;

    void Start()
    {
        homePosition = transform.position;
        currentState = State.Searching;

        if (laserBeam != null)
        {
            initialLaserScale = laserBeam.transform.localScale;
            laserBeam.SetActive(false); // Make sure it's disabled at start
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Searching:
                DoSearching();
                // Debug.Log("Searching");
                break;

            case State.Chasing:
                DoChasing();
                // Debug.Log("Chasing");
                break;

            case State.SearchingLostArea:
                DoSearchLostArea();
                // Debug.Log("Searching lost area");
                break;

            case State.Returning:
                DoReturning();
                // Debug.Log("Returning");
                break;

            case State.Attacking:
                DoAttacking();
                // Debug.Log("Attacking");
                break;
        }

        DetectPlayer();

        CheckBeamStatus();
    }

    void DetectPlayer()
    {
        // Only detect if not attacking
        if (currentState == State.Attacking)
            return;

        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.magnitude <= sightRange)
        {
            Ray ray = new Ray(transform.position, directionToPlayer.normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
            {
                if (hit.transform == player)
                {
                    lastKnownPlayerPos = player.position;

                    if (currentState != State.Chasing)
                    {
                        currentState = State.Chasing;
                    }
                }
            }
        }
    }

    void DoSearching()
    {
        if (!hasSearchTarget || Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
                                                  new Vector3(searchTarget.x, 0f, searchTarget.z)) < searchTargetReachThreshold)
        {
            // Pick a new random target near home
            float randomX = Random.Range(-searchRadius, searchRadius);
            float randomZ = Random.Range(-searchRadius, searchRadius);
            searchTarget = homePosition + new Vector3(randomX, 0f, randomZ);
            hasSearchTarget = true;
        }

        // Move towards the searchTarget
        MoveTowards(searchTarget);

        // Rotate slowly while moving
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void DoChasing()
    {
        MoveTowards(lastKnownPlayerPos);

        Vector3 flatUFOPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatTargetPos = new Vector3(lastKnownPlayerPos.x, 0f, lastKnownPlayerPos.z);
        float distanceToTarget = Vector3.Distance(flatUFOPos, flatTargetPos);

        // Debug.Log($"[Chasing] Distance to target: {distanceToTarget}");

        if (distanceToTarget < 1f)
        {
            // Debug.Log("[Chasing] Reached last known player position.");

            Vector3 directionToPlayer = player.position - transform.position;

            if (directionToPlayer.magnitude <= sightRange)
            {
                Ray ray = new Ray(transform.position, directionToPlayer.normalized);
                if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
                {
                    // Debug.Log($"[Chasing] Raycast hit: {hit.transform.name}");

                    if (hit.transform == player)
                    {
                        // Correct flat 2D distance
                        Vector3 flatPlayerPos = new Vector3(player.position.x, 0f, player.position.z);
                        float flatDistanceToPlayer = Vector3.Distance(flatUFOPos, flatPlayerPos);

                        if (flatDistanceToPlayer <= attackRange)
                        {
                            // Debug.Log("[Chasing] Player in range. Switching to Attacking.");
                            currentState = State.Attacking;
                        }

                        return; // Still sees player; keep chasing
                    }
                }
            }

            // Debug.Log("[Chasing] Player not visible. Switching to SearchingLostArea.");
            currentState = State.SearchingLostArea;
            searchTimer = searchDuration;
        }
    }

    void DoSearchLostArea()
    {
        if (!beamActive)
        {
            // Reactivate the beam while searching
            beamActive = true;
            laserBeam.SetActive(true);
            laserBeam.transform.localScale = initialLaserScale;
            isGrowing = true;
            laserTimer = 0f;
            laserCanDamage = false;
        }

        // Sweep rotation (rotate back and forth)
        float sweepAngle = Mathf.Sin(Time.time * rotateSpeed) * 45f; // 45 degree sweep
        transform.rotation = Quaternion.Euler(0f, sweepAngle + homePosition.y, 0f);

        searchTimer -= Time.deltaTime;

        // Only switch to Attacking if player is visible and in range
        if (CanSeePlayer())
        {
            float flatDistanceToPlayer = Vector3.Distance(
                new Vector3(transform.position.x, 0f, transform.position.z),
                new Vector3(player.position.x, 0f, player.position.z)
            );

            if (flatDistanceToPlayer <= attackRange)
            {
                // Debug.Log("[SearchingLostArea] Player found! Switching to Attacking.");
                canDetectPlayer = true; // Allow detection again
                currentState = State.Attacking;
                return;
            }
        }

        if (searchTimer <= 0)
        {
            // Debug.Log("[SearchingLostArea] Search timed out. Returning to base.");
            laserBeam.SetActive(false);
            beamActive = false;
            canDetectPlayer = true; // Allow detection again
            currentState = State.Returning;
        }
    }


    void DoReturning()
    {
        MoveTowards(homePosition);

        if (Vector3.Distance(transform.position, homePosition) < 1f)
        {
            currentState = State.Searching;
        }
    }

    void DoAttacking()
    {
        ShootLaser();

        // Flat 2D distance check (XZ only)
        Vector3 flatUFOPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatPlayerPos = new Vector3(player.position.x, 0f, player.position.z);
        float flatDistanceToPlayer = Vector3.Distance(flatUFOPos, flatPlayerPos);

        // If player is out of attack range but still visible (in sightRange)
        if (flatDistanceToPlayer > attackRange && flatDistanceToPlayer <= sightRange)
        {
            // Debug.Log("[Attacking] Player moved out of attack range but still visible. Chasing.");
            currentState = State.Chasing;
        }
        // If player is completely gone (out of sightRange)
        else if (flatDistanceToPlayer > sightRange)
        {
            // Debug.Log("[Attacking] Player lost. Switching to SearchingLostArea.");
            currentState = State.SearchingLostArea;
            searchTimer = searchDuration;
        }
    }

    void CheckBeamStatus()
    {
        if (!CanSeePlayer() && beamActive)
        {
            // Debug.Log("[Beam] Lost player completely. Turning off laser.");

            laserBeam.SetActive(false);
            beamActive = false;

            // Also, if still in Attacking, exit to SearchingLostArea
            if (currentState == State.Attacking)
            {
                // Debug.Log("[Beam] Lost player during Attacking. Switching to SearchingLostArea.");

                currentState = State.SearchingLostArea;
                searchTimer = searchDuration;

                canDetectPlayer = false; // Prevent immediate re-detection during searching
            }
        }
    }


    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        if (directionToPlayer.magnitude > sightRange)
            return false;

        Ray ray = new Ray(transform.position, directionToPlayer.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
        {
            return hit.transform == player;
        }

        return false;
    }

    void MoveTowards(Vector3 targetPos)
    {
        // Flatten both current and target positions to ignore Y differences
        Vector3 currentFlatPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetFlatPos = new Vector3(targetPos.x, 0f, targetPos.z);

        Vector3 direction = (targetFlatPos - currentFlatPos).normalized;

        // Debug.Log($"[MoveTowards] Direction: {direction}, Target: {targetFlatPos}");

        if (direction.sqrMagnitude < 0.001f)
        {
            // Debug.Log("[MoveTowards] Already at target.");
            return;
        }

        // Move UFO while keeping Y fixed
        Vector3 moveStep = direction * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + moveStep.x, transform.position.y, transform.position.z + moveStep.z);

        // Safe rotation
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }

    void ShootLaser()
    {
        // Debug.Log("pew pew miku miku beammmmmm");

        if (laserBeam == null) return;

        if (!beamActive)
        {
            beamActive = true;
            laserBeam.SetActive(true);
            laserBeam.transform.localScale = initialLaserScale; // Reset to original size
            isGrowing = true; // Always start by growing

            // Reset laser charging
            laserTimer = 0f;
            laserCanDamage = false;
        }

        Vector3 scale = laserBeam.transform.localScale;

        if (isGrowing)
        {
            float newScaleX = scale.x + laserGrowSpeed * Time.deltaTime;
            float newScaleZ = scale.z + laserGrowSpeed * Time.deltaTime;

            if (newScaleX >= maxLaserScale)
            {
                newScaleX = maxLaserScale;
                newScaleZ = maxLaserScale;
                isGrowing = false; // Start shrinking
            }

            laserBeam.transform.localScale = new Vector3(newScaleX, scale.y, newScaleZ);
        }
        else
        {
            float newScaleX = scale.x - laserGrowSpeed * Time.deltaTime;
            float newScaleZ = scale.z - laserGrowSpeed * Time.deltaTime;

            if (newScaleX <= initialLaserScale.x)
            {
                newScaleX = initialLaserScale.x;
                newScaleZ = initialLaserScale.x;
                isGrowing = true; // Start growing again
            }

            laserBeam.transform.localScale = new Vector3(newScaleX, scale.y, newScaleZ);
        }

        // Laser charging up
        if (beamActive)
        {
            laserTimer += Time.deltaTime;

            if (laserTimer >= laserChargeTime)
            {
                laserCanDamage = true;
            }
        }

        // Only check damage if laser is fully charged
        if (laserCanDamage && beamActive && player != null)
        {
            float distanceToLaser = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z),
                                                      new Vector3(player.position.x, 0f, player.position.z));

            if (distanceToLaser <= laserHitRadius)
            {
                // Debug.Log("[Laser] Hit the player!");

                if (respawnPoint != null)
                {
                    player.position = respawnPoint.position;
                }
                else
                {
                    // Debug.LogWarning("[Laser] No respawn point assigned!");
                }
            }
        }
    }
}
