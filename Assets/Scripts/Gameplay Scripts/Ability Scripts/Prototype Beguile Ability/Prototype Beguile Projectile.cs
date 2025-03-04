using UnityEngine;

public class ProtopyeBeguileProjectile : MonoBehaviour
{

    private float beguileTime = 10f;
    private const float BEGUILE_TIME = 10f;

    public float selfDestructTime = 4f;

    public void Start() 
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    public float getBeguileTime()
    {
        return beguileTime;
    }

    public void setBeguileTime(float beguileTime)
    {
        // Default beguile time if input is invalid
        if (beguileTime < 0)
        {
            beguileTime = BEGUILE_TIME;
        }

        this.beguileTime = beguileTime;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Beguile(beguileTime);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Ground" ||
                other.gameObject.tag == "Lighthouse")
        {
            Destroy(this.gameObject);
        }
    }
}
