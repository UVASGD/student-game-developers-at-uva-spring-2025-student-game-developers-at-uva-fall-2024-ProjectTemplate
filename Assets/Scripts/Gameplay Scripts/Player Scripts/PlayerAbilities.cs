using UnityEngine;

public class Player : MonoBehaviour
{
    private AbilityManager abilityManager;

    public GameObject prototype1Prefab;
    public GameObject prototype2Prefab;
    public GameObject prototypeBurnPrefab;
    public GameObject prototypeVulnerablePrefab;
    public GameObject prototypeSlowPrefab;
    public GameObject prototypePoisonPrefab;

    private int wisdomPoints = 2;
    //private static bool abilitiesRegistered = false;

    void Awake()
    {
        abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();
        abilityManager.AddAbility(new PrototypeAbility(prototype1Prefab));
        abilityManager.AddAbility(new Prototype2Ability(prototype2Prefab));
        abilityManager.AddAbility(new PrototypeBurnAbility(prototypeBurnPrefab));
        abilityManager.AddAbility(new PrototypeVulnerableAbility(prototypeVulnerablePrefab));
        abilityManager.AddAbility(new PrototypeSlowAbility(prototypeSlowPrefab));
        abilityManager.AddAbility(new PrototypePoisonAbility(prototypePoisonPrefab));
        //abilitiesRegistered = true;
    }

    public void AwardWisdomPoints(int points)
    {
        wisdomPoints += points;
    }

    public int GetWisdomPoints()
    {
        return wisdomPoints;
    }

}
