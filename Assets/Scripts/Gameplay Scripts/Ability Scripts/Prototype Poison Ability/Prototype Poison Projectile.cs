using UnityEngine;

public class ProtopyePoisonProjectile : MonoBehaviour
{

    private float poisonTime = 5f;
    private float poisonWeakness = 0.8f; //loses 20% damage (multiplicative)
    private float selfDestructTime = 10f;

    public float PoisonTime { get => poisonTime; set => poisonTime = value; }
    public float PoisonWeakness { get => poisonWeakness; set => poisonWeakness = value; }

    public void Start()
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("outside Poision");
        if (EnemyTags.IsEnemyTag(other.gameObject.tag))
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponentInChildren<Enemy>().ApplyPoison(PoisonTime, poisonWeakness);
        }
        else if (other.gameObject.tag == "Ground" ||
                   other.gameObject.tag == "Lighthouse")
        {
            Destroy(this.gameObject);
        }
    }
}
