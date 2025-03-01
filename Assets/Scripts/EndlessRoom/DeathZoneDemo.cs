using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeathZoneDemo : MonoBehaviour {
    public Image fader;
    public Transform destination;

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            EndlessRoomManager.Instance.player.SetActive(false);
            fader.gameObject.SetActive(true);
            fader.DOFade(1f, 1f).OnComplete(() => {
                other.gameObject.transform.position = destination.position;
                fader.DOFade(0f, 1f).OnComplete(() => {
                    fader.gameObject.SetActive(false);
                    EndlessRoomManager.Instance.player.SetActive(true);
                });
            });
        }
    }
}