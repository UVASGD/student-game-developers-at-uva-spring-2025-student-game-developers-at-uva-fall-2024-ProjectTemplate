using System;
using System.Collections.Generic;

namespace TastyTavern.Gameplay.CookingGameplay
{
    public class Shop
    {
        private Dictionary<string, int> inventory;
        private Dictionary<string, int> prices;

        public Shop()
        {
            inventory = new Dictionary<string, int>();
            prices = new Dictionary<string, int>();
        }

        public void AddItem(string itemName, int quantity, int price)
        {
            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName] += quantity;
            }
            else
            {
                inventory[itemName] = quantity;
                prices[itemName] = price;
            }
        }

        public bool BuyItem(string itemName, int quantity, ref int playerGold)
        {
            if (inventory.ContainsKey(itemName) && inventory[itemName] >= quantity)
            {
                int totalCost = prices[itemName] * quantity;
                if (playerGold >= totalCost)
                {
                    inventory[itemName] -= quantity;
                    playerGold -= totalCost;
                    return true;
                }
            }
            return false;
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Shop Inventory:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Key}: {item.Value} available at {prices[item.Key]} gold each");
            }
        }
    }
}