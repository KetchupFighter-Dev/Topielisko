using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float meleeSPD = 1f; // attack cooldown in seconds
    [SerializeField] private float meleeDMG = 1f;

    private bool canAttack = true;
    public Animator animator;

    void Update()
    {
        // Trigger attack only once when clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (canAttack)
            {
                animator.SetTrigger("Attack");
                canAttack = false;
                StartCoroutine(MeleeCooldown());
            }
        }
    }

    private IEnumerator MeleeCooldown()
    {
        // Wait for cooldown duration
        yield return new WaitForSeconds(meleeSPD);
        canAttack = true;
        Debug.Log("Attack ready again!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(meleeDMG);
            Debug.Log("Enemy Hit");
        }
    }
}