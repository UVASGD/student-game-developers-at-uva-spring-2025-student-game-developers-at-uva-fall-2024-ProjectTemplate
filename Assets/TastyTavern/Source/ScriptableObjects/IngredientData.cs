using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData", order = 0)]
public class IngredientData : BuyableData 
{

    /// improve this structure
    /// For demo, 0 is icon, 1 is raw, 2 is cut, 3 is cut+cooked
    [field: SerializeField]
    public Sprite[] Sprites { get; set; }  

    // Factory method to make instance of Ingredient
    public Ingredient Create(){
        return new Ingredient(this);
    }

}

