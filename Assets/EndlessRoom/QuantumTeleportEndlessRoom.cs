using UnityEngine;
public class QuantumTeleportEndlessRoom : QuantumObjectBase
{
    public Transform destination;
    public QuantumTeleportEndlessRoom otherQuantumTeleport;

    void Update(){
        if (!isVisible() && !otherQuantumTeleport.isVisible() && EndlessRoomManager.Instance.player.transform.position.z > transform.position.z && !EndlessRoomManager.Instance.isA1Triggered) {
            EndlessRoomManager.Instance.isA1Triggered = true;
            teleportObject(EndlessRoomManager.Instance.player);
        }
    }

    protected void teleportObject(GameObject go)
    {
        Vector3 positionOffset = go.transform.position - transform.position;
        go.transform.position = destination.position + positionOffset;
        // Make an offset from go's rotation and the destination's rotation
        Quaternion rotationOffset = go.transform.rotation * Quaternion.Inverse(transform.rotation);
        go.transform.rotation = destination.rotation * rotationOffset;
    }
}