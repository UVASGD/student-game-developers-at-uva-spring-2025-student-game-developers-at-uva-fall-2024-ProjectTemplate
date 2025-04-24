using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopView : MonoBehaviour
{

    [SerializeField]
    private UIDocument document;

    private VisualElement root;
    private Button ingredientsBtn;
    private Button recipesBtn;
    private Button equipmentBtn;
    private Button biomesBtn;
    private VisualElement ingredientsPage;
    private VisualElement recipesPage;
    private VisualElement equipmentPage;
    private VisualElement biomesPage;

    private VisualElement playerMoneyText;
    private VisualElement backButton;

    // Action for when a page is clicked, in <newly selected page, old page> format
    // public Action<VisualElement,VisualElement> OnPageClicked;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = document.rootVisualElement;

        // Retrieve all category buttons and pages from the UI
        ingredientsBtn = (Button)root.Q<VisualElement>("IngredientsBtn");
        recipesBtn = (Button)root.Q<VisualElement>("RecipesBtn");   
        equipmentBtn = (Button)root.Q<VisualElement>("EquipmentBtn");
        biomesBtn = (Button)root.Q<VisualElement>("BiomesBtn");
        ingredientsPage = root.Q<VisualElement>("IngredientsPage");
        recipesPage = root.Q<VisualElement>("RecipesPage");
        equipmentPage = root.Q<VisualElement>("EquipmentPage");
        biomesPage = root.Q<VisualElement>("BiomesPage");

        // Retrieving other UI (player money and back button)
        playerMoneyText = root.Q<VisualElement>("PlayerMoney");
        backButton = root.Q<VisualElement>("BackButton");
    }

    void OnEnable()
    {
        ingredientsBtn.RegisterCallback<ClickEvent, string>(OnPageClicked, "Ingredients");
        recipesBtn.RegisterCallback<ClickEvent, string>(OnPageClicked, "Recipes");
        equipmentBtn.RegisterCallback<ClickEvent, string>(OnPageClicked, "Equipment");
        biomesBtn.RegisterCallback<ClickEvent, string>(OnPageClicked, "Biomes");
    }

    void OnDisable()
    {
        ingredientsBtn.UnregisterCallback<ClickEvent, string>(OnPageClicked);
        recipesBtn.UnregisterCallback<ClickEvent, string>(OnPageClicked);
        equipmentBtn.UnregisterCallback<ClickEvent, string>(OnPageClicked);
        biomesBtn.UnregisterCallback<ClickEvent, string>(OnPageClicked);
    }
    
    // Handles page switching
    void OnPageClicked(ClickEvent evt, string pageName)
    {
        // Hide all pages
        ingredientsPage.style.display = DisplayStyle.None;
        recipesPage.style.display = DisplayStyle.None;
        equipmentPage.style.display = DisplayStyle.None;
        biomesPage.style.display = DisplayStyle.None;

        // Show the selected page, make the button selected
        switch (pageName)
        {
            case "Ingredients":
                ingredientsBtn.AddToClassList("page-btn-selected");
                ingredientsPage.style.display = DisplayStyle.Flex;
                break;
            case "Recipes":
                recipesBtn.AddToClassList("page-btn-selected");
                recipesPage.style.display = DisplayStyle.Flex;
                break;
            case "Equipment":
                equipmentBtn.AddToClassList("page-btn-selected");
                equipmentPage.style.display = DisplayStyle.Flex;
                break;
            case "Biomes":
                biomesBtn.AddToClassList("page-btn-selected");
                biomesPage.style.display = DisplayStyle.Flex;
                break;
            default:
                Debug.LogError("Unknown page name: " + pageName);
                break;
        }
    }
}
