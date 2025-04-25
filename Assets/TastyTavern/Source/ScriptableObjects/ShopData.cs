using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "ScriptableObjects/Shop")]
public class ShopData : ScriptableObject 
{

    [field: SerializeField]
    public List<ShopItem> IngredientItems { get; } = new List<ShopItem>();

    [field: SerializeField]
    public List<ShopItem> RecipeItems { get; } = new List<ShopItem>();

    [field: SerializeField]
    public List<ShopItem> EquipmentItems { get; } = new List<ShopItem>();

    [field: SerializeField]
    public List<ShopItem> BiomeItems { get; } = new List<ShopItem>();
    
}
