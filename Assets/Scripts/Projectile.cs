using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float projectileDespawnTime = 10f;

    public float GetDamage()
    {
        return damage;
    }

    void OnEnable()
    {
        Invoke("DisableProjectile", projectileDespawnTime);
    }

    void DisableProjectile()
    {
        gameObject.SetActive(false);
    }
}
