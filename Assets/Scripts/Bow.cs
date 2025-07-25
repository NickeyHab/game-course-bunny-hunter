using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowLocation;
    public float arrowSpeed = 100f;
    private InputSystem_Actions playerControls;
    private InputAction fire;
    public float fireCD = 1f;
    private float lastFireTime = 0f;
    private ArrowPool arrowPool;
    private AudioSource audioSource;
    [SerializeField] private AudioClip bowShotSFX;

    private void Start()
    {
        arrowPool = FindAnyObjectByType<ArrowPool>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
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
        if (Time.time < lastFireTime + fireCD)
            return;

        lastFireTime = Time.time;

        GameObject arrow = arrowPool.GetArrow();
        arrow.transform.SetPositionAndRotation(arrowLocation.position, arrowLocation.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = Camera.main.transform.forward * arrowSpeed;

        audioSource.PlayOneShot(bowShotSFX);
    }
}

