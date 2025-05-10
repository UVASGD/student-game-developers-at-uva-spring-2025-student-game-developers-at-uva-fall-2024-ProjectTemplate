using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : QuantumObjectBase
{
    [SerializeField] private List<Color> colors;
    public override void onLookAway() {
        if(colors.Count > 0)
        {
            mr.material.color = colors[Random.Range(0, colors.Count)];
        }
    }
}
