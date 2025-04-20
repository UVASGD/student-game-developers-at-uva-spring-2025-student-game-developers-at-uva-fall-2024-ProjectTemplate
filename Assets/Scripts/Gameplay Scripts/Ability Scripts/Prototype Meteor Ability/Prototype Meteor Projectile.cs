using UnityEngine;

public class ProtopyeMeteorProjectile : MonoBehaviour
{

    private float selfDestructTime = 10f;
    private float aoeRadius = 10;
    private float damage = 100f;

    public void Start()
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        //AOE
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Lighthouse")
        {
            Vector3 pos = transform.position;
            Destroy(this.gameObject);

            Collider[] hitColliders = Physics.OverlapSphere(pos, aoeRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Enemy")
                {
                    hitCollider.gameObject.GetComponentInChildren<Enemy>().TakeDamage(damage);
                }
            }
        }
    }
}
