using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [Header("UI Settings")]
    public Transform abilitiesContainer;
    public GameObject abilityUIPrefab;

    private AbilityManager abilityManager;
    private AbilityBase[] previousAbilities;

    void Start()
    {
        abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();
        CreateAllAbilityDisplays();
    }

    void Update()
    {
        // Only update if abilities list changes
        if (AbilityListChanged())
        {
            ClearAbilityDisplays();
            CreateAllAbilityDisplays();
        }

        UpdateCooldowns();
    }

    bool AbilityListChanged()
    {
        var currentAbilities = abilityManager.GetAbilities();
        if (previousAbilities == null || currentAbilities.Count != previousAbilities.Length) 
            return true;

        for (int i = 0; i < currentAbilities.Count; i++)
        {
            if (currentAbilities[i] != previousAbilities[i]) 
                return true;
        }
        
        return false;
    }

    void CreateAllAbilityDisplays()
    {
        var abilities = abilityManager.GetAbilities();
        previousAbilities = abilities.ToArray();

        foreach (var ability in abilities)
        {
            GameObject abilityUI = Instantiate(abilityUIPrefab, abilitiesContainer);
            
            // Set basic info
            abilityUI.transform.Find("Icon").GetComponent<Image>().sprite = ability.AbilityIcon;
            abilityUI.transform.Find("KeyText").GetComponent<TMP_Text>().text = ability.activationKey.ToString();
        }
    }

    void UpdateCooldowns()
    {
        int childIndex = 0;
        foreach (var ability in abilityManager.GetAbilities())
        {
            Transform abilityUI = abilitiesContainer.GetChild(childIndex);
            float remainingCD = Mathf.Max(0, ability.lastActivatedTime + ability.cooldownTime - Time.time);
            
            // Update cooldown text
            TMP_Text cdText = abilityUI.Find("CooldownText").GetComponent<TMP_Text>();
            cdText.text = remainingCD > 0 ? Mathf.Ceil(remainingCD).ToString() : "";
            
            // Update level text
            abilityUI.Find("LevelText").GetComponent<TMP_Text>().text = $"Lv.{ability.level}";
            
            childIndex++;
        }
    }

    void ClearAbilityDisplays()
    {
        foreach (Transform child in abilitiesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}