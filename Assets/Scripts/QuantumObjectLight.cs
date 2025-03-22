using UnityEngine;

public class QuantumObjectLight : QuantumGroupObject
{
    public bool litUp = true;
    // overriding the isVisible to work with flashlight stuff
    new public bool isVisible()
    {
        if (Camera.main != null)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), mr.bounds) && litUp;
        }
        return false;
    }
}
