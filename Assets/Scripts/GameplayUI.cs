using UnityEngine;
using UnityEngine.UI;
public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayer(GameObject go)
    {
        player = go;
    }

    public void activate()
    {
        panel.SetActive(true);
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<PlayerCameraMovement>().canPan = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void deactivate()
    { 
        panel.SetActive(false);
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponent<PlayerCameraMovement>().canPan = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
