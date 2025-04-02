using System.Collections.Generic;
using UnityEngine;

public class StationWorkspace : MonoBehaviour
{
    //TODO: Add specific station tops: Burner, Grill, Fryer, etc.

    // Equipment sprites, the top overlays ingredients, while bottom layers behind them. Set in the inspector.
    [field: SerializeField]
    public GameObject _equipmentTop { get; set; }

    [field: SerializeField]
    public GameObject _equipmentBottom { get; set; }

    [field: SerializeField]
    public GameObject _cuttingBoardSlot { get; set; }

    [field: SerializeField]
    public List<GameObject> _panSlots { get; set; }

    [field: SerializeField]
    public List<GameObject> _grillSlots { get; set; }

    [field: SerializeField]
    public List<GameObject> _potSlots { get; set; }

    [field: SerializeField]
    public List<GameObject> _mixingBowlSlots { get; set; }

    [field: SerializeField]
    public GameObject _fryerSlot { get; set; }

    // This script is attached to the Station Workspace prefab

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddToStationWorkspace(Ingredient ingredient){
        // only add to active station type

        // Add ingredient to workspace
        // GameObject ingredientObject = new GameObject(ingredient.ToString());
        // ingredientObject.transform.SetParent(_ingredients.transform);
        // ingredientObject.transform.localPosition = Vector3.zero;
        // ingredientObject.AddComponent<SpriteRenderer>().sprite = ingredient.Data.Sprites[0].texture;
        // ingredientObject.AddComponent<BoxCollider2D>();
        // ingredientObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
