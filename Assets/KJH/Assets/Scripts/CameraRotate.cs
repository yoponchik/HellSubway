using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� ���콺�� ȸ���ϸ� ī�޶� ȸ���ϰ� �ʹ�.
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
        // ����ڰ� ���콺�� ȸ���ϸ�
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;

        //rx�� ȸ������ �����ϰ�ʹ�.
        rx = Mathf.Clamp(rx, -90, 90);  //���� ����

        //print(mx + "," + my);
        // ī�޶� ȸ���ϰ� �ʹ�.
        Vector3 angle = new Vector3(my, mx, 0);
        //transform.Rotate(angle);
        // transform.eulerAngles = angle; 0���� ���ư� �����ΰ��� �����ؼ� �־������
        transform.eulerAngles = new Vector3(-rx, ry, 0);

    }

    public static float Clamp(float value, float min, float max)
    {
        // ���� value�� min���� �۴ٸ� min�� ��ȯ�ϰ� �ʹ�.
        if (value < min)
        {
            return min;
        }
        // ���� value�� max���� ũ�ٸ� max�� ��ȯ�ϰ� �ʹ�.
        else if (value > max)
        {
            return max;
        }
        // value�� ��ȯ�ϰ� �ʹ�
         return value;
        
        
    }


}
