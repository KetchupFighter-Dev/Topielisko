using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float meleeSPD = 1f; 
    [SerializeField] private float meleeDMG = 1f;


    private bool canAttack = true;
    public Animator animator;

    void Start()
    {
        RendererOFF();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (canAttack)
            {
                RendererON();
                animator.SetTrigger("Attack");
                canAttack = false;
                StartCoroutine(MeleeCooldown());
            }
        }
    }

    public void RendererOFF()
    {
        GetComponent<BoxCollider>().enabled = false;
    }


    public void RendererON()
    {
        GetComponent<BoxCollider>().enabled = true;

    }

    private IEnumerator MeleeCooldown()
    {
        RendererOFF();
        yield return new WaitForSeconds(meleeSPD);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>()?.TakeDamage(meleeDMG);
            Debug.Log("Enemy Hit");
        }
    }
}
