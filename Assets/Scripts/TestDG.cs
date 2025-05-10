using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class TestDG : MonoBehaviour {
    public Transform cube;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            cube.DOMove(new Vector3(0, 4, 0), 1);
        }
    }
}