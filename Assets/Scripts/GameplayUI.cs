using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.Init(false, false, DOTween.logBehaviour);
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
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.localScale = new Vector3(0, 0, 1);
        rt.DOScaleX(1,0.5f);
        rt.DOScaleY(1,0.5f);
        //animation !
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().lockMovement();
            player.GetComponent<PlayerCameraMovement>().lockPan();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Debug.Log("hello?");
        }
    }
    public void deactivate()
    { 
        panel.SetActive(false);
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().unlockMovement();
            player.GetComponent<PlayerCameraMovement>().unlockPan();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
