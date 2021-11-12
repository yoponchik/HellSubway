using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemManager : MonoBehaviour
{
    public Transform content;
    public GameObject ItemFactory;
    // Start is called before the first frame update
    void Start()
    {
        ItemFactory = Resources.Load<GameObject>("Item");
    }

    // Update is called once per frame
    void Update()
    {
        // 아이템을 먹으면으로 수정..
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // prefab = Resources.Load<GameObject>("Item");
            GameObject item = Instantiate(ItemFactory);
            item.transform.parent = content;
            item.GetComponent<Item>().Init((ItemType)Random.Range(0,4));
        }
    }
}
