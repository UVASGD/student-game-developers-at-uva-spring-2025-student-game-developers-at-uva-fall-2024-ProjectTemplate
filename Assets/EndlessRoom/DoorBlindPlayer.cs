using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoorBlindPlayer : Door {   
    public Image whiteFlash;
    public AudioSource lightHum;

    public override void Interact() {
        base.Interact();
        whiteFlash.DOFade(1f, 5f);
        lightHum.DOFade(1f, 2f);
    }
}