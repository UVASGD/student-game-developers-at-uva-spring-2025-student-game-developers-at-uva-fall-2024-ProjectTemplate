using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        panel.SetActive(true);
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<PlayerCameraMovement>().canPan = false;
        }
    }
    public void deactivate()
    { 
        panel.SetActive(false);
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponent<PlayerCameraMovement>().canPan = true;
        }
    }
}
