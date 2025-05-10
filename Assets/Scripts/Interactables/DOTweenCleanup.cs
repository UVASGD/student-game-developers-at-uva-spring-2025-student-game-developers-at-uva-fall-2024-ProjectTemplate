using DG.Tweening;
using UnityEngine;

public class DOTweenCleanup : MonoBehaviour
{
    void Start()
    {
        DOTween.KillAll(); // Kills any tweens still running from a previous scene
    }
}