using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자신을 쥐고있는 VRGrabber를 알고싶다.
// VRGrabber가 자신을 쥐었을때 만약 이전에 쥐고있던 VRGrabber가 있었다면
// 그 녀석에게 나 딴데로간다 라고 알려주고싶다.
public class Grabbable : MonoBehaviour
{
    protected Grabber grabber;
    protected virtual void Start()
    {

    }
    // VRGrabber 가 나를 잡았을때 호출되어야함.
    public void SetGrabber(Grabber newGrabber)
    {
        // 1. 만약 grabber가 null이 아니라면 (이미 잡혀있는 경우)
        if (grabber != null)
        {
            // 2. grabber에게 ForgetMe 라고 요청하고싶다.
            grabber.ForgetMe();
        }
        grabber = newGrabber;
    }

    public virtual void Fire()
    {

    }
}
