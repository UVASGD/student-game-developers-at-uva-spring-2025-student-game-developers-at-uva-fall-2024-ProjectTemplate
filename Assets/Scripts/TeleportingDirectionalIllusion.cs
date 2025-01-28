using UnityEngine;

public class TeleportingDirectionalIllusion : TeleportingIllusion
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float angleTolerance;

    public override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            Debug.Log(Vector3.Angle(Camera.main.transform.forward, direction));
            if(Vector3.Angle(Camera.main.transform.forward, direction) < angleTolerance)
            {
                teleportObject(player);
            }
        }
    }
}
