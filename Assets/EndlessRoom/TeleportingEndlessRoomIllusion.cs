using UnityEngine;

public enum PortalNumber
{
    A1, A2, A3, A4, A5, A6, A7, A8, A9, A10
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
        Debug.Log("Teleported player");
        Vector3 positionOffset = go.transform.position - transform.position;
        go.transform.position = destination.position + positionOffset;
        Quaternion rotationOffset = destination.rotation * Quaternion.Inverse(transform.rotation);
        go.transform.rotation = go.transform.rotation * rotationOffset;
        if (portalNumber == PortalNumber.A2) EndlessRoomManager.Instance.numA3Triggered = 0;
        if (portalNumber == PortalNumber.A3) EndlessRoomManager.Instance.A3Triggered();
        if (portalNumber == PortalNumber.A6) {
            EndlessRoomManager.Instance.mazeGenerator.CreateMaze();
            EndlessRoomManager.Instance.ActivateMazeBoard();
        }
    }
}