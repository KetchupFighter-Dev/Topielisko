using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Target & Stats")]
    public Transform player;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float sightRange = 5f;
    public float attackRange = 1f;
    public float attackDelay = 2f;
    public float attackWarningTime = 0.8f; // czas ostrzeżenia linii
    public float attackDuration = 0.2f;    // czas samego ataku
    public float Damage = 10f;
    public GameObject PlayerOBJ;


    private bool isChasing = false;
    private bool isAttacking = false;

    private Vector3 patrolPointA;
    private Vector3 patrolPointB;
    private float lastAttackTime = 0f;

    private LineRenderer attackLine;

    private void Start()
    {
        patrolPointA = transform.position;
        patrolPointB = transform.position + Vector3.right * 5f;

        attackLine = GetComponent<LineRenderer>();
        attackLine.enabled = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < sightRange && !isChasing)
            isChasing = true;

        if (distance < attackRange && Time.time - lastAttackTime > attackDelay && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
            return;
        }

        if (isAttacking)
            return;

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    private void Patrol()
    {
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, patrolPointA, step);

        if (Vector3.Distance(transform.position, patrolPointA) < 0.1f)
        {
            Vector3 temp = patrolPointA;
            patrolPointA = patrolPointB;
            patrolPointB = temp;
        }
    }

    private void ChasePlayer()
    {
        float step = chaseSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        Vector3 attackStart = transform.position;
        Vector3 attackEnd = player.position;

        attackLine.enabled = true;
        attackLine.SetPosition(0, attackStart);
        attackLine.SetPosition(1, attackEnd);

        yield return new WaitForSeconds(attackWarningTime);

        Vector3 center = (attackStart + attackEnd) / 2f; 
        Vector3 size = new Vector3(attackRange * 2f, 2f, Vector3.Distance(attackStart, attackEnd));
        Quaternion orientation = Quaternion.LookRotation(attackEnd - attackStart);

        Collider[] hits = Physics.OverlapBox(center, size / 2f, orientation);
        foreach (Collider hit in hits)
        {
            if (hit.transform == player)
            {
                Debug.Log("Gracz trafiony!");
                PlayerOBJ.gameObject.GetComponent<Health>()?.TakeDamage(Damage);

            }
        }

        yield return new WaitForSeconds(attackDuration);

        attackLine.enabled = false;
        isAttacking = false;
    }


}