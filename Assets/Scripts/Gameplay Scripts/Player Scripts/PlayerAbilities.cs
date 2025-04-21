using UnityEngine;

public class Player : MonoBehaviour
{
    public AbilityManager abilityManager { get; private set; }

    [SerializeField] private int wisdomPoints = 10;

    void Awake()
    {
        // Find the AbilityManager if not assigned
        if (abilityManager == null)
        {
            abilityManager = FindFirstObjectByType<AbilityManager>();
             if (abilityManager == null)
             {
                 Debug.LogError("Player could not find AbilityManager!");
             }
        }
    }

    public void AwardWisdomPoints(int points)
    {
        if (points > 0)
        {
            wisdomPoints += points;
            Debug.Log($"Awarded {points} WP. Total: {wisdomPoints}");
             FindFirstObjectByType<ShopManager>()?.RefreshShopItemStates();
             FindFirstObjectByType<LibraryUI>()?.UpdateWisdomPointsDisplay();
        }
    }

    public bool CanAfford(int cost)
    {
        return wisdomPoints >= cost;
    }

    public bool SpendWisdomPoints(int amount)
    {
        if (amount <= 0) return false;

        if (wisdomPoints >= amount)
        {
            wisdomPoints -= amount;
            Debug.Log($"Spent {amount} WP. Remaining: {wisdomPoints}");

             FindFirstObjectByType<ShopManager>()?.RefreshShopItemStates();
             FindFirstObjectByType<LibraryUI>()?.UpdateWisdomPointsDisplay();
            return true;
        }
        else
        {
            Debug.Log($"Cannot spend {amount} WP. Only have {wisdomPoints}");
            return false;
        }
    }

    public void RefundWisdomPoints(int amount)
    {
        if (amount > 0)
        {
            wisdomPoints += amount;
            Debug.Log($"Refunded {amount} WP. Total: {wisdomPoints}");
             FindFirstObjectByType<ShopManager>()?.RefreshShopItemStates();
             FindFirstObjectByType<LibraryUI>()?.UpdateWisdomPointsDisplay();
        }
    }

    public int GetWisdomPoints()
    {
        return wisdomPoints;
    }
}