using System.Collections.Generic;
using UnityEngine;

public class LibraryShopManager : MonoBehaviour
{
    public static LibraryShopManager Instance { get; private set; }
    
    [SerializeField] private List<LibraryShopItem> availableItems = new List<LibraryShopItem>();
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private Player player;
    
    [Header("Prefabs for New Abilities")]
    [SerializeField] private GameObject example1Prefab;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        // Initialize shop items
        InitializeShopItems();
    }
    
    private void Start()
    {
        // Get references
        if (abilityManager == null)
            abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();
            
        if (player == null)
            player = GameObject.Find("Player").GetComponent<Player>();
    }
    
    private void InitializeShopItems()
    {
        // Add upgrades for existing abilities

        

    
    }
    
    public bool PurchaseItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= availableItems.Count)
        {
            Debug.LogError("Invalid item index");
            return false;
        }
        
        LibraryShopItem item = availableItems[itemIndex];
        int wisdomPoints = player.GetWisdomPoints();
        
        if (wisdomPoints < item.wisdomCost)
        {
            Debug.Log("Not enough wisdom points");
            return false;
        }
        
        // Deduct wisdom points
        player.AwardWisdomPoints(-item.wisdomCost);
        
        if (item.isOneTime)
        {
            // Handle one-time ability purchase
            if (item.isOwned)
            {
                // Find the existing ability and add charges
                AbilityBase existingAbility = abilityManager.GetAbilityByName(item.itemName);
                if (existingAbility != null && existingAbility is OneTimeAbility oneTimeAbility)
                {
                    oneTimeAbility.AddCharges(item.chargesPerPurchase);
                    Debug.Log($"Added {item.chargesPerPurchase} charges to {item.itemName}");
                }
                else
                {
                    Debug.LogError($"Ability {item.itemName} not found or not a OneTimeAbility");
                    return false;
                }
            }
            else
            {
                // Create new one-time ability
                OneTimeAbility newAbility = CreateOneTimeAbility(item);
                if (newAbility != null)
                {
                    abilityManager.AddAbility(newAbility);
                    item.isOwned = true;
                    Debug.Log($"Added new one-time ability: {item.itemName}");
                }
                else
                {
                    Debug.LogError($"Failed to create one-time ability: {item.itemName}");
                    return false;
                }
            }
        }
        else
        {
            // Handle regular ability upgrade
            string abilityName = item.itemName.Replace(" Upgrade", "");
            AbilityBase ability = abilityManager.GetAbilityByName(abilityName);
            
            if (ability != null)
            {
                ability.UpgradeAbility();
                ability.level++;
                item.level = ability.level;
                Debug.Log($"Upgraded {abilityName} to level {item.level}");
            }
            else
            {
                Debug.LogError($"Ability {abilityName} not found for upgrade");
                return false;
            }
        }
        
        return true;
    }
    
    private OneTimeAbility CreateOneTimeAbility(LibraryShopItem item)
    {
        // This is where you would instantiate the specific one-time ability
        // based on the item's abilityType
        
        
        Debug.LogError($"Unknown ability type: {item.abilityType.Name}");
        return null;
    }
    
    public List<LibraryShopItem> GetAvailableItems()
    {
        return availableItems;
    }
}