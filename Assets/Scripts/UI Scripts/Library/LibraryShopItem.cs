using UnityEngine;

[System.Serializable]
public class LibraryShopItem
{
    public string itemName;
    public string description;
    public int wisdomCost;
    public Sprite icon;
    public bool isOneTime;
    public bool isOwned;
    public int level = 0;
    
    // For creating the ability
    public System.Type abilityType;
    public GameObject prefab;
    
    // For one-time abilities
    public int chargesPerPurchase = 1;
}