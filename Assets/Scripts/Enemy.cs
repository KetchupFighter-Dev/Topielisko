using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public bool providesNextLVL = false;

    public void TakeDamage(float damage)
    {
        health -= damage+6;

        if (health <= 0)
        {
            Destroy(gameObject);
            if (providesNextLVL)
            {
                int currentIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentIndex + 1);
            }
        }
    }
}