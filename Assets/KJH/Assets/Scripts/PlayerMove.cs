using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿� ���� �յ��¿�� �̵��ϰ� �ʹ�.
// ����ڰ� ����Ű�� ������ ������ �ٰ�ʹ�.
// LShiftŰ�� ������ ���� �޸��� �ʹ�.
public class PlayerMove : MonoBehaviour
{

    // VIVE
    /*SteamVR_Input_Sources left = SteamVR_Input_Sources.LeftHand;
    SteamVR_Input_Sources right = SteamVR_Input_Sources.RightHand;*/


    /*SteamVR_Input_Sources left = (SteamVR_Input_Sources)OVRInput.Controller.LTouch;
    SteamVR_Input_Sources right = (SteamVR_Input_Sources)OVRInput.Controller.RTouch;*/

    /*public SteamVR_Action_Boolean pinch;
    public SteamVR_Action_Boolean grip;*/

    // public SteamVR_Action_Vector2 playerMove;

    public float speed = 1;
    float fastSpeed;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // 2�� ������ �ٰ� �ʹ�.
    // ����ĭ��Ʈ
    int jumpCount= 0;
    // �ִ����� ī��Ʈ
    public int maxJumpCount = 2;

    // Fixed 1s - 40�� ȣ�� update 1s-60�� ȣ��
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // VIVE
        //Vector2 dir = playerMove.GetAxis(left);

        // ������ �̵� ���� ������ ..
        Vector2 dir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        float h = dir.x;
        float v = dir.y;
        //print(dir);

        // �յ��¿� ������ �����
        Vector3 direction = new Vector3(h, 0, v);
        
        // ���� �ٶ󺸰� �ִ� ���� ��¥ �չ����� �ǰ� �ϰ� �ʹ�.
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0;
        // direction�� ũ�⸦ 1�� ����� �ʹ�. (Normalize)
        direction.Normalize();

        //  float finalSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;

        //Vector3 velocity = direction * (Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;);
        Vector3 velocity = direction * speed;
        // 4. yVelocity�� ���� ���⿡ �����ϰ� �ʹ�.
        // �� �������� �̵��ϰ� �ʹ�.
        transform.position += direction * speed * Time.deltaTime;       // ���������� ���ؼ�
      
        
    }


    public void SpeedChange(float speed)
    {
        speed *= speed;
        fastSpeed *= speed;
    }
}
