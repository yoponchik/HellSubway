using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라위치에서 카메라앞방향으로 시선을 만들고
// 바라본곳에 총알자국을 남기고싶다.
// 적이 있다면 파괴하고싶다.
public class Gun : MonoBehaviour
{

    public GameObject bloodImpactFactory; //ywp
    // 카메라
    Camera cam;
    // 총알자국VFX
    public GameObject bulletImpactFactory;
    ParticleSystem ps;
    public GameObject targetPlayer;
    public Transform gun;
    int damage = 50;

    // VIVE
    // 오른 손으로 발사 하면  발사!!
    /*SteamVR_Input_Sources right = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean pinch;*/

    //public Transform rightHand;

    // Start is called before the first frame update
    void Start()
    {
        ps = bulletImpactFactory.GetComponent<ParticleSystem>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        /*if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            print("오른손 pinch");
        }*/
        // 만약 마우스 왼쪽 버튼이 눌리면
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            // 카메라위치에서 카메라앞방향으로 시선을 만들고
            //Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Ray ray = new Ray(gun.position, gun.forward);

            // 만약 바라봤는데 닿은곳이 있다면
            RaycastHit hitInfo;
            int layer = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Player"));
            //  ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("GUN")))
            int layer2 = 1 << LayerMask.NameToLayer("Enemy");

            //if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layer))
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layer))
            {

                if (hitInfo.transform.gameObject.CompareTag("Enemy") == false)
                {
                    print(hitInfo.transform.name);
                    GameObject bulletImpact = Instantiate(bulletImpactFactory);
                    // 그곳에 총알자국을 남기고싶다.
                    bulletImpact.transform.position = hitInfo.point;
                    // 총알자국의Forward방향을 부딪힌곳의 Normal방향으로 하고싶다.
                    bulletImpact.transform.forward = hitInfo.normal;
                    ps.Stop();
                    ps.Play();
                }

                else if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    GameObject bloodImpact = Instantiate(bloodImpactFactory); //ywp
                    bloodImpact.transform.position = hitInfo.point; //ywp
                    bloodImpact.transform.forward = hitInfo.normal;  //ywp
                    print("hit enemy");

                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    //enemy1.SetChaseTarget(targetPlayer);
                    enemy.SetChaseTarget(gameObject);
                    enemy.OnDamaged(damage);


                    /*if (hitInfo.transform.gameObject.name.Contains("Enemy_3"))
                    {
                        Enemy_Type3 enemy3 = hitInfo.transform.GetComponent<Enemy_Type3>();
                        //enemy3.SetChaseTarget(targetPlayer);
                        enemy3.SetChaseTarget(gameObject);
                        enemy3.OnDamaged(damage);

                    }
                    else
                    {
                        Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                        //enemy1.SetChaseTarget(targetPlayer);
                        enemy.SetChaseTarget(gameObject);
                        enemy.OnDamaged(damage);
                    }*/


                    //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    //enemy.OnDamaged(50);
                    /*if (hitInfo.transform.gameObject.name.Contains("Enemy_1"))
                    {
                        Enemy_Type1 enemy1 = hitInfo.transform.GetComponent<Enemy_Type1>();
                        //enemy1.SetChaseTarget(targetPlayer);
                        enemy1.SetChaseTarget(gameObject);
                        enemy1.OnDamaged(damage);

                    }

                    if (hitInfo.transform.gameObject.name.Contains("Enemy_Wait"))
                    {
                        Enemy_Type2 enemy2 = hitInfo.transform.GetComponent<Enemy_Type2>();
                        //enemy2.SetChaseTarget(targetPlayer);
                        enemy2.SetChaseTarget(gameObject);
                        enemy2.OnDamaged(damage);
                    }

                    if (hitInfo.transform.gameObject.name.Contains("Enemy_3"))
                    {
                        Enemy_Type3 enemy3 = hitInfo.transform.GetComponent<Enemy_Type3>();
                        //enemy3.SetChaseTarget(targetPlayer);
                        enemy3.SetChaseTarget(gameObject);
                        enemy3.OnDamaged(damage);

                    }

                    if (hitInfo.transform.gameObject.name.Contains("Enemy_4"))
                    {
                        Enemy_Type4 enemy4 = hitInfo.transform.GetComponent<Enemy_Type4>();
                        //enemy3.SetChaseTarget(targetPlayer);
                        enemy4.SetChaseTarget(gameObject);
                        enemy4.OnDamaged(damage);
                    }*/

                }


            }
        }
    }
}
