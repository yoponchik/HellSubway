using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public GameObject light;
    public GameObject Spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name.Contains("Player"))
        {
            light.SetActive(true);
            Spawn.SetActive(true);
        }
    }
}
