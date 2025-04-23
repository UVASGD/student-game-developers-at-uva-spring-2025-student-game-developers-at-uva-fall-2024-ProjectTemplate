
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public float money;


    [SerializeField]
    private CookingUIEventChannel cookingUIEventChannel;

    //All Ingredients, Equipment, Recipes, and Biome scriptable objects get placed in their respective lists
    public List<IngredientData> allIngredient = new List<IngredientData>();
    public List<StationData> allEquipment = new List<StationData>();
    public List<RecipeData> allRecipe = new List<RecipeData>();
    public List<BiomeData> allBiome = new List<BiomeData>();

    //All the scriptable objects get placed into their correct dictionaries, all bools are initially set to false
    public Dictionary<IngredientData, bool> IngredientUnlocked = new Dictionary<IngredientData, bool>();
    public Dictionary<StationData, bool> StationUnlocked = new Dictionary<StationData, bool>();
    public Dictionary<RecipeData, bool> RecipeUnlocked = new Dictionary<RecipeData, bool>();
    public Dictionary<BiomeData, bool> BiomeUnlocked = new Dictionary<BiomeData, bool>();

    void Start()
    {
        //Enter the Ingredients
        for (int i = 0; i < allIngredient.Count; i++)
        {
            IngredientUnlocked.Add(allIngredient[i], false);
        }
        //Enter the Equipment/Actions
        for (int i = 0; i < allEquipment.Count; i++)
        {
            StationUnlocked.Add(allEquipment[i], false);
        }
        //Enter the Recipes
        for (int i = 0; i < allRecipe.Count; i++)
        {
            RecipeUnlocked.Add(allRecipe[i], false);
        }
        //Enter the Biomes
        for (int i = 0; i < allBiome.Count; i++)
        {
            BiomeUnlocked.Add(allBiome[i], false);
        }
    }
    [System.Serializable]
    private class PlayerData
    {
        // TODO: Everything
        PlayerData()
        {
            
        }

    }

    private void OnEnable()
    {
        cookingUIEventChannel.OnChangePlayerMoney += ChangeMoney;
    }

    private void OnDisable()
    {
        cookingUIEventChannel.OnChangePlayerMoney -= ChangeMoney;
    }

    public void ChangeMoney(float deltaMoney)
    {
        if (deltaMoney < 0 && deltaMoney > money)
        {
            Debug.Log("Not enough money!!");
        }
        else
        {
            money += deltaMoney;
        }
    }

    public void AddItemToInventory(ShopItem item) {
        if (item.Type == ItemType.Ingredient) {
            for (int i = 0; i < allIngredient.Count; i++) {
                if (allIngredient[i].Name == item.Data.Name) {
                    IngredientUnlocked[allIngredient[i]] = true;
                    break;
                }
            }
        } else if (item.Type == ItemType.Equipment) {
            for (int i = 0; i < allEquipment.Count; i++) {
                if (allEquipment[i].Name == item.Data.Name) {
                    StationUnlocked[allEquipment[i]] = true;
                    break;
                }
            }
        } else if (item.Type == ItemType.Recipe) {
            for (int i = 0; i < allRecipe.Count; i++) {
                if (allRecipe[i].Name == item.Data.Name) {
                    RecipeUnlocked[allRecipe[i]] = true;
                    break;
                }
            }
        } else if (item.Type == ItemType.Biome) {
            for (int i = 0; i < allBiome.Count; i++) {
                if (allBiome[i].Name == item.Data.Name) {
                    BiomeUnlocked[allBiome[i]] = true;
                    break;
                }
            }
        }

    }
}
