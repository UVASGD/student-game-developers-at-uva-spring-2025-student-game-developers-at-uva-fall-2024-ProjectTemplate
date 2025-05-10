using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using MEET_AND_TALK;
using System.Collections;

public class Cutscene : MonoBehaviour
{
    public Image fader;
    public float fadeTime = 1f;
    public DialogueContainerSO dialogue;

    void Start()
    {
        StartCoroutine(CutsceneCo());
    }

    IEnumerator CutsceneCo(){
        fader.DOFade(0, fadeTime);
        yield return new WaitForSeconds(fadeTime);
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
