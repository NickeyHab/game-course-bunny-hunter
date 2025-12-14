using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    private TextMeshProUGUI ammoCount;
    void Start()
    {
        ammoCount = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.AmmoEvent.AddListener(AmmoHandler);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void AmmoHandler(int ammo)
    {
        if (ammoCount != null)
        {
            ammoCount.text = $"{ammo}";
        }
        Debug.Log("ammo update:" + ammo);
    }
}
