using UnityEngine;

public class Prototype2Ability : AbilityBase
{
    private GameObject projectilePrefab; // Reference to the prefab for the projectile
    private Transform orientationTransform;  // Reference to the player's orientation

    // Constructor
    public Prototype2Ability(GameObject prefab) 
        : base("Prototype 2 Ability", KeyCode.F, 2f)
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
        Protopye2Projectile freezeProjectile = projectilePrefab.GetComponent<Protopye2Projectile>();
        freezeProjectile.FreezeTime = freezeProjectile.FreezeTime * 1.1f;
    }
}