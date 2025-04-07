using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<AbilityBase> abilities = new List<AbilityBase>();
    [SerializeField] private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.useWorldSpace = true;
    }

    private void Update()
    {
        HandleInput();
    }


    // Implement hold abilities
    private void HandleInput()
    {
        foreach (var ability in abilities)
        {
            if (ability.abilityFireType == AbilityFireType.TAP)
            {
                if (Input.GetKeyDown(ability.activationKey))
                {
                    ability.Activate();
                }
            }
            else if (ability.abilityFireType == AbilityFireType.HOLD)
            {
                if (Input.GetKey(ability.activationKey))
                {
                    ability.ToggleHold(AbilityHoldAction.HOLD_START);
                }
                else if (Input.GetKeyUp(ability.activationKey))
                {
                    ability.ToggleHold(AbilityHoldAction.HOLD_END);
                }
            }
        }
    }

    public AbilityBase GetAbilityByName(string name)
    {
        return abilities.Find(ability => ability.abilityName == name);
    }

    public void AddAbility(AbilityBase ability)
    {
        if (!abilities.Contains(ability))
        {
            abilities.Add(ability);
            ability.SetLineRenderer(lineRenderer);
            Debug.Log("Added " + lineRenderer + " for " + ability.abilityName);
        }
        else
        {
            Debug.Log($"Ability {ability.abilityName} is already added.");
        }
    }

    public void RemoveAbility(AbilityBase ability)
    {
        if (abilities.Contains(ability))
        {
            abilities.Remove(ability);
        }
        else
        {
            Debug.Log($"Ability {ability.abilityName} not found in the list.");
        }
    }


    public List<AbilityBase> GetAbilities()
    {
        return new List<AbilityBase>(abilities);
    }
}