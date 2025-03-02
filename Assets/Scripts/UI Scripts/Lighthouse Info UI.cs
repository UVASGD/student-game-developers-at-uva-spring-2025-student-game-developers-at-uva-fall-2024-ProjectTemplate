using TMPro;
using UnityEngine;

public class LighthouseInfoUI : MonoBehaviour
{

    [SerializeField] Lighthouse lighthouse;
    [SerializeField] TextMeshProUGUI lighthouseHealthText;
    

    void Update()
    {
        string lighthouseHealth = ((int) lighthouse.GetHealth()).ToString();
        lighthouseHealthText.text = "Lighthouse Health: " +  lighthouseHealth;
    }

}
