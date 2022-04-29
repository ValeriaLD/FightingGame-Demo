using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponScriptable equippedWeapon;

    [SerializeField]
    private GameObject currentWeapon;

    [SerializeField]
    private Vector3 positionInHand;

    [SerializeField]
    private Vector3 rotationInHand;

    private GameObject hand;

    private void Start()
    {
        hand = GameObject.Find("RightHand");
    }

    public void EquipWeapon(WeaponScriptable weaponData)
    {
        if (currentWeapon == null) // in cazul in care nu avem nimic in mina
        {
            equippedWeapon = weaponData; // asignam datele care le primim de la obiectul cu care intram in triger la obiectul curent echipat  10 axe axeprefab
            currentWeapon = Instantiate(weaponData.weaponPrefab); // instantiem gameobjectul obiectului pe care il primim  axeprefab
            currentWeapon.transform.parent = hand.transform;
            currentWeapon.transform.localPosition = positionInHand;
            currentWeapon.transform.localEulerAngles = rotationInHand;
            Debug.Log("Axe added!");
        }
        else  //cind avem ceva in mina 
        {
            currentWeapon.SetActive(false);
            currentWeapon = Instantiate(weaponData.weaponPrefab);
            currentWeapon.transform.parent = hand.transform;
            currentWeapon.transform.localPosition = positionInHand;
            currentWeapon.transform.localEulerAngles = rotationInHand;
            Debug.Log("Mace added!");
        }

        InventoryManager.instance.weapons.Add(currentWeapon);
    }

    //public void Drop()
    //{
    //    Destroy(currentWeapon)
    //    currentWeapon.transform.parent = null;
    //    currentWeapon.transform.position = Vector3.zero;
    //    currentWeapon = null;
    //}
}
