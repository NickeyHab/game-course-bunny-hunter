using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : Weapon
{
    public GameObject arrowPrefab;
    public Transform arrowLocation;
    public float arrowSpeed = 100f;
    private InputSystem_Actions playerControls;
    private InputAction fire;
    private float fireCD = 1f;
    private float lastFireTime = 0f;
    private ArrowPool arrowPool;
    private AudioSource audioSource;
    [SerializeField] private AudioClip bowShotSFX;

    private void Start()
    {
        arrowPool = FindAnyObjectByType<ArrowPool>();
        audioSource = GetComponent<AudioSource>();
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


    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.time < lastFireTime + fireCD || ammo <= 0)
            return;

        lastFireTime = Time.time;

        GameObject arrow = arrowPool.GetArrow();
        arrow.transform.SetPositionAndRotation(arrowLocation.position, arrowLocation.rotation);
        arrow.GetComponent<Rigidbody>().linearVelocity = Camera.main.transform.forward * arrowSpeed;

        audioSource.PlayOneShot(bowShotSFX);
        ammo--;
        UpdateAmmoDisplay();
    }
}

