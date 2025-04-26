using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class QuantumGroup : QuantumGroupBase
{
    private bool considerLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        
    }


    public override void switchObject()
    {
        int n = objects.Count;
        List<int> indicies = Enumerable.Range(0, n).ToList();
        indicies.Remove(curActiveIndex);
            
        for (int i = 0; i < n - 1; i++)
        {
            int r = Random.Range(i, n - 1);
            int temp = indicies[i];
            indicies[i] = indicies[r];
            indicies[r] = temp;
        }

        for (int i = 0; i < n - 1; i++)
        {
            // Debug.Log("tryswitchobject to " + indicies[i]);
            if (trySwitchObject(indicies[i])) break;
        }
    }
}
