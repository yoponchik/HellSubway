using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public static HitManager instance;
    bool isHit = false;

    void Awake() {
        instance = this;
    }


    public GameObject hitUi;


    // Start is called before the first frame update
    void Start()
    {
        hitUi.SetActive(false);    
    }

    IEnumerator IEHit()
    {
        hitUi.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitUi.SetActive(false);
        isHit = false;
    }

    public void Hit()
    {
        if (isHit==false)
        {
            isHit = true;
            StopCoroutine("IEHit");
            StartCoroutine("IEHit");

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
