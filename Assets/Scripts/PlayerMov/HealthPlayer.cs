using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Health : MonoBehaviour
{
    [Header("UI")]
    public Image healthBar;
    public TextMeshProUGUI potionText;

    public float maxHealth = 100;
    public float currentHealth;
    public GameObject player;  
    public int healthPot = 3;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBarUpdate();
        UpdatePotionUI();
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
                HealthBarUpdate();
                UpdatePotionUI();
            }
        }
    }

    public void HealthBarUpdate()
    {
        healthBar.fillAmount = currentHealth/maxHealth;
    }
    void UpdatePotionUI()
    {
        potionText.text = "X " + healthPot;
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