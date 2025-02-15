using UnityEngine;

public class TeleportingIllusionBase : MonoBehaviour
{
    [SerializeField] protected Transform destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void teleportObject(GameObject go)
    {
        go.transform.position = destination.position;
        go.transform.eulerAngles = destination.eulerAngles;
    }
}
