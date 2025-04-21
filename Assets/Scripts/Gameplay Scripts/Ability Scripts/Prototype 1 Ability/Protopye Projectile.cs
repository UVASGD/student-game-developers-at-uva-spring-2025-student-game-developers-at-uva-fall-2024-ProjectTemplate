using Unity.VisualScripting;
using UnityEngine;

public class ProtopyeProjectile : MonoBehaviour
{

    private float damage = 100f;

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
        if (EnemyTags.IsEnemyTag(other.gameObject.tag))
        {
            Destroy(this.gameObject);
            Enemy enemy = other.gameObject.GetComponentInChildren<Enemy>();
            enemy.TakeDamage(damage);
            
        } else if (other.gameObject.tag == "Ground" ||
                   other.gameObject.tag == "Lighthouse")
        {
            Destroy(this.gameObject);
        }
    }
}
