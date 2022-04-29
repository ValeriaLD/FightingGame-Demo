using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject currentWeapon;

    [SerializeField]
    private int weaponIndex = 0;

    [SerializeField]
    public List<GameObject> weapons = new List<GameObject>();

   
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        currentWeapon = PlayerController.instance.weaponInHands;
        if (currentWeapon)
        {
            ChangeWeapon(PlayerController.instance.weaponInHands);
        }
    }

    void ChangeWeapon(GameObject weaponInHand)
    {
        currentWeapon = weaponInHand;

        if (Input.GetKeyDown(KeyCode.Alpha1)) weaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) weaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) weaponIndex = 2;

        weaponInHand.SetActive(false);

        if(weaponIndex < weapons.Count)
        {
            switch (weaponIndex)
            {
                case 0:
                    weapons[weaponIndex].gameObject.SetActive(true);
                    break;
                case 1:
                    weapons[weaponIndex].gameObject.SetActive(true);
                    break;
                case 2:
                    weapons[weaponIndex].gameObject.SetActive(true);
                    break;
            }

            PlayerController.instance.weaponInHands = weapons[weaponIndex];
        }
        else
        {
           // Debug.Log("No weapon on this slot!");
        }
    }
}
