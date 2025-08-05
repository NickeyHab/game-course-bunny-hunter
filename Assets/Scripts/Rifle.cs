using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rifle : Weapon
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    private InputSystem_Actions playerControls;
    private InputAction fire;
    private float fireCD = 1f;
    private float lastFireTime = 0f;
    Camera camera;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Awake()
    {
        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Attack;
        fire.Enable();
        fire.performed += Fire;
    }
    private void OnDisable()
    {
        fire.performed -= Fire;
        fire.Disable();
    }
    void Update()
    {

    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.time < lastFireTime + fireCD || ammo <= 0)
            return;

        lastFireTime = Time.time;

        RaycastHit hit;
        bool isHit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);
        if (!isHit) return;
        HandleDamage(hit);

        ammo--;
        UpdateAmmoDisplay();
    }

    private void HandleDamage(RaycastHit hit)
    {
        NPCLife npcLife = hit.transform.GetComponent<NPCLife>();
        if (!npcLife) return;
        npcLife.TakeDamage(damage);
    }
}
