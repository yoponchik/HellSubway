using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Active가 false되면(OnDiable가 호출되면) ObjectPool에게 "나 비활성화되었어"라고 통지하고싶다.
// 2. 일정시간 후에 보이지 않게 하고싶다.
public class ObjectPoolObject : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPool.instance.SetDeactiveInstancePlz(this);
    }
    System.Action callback;
    // time 초 후에 보이지 않게 하고싶다.
    public void SetReserveDisable(float time, System.Action callback)
    {
        this.callback = callback;

        StopCoroutine("IEReserveDisable");
        StartCoroutine("IEReserveDisable", time);
    }

    IEnumerator IEReserveDisable(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        if (callback != null)
        {
            callback();
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
