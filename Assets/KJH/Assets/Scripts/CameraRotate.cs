using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스를 회전하면 카메라를 회전하고 싶다.
public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rx = transform.rotation.x;
        ry = transform.rotation.y;
    }

    float rx, ry;
    public float rotSpeed = 200;

    // Update is called once per frame
    void Update()
    {
        // 사용자가 마우스를 회전하면
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;

        //rx의 회전값을 제한하고싶다.
        rx = Mathf.Clamp(rx, -90, 90);  //값을 제한

        //print(mx + "," + my);
        // 카메라를 회전하고 싶다.
        Vector3 angle = new Vector3(my, mx, 0);
        //transform.Rotate(angle);
        // transform.eulerAngles = angle; 0으로 돌아감 움직인값을 누적해서 넣어줘야함
        transform.eulerAngles = new Vector3(-rx, ry, 0);

    }

    public static float Clamp(float value, float min, float max)
    {
        // 만약 value가 min보다 작다면 min을 반환하고 싶다.
        if (value < min)
        {
            return min;
        }
        // 만약 value가 max보다 크다면 max를 반환하고 싶다.
        else if (value > max)
        {
            return max;
        }
        // value를 반환하고 싶다
         return value;
        
        
    }


}
