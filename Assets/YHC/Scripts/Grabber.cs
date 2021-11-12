using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Grip버튼을 눌렀을때 반경 10cm안에 Grabbable컴포넌트를 가진 물체가 있다면
// 그 물체를 손에 쥐고싶다.
// Grabbable에게 너를 잡은 VRGrabber가 나야 라고 알려주고싶다.
// Grip버튼을 뗏을때 손에 쥔 물체가 있다면 
// 컨트롤러의 velocity를 적용하면서 놓고싶다.
public class Grabber : MonoBehaviour
{
    public OVRInput.Controller controller;
    GameObject grabObject;
    public float grabDistance = 0.1f;
    void Update()
    {


        // 1. 만약 Grip버튼을 눌렀을때
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            print("down");

            //  1.1 반경 10cm안에 충돌체들 중에 가장 가까운 Grabbable컴포넌트를 가진 
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
                //  1.2 그 물체를 손에 쥐고싶다. grabObject의 부모 = 나
                grabObject.transform.parent = transform;
                //  1.4 물리행위를 하지않고싶다.
                grabObject.GetComponent<Rigidbody>().useGravity = false;
                grabObject.GetComponent<Rigidbody>().isKinematic = true;
                //  1.3 Grabbable에게 너를 잡은 VRGrabber가 나야 라고 알려주고싶다.
                grabObject.GetComponent<Grabbable>().SetGrabber(this);
            }
        }
        // 2. 만약 Grip버튼을 뗏을때
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            print("up");

            //  2.1 만약 손에 쥔 물체가 있다면
            if (grabObject != null)
            {
                // 인벤토리에 넣을것인지 체크하고
                CheckPutInventory();

                if (grabObject != null)
                {
                    print("22222222222");
                    // 인벤토리에 안들어갔으니까 던지고싶다.
                    ThrowGrabObject();
                }
            }
        }
    }

    private void ThrowGrabObject()
    {
        //  2.2 컨트롤러의 velocity를 적용하면서 놓고싶다.
        grabObject.gameObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
        grabObject.gameObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
        //  2.3 grabObject의 부모자식 관계를 끊고싶다. 
        grabObject.transform.parent = null;

        //  1.4 물리행위를 하게 하고싶다.
        Rigidbody grabRB = grabObject.GetComponent<Rigidbody>();
        if (grabRB != null)
        {
            grabRB.useGravity = true;
            grabRB.isKinematic = false;
        }

        //  2.5 손에쥔 물체가 없게 하고싶다.
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

        //  1.1 반경 10cm안에 충돌체들 중에 인벤토리가 있다면.. 그 중 가장 가까운 것을 선택
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
        // 인벤토리가 있다면
        if (inventoryObject != null)
        {
            print("1111111111111111111" + grabObject.name);
            //  잡고있는 물체를 인벤토리의 자식으로 붙이고싶다.
            grabObject.transform.parent = inventoryObject.transform;
            grabObject.transform.position = inventoryObject.transform.position;
            grabObject.transform.rotation = inventoryObject.transform.rotation;
            //  1.3 Grabbable에게 너를 잡은 VRGrabber이 없다고 알려주고싶다.
            //  1.4 물리행위를 하지않고싶다.
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
        // grabObject를 null로 하고싶다.
        grabObject = null;
    }
}
