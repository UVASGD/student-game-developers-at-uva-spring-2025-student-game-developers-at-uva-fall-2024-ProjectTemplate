using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class QuantumObjectBase : MonoBehaviour, IQuantumObject
{
    protected MeshRenderer mr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }
    public bool isVisible()
    {
        if (Camera.main != null)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), mr.bounds);
        }
        return false;
    }

    private void OnBecameVisible()
    {
        onLookAt();
    }

    private void OnBecameInvisible()
    {
        onLookAway();
    }
    public virtual void onLookAway(){}

    public virtual void onLookAt(){}
}
public interface IQuantumObject
{
    void onLookAway();
    void onLookAt();
    bool isVisible();
}