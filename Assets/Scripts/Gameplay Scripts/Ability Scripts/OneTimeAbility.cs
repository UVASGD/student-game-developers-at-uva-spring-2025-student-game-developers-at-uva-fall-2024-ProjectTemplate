// using UnityEngine;

// public abstract class OneTimeAbility : AbilityBase
// {
//     public int charges { get; protected set; }
    
//     public OneTimeAbility(string name, KeyCode key, float cTime, int initialCharges = 1) 
//         : base(name, key, cTime, AbilityFireType.HOLD)
//     {
//         charges = initialCharges;
//     }
    
//     public new void Activate()
//     {
//         // Override the base Activate method to check for charges
//         if (charges <= 0)
//         {
//             Debug.Log($"{abilityName} has no charges remaining.");
//             return;
//         }
        
//         // Check if the current game phase allows ability activation
//         if (RoundManager.Instance.GetCurrentRoundPhase() != RoundManager.RoundPhase.EnemiesSpawning &&
//             RoundManager.Instance.GetCurrentRoundPhase() != RoundManager.RoundPhase.EnemiesNoLongerSpawning)
//         {
//             Debug.Log($"{abilityName} cannot be activated during the {RoundManager.Instance.GetCurrentRoundPhase()} phase.");
//             return;
//         }

//         // Check cooldown
//         if (Time.time >= lastActivatedTime + cooldownTime)
//         {
//             lastActivatedTime = Time.time;
//             charges--;
//             Execute();
//         }
//         else
//         {
//             Debug.Log($"{abilityName} is on cooldown.");
//         }
//     }
    
//     public void AddCharges(int amount)
//     {
//         charges += amount;
//         Debug.Log($"Added {amount} charges to {abilityName}. Total charges: {charges}");
//     }
// }