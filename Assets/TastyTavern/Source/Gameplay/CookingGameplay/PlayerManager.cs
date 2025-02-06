using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int money;

    public bool[] biomeAccess = new bool[3];

    /*
     * first way to manage unlocking equip/recipes/biomes that comes to mind is adding objects to a list
     * that would require a more robust system + time, so bools will do for now
     */

    //TEMPORARY
    public bool equip1;
    public bool equip2;
    public bool equip3;
    public bool recipe1;
    public bool recipe2;
    public bool recipe3;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void changeMoney(int deltaMoney)
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
    public void unlockNextBiome()
    {
        for (int i = 0; i < biomeAccess.Length; i++)
        {
            if (!biomeAccess[i])
            {
                biomeAccess[i] = false;
            }
        }
    }
}
