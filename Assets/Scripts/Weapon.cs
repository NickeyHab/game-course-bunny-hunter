using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammo = 10;
    public int GetAmmo()
    {
        return ammo;
    }
    public void UpdateAmmoDisplay()
    {
        GameManager.Instance.AmmoEvent.Invoke(ammo);
    }
}
