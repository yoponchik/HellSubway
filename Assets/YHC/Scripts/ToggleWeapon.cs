using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWeapon : MonoBehaviour
{
    [SerializeField] private WeaponManager[] weapons;
    public OVRInput.Controller controller;
    private int curWeaponIndex;

    private void Start()
    {
        curWeaponIndex = 0;
        weapons[curWeaponIndex].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            int switchWeapon = curWeaponIndex;
            switchWeapon++;
            switchWeapon %= weapons.Length;
            TurnOnSelectedWeapon(switchWeapon);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        weapons[curWeaponIndex].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        curWeaponIndex = weaponIndex;
    }
}
