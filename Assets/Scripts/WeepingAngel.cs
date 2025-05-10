using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class WeepingAngel : QuantumObjectBase
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
    public string sceneAfterDeath;

    public float distanceBeforeActivation;

    //boolean flag
    private bool firstLookedAt = true;
    private bool jmpSfxPlayed;
    private bool lookSfxPlayed;
    private bool isTransitioningScene = false;



    private async void Update()
    {
        Debug.Log(firstLookedAt);
        if (isTransitioningScene || player == null)
            return;

        //Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        float distance = Vector3.Distance(transform.position, player.position);

        if (isVisible())
        {
            aiAnim.SetBool("LookedAt", true);
            if (!jmpSfxPlayed)
            {
                ai.speed = 0;
                //aiAnim.speed = 0;
                ai.SetDestination(transform.position);
            }
            if (!lookSfxPlayed)
            {
                playLookSfx();
                lookSfxPlayed = true;
            }
            


        }
        else if (!isVisible() && !firstLookedAt)
        {
            ai.speed = aiSpeed;
            //aiAnim.speed = 1;
            dest = player.position;
            ai.destination = dest;
            lookSfxPlayed = false;
            if (distance <= catchDistance)
            {
                jumpscareCam.gameObject.SetActive(true);
                // Wait for the camera to fully activate
                await WaitForCameraActivation(jumpscareCam);
                player.gameObject.SetActive(false);

                if (!jmpSfxPlayed)
                {
                    playScareSfx();
                    jmpSfxPlayed = true;
                    aiAnim.SetTrigger("Jumpscare");
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

        SceneManager.LoadScene(sceneAfterDeath);
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
