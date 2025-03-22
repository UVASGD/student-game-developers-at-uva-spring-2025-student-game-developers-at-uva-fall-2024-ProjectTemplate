using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MutableDoor : MonoBehaviour {
    public float playerDistToDoorTriggerCreak;
    public float playerDistToDoorTriggerKill;
    public Animator doorAnimator;
    public string doorCreakAnimationName;
    public string doorOpenAnimationName;
    public AudioSource doorCreakSFX;
    public AudioSource doorKillSFX;

    void Update(){
        if (Vector3.Distance(EndlessRoomManager.Instance.player.transform.position, transform.position) < playerDistToDoorTriggerKill) {
            if (doorKillSFX != null) doorKillSFX.Play();
            if (doorAnimator != null) doorAnimator.Play(doorOpenAnimationName);
        } else if (Vector3.Distance(EndlessRoomManager.Instance.player.transform.position, transform.position) < playerDistToDoorTriggerCreak) {
            if (doorCreakSFX != null) doorCreakSFX.Play();
            if (doorAnimator != null) doorAnimator.Play(doorCreakAnimationName);
        }
    }
}