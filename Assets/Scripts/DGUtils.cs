using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DGUtils : MonoBehaviour {
    [Header("Move/Scale/Rotate To")]
    public new Transform transform;
    public Vector3 targetPosition;
    public Vector3 targetScale;
    public Vector3 targetRotation;
    public float duration;
    [Header("Scene Transition")]
    [Tooltip("Eventually this will be removed, as the plan is to have all levels in one scene")]
    public string sceneToLoad;
    [Header("Particle System")]
    public ParticleSystem particles;
    [Header("Audio Source")]
    public AudioSource audioSource;
    [Header("Animation")]
    public Animator animator;
    public string animToPlay;

    public void TriggerMoveTo(float delay){
        MoveTo(transform, targetPosition, duration, delay);
    }

    public void MoveTo(Transform transform, Vector3 targetPosition, float duration, float delay){
        transform.DOMove(targetPosition, duration).SetDelay(delay);
    }

    public void TriggerScaleTo(float delay){
        ScaleTo(transform, targetScale, duration, delay);
    }

    public void ScaleTo(Transform transform, Vector3 targetScale, float duration, float delay){
        transform.DOScale(targetScale, duration).SetDelay(delay);
    }

    public void TriggerRotateTo(float delay){
        RotateTo(transform, targetRotation, duration, delay);
    }

    public void RotateTo(Transform transform, Vector3 targetRotation, float duration, float delay){
        transform.DORotate(targetRotation, duration).SetDelay(delay);
    }

    public void PlayParticles(float delay){
        StartCoroutine(PlayParticlesCo(delay));
    }

    public IEnumerator PlayParticlesCo(float delay){
        yield return new WaitForSeconds(delay);
        particles.Play();
    }

    public void PlaySound(float delay){
        StartCoroutine(PlaySoundCo(delay));
    }

    public IEnumerator PlaySoundCo(float delay){
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }

    public void PlayAnimation(float delay){
        StartCoroutine(PlayAnimationCo(delay));
    }

    public IEnumerator PlayAnimationCo(float delay){
        yield return new WaitForSeconds(delay);
        animator.Play(animToPlay);
    }

    public void TransitionToScene(float delay){
        StartCoroutine(TransitionToSceneCo(delay));
    }

    public IEnumerator TransitionToSceneCo(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}