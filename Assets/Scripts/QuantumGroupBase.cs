using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class QuantumGroupBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected List<QuantumGroupObject> objects = new List<QuantumGroupObject>();
    protected int curActiveIndex;
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

    public virtual void switchObject()
    {

    }
    public bool trySwitchObject(int newIndex)
    {
        Debug.Log(newIndex);
        QuantumGroupObject newActive = objects[newIndex];
        QuantumGroupObject curActive = objects[curActiveIndex];

        if (newActive.isVisible() || curActive.isVisible())
        {
            return false;
        }
        else
        {
            curActive.setInvisible();
            newActive.setVisible();
            curActiveIndex = newIndex;
            return true;
        }
    }
}
