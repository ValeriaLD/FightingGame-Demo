using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public WeaponScriptable equippedWeapon;

    [SerializeField]
    private GameObject currentWeapon, player = null;

    [SerializeField]
    private Vector3 weaponPosition;

    [SerializeField]
    private Vector3 weaponRotation;

    [SerializeField]
    private Vector3 weaponDropPosition;

    [SerializeField]
    private Vector3 weaponDropRotation;

    private bool pick = false;
    private GameObject hand;

    private void Start()
    {
        hand = GameObject.Find("RightHand");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(PlayerController.instance.weaponInHands != null)
            {
                StartCoroutine(DropItem());
                pick = false;
            }
        }

        if (pick)
        {
            if (Input.GetKeyDown(KeyCode.E) && pick)
            {
                StartCoroutine(PickItem());
                pick = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            currentWeapon = gameObject;
            player = other.gameObject;
            pick = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        pick = false;
    }

    private IEnumerator PickItem() 
    {
        player.GetComponentInChildren<Animator>().SetTrigger("pick");

        yield return new WaitForSeconds(1.2f);

        if(PlayerController.instance.weaponInHands == null)
        {
            PlayerController.instance.weaponInHands = currentWeapon;
            PlayerController.instance.weaponInHands.transform.parent = hand.transform;
            PlayerController.instance.weaponInHands.transform.localPosition = weaponPosition;
            PlayerController.instance.weaponInHands.transform.localEulerAngles = weaponRotation;
        }
        else
        {
            PlayerController.instance.weaponInHands.SetActive(false);
            PlayerController.instance.weaponInHands = currentWeapon;
            PlayerController.instance.weaponInHands.transform.parent = hand.transform;
            PlayerController.instance.weaponInHands.transform.localPosition = weaponPosition;
            PlayerController.instance.weaponInHands.transform.localEulerAngles = weaponRotation;
        }
        InventoryManager.instance.weapons.Add(currentWeapon);
    }

    private IEnumerator DropItem()
    {
        var item = PlayerController.instance.weaponInHands;
        if(item != null)
        {
            InventoryManager.instance.weapons.Remove(item);
            //item.transform.localPosition = weaponDropPosition;
            //item.transform.localEulerAngles = weaponDropRotation;
            item.transform.position = player.transform.position + player.transform.right;
            item.transform.parent = null;
            PlayerController.instance.weaponInHands = null;
        }

        yield return new WaitForSeconds(1.2f);
    }
}
