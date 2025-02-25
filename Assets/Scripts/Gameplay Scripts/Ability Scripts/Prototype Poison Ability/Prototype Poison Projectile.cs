using UnityEngine;

public class ProtopyePoisonProjectile : MonoBehaviour
{

    private float poisonTime = 5f;
    private float poisonWeakness = 0.8f; //loses 20% damage (multiplicative)
    private float selfDestructTime = 10f;

    public void Start()
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Enemy>().ApplyPoison(poisonTime, poisonWeakness);
        }
        else if (other.gameObject.tag == "Ground" ||
                   other.gameObject.tag == "Townhall")
        {
            Destroy(this.gameObject);
        }
    }
}
