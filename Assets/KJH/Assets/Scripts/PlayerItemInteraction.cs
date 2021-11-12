using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerItemInteraction : MonoBehaviour
{
    public static PlayerItemInteraction instance;

    public void Awake()
    {
        instance = this;
    }

    PlayerMove pm;
    PlayerHP hp;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMove>();
        hp = GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        int layer = 1 << LayerMask.NameToLayer("Item");

        // �������� �ٶ󺸰� �ݱ� ���
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
        {
            if (Input.GetMouseButtonDown(0))
            {

                // ������ ����
                print(hitInfo.transform.name);
                // PlayerInventory.instance.InsertItem();
                if (hitInfo.transform.name.Contains("aid_kit"))
                {
                    // aid_kit ���
                    // ŰƮ ������ �Ѱ� �����Ѵ�.
                    Kit.instance.COUNT++;
                }
                else if (hitInfo.transform.name.Contains("key"))
                {
                    // ������
                    // ���� ������ �Ѱ� �����Ѵ�. or SetActive On �̹����� �Ҵ�
                    PlayerInventory.instance.InsertItem(1);
                }
                else if (hitInfo.transform.name.Contains("Bomb"))
                {
                    // ��ź�̶�� 
                    // ��ź ������ �Ѱ� �����Ѵ�.
                    PlayerInventory.instance.InsertItem(2);
                }
                else if (hitInfo.transform.name.Contains("bullet"))
                {
                    // źâ�̶��
                    GetComponent<GunSystem>().bulletsLeftAll += 30;
                    GetComponent<GunSystem>().bulletsLeft += 1;
                    hitInfo.transform.gameObject.SetActive(false);
                }



            }
        }
    }

    // heal item
    public void HealItem(int heal)
    {
        hp.currentHP += heal;
    }

    public void SpeedUp()
    {
        StopCoroutine("SpeedItem");
        StartCoroutine("SpeedItem");
    }

    IEnumerator SpeedItem()
    {
        pm.SpeedChange(2);
        yield return new WaitForSeconds(3);
        pm.SpeedChange(0.5f);
    }

}
