using UnityEngine;

public class PrototypeSlowAbility : AbilityBase
{
    private GameObject projectilePrefab; // Reference to the prefab for the projectile
    private Transform orientationTransform;  // Reference to the player's orientation

    // Constructor
    public PrototypeSlowAbility(GameObject prefab) 
        : base("Prototype Slow Ability", KeyCode.U, 2f)
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
        ProtopyeSlowProjectile slowProjectile = projectilePrefab.GetComponent<ProtopyeSlowProjectile>();
        slowProjectile.setSlowTime(slowProjectile.getSlowTime() * 1.1f);
        slowProjectile.SlowMagnitude *= .90f; //10 percent slower
    }
}