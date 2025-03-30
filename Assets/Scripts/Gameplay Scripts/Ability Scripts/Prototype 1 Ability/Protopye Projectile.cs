using Unity.VisualScripting;
using UnityEngine;

public class ProtopyeProjectile : MonoBehaviour
{

    private float damage = 5f;

    public float selfDestructTime = 4f;

    public void Start() 
    {
        Destroy(this.gameObject, selfDestructTime);
    }

    public float getDamage()
    {
        return damage;
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        string tag = other.gameObject.tag;
        
        if (EnemyTags.IsEnemyTag(tag))
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            
        } else if (other.gameObject.tag == "Ground" ||
                   other.gameObject.tag == "Lighthouse")
        {
            Destroy(this.gameObject);
        }
    }
}
