using UnityEngine;

public class MeleAttack : MonoBehaviour
{

    [SerializeField] private Animator anim; 
    [SerializeField] private float meleeSPD = 1f;
    [SerializeField] private float meleeDMG = 1f;
    float timeUntilMelee;

    private void Update()
    {
        if (timeUntilMelee <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSPD;

            }
            else
            {
                timeUntilMelee -= Time.deltaTime;
            }
        }
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //other.GetComponent<Enemy>().TakeDamage(meleeDMG);
            Debug.Log("Enemy Hit");
        }
    }
}
