using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunSystem : MonoBehaviour
{
    public GameObject bloodImpactFactory; //ywp
    public Transform gun;
    public AudioSource aud;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    // Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetWeenShots;
    public int magazimeSize, bulletsPerTap; // 탄창
    public bool allowButtonHold;
    public int bulletsLeft, bulletShot;

    public int bulletsLeftAll;   // 총 탄알 

    // bools
    bool shooting, readyToShoot, reloading;

    //Reference
    //public Camera fpsCam;
    //public Transform attackPoint;
    public RaycastHit hitInfo;
    public LayerMask whatIsEnemy;
    public Text textBullet;


    // bulletImpact
    public GameObject bulletImpactFactory;
    ParticleSystem ps;

    // Graphics
    //public CamShake camShake;

    private void Awake()
    {
        bulletsLeft = magazimeSize;
        readyToShoot = true;
        
    }

    private void Start()
    {
        ps = bulletImpactFactory.GetComponent<ParticleSystem>();
        aud = aud.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gun.gameObject.activeSelf)
        {
            return;
        }

        MyInput();
        textBullet.text = bulletsLeft.ToString() + "/" + bulletsLeftAll.ToString();
        //print(bulletsLeft.ToString ()+ "/"+ bulletsLeftAll.ToString());
    }

    void MyInput()
    {   
        // 연발 모드를 허용할지
        if (allowButtonHold)
        {
            shooting = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }
        else
        {
            shooting = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }

        // 재장전
        //if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazimeSize && !reloading)
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch) && bulletsLeft < magazimeSize && !reloading)
        {
            Reload();
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletShot = bulletsPerTap;
            Shoot();
        }

    }
    void Shoot()
    {
        readyToShoot = false;
        aud.Stop();
        aud.clip = shootSound;
        aud.Play();
        // 움직일때 조준점이 흔들리게하기위해서

        VibrationManager.instance.TriggerVibration(shootSound, OVRInput.Controller.RTouch); //ywp 슈팅할때 컨트롤러 진동 

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate Direction with Spread
        //Vector3 direction = Camera.main.transform.forward + new Vector3(x, y, 0);

        //int layer = 1 << LayerMask.NameToLayer("Player");


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
            }


        }


        bulletsLeft--;
        bulletShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletShot > 0 && bulletsLeft > 0)      // 총쏘는간격
        {
            Invoke("Shoot", timeBetWeenShots);
        }
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    void Reload()
    {
        aud.Stop();
        aud.clip = reloadSound;
        aud.Play();

        VibrationManager.instance.TriggerVibration(reloadSound, OVRInput.Controller.RTouch); //ywp 재장전 할때 컨트롤러 진동

        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }

    void ReloadFinished()
    {
        if (bulletsLeftAll > magazimeSize)
        {
            bulletsLeftAll = bulletsLeftAll - magazimeSize + bulletsLeft;
            bulletsLeft = magazimeSize;
        }
        else
        {
            bulletsLeft = bulletsLeftAll;
            bulletsLeftAll = 0;
        }
        
        reloading = false;
    }

}
