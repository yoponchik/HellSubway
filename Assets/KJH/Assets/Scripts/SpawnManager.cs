using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyFactory;
    public List<Transform> path;

    public float createTime = 1;
    public float currentTime;
    int enemyCount;
    public int maxEnemyCount = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime && enemyCount < maxEnemyCount)
        {
            currentTime = 0;
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = GetRandSpawnPoint();
            enemyCount++;
        }
    }

    int preIndex = -1;

    private Vector3 GetRandSpawnPoint()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < path.Count; i++)
        {
            if (i == preIndex)
            {
                continue;
            }
            temp.Add(i);
        }

        int r = Random.Range(0, temp.Count);
        int index = temp[r];

        preIndex = index;
        // 2. 그 인덱스에 해당하는 Spawn의 위치를 반환하고싶다.
        return path[index].position;
    }
}
