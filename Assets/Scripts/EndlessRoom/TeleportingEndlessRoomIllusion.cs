using UnityEngine;

public enum PortalNumber
{
    A1,
    A2,
    A3
}

public class TeleportingEndlessRoomIllusion : TeleportingIllusion {
    public bool isActive;
    public PortalNumber portalNumber;
    private Vector3 direction;

    public override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            if (isActive) teleportObject(player);
        }
    }

    public new void teleportObject(GameObject go)
    {
        Vector3 positionOffset = Quaternion.FromToRotation(destination.forward, direction) * (go.transform.position - transform.position);
        go.transform.position = destination.position + positionOffset;
        if (portalNumber == PortalNumber.A2) EndlessRoomManager.Instance.numA3Triggered = 0;
        if (portalNumber == PortalNumber.A3) EndlessRoomManager.Instance.A3Triggered();
    }
}