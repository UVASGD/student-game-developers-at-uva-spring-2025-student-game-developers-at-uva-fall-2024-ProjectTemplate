using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FlashlightRoomManager : MonoBehaviour
{
    public static FlashlightRoomManager Instance;
    public GameObject keypad;
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
        keypad.SetActive(true);
    }
}