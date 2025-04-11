using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : GameplayUI
{
    [SerializeField] private string passkey;
    [SerializeField] private TextMeshProUGUI display;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DigitInput(int digit)
    {
        if(display.text.Length < 4)
        {
            display.text += digit;
        }
        
    }

    public void Enter()
    {
        if(passkey == display.text)
        {
            Debug.Log("correct passkey");
        }
        else
        {
            Debug.Log("incorrect");
        }
        Clear();
    }

    public void Clear()
    {
        display.text = string.Empty;
    }
}
