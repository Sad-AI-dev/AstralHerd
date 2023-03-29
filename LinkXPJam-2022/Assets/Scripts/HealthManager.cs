using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Transform healthBar;
    float startSize;
    [SerializeField] float maxHealth;
    float health;

    public UnityEvent onHit = new();
    public UnityEvent onDeath = new();

    private void Start()
    {
        if (healthBar) { startSize = healthBar.localScale.x; }
        health = maxHealth;
        UpdateHealthBar();
    }

    public void DealDamage(float dmg)
    {
        health -= dmg;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0) { Die(); }
        else { onHit?.Invoke(); }
        //update UI
        UpdateHealthBar();
    }

    void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        if (healthBar != null) {
            healthBar.localScale = new Vector2((health / maxHealth) * startSize, healthBar.localScale.y);
        }
    }
}