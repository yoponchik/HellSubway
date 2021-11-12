using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum ItemType
{
    KEY,
    AID_BOX,
    Gun,
    Axe,
    Bomb

}



public class Item : MonoBehaviour
{
    // ItemType itemType;
    public Image ImageIcon;
    ItemType itemType;

    int count;

    public void Init(ItemType type)
    {
        itemType = type;
        string id = "item_" + (int)type;
        ImageIcon.sprite = Resources.Load<Sprite>(id);

    }

    // item use
    public void OnClick()
    {
        GameObject clickItem = EventSystem.current.currentSelectedGameObject;

        if (clickItem.name.Contains("Gun"))
        {
            WeaponSwap.instance.OnActiveGun();

        }
        else if (clickItem.name.Contains("Axe"))
        {
            WeaponSwap.instance.OnActiveAxe();
        }
        else if (clickItem.name.Contains("Key"))
        {
            // Key�� ��ȯ 
            // or Ư���������� ���������� ȿ�� �ߵ�
        }
        else if (clickItem.name.Contains("Bomb"))
        {
            WeaponSwap.instance.OnActiveBomb();
        }
        else if(clickItem.name.Contains("Aid_Kit"))
        {
            if (Kit.instance.COUNT > 0)
            {
                PlayerHP.instance.CURRENTHP += 30;
                Kit.instance.COUNT--;
                print(Kit.instance.COUNT);

            }
            
        }
        
        // ���̶��
        
        // �������
        // ��ź�̶��
        // ȸ��Ŷ�̶��
        // ������

        /*if (bb[1].sprite.name.Contains("item_0"))
        {
            // Ű �����°�
        }
        if (bb[1].sprite.name.Contains("item_1"))
        {
            if (count>0)
            {
                // hp ȸ��
                PlayerItemInteraction.instance.HealItem(30);
                count--;
            }

        }
        if (bb[1].sprite.name.Contains("item_2"))
        {
            // �ӵ� ��
            PlayerItemInteraction.instance.SpeedUp();
            Destroy(gameObject);
        }*/

    }


}
