using UnityEditor;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float health = 100f;

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

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <=0)
        {
            health = 0;
            Death();
        }
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
