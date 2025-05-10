using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Fader : MonoBehaviour
{
    private Image _fader;
    public float fadeTime = 1f;

    void Start()
    {
        _fader = GetComponent<Image>();
    }

    public void FadeIn(float delay)
    {
        StartCoroutine(FadeInCo(delay));
    }

    IEnumerator FadeInCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        _fader.DOFade(1, fadeTime);
    }

    public void FadeOut(float delay)
    {
        StartCoroutine(FadeOutCo(delay));
    }

    IEnumerator FadeOutCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        _fader.DOFade(0, fadeTime);
    }
}