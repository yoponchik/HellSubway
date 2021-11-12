using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    public OVRInput.Controller hand;     // 왼손인지 오른손인지 
    public LineRenderer lr;
    GameObject aidkit;     // 잡고 있는 물체
    bool isGrip;    // 뭔가를 잡고 있는가? 

    // Update is called once per frame
    void Update()
    {
        // 1. Hand의 앞방향으로 Ray를 쏘는데 
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        lr.SetPosition(0, ray.origin);
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            // 2. Pinch 버튼을 눌렀을 때 
            if (aidkit == null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                // ray에 닿은 물체가 AidKit라면 aidkit변수에 담고싶다. 
                if (hitInfo.transform.gameObject.CompareTag("AidKit"))
                {
                    isGrip = true;
                    aidkit = hitInfo.transform.gameObject;
                    aidkit.GetComponent<Rigidbody>().useGravity = false;
                    aidkit.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        else     // 허공 
        {
            lr.SetPosition(1, ray.origin + ray.direction * 20);
        }

        if (isGrip)
        {
            if(aidkit.transform.parent == null)
            {
                // 3. pinch 버튼을 누르고 있으면 AidKit를 내 손으로 가져오고 싶다.   >> 'AidKit를 내 인벤토리에 넣고 싶다.'로 바꾸고 싶음.
                aidkit.transform.position = Vector3.Lerp(aidkit.transform.position, transform.position, Time.deltaTime * 5);
                float dist = Vector3.Distance(aidkit.transform.position, transform.position);
                if(dist < 0.1f)
                {
                    aidkit.transform.position = transform.position;
                    aidkit.transform.parent = transform;
                }
            }
        }

        // 4. Pinch버튼을 떼면 AidKit를 손에서 놓고 싶다. 
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, hand))
        {
            if (isGrip)
            {
                isGrip = false;
                aidkit.GetComponent<Rigidbody>().useGravity = true;
                aidkit.GetComponent<Rigidbody>().isKinematic = false;
                aidkit = null;
            }
             

        }
    }
}
