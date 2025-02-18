using UnityEngine;

public class ProtopyeBurnProjectile : MonoBehaviour
{

    private float poisonTime = 5f;

    public void Start()
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Enemy>().ApplyPoison(poisonTime)
        }
        else if (other.gameObject.tag == "Ground" ||
                   other.gameObject.tag == "Townhall")
        {
            Destroy(this.gameObject);
        }
    }
}
