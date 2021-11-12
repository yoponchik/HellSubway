using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    public OVRInput.Controller hand;     // �޼����� ���������� 
    public LineRenderer lr;
    GameObject aidkit;     // ��� �ִ� ��ü
    bool isGrip;    // ������ ��� �ִ°�? 

    // Update is called once per frame
    void Update()
    {
        // 1. Hand�� �չ������� Ray�� ��µ� 
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        lr.SetPosition(0, ray.origin);
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            // 2. Pinch ��ư�� ������ �� 
            if (aidkit == null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                // ray�� ���� ��ü�� AidKit��� aidkit������ ���ʹ�. 
                if (hitInfo.transform.gameObject.CompareTag("AidKit"))
                {
                    isGrip = true;
                    aidkit = hitInfo.transform.gameObject;
                    aidkit.GetComponent<Rigidbody>().useGravity = false;
                    aidkit.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        else     // ��� 
        {
            lr.SetPosition(1, ray.origin + ray.direction * 20);
        }

        if (isGrip)
        {
            if(aidkit.transform.parent == null)
            {
                // 3. pinch ��ư�� ������ ������ AidKit�� �� ������ �������� �ʹ�.   >> 'AidKit�� �� �κ��丮�� �ְ� �ʹ�.'�� �ٲٰ� ����.
                aidkit.transform.position = Vector3.Lerp(aidkit.transform.position, transform.position, Time.deltaTime * 5);
                float dist = Vector3.Distance(aidkit.transform.position, transform.position);
                if(dist < 0.1f)
                {
                    aidkit.transform.position = transform.position;
                    aidkit.transform.parent = transform;
                }
            }
        }

        // 4. Pinch��ư�� ���� AidKit�� �տ��� ���� �ʹ�. 
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
