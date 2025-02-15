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
        Debug.Log(go.transform.position - transform.position);
        Vector3 positionOffset = Quaternion.FromToRotation(destination.forward, direction) * (go.transform.position - transform.position);
        go.transform.position = destination.position + positionOffset;
        Quaternion rotationOffset = go.transform.rotation * Quaternion.Inverse(transform.rotation);
        go.transform.rotation = destination.rotation * rotationOffset;
        if (portalNumber == PortalNumber.A2) EndlessRoomManager.Instance.numA3Triggered = 0;
        if (portalNumber == PortalNumber.A3) EndlessRoomManager.Instance.A3Triggered();
        if (portalNumber == PortalNumber.A5) {
            foreach (MazeGenerator mazeGenerator in EndlessRoomManager.Instance.mazeGenerators){
                mazeGenerator.CreateMaze();
            }
        }
    }
}