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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            TakeDamage(10);
        }
    }
}