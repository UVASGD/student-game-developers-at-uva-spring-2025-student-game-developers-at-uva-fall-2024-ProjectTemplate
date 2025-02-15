using UnityEngine;
public class QuantumGroupObject : QuantumObjectBase
{
    private QuantumGroupBase group;
    private Collider coll;
    private bool isActive;

    private Material defaultMaterial;
    private static Material invisMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent<Collider>(out coll);
        defaultMaterial = mr.sharedMaterial;
        if(invisMaterial == null)
        {
            invisMaterial = Resources.Load<Material>("Materials/Invisible");
        }
    }

    public override void onLookAway()
    {
        group.switchObject();
    }

    public void attachToGroup(QuantumGroupBase newGroup)
    {
        group = newGroup;
    }

    public void setVisible()
    {
        mr.material = defaultMaterial;
        if (coll != null)
        {
            coll.enabled = true;
        }
        
    }

    public void setInvisible()
    {
        mr.material = invisMaterial;
        if (coll != null)
        {
            coll.enabled = false;
        }
    }

}
