using UnityEngine;

public class PrototypePoisonAbility : AbilityBase
{
    private GameObject projectilePrefab; // Reference to the prefab for the projectile
    private Transform orientationTransform;  // Reference to the player's orientation

    // Constructor
    public PrototypePoisonAbility(GameObject prefab)
        : base("Prototype Poison Ability", KeyCode.T, 2f, AbilityFireType.TAP)
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
        ProtopyePoisonProjectile poisonProjectile = projectilePrefab.GetComponent<ProtopyePoisonProjectile>();
        poisonProjectile.PoisonTime = poisonProjectile.PoisonTime * 1.1f;
        poisonProjectile.PoisonWeakness = poisonProjectile.PoisonWeakness * .9f;
    }





    protected override void HoldExecute(float holdTime, Vector3 targetPos)
    {
        throw new System.NotImplementedException();
    }
}