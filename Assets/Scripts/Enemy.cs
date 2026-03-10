using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float dmg = 10;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);
    }
}