using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int baseDamage = 1;  
    public int weaponDamage = 10; 
    public float attackSpeed = 1f;

    public AudioSource audioSource;
    public AudioClip attackSound;

    protected bool canAttack = true;
    public bool found = false; 

    public virtual int GetDamage()
    {
        return found ? weaponDamage : baseDamage;
    }

    public virtual void Attack()
    {
        Debug.Log($"{weaponName} attacked with {GetDamage()} damage!");
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    protected void PerformAttack()
    {
        if (canAttack)
        {
            Debug.Log($"{weaponName} attacked with {weaponDamage} damage!");
            audioSource.PlayOneShot(attackSound);
            canAttack = false;
            Invoke("ResetAttack", attackSpeed); 
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}