using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public float maxHealth = 10f;
    public GameObject hitEffect, healthBar;
    public bool isEnemy = true;

    private string tagName;
    private float currentHealth;
    private DeathSystem deathScript;
	// Use this for initialization
	void OnEnable ()
    {
        if (isEnemy)
            tagName = "Bullet";
        else
            tagName = "EnemyBullet";

        currentHealth = maxHealth;
	}

    private void Start()
    {
        deathScript = GetComponent<DeathSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);
            Vector3 direction = triggerPosition - transform.position;

            GameObject fx = PoolingManager.instance.UseObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

            PoolingManager.instance.ReturnObject(fx, 1f);

            // do damage here
            float damage = float.Parse(other.name);
            TakeDamage(damage);

            PoolingManager.instance.ReturnObject(other.gameObject);
        }
    }

    public void TakeDamage (float damage)
    {
        currentHealth -= damage;
        CheckHealth();
    }

    void CheckHealth()
    {
        if(currentHealth <= 0f)
        {
            //die
            if (deathScript != null)
                deathScript.Death();
            // if it is enemy, add points
        }
    }
}
