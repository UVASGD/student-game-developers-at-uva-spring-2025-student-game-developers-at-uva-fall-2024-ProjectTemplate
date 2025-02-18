using UnityEngine;

public class ProtopyeBurnProjectile : MonoBehaviour
{

    private float tickDuration = 1;
    private float burnDamage = 25;
    private int numTicks = 2;

    public float selfDestructTime = 4f;

    public void Start() 
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Enemy>().ApplyBurn(tickDuration, burnDamage, numTicks);
        } else if (other.gameObject.tag == "Ground" ||
                   other.gameObject.tag == "Townhall")
        {
            Destroy(this.gameObject);
        }
    }
}
