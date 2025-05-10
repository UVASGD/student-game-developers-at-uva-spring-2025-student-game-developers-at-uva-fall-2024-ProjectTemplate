using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class DeathZoneDemo : MonoBehaviour {
    public Image fader;
    public Transform destination;
    public GameObject wallToEnable;
    public List<GameObject> deathZonesToDisable;
    public List<GameObject> deathBotsToEnable;

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            GameObject player = other.gameObject;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.GetComponent<PlayerCameraMovement>().enabled = false;

            Debug.Log("amazing");

            foreach (GameObject deathZone in deathBotsToEnable)
            {
                deathZone.SetActive(true);
            }

            /*
            fader.gameObject.SetActive(true);
            fader.DOFade(1f, 1f).OnComplete(() => {
                player.transform.position = destination.position;
                fader.DOFade(0f, 1f).OnComplete(() => {
                    fader.gameObject.SetActive(false);
                    player.GetComponent<PlayerMovement>().enabled = true;
                    player.GetComponent<PlayerCameraMovement>().enabled = true;
                    wallToEnable.SetActive(true);
                    foreach (GameObject deathZone in deathZonesToDisable) {
                        deathZone.SetActive(false);
                    }
                });
            });
            */
        }
    }
}