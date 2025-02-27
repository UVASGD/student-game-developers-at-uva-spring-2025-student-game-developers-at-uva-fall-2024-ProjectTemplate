/* Shop: - A class that represents a shop in the game. It has an inventory of items that can be bought and sold, and prices for each item.
        - The player can buy items from the shop if they have enough money.
        - Once a user completes a purchase, the listing for that item is updated to say something like 'in kitchen/stock'.
        - Once a user buys an item, they do not need to buy it again. The item is available for use in the kitchen. (Buying = unlocking)
        - Items PC can buy: Ingredients, Recipes, Kitchen Upgrades (Equipment), Biomes

        Notes:
        - Item state: lock/unlock
        - Make a shop scriptable object so UI will just need to reference it
        
        - Shop will have a list of items, each with a name, price, status, icon, and description
        - Separate script for shop functions (buying, adding to player inventory, changing status of items in shop)
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager
{
    private List<ShopItem> inventory;
    private ShopData shopData;
    private PlayerManager playerManager;

    public ShopManager(ShopData shopData, PlayerManager playerManager)
    {
        this.shopData = shopData;
        this.playerManager = playerManager;
        inventory = shopData.ShopItems;
    }

    /* Player can buy an item under the following conditions
        1. They have enough gold
        2. The item is not already purchased
        3. Display only stuff for biome chosen
        4. Recipies cannot be bought unless all ingredients are unlocked
    If the player meets these conditions, the item is marked as purchased and the player's gold is reduced by the item's price
    public bool BuyItem(ShopItem item, ref int playerGold) 
    */
    public bool BuyItem(ShopItem item, ref float playerGold)
    {
        playerGold = playerManager.money;
        if (playerGold >= item.Price)
        {
            if (item.Purchased == true)
            {
                Debug.Log("You already bought that item!");
                return false;
            } else {
                playerGold -= item.Price;
                item.Purchased = true;
                if (item.Type == ShopItem.ItemType.Ingredient) // will this work idk
                {
                    
                }
                Debug.Log($"You bought {item.Name} for {item.Price} gold!");
                //how to update information in player?
                // add item to respective dictionaries for player inventory
                    // allIngredient or IngredientUnlocked?
                return true;
            }
        }
        else
        {
            Debug.Log("You don't have enough gold to buy that item!");
            return false;
        }
    }
}

/* Player can onl


    // public void DisplayInventory()
    // {
    //     Debug.Log("Shop Inventory:");
    //     foreach (var item in inventory)
    //     {
    //         Debug.Log($"{item.Name}: {item.Description}, {item.Price} gold, Purchased: {item.Purchased}");
    //     }
    // }
}*/