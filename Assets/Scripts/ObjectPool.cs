using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate하는 모든 객체(구분자는 prefab 이름)를 ObjectPool로 관리하고싶다.
// 필요속성 
// - 생성된 객체를 담을 목록
// - 비활성화된 목록
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    Dictionary<string, List<GameObject>> dic;
    Dictionary<string, List<GameObject>> deActiveDic;

    // 매개변수로 넘어온 객체를 deActiveDic에 추가하고싶다.
    internal void SetDeactiveInstancePlz(ObjectPoolObject objectPoolObject)
    {
        List<GameObject> list = deActiveDic[objectPoolObject.gameObject.name];
        list.Add(objectPoolObject.gameObject);
    }

    private void Awake()
    {
        instance = this;
        dic = new Dictionary<string, List<GameObject>>();
        deActiveDic = new Dictionary<string, List<GameObject>>();
    }

    // ObjectPool로 사용할 객체를 초기 생성 하고싶다.
    /// <summary>
    /// key : prefab name, count : 미리 생성해 놓을 객체 수
    /// </summary>
    public void CreateInstance(string key, int count)
    {
        // 만약 dic에 있는 키를 생성해달라고 했다면
        if (true == dic.ContainsKey(key))
        {
            // 안넣는다
            return;
        }
        GameObject prefab = Resources.Load<GameObject>(key);
        List<GameObject> list = new List<GameObject>();
        
        dic.Add(key, list);
        deActiveDic.Add(key, list);
        
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.name = key;
            go.AddComponent<ObjectPoolObject>();
            go.SetActive(false);
            list.Add(go);
        }
    }

    public GameObject GetDeactiveInstance(string key)
    {
        // 만약 key가 deActiveDic에 없거나 deActiveDic[key].Count가 0이라면
        if (false == deActiveDic.ContainsKey(key) || deActiveDic[key].Count == 0)
        {
            // null을 반환하고싶다.
            return null;
        }
        // 그렇지 않다면
        // deActiveDic의 첫번째 값을 반환하고싶다.
        List<GameObject> list = deActiveDic[key];
        GameObject temp = list[0];
        // 반환하기전에 deActiveDic의 목록에서 제외하고싶다.
        list.Remove(temp);
        temp.SetActive(true);
        return temp;

    }

    public GameObject GetDeactiveInstanceOld(string key)
    {
        // 만약 dic에 없는 키를 조회했다면
        if (false == dic.ContainsKey(key))
        {
            // 없어!!
            return null;
        }

        List<GameObject> list = dic[key];
        for (int i = 0; i < list.Count; i++)
        {
            if (false == list[i].activeSelf)
            {
                return list[i];
            }
        }
        return null;
    }
}
