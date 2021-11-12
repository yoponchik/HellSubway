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
            // Key를 소환 
            // or 특정지역에서 눌렀을때만 효과 발동
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
        
        // 총이라면
        
        // 도끼라면
        // 폭탄이라면
        // 회복킷이라면
        // 열쇠라면

        /*if (bb[1].sprite.name.Contains("item_0"))
        {
            // 키 나오는거
        }
        if (bb[1].sprite.name.Contains("item_1"))
        {
            if (count>0)
            {
                // hp 회복
                PlayerItemInteraction.instance.HealItem(30);
                count--;
            }

        }
        if (bb[1].sprite.name.Contains("item_2"))
        {
            // 속도 업
            PlayerItemInteraction.instance.SpeedUp();
            Destroy(gameObject);
        }*/

    }


}
