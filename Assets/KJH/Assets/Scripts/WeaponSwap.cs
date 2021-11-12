using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public static WeaponSwap instance;

    

    private void Awake()
    {
        instance = this;
    }

    public GameObject gun;
    public GameObject axe;
    public GameObject bomb;

    public Transform rightHand;
    // Start is called before the first frame update
    void Start()
    {
        //OffGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            if (axe.activeSelf)
            {
                OffGameObject();
                gun.SetActive(true);
            }
            else if (gun.activeSelf)
            {
                OffGameObject();
                axe.SetActive(true);
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OffGameObject();
            gun.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OffGameObject();
            axe.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OffGameObject();
            Rigidbody rb =bomb.GetComponentInChildren<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            bomb.SetActive(true);
        }*/

    }

    void OffGameObject()
    {
        gun.SetActive(false);
        axe.SetActive(false);
        bomb.SetActive(false);
    }


    public void OnActiveGun()
    {
        OffGameObject();
        gun.SetActive(true);
    }

    public void OnActiveAxe()
    {
        OffGameObject();
        axe.SetActive(true);
    }

    public void OnActiveBomb()
    {
        OffGameObject();
        bomb.SetActive(true);
    }
}
