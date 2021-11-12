using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{

    public static VibrationManager instance;


    // Start is called before the first frame update
    void Start()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller) {

        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

        if (controller == OVRInput.Controller.LTouch) { //trigger on left controller
            OVRHaptics.LeftChannel.Preempt(clip);
        }

        else if (controller == OVRInput.Controller.RTouch) //trigger on right controller
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }

    }
}
