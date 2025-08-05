using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwapping : MonoBehaviour
{
    [SerializeField] Transform weaponsList;
    private int currentWeaponID = 0;
    private int numberOfWeapons;
    private InputSystem_Actions controls;
    private void Awake()
    {
        controls = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        controls.Player.ScrollWeapon.Enable();
        controls.Player.ScrollWeapon.performed += OnScroll;

        controls.Player.WeaponSlot.Enable();
        controls.Player.WeaponSlot.performed += OnWeaponButton;
    }
    private void OnDisable()
    {
        controls.Player.ScrollWeapon.Disable();
        controls.Player.ScrollWeapon.performed -= OnScroll;

        controls.Player.WeaponSlot.Disable();
        controls.Player.WeaponSlot.performed -= OnWeaponButton;
    }
    private void Start()
    {
        numberOfWeapons = weaponsList.childCount;
        SelectWeapon(0);
    }

    private void OnWeaponButton(InputAction.CallbackContext ctx)
    {
        string ButtonName = ctx.control.name; // "1", "2", ..., "0"
        int S = ButtonName == "0" ? 9 : int.Parse(ButtonName) - 1;

        if (S >= 0 && S < numberOfWeapons)
        {
            currentWeaponID = S;
            SelectWeapon(currentWeaponID);
        }
    }
    private void OnScroll(InputAction.CallbackContext ctx)
    {
        float scrollY = ctx.ReadValue<Vector2>().y;

        if (scrollY > 0.5f)
        {
            currentWeaponID = (currentWeaponID + 1) % numberOfWeapons;
            SelectWeapon(currentWeaponID);
        }
        if (scrollY < -0.5f)
        {
            currentWeaponID = (currentWeaponID - 1 + numberOfWeapons) % numberOfWeapons;
            SelectWeapon(currentWeaponID);
        }
    }

    private void SelectWeapon(int weaponID)
    {
        // int counter = 0;
        // foreach (Transform weaponTransform in weaponsList)
        // {
        //     if (counter == weaponID)
        //     {
        //         weaponTransform.gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         weaponTransform.gameObject.SetActive(false);
        //     }
        //     counter += 1;
        // }

        for (int i = 0; i < weaponsList.childCount; i++)
        {
            weaponsList.GetChild(i).gameObject.SetActive(i == weaponID);
        }
        Transform weaponTransform = weaponsList.GetChild(weaponID);
        Weapon currentWeapon = weaponTransform.GetComponent<Weapon>();
        currentWeapon.UpdateAmmoDisplay();
    }
}
