using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    public static ClearManager instance;

    private void Awake()
    {
        instance = this;
    }
    public GameObject Spawn;
    public GameObject gameclearUI;

    public float clearTime= 30f;
    public float currentTime;

    public int enemyCount;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Spawn.activeSelf)
        {
            currentTime += Time.deltaTime;
            if (currentTime>clearTime && enemyCount <= 0)
            {
                // Scene을 만들어도되고  클리어 처리
                print("Clear");
                gameclearUI.SetActive(true);
            }
        }
    }

/*    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            enemyCount++;
            print("enemyCount:"+enemyCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //print(other.name);
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            enemyCount--;
            print("enemyCount:" + enemyCount);
        }
    }*/
}
