using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float attackDistance = 20f;
    [SerializeField] private float stopAttackDistance = 20f;
    [SerializeField] private bool isFleeing = false;
    [SerializeField] private float fleeDistance = 20f;
    private float CurrentDistance;
    private Vector3 distanceVector;
    private NavMeshAgent agent;
    private Animator animator;
    void Start()
    {
        target = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Distance();
        Attacking();
        Fleeing();
    }

    private void Distance()
    {
        Vector3 distanceVector = target.position - transform.position;
        float CurrentDistance = Vector3.Magnitude(distanceVector);
    }
    private void Attacking()
    {
        if (CurrentDistance < attackDistance && isAttacking)
        {
            agent.SetDestination(target.position);
            isAttacking = true;
        }
        else if (CurrentDistance > attackDistance)
        {
            isAttacking = false;
        }
        Debug.Log("distance is" + distanceVector);
    }

    private void Fleeing()
    {
        if (CurrentDistance < fleeDistance && isFleeing)
        {
            agent.SetDestination(target.position + Vector3.Normalize(distanceVector));
        }
    }
}