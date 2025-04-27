using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;

public class WeepingAngelJumpscare : QuantumObjectBase
{

    public NavMeshAgent ai;
    public Transform player;

    public Animator aiAnim;

    public AudioSource src;
    public AudioClip scareSfx; //footsteps later(?)
    public AudioClip shockSfx;

    Vector3 dest;
    public Camera playerCam, jumpscareCam;
    public float aiSpeed, catchDistance, jumpscareTime;

    public float distanceBeforeActivation;

    //boolean flag
    private bool firstLookedAt = true;
    private bool jmpSfxPlayed;
    private bool lookSfxPlayed;
    private bool isTransitioningScene = false;
    private bool jumpscareTriggered = false;

    public Image fader;
    public Transform destination;
    public GameObject wallToEnable;
    public List<GameObject> deathZonesToDisable;


    private void Update()
    {
        if (isTransitioningScene || player == null || jumpscareTriggered)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        ai.speed = aiSpeed;
        dest = player.position;
        ai.destination = dest;
        lookSfxPlayed = false;

        if (distance <= catchDistance && !jumpscareTriggered)
        {
            jumpscareTriggered = true;
            StartCoroutine(TriggerJumpscare());
        }
    }

    IEnumerator TriggerJumpscare()
    {
        // Disable player control during jumpscare
        if (player.GetComponent<PlayerMovement>() != null)
            player.GetComponent<PlayerMovement>().enabled = false;

        if (player.GetComponent<PlayerCameraMovement>() != null)
            player.GetComponent<PlayerCameraMovement>().enabled = false;

        // Activate jumpscare camera
        jumpscareCam.gameObject.SetActive(true);

        // Wait a couple of frames for camera to initialize
        yield return null;
        yield return null;

        // Deactivate player camera
        playerCam.gameObject.SetActive(false);

        // Play jumpscare effects
        if (!jmpSfxPlayed)
        {
            playScareSfx();
            jmpSfxPlayed = true;
            aiAnim.SetTrigger("Jumpscare");
        }

        // Start the kill sequence
        StartCoroutine(killPlayer());
    }

    IEnumerator killPlayer()
    {
        Debug.Log("Kill player sequence started");
        isTransitioningScene = true;

        // Wait for jumpscare animation to play
        yield return new WaitForSeconds(jumpscareTime);

        // Begin fade transition
        fader.gameObject.SetActive(true);
        fader.DOFade(1f, 1f).OnComplete(() => {
            // Teleport player to new position
            player.transform.position = destination.position;

            // Reset cameras
            jumpscareCam.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);

            // Fade back in
            fader.DOFade(0f, 1f).OnComplete(() => {
                fader.gameObject.SetActive(false);

                // Re-enable player controls
                if (player.GetComponent<PlayerMovement>() != null)
                    player.GetComponent<PlayerMovement>().enabled = true;

                if (player.GetComponent<PlayerCameraMovement>() != null)
                    player.GetComponent<PlayerCameraMovement>().enabled = true;

                // Set up new scene state
                wallToEnable.SetActive(true);
                foreach (GameObject deathZone in deathZonesToDisable)
                {
                    deathZone.SetActive(false);
                }

                // Reset for future encounters
                jumpscareTriggered = false;
                jmpSfxPlayed = false;
                isTransitioningScene = false;
            });
        });
    }

    private void playScareSfx()
    {
        src.clip = scareSfx;
        src.Play();
    }

    private void playLookSfx()
    {
        src.clip = shockSfx;
        src.Play();
    }


    public override void onLookAt()
    {
        if (firstLookedAt)
        {
            firstLookedAt = false;
        }
    }

}
