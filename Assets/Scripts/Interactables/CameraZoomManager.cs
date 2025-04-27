using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CameraZoomManager : MonoBehaviour 
{
    public static CameraZoomManager Instance;
    public GameObject interactable;
    [HideInInspector] public GameObject player;
    public float cameraMoveZoomTime = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ActivateMazeBoard()
    {
        interactable.SetActive(true);
    }
}