using NUnit.Framework;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isChasing = false;
    [SerializeField] private float chaseDistance = 20f;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float attackDistance = 2.1f;
    [SerializeField] private float stopAttackDistance = 20f;
    [SerializeField] private bool isFleeing = false;
    [SerializeField] private float fleeDistance = 20f;
    private float CurrentDistance;
    private Vector3 distanceVector;
    private NavMeshAgent agent;
    private Animator animator;
    private NPCLife npcLife;
    void Start()
    {
        target = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        npcLife = GetComponent<NPCLife>();
    }

    void Update()
    {
        if (!npcLife.isAlive) return;
        Distance();
        Attack();
        Flee();
    }

    private void Distance()
    {
        Vector3 distanceVector = target.position - transform.position;
        CurrentDistance = Vector3.Magnitude(distanceVector);
    }
    private void Attack()
    {
        if (!isAttacking && CurrentDistance < attackDistance && isChasing)
        {
            isAttacking = true;
            animator.SetBool("attack", isAttacking);
        }
        else if (CurrentDistance < chaseDistance && isChasing)
        {
            agent.SetDestination(target.position);
            agent.speed = 3.5f;
            animator.SetTrigger("walk");
            animator.ResetTrigger("idle");
            animator.ResetTrigger("run");

            if (CurrentDistance < chaseDistance / 2 && isChasing)
            {
                agent.SetDestination(target.position);
                agent.speed = 10f;
                animator.SetTrigger("run");
                animator.ResetTrigger("walk");
            }
        }
        else if (CurrentDistance > stopAttackDistance)
        {
            agent.ResetPath();
            animator.SetTrigger("idle");
            animator.ResetTrigger("walk");
        }
    }

    private void Flee()
    {
        if (CurrentDistance < fleeDistance && isFleeing)
        {
            agent.SetDestination(transform.position + (transform.position - target.position).normalized * fleeDistance);
            agent.speed = 3.5f;
            animator.SetTrigger("walk");

            if (CurrentDistance < fleeDistance / 2)
            {
                agent.speed = 10f;
                animator.SetTrigger("run");
                animator.ResetTrigger("walk");
            }

        }
        else if (CurrentDistance > fleeDistance)
        {
            agent.ResetPath();
            animator.SetTrigger("idle");
        }
    }
}