using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItem")]
public class ShopItem : ScriptableObject 
{

    [field: SerializeField]
    public int Price { get; set; } 

    [field: SerializeField]
    public ItemType Type { get; set; }

    // This can be IngredientData, RecipeData, StationData, or BiomeData
    [field: SerializeField]
    public BuyableData Data { get; set; } 
}

public enum ItemType
{
    Ingredient,
    Recipe,
    Equipment,
    Biome
} 