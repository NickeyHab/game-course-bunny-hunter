using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float runningDistance = 20f;
    [SerializeField] private float stopRunningDistance = 20f;
    [SerializeField] private bool flee = false;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isMoving = false;
    void Start()
    {
        target = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 distanceVector = target.position - transform.position;
        float distance = Vector3.Magnitude(distanceVector);
        // float distance = Vector3.Distance(target.position, transform.position);
        if (distance < runningDistance)
        {
            isMoving = true;
            animator.SetTrigger("walk");
        }
        else if (distance > stopRunningDistance)
        {
            isMoving = false;
            animator.SetTrigger("idle");
        }
        if (isMoving)
        {
            if (flee)
            {
                agent.SetDestination(transform.position - Vector3.Normalize(distanceVector));
                if (distance < runningDistance / 2)
                {
                    agent.speed = 10f;
                    animator.SetTrigger("run");
                }
                else
                {
                    agent.speed = 3.5f;
                    animator.SetTrigger("walk");
                }
            }
            else
            {
                agent.SetDestination(transform.position + Vector3.Normalize(distanceVector));
            }
            Debug.Log("distance is" + distance);
        }
    }
}
