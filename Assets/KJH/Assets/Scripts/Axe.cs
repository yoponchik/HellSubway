using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public GameObject bloodSlashFactory;

    public AudioSource audioSourceAxe;  //Axe gameObject 할당하기


    public AudioClip axeImpactSound; //해당 sfx 할당
    public AudioClip axeWhooshSound;
    public AudioClip axeWhooshVibration;

    float swingStrength;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceAxe = audioSourceAxe.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swingStrength >= 6) { //Axe 휘두룰 때 소리
            audioSourceAxe.Stop();
            audioSourceAxe.clip = axeWhooshSound;
            audioSourceAxe.Play();

            VibrationManager.instance.TriggerVibration(axeWhooshVibration, OVRInput.Controller.RTouch); //Axe 휘두룰때 진동

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            swingStrength = SwingStrength(); //Axe 휘두루기 강도
            print(swingStrength);
   
            ContactPoint contact = other.contacts[0]; //Axe와 에너미 충돌점
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;

            audioSourceAxe.Stop(); //에너미 맞는 소리
            audioSourceAxe.clip = axeImpactSound;
            audioSourceAxe.Play();

            VibrationManager.instance.TriggerVibration(axeImpactSound, OVRInput.Controller.RTouch); //어너미 맞을 때 진동

            if (swingStrength <= 10) { //light attack
                Instantiate(bloodSlashFactory, position, rotation);
                other.transform.gameObject.GetComponent<Enemy>().OnDamaged((int)swingStrength);
            }

            else if (swingStrength <= 30) //medium attack
            { //light attack
                Instantiate(bloodSlashFactory, position, rotation);
                other.transform.gameObject.GetComponent<Enemy>().OnDamaged((int)swingStrength);
            }
            else if(swingStrength >= 60) { //heavy attack
                Instantiate(bloodSlashFactory, position, rotation);
                other.transform.gameObject.GetComponent<Enemy>().OnDamaged((int)swingStrength);
            }



        }
    }

    float SwingStrength() {
        //컨트롤러 가속도 측정

        Vector3 accelerationRHand = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch); //ywp
        float accelerationMagnitude = accelerationRHand.magnitude;
        accelerationMagnitude = Mathf.Abs(accelerationMagnitude);


        return accelerationMagnitude;
    }
}
