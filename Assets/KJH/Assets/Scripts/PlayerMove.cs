using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
// 사용자가 점프키를 누르면 점프를 뛰고싶다.
// LShift키를 누르면 빨리 달리고 싶다.
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


    // 2단 점프를 뛰고 싶다.
    // 점프칸운트
    int jumpCount= 0;
    // 최대점프 카운트
    public int maxJumpCount = 2;

    // Fixed 1s - 40번 호출 update 1s-60번 호출
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // VIVE
        //Vector2 dir = playerMove.GetAxis(left);

        // 왼쪽의 이동 값을 받으면 ..
        Vector2 dir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        float h = dir.x;
        float v = dir.y;
        //print(dir);

        // 앞뒤좌우 방향을 만들고
        Vector3 direction = new Vector3(h, 0, v);
        
        // 내가 바라보고 있는 앞이 진짜 앞방향이 되게 하고 싶다.
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0;
        // direction의 크기를 1로 만들고 싶다. (Normalize)
        direction.Normalize();

        //  float finalSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;

        //Vector3 velocity = direction * (Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;);
        Vector3 velocity = direction * speed;
        // 4. yVelocity를 실제 방향에 대입하고 싶다.
        // 그 방향으로 이동하고 싶다.
        transform.position += direction * speed * Time.deltaTime;       // 공평해지기 위해서
      
        
    }


    public void SpeedChange(float speed)
    {
        speed *= speed;
        fastSpeed *= speed;
    }
}
