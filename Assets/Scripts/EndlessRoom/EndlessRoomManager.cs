using UnityEngine;

public class EndlessRoomManager : MonoBehaviour {
    public static EndlessRoomManager Instance;
    public int numA3TriggersToProgress = 5;
    public TeleportingEndlessRoomIllusion teleportingA3EndlessRoomIllusion;
    public TeleportingEndlessRoomIllusion teleportingA4EndlessRoomIllusion;
    public int numA3Triggered = 0;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            teleportingA3EndlessRoomIllusion.isActive = true;
            teleportingA4EndlessRoomIllusion.isActive = false;
        } else {
            Destroy(gameObject);
        }
    }

    public void A3Triggered() {
        numA3Triggered++;
        if (numA3Triggered >= numA3TriggersToProgress) {
            Debug.Log("A3 Triggered " + numA3Triggered + " times. Progressing to next room.");
            teleportingA3EndlessRoomIllusion.isActive = false;
            teleportingA4EndlessRoomIllusion.isActive = true;
        }
    }
}