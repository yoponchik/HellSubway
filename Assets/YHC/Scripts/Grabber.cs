using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Grip��ư�� �������� �ݰ� 10cm�ȿ� Grabbable������Ʈ�� ���� ��ü�� �ִٸ�
// �� ��ü�� �տ� ���ʹ�.
// Grabbable���� �ʸ� ���� VRGrabber�� ���� ��� �˷��ְ�ʹ�.
// Grip��ư�� ������ �տ� �� ��ü�� �ִٸ� 
// ��Ʈ�ѷ��� velocity�� �����ϸ鼭 ����ʹ�.
public class Grabber : MonoBehaviour
{
    public OVRInput.Controller controller;
    GameObject grabObject;
    public float grabDistance = 0.1f;
    void Update()
    {


        // 1. ���� Grip��ư�� ��������
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            print("down");

            //  1.1 �ݰ� 10cm�ȿ� �浹ü�� �߿� ���� ����� Grabbable������Ʈ�� ���� 
            float tempDistance = float.MaxValue;
            Collider[] cols = Physics.OverlapSphere(transform.position, grabDistance);
            for (int i = 0; i < cols.Length; i++)
            {
                Grabbable tempGrabbable = cols[i].transform.gameObject.GetComponent<Grabbable>();
                if (tempGrabbable != null)
                {
                    float dist = Vector3.Distance(transform.position, cols[i].transform.position);

                    if (tempDistance > dist)
                    {
                        tempDistance = dist;
                        grabObject = cols[i].gameObject;
                    }
                }
            }
            if (grabObject != null)
            {
                //  1.2 �� ��ü�� �տ� ���ʹ�. grabObject�� �θ� = ��
                grabObject.transform.parent = transform;
                //  1.4 ���������� �����ʰ�ʹ�.
                grabObject.GetComponent<Rigidbody>().useGravity = false;
                grabObject.GetComponent<Rigidbody>().isKinematic = true;
                //  1.3 Grabbable���� �ʸ� ���� VRGrabber�� ���� ��� �˷��ְ�ʹ�.
                grabObject.GetComponent<Grabbable>().SetGrabber(this);
            }
        }
        // 2. ���� Grip��ư�� ������
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            print("up");

            //  2.1 ���� �տ� �� ��ü�� �ִٸ�
            if (grabObject != null)
            {
                // �κ��丮�� ���������� üũ�ϰ�
                CheckPutInventory();

                if (grabObject != null)
                {
                    print("22222222222");
                    // �κ��丮�� �ȵ����ϱ� ������ʹ�.
                    ThrowGrabObject();
                }
            }
        }
    }

    private void ThrowGrabObject()
    {
        //  2.2 ��Ʈ�ѷ��� velocity�� �����ϸ鼭 ����ʹ�.
        grabObject.gameObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
        grabObject.gameObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
        //  2.3 grabObject�� �θ��ڽ� ���踦 ����ʹ�. 
        grabObject.transform.parent = null;

        //  1.4 ���������� �ϰ� �ϰ�ʹ�.
        Rigidbody grabRB = grabObject.GetComponent<Rigidbody>();
        if (grabRB != null)
        {
            grabRB.useGravity = true;
            grabRB.isKinematic = false;
        }

        //  2.5 �տ��� ��ü�� ���� �ϰ�ʹ�.
        grabObject.GetComponent<Grabbable>().SetGrabber(null);
        grabObject = null;
    }

    void CheckPutInventory()
    {
        if (grabObject == null)
        {
            print("333333333333333");
            return;
        }

        //  1.1 �ݰ� 10cm�ȿ� �浹ü�� �߿� �κ��丮�� �ִٸ�.. �� �� ���� ����� ���� ����
        float tempDistance = float.MaxValue;
        GameObject inventoryObject = null;
        Collider[] cols = Physics.OverlapSphere(transform.position, grabDistance);
        print("cols.len" + cols.Length);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.CompareTag("Inventory"))
            {
                float dist = Vector3.Distance(transform.position, cols[i].transform.position);

                if (tempDistance > dist)
                {
                    tempDistance = dist;
                    inventoryObject = cols[i].gameObject;
                }
            }
        }
        // �κ��丮�� �ִٸ�
        if (inventoryObject != null)
        {
            print("1111111111111111111" + grabObject.name);
            //  ����ִ� ��ü�� �κ��丮�� �ڽ����� ���̰�ʹ�.
            grabObject.transform.parent = inventoryObject.transform;
            grabObject.transform.position = inventoryObject.transform.position;
            grabObject.transform.rotation = inventoryObject.transform.rotation;
            //  1.3 Grabbable���� �ʸ� ���� VRGrabber�� ���ٰ� �˷��ְ�ʹ�.
            //  1.4 ���������� �����ʰ�ʹ�.
            print("grabObject" + grabObject);
            Rigidbody grabRB = grabObject.GetComponent<Rigidbody>();
            if (grabRB != null)
            {
                grabRB.useGravity = false;
                grabRB.isKinematic = true;
            }

            grabObject.GetComponent<Grabbable>().SetGrabber(null);
            grabObject = null;
        }
        else
        {
            print("inventoryObject is null");
        }
    }

    public void ForgetMe()
    {
        print("ForgetMe");
        // grabObject�� null�� �ϰ�ʹ�.
        grabObject = null;
    }
}
