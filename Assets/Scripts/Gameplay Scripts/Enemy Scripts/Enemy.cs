using System;
using System.Collections;
using System.Linq;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float maxHealth = 100f;
    private float health = 100f;
    private const float DEFAULT_SPEED = 3f;
    private const float BASE_DAMAGE = 10f;
    private float speed = DEFAULT_SPEED;
    private float damage;
    private Transform target;

    private bool isFrozen = false;
    
    private bool isBurned = false;
    private float burnTimer = 0f;
    private int burnTick = 0;
    private float tickDuration = 0f; // for burn
    private float burnDamage = 0f;

    private bool isVulnerable = false;
    private float vulnerableHealth = 0f;
    private float vulnerableTimer = 0f;
    private bool isDoingDamage = false;
    private bool isBeguiled = false;
    private bool isSlowed = false;
    private float slowTimer = 0f;

    private bool isPoisoned = false;
    private float poisonTimer = 0f;
    private float damageWeakness = 0.8f; //loses 20% damage when weak

    private bool isDefenseDown = false;
    private float defenseTimer = 0f;
    private float healthWeakness = 0.8f;
    private GameObject townHall;
    private Lighthouse lighthouse;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image sliderBar;
    [SerializeField] private Transform healthBarTransform;
    [SerializeField] private TextMeshProUGUI healthText; //only needed for text over health bar for debugging

    private EnemySpawnManager enemySpawnManager;

    private void Start() 
    {
        townHall = GameObject.Find("Lighthouse");
        enemySpawnManager = GameObject.Find("Enemy Spawn Manager").GetComponent<EnemySpawnManager>();
        lighthouse = townHall.GetComponent<Lighthouse>();
        target = lighthouse.transform;
        SetRoundDamage();
        //healthText.text = health.ToString("#.0") + " / " + maxHealth.ToString("#.0");
    }

    private void Update()
    {
         // If the enemy is beguiled, BeguileTimer() will run MoveTo()
        if (!isBeguiled)
        {
            Move();
        }

        //if (isDefenseDown) { HandleDefenseDown(); }

        if (isPoisoned) {HandlePoison();}
        // if (isFrozen){HandleFreeze();}
        if (isSlowed){HandleSlow();}
        if (isBurned){HandleBurn();}
        if (isVulnerable){HandleVulnerable();}
    }

    private void LateUpdate()
    {
        healthBarTransform.LookAt(Camera.main.transform);
    }

    // Basic movement logic shared by all enemies
    protected virtual void Move()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for " + name);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    protected virtual void MoveToEnemy(Enemy enemy)
    {
        // If the enemy its chasing is killed it'll return back to normal
        if (enemy == null)
        {
            isBeguiled = false;
            return;
        }

        Vector3 directionToEnemy = enemy.transform.position - transform.position;
        transform.position += directionToEnemy.normalized * speed * Time.deltaTime;
    }

    private void SetRoundDamage()
    {
        int currentRound = RoundManager.Instance.GetCurrentRound();
        float damageMultiplier;

        if (currentRound >= 20)
        {
            damageMultiplier = (float) (Math.Log10(currentRound - 19) + 4);
        }
        else if (currentRound >= 10)
        {
            damageMultiplier = (float)(Math.Log10(currentRound - 9) + 2.5);
        }
        else
        {
            damageMultiplier = (float) (Math.Log10(currentRound + 1) + .7);
        }
        
        damage = BASE_DAMAGE * damageMultiplier;
        Debug.Log($"Doing {damage} damage");
    }

    // Generic method to take damage
    public virtual void TakeDamage(float amount)
    {
        health -= amount;

        if (isVulnerable)
        {
            vulnerableHealth -= amount;
            if(vulnerableHealth <= 0)
            {
                Die();
            }
            healthBar.value = vulnerableHealth / maxHealth;
            //healthText.text = vulnerableHealth.ToString("#.0") + " / " + maxHealth.ToString("#.0");
        }
        else
        {
            if (health <= 0)
            {
                Die();
            }
            healthBar.value = health / maxHealth;
            //healthText.text = health.ToString("#.0") + " / " + maxHealth.ToString("#.0");
        }
    }

    public void Beguile(float beguileTime)
    {
        Debug.Log("Beguiled!");
        if (isBeguiled)
        {
            return;
        }

        // If no enemies to chase
        if (enemySpawnManager.GetAliveEnemiesCount() == 0)
        {
            return;
        }

        StartCoroutine(BeguileTimer(beguileTime));
    }

    private IEnumerator BeguileTimer(float beguileTime)
    {
        isBeguiled = true;
        Enemy closestEnemy = FindClosestEnemy();
        if (closestEnemy == null)
        {
            yield break;
        }

        float beguileTimer = 0f;
        while (isBeguiled)
        {
            MoveToEnemy(closestEnemy);
            beguileTimer += Time.deltaTime;
            if (beguileTimer > beguileTime)
            {
                isBeguiled = false;
            }
            yield return null;
        }
    }

    public Enemy FindClosestEnemy()
    {
        Enemy closestEnemy = enemySpawnManager.GetEnemies()
            .Where(enemy => enemy != this)
            .OrderBy(enemy => (enemy.transform.position - this.transform.position).sqrMagnitude)
            .FirstOrDefault();

        return closestEnemy;
    }

    public void ApplyFreeze(float freezeTime)
    {
        if (!isFrozen)
        {
            StartCoroutine(HandleFreeze(freezeTime));
        }
    }

    private IEnumerator HandleFreeze(float freezeTime)
    {
        isFrozen = true;
        isDoingDamage = false;
        SetSpeed(0f);
        lighthouse.RemoveFromEnemiesList(this);

        yield return new WaitForSeconds(freezeTime);
        isFrozen = false;
        isDoingDamage = true;
        ResetSpeed();
        lighthouse.AddToEnemiesList(this);
    }

    private void SetSpeed(float speed)
    {
        if (speed < 0f)
        {
            speed = 0f;
        }
            
        this.speed = speed;
    }
    
    public void ApplyPoison(float poisonTime, float damageWeak) {
        if(!isPoisoned) {
            isPoisoned = true;
            damageWeakness = damageWeak;
            damage *= damageWeakness;
            poisonTimer = poisonTime;
        }
    }
    private void HandlePoison()
    {
        poisonTimer -= Time.deltaTime;
        
        if (poisonTimer <= 0)
        {
            isPoisoned = false;
            damage /= damageWeakness;
        }
    }

    public void ApplySlow(float slowTime, float slowMangitude)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            slowTimer = slowTime;
            speed *= slowMangitude;
        }
    }

    private void HandleSlow()
    {
        slowTimer -= Time.deltaTime;
        
        if (slowTimer <= 0)
        {
            isSlowed = false;
            ResetSpeed();
        }
    }
    public void ApplyBurn(float tickDur, float burnDmg, int numTicks)
    {
        if (!isBurned)
        {
            isBurned = true;
            burnTick = numTicks;
            tickDuration = tickDur;
            burnTimer = tickDuration;
            burnDamage = burnDmg;
        }
    }

    private void HandleBurn()
    {
        burnTimer -= Time.deltaTime;

        if (burnTimer <= 0)
        {
            TakeDamage(burnDamage);

            burnTimer += tickDuration;
            burnTick--;

            if (burnTick <= 0)
            {
                isBurned = false;
            }
        }
    }

    public void ApplyVulnerable(float vulnerableTime, float vulnerablePercentage)
    {
        if (!isVulnerable)
        {
            isVulnerable = true;
            vulnerableHealth = health * vulnerablePercentage;
            vulnerableTimer = vulnerableTime;
            sliderBar.color = Color.green;
            healthBar.value = vulnerableHealth / maxHealth;
            //healthText.text = vulnerableHealth.ToString("#.0") + " / " + maxHealth.ToString("#.0");
        }
    }

    private void HandleVulnerable()
    {
        vulnerableTimer -= Time.deltaTime;

        if (vulnerableTimer <= 0)
        {
            isVulnerable = false;
            sliderBar.color = Color.red;
            healthBar.value = health / maxHealth;
            //healthText.text = health.ToString("#.0") + " / " + maxHealth.ToString("#.0");
        }
    }

    public void ApplyDefenseDown(float defenseTime)
    {
        if (!isDefenseDown)
        {
            isDefenseDown = true;
            defenseTimer = defenseTime;
            health *= healthWeakness;
        }
    }

    private void HandleDefenseDown()
    {
        defenseTimer -= Time.deltaTime;
        
        if (defenseTimer <= 0)
        {
            isDefenseDown = false;
            health /= healthWeakness;
        }
    }


    public void SetDoingDamage(bool doingDamage)
    {
        isDoingDamage = doingDamage;
    }

    public bool GetDoingDamage() => isDoingDamage;

    private void ResetSpeed()
    {
        speed = DEFAULT_SPEED;
    }

    // Death logic
    protected virtual void Die()
    {
        // Ensures out of townhall range do not effect enemiesInRange set
        if (isDoingDamage)
        {
            lighthouse.RemoveFromEnemiesList(this);
        }

        enemySpawnManager.RemoveEnemyFromList(this);
        Debug.Log($"{name} has died!");
        Destroy(gameObject);
    }

    // Placeholder for abilities (to be overridden by child or component scripts)
    public virtual void UseAbility()
    {
        Debug.Log($"{name} has no special ability!");
    }

    public float GetDamage() => damage;
}