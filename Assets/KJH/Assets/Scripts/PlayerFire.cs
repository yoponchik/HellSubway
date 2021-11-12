using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public static PlayerFire instance;

    // VIVE
    /*public SteamVR_Input_Sources right = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources left = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Behaviour_Pose rightController;
    public SteamVR_Behaviour_Pose leftController;

    public SteamVR_Action_Boolean grip;*/

    private void Awake()
    {
        instance = this;
    }

    public GameObject bombFactory;
    //public Transform firePosition;
/*    public Transform HMD;*/
    //public Transform rightHand;
    public float force = 5;

    GameObject grabObject;
    GameObject bomb;

    public Transform bombPo;
    public Transform righthand;

    public GameObject bo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bo.activeSelf)
        {
            return;
        }

        //if (Input.GetKeyDown(KeyCode.G))
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {

            /*float distance = Vector3.Distance(transform.position, HMD.position);
            if (distance <= 1.5f)
            {*/
                bomb = Instantiate(bombFactory);
                bomb.transform.parent = righthand;
                bomb.transform.position = righthand.position;
                bomb.GetComponent<Rigidbody>().isKinematic = true;
                bomb.GetComponent<Rigidbody>().useGravity = false;
                grabObject = bomb;
                //bomb.SetActive(true);
            //}
        }

        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (gameObject != null)
            {
                grabObject.transform.parent = null;
                Rigidbody grabRB = grabObject.GetComponent<Rigidbody>();

                grabRB.velocity = transform.TransformDirection(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch)) * 10;   
                grabRB.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
                bomb.GetComponent<Rigidbody>().useGravity = true;
                bomb.GetComponent<Rigidbody>().isKinematic = false;
                grabObject = null;
            }
        }
    }


}
