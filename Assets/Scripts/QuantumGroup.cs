using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class QuantumGroup : QuantumGroupBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<QuantumGroupObject> objects = new List<QuantumGroupObject>();
    private int curActiveIndex;
    void Start()
    {
        InitializeQuantumObjects();
    }

    private void InitializeQuantumObjects()
    {
        foreach (Transform child in transform)
        {
            objects.Add(child.AddComponent<QuantumGroupObject>());
        }
        foreach (QuantumGroupObject q in objects)
        {
            q.attachToGroup(this);
            q.setInvisible();
        }
        if (objects.Count >= 1)
        {
            objects[0].setVisible();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void switchObject()
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
            if (trySwitchObject(indicies[i])) break;
        }
    }
}
