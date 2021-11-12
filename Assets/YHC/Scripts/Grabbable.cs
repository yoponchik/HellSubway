using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڽ��� ����ִ� VRGrabber�� �˰�ʹ�.
// VRGrabber�� �ڽ��� ������� ���� ������ ����ִ� VRGrabber�� �־��ٸ�
// �� �༮���� �� �����ΰ��� ��� �˷��ְ�ʹ�.
public class Grabbable : MonoBehaviour
{
    protected Grabber grabber;
    protected virtual void Start()
    {

    }
    // VRGrabber �� ���� ������� ȣ��Ǿ����.
    public void SetGrabber(Grabber newGrabber)
    {
        // 1. ���� grabber�� null�� �ƴ϶�� (�̹� �����ִ� ���)
        if (grabber != null)
        {
            // 2. grabber���� ForgetMe ��� ��û�ϰ�ʹ�.
            grabber.ForgetMe();
        }
        grabber = newGrabber;
    }

    public virtual void Fire()
    {

    }
}
