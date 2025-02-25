using System;
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
    private float freezeTimer = 0f;
    
    private bool isBurned = false;
    private float burnTimer = 0f;
    private int burnTick = 0;
    private float tickDuration = 0f; // for burn
    private float burnDamage = 0f;

    private bool isVulnerable = false;
    private float vulnerableHealth = 0f;
    private float vulnerableTimer = 0f;
    private bool isDoingDamage = false;

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
    [SerializeField] private TextMeshProUGUI healthText;

    private EnemySpawnManager enemySpawnManager;

    private void Start() 
    {
        townHall = GameObject.Find("Lighthouse");
        enemySpawnManager = GameObject.Find("Enemy Spawn Manager").GetComponent<EnemySpawnManager>();
        lighthouse = townHall.GetComponent<Lighthouse>();
        target = lighthouse.transform;
        SetRoundDamage();
        healthText.text = health.ToString("#.0") + " / " + maxHealth;
    }

    private void Update()
    {
        if (isFrozen) {HandleFreeze();}
        if (isPoisoned) {HandlePoison();}
        if (isDefenseDown) {HandleDefenseDown();}
        if(isBurned) {HandleBurn();}
        if (isFrozen){HandleFreeze();}
        if (isSlowed){HandleSlow();}
        if (isBurned){HandleBurn();}
        if (isVulnerable){HandleVulnerable();}
        Move();
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
            healthText.text = vulnerableHealth.ToString("#.0") + " / " + maxHealth;
        }
        else
        {
            if (health <= 0)
            {
                Die();
            }
            healthBar.value = health / maxHealth;
            healthText.text = health.ToString("#.0") + " / " + maxHealth;
        }
    }

    public void ApplyFreeze(float freezeTime)
    {
        if (!isFrozen)
        {
            isFrozen = true;
            isDoingDamage = false;
            freezeTimer = freezeTime;
            speed = 0f;
            lighthouse.RemoveFromEnemiesList(this);

        } else {

            freezeTimer = freezeTime;
        }
    }

    private void HandleFreeze()
    {
        freezeTimer -= Time.deltaTime;
        
        if (freezeTimer <= 0)
        {
            isFrozen = false;
            isDoingDamage = true;
            ResetSpeed();
            lighthouse.AddToEnemiesList(this);
        }
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
            healthText.text = vulnerableHealth.ToString("#.0") + " / " + maxHealth;
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
            healthText.text = health.ToString("#.0") + " / " + maxHealth;
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
        
        enemySpawnManager.DecrementEnemyCount();
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