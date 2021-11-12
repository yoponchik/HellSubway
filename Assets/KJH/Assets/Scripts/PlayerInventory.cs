using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject inventory;
    public bool isInventoryOpen;

    public GameObject ItemFactory;
    GameObject item;
    public Transform content;

    // Start is called before the first frame update
    void Start()
    {
        ItemFactory = Resources.Load<GameObject>("Item");
        inventory.SetActive(false);
        isInventoryOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) && !inventory.activeSelf)
        {
            inventory.SetActive(true);
            isInventoryOpen = true;
        }

        else if (Input.GetKeyDown(KeyCode.B) && inventory.activeSelf)
        {
            inventory.SetActive(false);
            isInventoryOpen = false;
        }

    }

    public void InsertItem(int a)
    {
        if (a>3 || a<0)
        {
            return;
        }
        GameObject item = Instantiate(ItemFactory);
        //item.transform.parent = content;
        item.transform.SetParent(content);
        item.GetComponent<Item>().Init((ItemType)a);

        //item.GetComponent<Item>().Init((ItemType)Random.Range(0, 4));
    }

    public void VRInsertItem(int a)
    {
        

        
    }
}
