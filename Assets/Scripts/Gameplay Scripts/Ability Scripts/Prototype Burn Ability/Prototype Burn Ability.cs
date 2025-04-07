using UnityEngine;

public class PrototypeBurnAbility : AbilityBase
{
    private GameObject projectilePrefab; // Reference to the prefab for the projectile
    private Transform orientationTransform;  // Reference to the player's orientation

    // Constructor
    public PrototypeBurnAbility(GameObject prefab)
        : base("Prototype Burn Ability", KeyCode.G, 2f, AbilityFireType.TAP)
    {
        projectilePrefab = prefab;
    }

    protected override void Execute()
    {
        Debug.Log($"{abilityName} activated!");


        Transform cameraTransform = Camera.main.transform;
        // Instantiate the projectile
        GameObject projectile = GameObject.Instantiate(
            projectilePrefab,
            cameraTransform.position + cameraTransform.forward, // Spawn in front of player
            Quaternion.LookRotation(cameraTransform.forward)    // Orient forward
        );

        // Add velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = cameraTransform.forward * 10f;

    }

    public override void UpgradeAbility()
    {
        ProtopyeBurnProjectile burnProjectile = projectilePrefab.GetComponent<ProtopyeBurnProjectile>();
        burnProjectile.TickDuration = burnProjectile.TickDuration * 0.9f;
        burnProjectile.BurnDamage = burnProjectile.BurnDamage * 1.1f;
        burnProjectile.NumTicks = burnProjectile.NumTicks + 1;

    }





    protected override void HoldExecute(float holdTime, Vector3 targetPos)
    {
        throw new System.NotImplementedException();
    }
}