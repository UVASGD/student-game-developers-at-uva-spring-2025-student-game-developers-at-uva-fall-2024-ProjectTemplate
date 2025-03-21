using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MutableDoor : MonoBehaviour {
    public float playerDistToDoorTriggerCreak;
    public float playerDistToDoorTriggerKill;

    void Update(){
        if (Vector3.Distance(EndlessRoomManager.Instance.player.transform.position, transform.position) < playerDistToDoorTriggerKill) {
            GetComponent<AudioSource>().Play();
        } else if (Vector3.Distance(EndlessRoomManager.Instance.player.transform.position, transform.position) < playerDistToDoorTriggerCreak) {
            GetComponent<AudioSource>().Play();
        }
    }
}