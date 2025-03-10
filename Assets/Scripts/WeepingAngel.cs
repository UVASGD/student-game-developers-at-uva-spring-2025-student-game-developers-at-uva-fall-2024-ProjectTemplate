using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;

public class WeepingAngel : MonoBehaviour
{

    public NavMeshAgent ai;
    public Transform player;

    public Animator aiAnim;

    public AudioSource src;
    public AudioClip scareSfx; //footsteps later(?)

    Vector3 dest;
    public Camera playerCam, jumpscareCam;
    public float aiSpeed, catchDistance, jumpscareTime;
    public string sceneAfterDeath;

    //boolean flag
    private bool jmpSfxPlayed;

    private void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        float distance = Vector3.Distance(transform.position, player.position);

        if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = 0;
            aiAnim.speed = 0;
            ai.SetDestination(transform.position);
            
        }
        else if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = aiSpeed;
            aiAnim.speed = 1;
            dest = player.position;
            ai.destination = dest; 
            if(distance <= catchDistance)
            {
                if(!jmpSfxPlayed)
                {
                    playScareSfx();
                    jmpSfxPlayed = true;
                }
                player.gameObject.SetActive(false);
                aiAnim.SetTrigger("Jumpscare");
                jumpscareCam.gameObject.SetActive(true);
                StartCoroutine(killPlayer());
            }
        }
    }

    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(sceneAfterDeath);
    }

    private void playScareSfx()
    {
        src.clip = scareSfx;
        src.Play();
    }

}
