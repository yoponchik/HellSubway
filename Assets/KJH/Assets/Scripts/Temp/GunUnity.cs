using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라위치에서 카메라앞방향으로 시선을 만들고
// 바라본곳에 총알자국을 남기고싶다.
// 적이 있다면 파괴하고싶다.
public class GunUnity : MonoBehaviour
{
    // 카메라
    Camera cam;
    // 총알자국VFX
    public GameObject bulletImpactFactory;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = bulletImpactFactory.GetComponent<ParticleSystem>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 만약 마우스 왼쪽 버튼이 눌리면
        if (Input.GetButtonDown("Fire1"))
        {
            // 카메라위치에서 카메라앞방향으로 시선을 만들고
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            // 만약 바라봤는데 닿은곳이 있다면
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Player");
            int layer2 = 1 << LayerMask.NameToLayer("Enemy");
            
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layer))
            {
                print(hitInfo.transform.name);
                GameObject bulletImpact = Instantiate(bulletImpactFactory);
                // 그곳에 총알자국을 남기고싶다.
                bulletImpact.transform.position = hitInfo.point;
                // 총알자국의Forward방향을 부딪힌곳의 Normal방향으로 하고싶다.
                bulletImpact.transform.forward = hitInfo.normal;

                if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    //Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    //enemy.OnDamaged(50);


                    if (hitInfo.transform.gameObject.name.Contains("Enemy_3"))
                    {
                        Enemy_Type3 enemy3 = hitInfo.transform.GetComponent<Enemy_Type3>();
                        enemy3.SetChaseTarget(gameObject);
                        enemy3.OnDamaged(20);

                    }
                    else
                    {
                        Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                        enemy.SetChaseTarget(gameObject);
                        enemy.OnDamaged(20);

                    }

/*                    if (hitInfo.transform.gameObject.name.Contains("Enemy_1"))
                    {
                        Enemy_Type1 enemy1 = hitInfo.transform.GetComponent<Enemy_Type1>();
                        enemy1.SetChaseTarget(gameObject);
                        enemy1.OnDamaged(20);

                    }*/
/*
                    if (hitInfo.transform.gameObject.name.Contains("Enemy_Wait"))
                    {
                        Enemy_Type2 enemy2 = hitInfo.transform.GetComponent<Enemy_Type2>();
                        enemy2.SetChaseTarget(gameObject);
                        enemy2.OnDamaged(20);
                    }*/

                    /*if (hitInfo.transform.gameObject.name.Contains("Enemy_3"))
                    {
                        Enemy_Type3 enemy3 = hitInfo.transform.GetComponent<Enemy_Type3>();
                        enemy3.SetChaseTarget(gameObject);
                        enemy3.OnDamaged(20);

                    }*/
/*
                    if (hitInfo.transform.gameObject.name.Contains("Enemy_4"))
                    {
                        Enemy_Type4 enemy4 = hitInfo.transform.GetComponent<Enemy_Type4>();
                        enemy4.SetChaseTarget(gameObject);
                        enemy4.OnDamaged(20);

                    }*/

                    /*Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.OnDamaged(50);*/

                    /*Enemy_Type4 enemy2 = hitInfo.transform.GetComponent<Enemy_Type4>();
                    enemy2.OnDamaged(50);*/

                    /*else
                    {
                        Debug.LogError("Enemy게임오브젝트에 Enemy컴포넌트가 없음!!! : " + hitInfo.transform.gameObject.name);
                    }*/

                }
                ps.Stop();
                ps.Play();

            }
        }
    }
}
