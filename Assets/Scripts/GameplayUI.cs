using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject player;
    private RectTransform rt;
    public bool active;
    public Noteboard firstNoteboard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.Init(false, false, DOTween.logBehaviour);
        if(active)
        {
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().lockMovement();
                player.GetComponent<PlayerCameraMovement>().lockPan();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //Debug.Log("hello?");
            }
            else
            {
                Debug.Log("cant find player");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active && Input.GetKeyDown(KeyCode.E))
        {
            deactivate();
        }
    }

    public void setPlayer(GameObject go)
    {
        player = go;
    }

    public void activate()
    {
        active = true;
        panel.SetActive(true);
        rt = panel.GetComponent<RectTransform>();
        rt.localScale = new Vector3(0, 0, 1);
        rt.DOScaleX(1,0.5f);
        rt.DOScaleY(1, 0.5f);
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
    public void activate(Action completeFunc)
    {
        panel.SetActive(true);
        rt = panel.GetComponent<RectTransform>();
        rt.localScale = new Vector3(0, 0, 1);
        rt.DOScaleX(1, 0.5f);
        rt.DOScaleY(1, 0.5f).OnComplete(() => { completeFunc(); active = true; });
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
        active = false;
        panel.SetActive(false);
        if(firstNoteboard != null)
        {
            firstNoteboard.canInteract = false;
        }
        
        if (player != null)
        {
            if(EndlessRoomManager.Instance != null)
            {
                Camera.main.transform.DOLocalMove(Vector3.zero, EndlessRoomManager.Instance.cameraMoveZoomTime);
                Camera.main.transform.DORotate(EndlessRoomManager.Instance.player.transform.rotation.eulerAngles, EndlessRoomManager.Instance.cameraMoveZoomTime).OnComplete(
                () => {
                player.GetComponent<PlayerMovement>().unlockMovement();
                player.GetComponent<PlayerCameraMovement>().unlockPan();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                firstNoteboard.canInteract = true;
                }
                );
            }
            else
            {
                player.GetComponent<PlayerMovement>().unlockMovement();
                player.GetComponent<PlayerCameraMovement>().unlockPan();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
