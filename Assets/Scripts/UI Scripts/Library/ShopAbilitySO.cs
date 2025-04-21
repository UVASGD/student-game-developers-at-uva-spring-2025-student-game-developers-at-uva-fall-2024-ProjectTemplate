using UnityEngine;

// Makes it easy to create new ability shop items from the Assets menu
[CreateAssetMenu(fileName = "NewShopAbility", menuName = "Shop/Shop Ability Item", order = 1)]
public class ShopAbilitySO : ScriptableObject
{
    [Header("Shop Display Info")]
    public string abilityName = "New Ability"; // Name shown in the shop
    [TextArea(3, 5)] // Makes the description field larger in the inspector
    public string description = "Ability Description";
    public Sprite icon; // Icon shown in the shop

    [Header("Ability Core Reference")]
    // Reference to the actual prefab that contains the Ability's logic
    // (e.g., the prefab passed to Prototype2Ability constructor)
    // OR a direct reference if the ability logic is component-based
    public GameObject abilityPrefab;
    // We need the *type* of the AbilityBase script to instantiate it correctly.
    // You'll need to manually ensure the class name here matches the script
    // that should be instantiated (e.g., "Prototype2Ability").
    // A more robust system might use reflection or custom editors, but this is simpler.
    public string abilityClassName; // e.g., "Prototype2Ability"

    [Header("Progression & Cost")]
    public int maxLevel = 5;
    public int baseWisdomCost = 1; // Cost to buy (Level 1)
    // How much the cost increases for each subsequent level (Level 2, 3, etc.)
    public int wisdomCostIncreasePerLevel = 1;

    [Header("Ability Type")]
    public bool isOneTimePurchase = false; // If true, acts like a consumable

    // Helper method to get the cost for a specific target level (e.g., cost to upgrade TO level 3)
    public int GetCostForLevel(int targetLevel)
    {
        if (targetLevel <= 0) return 0; // Should not happen
        if (targetLevel == 1) return baseWisdomCost; // Cost of initial purchase
        // Cost for subsequent levels
        return baseWisdomCost + (wisdomCostIncreasePerLevel * (targetLevel - 1));
    }
}