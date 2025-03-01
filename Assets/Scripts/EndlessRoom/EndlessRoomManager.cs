using System.Collections.Generic;
using UnityEngine;

public class EndlessRoomManager : MonoBehaviour {
    public static EndlessRoomManager Instance;
    public bool isA1Triggered = false;
    public int numA3TriggersToProgress = 5;
    public TeleportingEndlessRoomIllusion teleportingA3EndlessRoomIllusion;
    public TeleportingEndlessRoomIllusion teleportingA4EndlessRoomIllusion;
    public int numA3Triggered = 0;
    public List<MazeGenerator> mazeGenerators;
    public GameObject player;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            teleportingA3EndlessRoomIllusion.isActive = true;
            teleportingA4EndlessRoomIllusion.isActive = false;
            player = GameObject.FindGameObjectWithTag("Player");
        } else {
            Destroy(gameObject);
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
}