using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public GameObject bloodSlashFactory;

    public AudioSource audioSourceAxe;  //Axe gameObject �Ҵ��ϱ�


    public AudioClip axeImpactSound; //�ش� sfx �Ҵ�
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
        if (swingStrength >= 6) { //Axe �ֵη� �� �Ҹ�
            audioSourceAxe.Stop();
            audioSourceAxe.clip = axeWhooshSound;
            audioSourceAxe.Play();

            VibrationManager.instance.TriggerVibration(axeWhooshVibration, OVRInput.Controller.RTouch); //Axe �ֵη궧 ����

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.CompareTag("Enemy"))
        {
            swingStrength = SwingStrength(); //Axe �ֵη�� ����
            print(swingStrength);
   
            ContactPoint contact = other.contacts[0]; //Axe�� ���ʹ� �浹��
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;

            audioSourceAxe.Stop(); //���ʹ� �´� �Ҹ�
            audioSourceAxe.clip = axeImpactSound;
            audioSourceAxe.Play();

            VibrationManager.instance.TriggerVibration(axeImpactSound, OVRInput.Controller.RTouch); //��ʹ� ���� �� ����

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
        //��Ʈ�ѷ� ���ӵ� ����

        Vector3 accelerationRHand = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch); //ywp
        float accelerationMagnitude = accelerationRHand.magnitude;
        accelerationMagnitude = Mathf.Abs(accelerationMagnitude);


        return accelerationMagnitude;
    }
}
