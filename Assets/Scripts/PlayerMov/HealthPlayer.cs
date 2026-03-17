using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("UI")]
    public Image healthBar;

    public float maxHealth = 100;
    public float currentHealth;
    public GameObject player;  

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player != null)
        {
            HealthBarUpdate();
        }
    }

    public void HealthBarUpdate()
    {
        healthBar.fillAmount = currentHealth/maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        HealthBarUpdate();
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }
}