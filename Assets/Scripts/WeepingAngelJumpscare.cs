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
    private AudioListener jumpscareListener;
    public float aiSpeed, catchDistance, jumpscareTime;

    public float distanceBeforeActivation;

    //fixedJumpscare?
    public GameObject mannequin;
    public Animator headAnim;

    //boolean flag
    private bool firstLookedAt = true;
    private bool jmpSfxPlayed;
    private bool lookSfxPlayed;
    private bool isTransitioningScene = false;
    private bool jumpscareTriggered = false;

    public Image fader;
    public Transform destination;
    public GameObject wallToEnable;
    public GameObject wallToDisable;
    public List<GameObject> deathZonesToDisable;
    public List<GameObject> deathBotsToEnable;

    private void Start()
    {
        if (jumpscareCam != null)
        {
            jumpscareCam.enabled = false; // it's active but disabled
        }
        if (jumpscareCam != null)
            jumpscareListener = jumpscareCam.GetComponent<AudioListener>();

        if (jumpscareListener != null)
            jumpscareListener.enabled = false; // Make sure it's disabled by default
    }

    private void Update()
    {
        if (isTransitioningScene || player == null || jumpscareTriggered)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        ai.speed = aiSpeed;
        dest = player.position;
        ai.destination = dest;

        if (distance <= catchDistance )//&& !jumpscareTriggered)
        {
            jumpscareTriggered = true;
            StartCoroutine(TriggerJumpscare());
            Debug.Log("THis is HAPPENING");
            aiAnim.SetTrigger("Jumpscare");
            aiAnim.SetBool("jumpscare", true);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            aiAnim.SetTrigger("Jumpscare"); // Make sure "attack" is the name in your Animator
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
        mannequin.SetActive(true);
        headAnim.enabled = true;
        AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.jumpscare);

        yield return null;  // This fixes the error

        Debug.Log("Setting trigger on: " + aiAnim.gameObject.name);

        // Start the kill sequence
        StartCoroutine(killPlayer());
    }

    IEnumerator killPlayer()
    {
        Debug.Log("Kill player sequence started");
        isTransitioningScene = true;

        // Wait for jumpscare animation to play
        yield return new WaitForSeconds(jumpscareTime);

        /*
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
        */
        // Re-enable player controls

        //Teleport player to new position
        player.transform.position = destination.position;

        // Reset cameras
        //jumpscareCam.gameObject.SetActive(false);
        //playerCam.gameObject.SetActive(true);
        headAnim.enabled = false;
        mannequin.SetActive(false);

        if (player.GetComponent<PlayerMovement>() != null)
            player.GetComponent<PlayerMovement>().enabled = true;

        if (player.GetComponent<PlayerCameraMovement>() != null)
            player.GetComponent<PlayerCameraMovement>().enabled = true;

        // Set up new scene state
        wallToEnable.SetActive(true);
        wallToDisable.SetActive(false);
        foreach (GameObject deathZone in deathZonesToDisable)
        {
            deathZone.SetActive(false);
        }

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
