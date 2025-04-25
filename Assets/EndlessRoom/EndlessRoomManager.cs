using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EndlessRoomManager : MonoBehaviour {
    public static EndlessRoomManager Instance;
    public bool isA1Triggered = false;
    public int numA3TriggersToProgress = 5;
    public TeleportingEndlessRoomIllusion teleportingA3EndlessRoomIllusion;
    public TeleportingEndlessRoomIllusion teleportingA4EndlessRoomIllusion;
    public int numA3Triggered = 0;
    public GameObject mazeBoard;
    public MazeGenerator mazeGenerator;
    public int numWallsHittingEndSegment = 0;
    public DoorTriggerMovingWalls doorTriggerMovingWalls;
    [HideInInspector] public GameObject player;
    public float cameraMoveZoomTime = 1f;
    public float delayBetweenFlickers = 7f;
    public Image scaryFace;
    public Image fader;
    public Transform spawnTransformSection2;
    public AudioSource wallMovingSFX;
    private Light[] _lights;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        teleportingA3EndlessRoomIllusion.isActive = true;
        teleportingA4EndlessRoomIllusion.isActive = false;
        player = GameObject.FindGameObjectWithTag("Player");
        _lights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
        StartCoroutine(StartFlickering());
    }

    IEnumerator StartFlickering()
    {
        while (true)
        {
            foreach (Light light in _lights)
            {
                light.transform.parent.gameObject.SetActive(!light.transform.parent.gameObject.activeSelf);
                scaryFace.enabled = !scaryFace.enabled;
            }
            yield return new WaitForSeconds(0.1f);
            foreach (Light light in _lights)
            {
                light.transform.parent.gameObject.SetActive(!light.transform.parent.gameObject.activeSelf);
                scaryFace.enabled = !scaryFace.enabled;
            }
            yield return new WaitForSeconds(delayBetweenFlickers);
        }
    }

    public void A3Triggered() {
        numA3Triggered++;
        if (numA3Triggered == numA3TriggersToProgress - 1) {
            Debug.Log("A3 Triggered " + numA3Triggered + " times. Progressing to next room.");
            teleportingA3EndlessRoomIllusion.isActive = false;
            Invoke("MakeA4Active", 1.0f);
        }
    }

    public void MakeA4Active() {
        teleportingA4EndlessRoomIllusion.isActive = true;
    }

    public void ActivateMazeBoard() {
        mazeBoard.SetActive(true);
    }
}