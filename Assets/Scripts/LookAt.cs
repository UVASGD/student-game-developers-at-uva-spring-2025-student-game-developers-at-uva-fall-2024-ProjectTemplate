using System.Collections.Generic;
using UnityEngine;

public class LookAt : QuantumObjectBase
{
    [SerializeField] private List<Color> colors;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void onLookAway()
    {
        if(player != null)
        {
            transform.LookAt(player.transform.position);
        }
    }
}
