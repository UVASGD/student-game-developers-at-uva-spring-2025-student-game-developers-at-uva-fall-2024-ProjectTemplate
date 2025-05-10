using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;

public class WeepingAngel : QuantumObjectBase
{

    public NavMeshAgent ai;
    public Transform player;

    public Animator aiAnim;
    public Animator headAnim;

    public AudioSource src;
    public AudioClip scareSfx; //footsteps later(?)
    public AudioClip shockSfx;

    Vector3 dest;
    public Camera playerCam, jumpscareCam;
    public float aiSpeed, catchDistance, jumpscareTime;
    public string sceneAfterDeath;
    public GameObject mannequin;

    public float distanceBeforeActivation;

    //boolean flag
    private bool firstLookedAt = true;
    private bool jmpSfxPlayed;
    private bool lookSfxPlayed;
    private bool isTransitioningScene = false;

    [SerializeField] public Transform playerRespawnPos;
    [SerializeField] private Transform weepRespawnPos;
    [SerializeField] private Image fader;

    // private int num_lookAt = 0;

    private async void Update()
    {
        //Debug.Log(firstLookedAt);
        if (isTransitioningScene || player == null)
            return;

        //Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        float distance = Vector3.Distance(transform.position, player.position);

        if (isVisible())
        {
            AudioManager.audioManagerInstance.StopSFX();

            aiAnim.SetBool("LookedAt", true);

            if (!jmpSfxPlayed)
            {
                ai.speed = 0;
                //aiAnim.speed = 0;
                ai.SetDestination(transform.position);
            }
            if (!lookSfxPlayed)
            {
                /*
                if (num_lookAt == 0)
                {
                    AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.vineboom);
                    num_lookAt = 1;
                }*/
                lookSfxPlayed = true;
            }
            


        }
        else if (!isVisible() && !firstLookedAt)
        {
            if (!AudioManager.audioManagerInstance.sfxSource.isPlaying)
            {
                AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.footsteps);
            }

            ai.speed = aiSpeed;
            //aiAnim.speed = 1;
            dest = player.position;
            ai.destination = dest;
            lookSfxPlayed = false;
            if (distance <= catchDistance)
            {
                //jumpscareCam.gameObject.SetActive(true);
                // Wait for the camera to fully activate
                
                //player.gameObject.SetActive(false);
                mannequin.SetActive(true);

                if (!jmpSfxPlayed)
                {
                    AudioManager.audioManagerInstance.PlaySFX(AudioManager.audioManagerInstance.jumpscare);
                    jmpSfxPlayed = true;
                    //aiAnim.SetTrigger("Jumpscare");
                    headAnim.enabled = true;
                }

                StartCoroutine(killPlayer());
            }
            aiAnim.SetBool("LookedAt", false);
        }
    }

    IEnumerator killPlayer()
    {
        isTransitioningScene = true;
        yield return new WaitForSeconds(jumpscareTime);
        headAnim.enabled = false;
        transform.position = weepRespawnPos.position;
        
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
        mannequin.SetActive(false);
        isTransitioningScene = false;
        firstLookedAt = true;
        jmpSfxPlayed = false;
        ai.speed = 0;
        if(fader != null)
        {
            fader.gameObject.SetActive(true);
            fader.DOFade(1f, 1f).OnComplete(() => {
                Debug.Log("teleport");
                player.transform.position = playerRespawnPos.position;
                fader.DOFade(0f, 1f).OnComplete(() => {
                    fader.gameObject.SetActive(false);
                });
            });
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

    private async Task WaitForCameraActivation(Camera cam)
    {
        // Wait for camera to be fully enabled
        await Task.Delay(50); // Wait 50ms for the camera to initialize

        // You could also wait for the first frame render
        await Task.Yield(); // Wait for next frame

        // For more reliability, you could wait a couple of frames
        for (int i = 0; i < 2; i++)
        {
            await Task.Yield();
        }

        // The camera should now be fully activated
    }

    public override void onLookAt()
    {
        if (firstLookedAt )
        {
            firstLookedAt = false;
        }
    }

}
