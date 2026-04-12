using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("UI")]
    public Image healthBar;

    public float maxHealth = 100;
    public float currentHealth;
    public GameObject player;  
    public int healthPot = 3;

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

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            if (healthPot != 0) { 
                healthPot --;
                currentHealth = maxHealth;
            }
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

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}