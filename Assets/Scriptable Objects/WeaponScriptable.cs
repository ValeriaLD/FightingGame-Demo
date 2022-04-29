using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public int damage;
    public string weaponName;
    public GameObject weaponPrefab;

    public Sprite weaponIcon;
}
