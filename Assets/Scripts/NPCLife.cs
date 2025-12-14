using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCLife : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthBar;
    private float health;
    private Animator animator;
    private NavMeshAgent agent;
    public bool isAlive { get; private set; } = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Projectile")
        {
            GameObject collisionGameObject = other.collider.gameObject;
            Projectile projectile = collisionGameObject.GetComponent<Projectile>();
            float damage = projectile.GetDamage();
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isAlive = false;
            agent.isStopped = true;
            animator.SetTrigger("death");
            Invoke("Death", 5f);
        }
        healthBar.gameObject.SetActive(true);
        healthBar.value = health;

    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
