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

    public void Attack()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

}