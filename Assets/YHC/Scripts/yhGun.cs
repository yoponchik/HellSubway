using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yhGun : Grabbable
{
    public GameObject bulletImpactFactory;
    LineRenderer lr;
    public Transform firePosition;
    protected override void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (lr != null)
        {
            lr.enabled = grabber != null;
            if (grabber != null)
            {
                Ray ray = new Ray(firePosition.position, firePosition.forward);
                lr.SetPosition(0, ray.origin);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    lr.SetPosition(1, hitInfo.point);
                }
                else
                {
                    lr.SetPosition(1, ray.origin + ray.direction * 100);
                }
            }
        }
    }

    public override void Fire()
    {
        Ray ray = new Ray(firePosition.position, firePosition.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject bulletImpact = Instantiate(bulletImpactFactory);
            bulletImpact.transform.position = hitInfo.point;
            bulletImpact.transform.forward = hitInfo.normal;
            Destroy(bulletImpact, 3);
        }
    }
}
