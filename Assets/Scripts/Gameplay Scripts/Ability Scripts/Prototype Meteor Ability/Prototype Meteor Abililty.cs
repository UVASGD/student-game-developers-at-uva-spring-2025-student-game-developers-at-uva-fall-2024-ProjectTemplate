// using UnityEngine;

// public class PrototypeMeteorAbility : AbilityBase
// {
//     private GameObject projectilePrefab; // Reference to the prefab for the projectile
//     private Transform orientationTransform;  // Reference to the player's orientation

//     private float maxCharge = 5f;
//     private Vector3 targetPos = Vector3.zero;

//     // Constructor
//     public PrototypeMeteorAbility(GameObject prefab)
//         : base("Prototype Meteor Ability", KeyCode.M, 5f, AbilityFireType.HOLD)
//     {
//         projectilePrefab = prefab;
//     }

//     protected override void Execute()
//     {

//     }

//     public override void UpgradeAbility()
//     {

//     }

//     protected override void HoldExecute(float holdTime, Vector3 targetPos)
//     {
//         if (holdTime > 0)
//         {
//             Debug.Log("Meteor fired with " + holdTime + "s of charge");

//             // Define random XZ offset for spawn
//             float xOffset = Random.Range(-30f, 30f);
//             float zOffset = Random.Range(-30f, 30f);
//             float spawnHeight = 30f; // How high in the sky to spawn from

//             Vector3 randomSpawnPos = targetPos + new Vector3(xOffset, spawnHeight, zOffset);
//             Vector3 direction = (targetPos - randomSpawnPos).normalized;

//             GameObject projectile = GameObject.Instantiate(
//                 projectilePrefab,
//                 randomSpawnPos,
//                 Quaternion.LookRotation(direction)
//             );

//             Rigidbody rb = projectile.GetComponent<Rigidbody>();
//             rb.useGravity = true;
//             rb.linearVelocity = direction * 40f;
//         }
//     }
// }