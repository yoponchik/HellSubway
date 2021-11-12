using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody rb;
    public float force = 10;
    public GameObject expFactory;
    AudioSource aud;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //rb.velocity = transform.forward * force;
    }

    public void Start()
    {
        rb.velocity = transform.forward * force;
        aud = GetComponent<AudioSource>();
        //gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        int layer = 1 << LayerMask.NameToLayer("Enemy");
        aud.Play();

        Collider[] cols = Physics.OverlapSphere(other.contacts[0].point, 1.5f, layer);
        Collider[] cols2 = Physics.OverlapSphere(other.contacts[0].point, 10f, layer);
        if (cols.Length>0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].gameObject.GetComponent<Enemy>().OnDamaged(50);
                /*if (cols[i].gameObject.name.Contains("Enemy_3"))
                {
                    cols[i].gameObject.GetComponent<Enemy_Type3>().OnDamaged(50);
                }
                else
                {
                    cols[i].gameObject.GetComponent<Enemy>().OnDamaged(50);
                }*/


                /*if (cols[i].gameObject.name.Contains("Enemy_1")) {
                    cols[i].gameObject.GetComponent<Enemy_Type1>().OnDamaged(50);
                }
                if (cols[i].gameObject.name.Contains("Enemy_Wait"))
                {
                    cols[i].gameObject.GetComponent<Enemy_Type2>().OnDamaged(50);
                }

                if (cols[i].gameObject.name.Contains("Enemy_3"))
                {
                    cols[i].gameObject.GetComponent<Enemy_Type3>().OnDamaged(50);
                }

                if (cols[i].gameObject.name.Contains("Enemy_4"))
                {
                    cols[i].gameObject.GetComponent<Enemy_Type4>().OnDamaged(50);
                }*/
            }
        }

        if (cols2.Length>0)
        {
            for (int i = 0; i < cols2.Length; i++)
            {
                cols2[i].gameObject.GetComponent<Enemy>().SetMoveTarget(other.contacts[0].point);
                /*if (cols2[i].gameObject.name.Contains("Enemy_3"))
                {
                    cols2[i].gameObject.GetComponent<Enemy_Type3>().SetMoveTarget(other.contacts[0].point);
                }
                else
                {
                    cols2[i].gameObject.GetComponent<Enemy>().SetMoveTarget(other.contacts[0].point);
                }*/


               /* if (cols2[i].gameObject.name.Contains("Enemy_1"))
                {
                    cols2[i].gameObject.GetComponent<Enemy_Type1>().SetMoveTarget(other.contacts[0].point);
                }
                if (cols2[i].gameObject.name.Contains("Enemy_Wait"))
                {
                    cols2[i].gameObject.GetComponent<Enemy_Type2>().SetMoveTarget(other.contacts[0].point);
                }
                if (cols2[i].gameObject.name.Contains("Enemy_3"))
                {
                    cols2[i].gameObject.GetComponent<Enemy_Type3>().SetMoveTarget(other.contacts[0].point);
                }
                if (cols2[i].gameObject.name.Contains("Enemy_4"))
                {
                    cols2[i].gameObject.GetComponent<Enemy_Type4>().SetMoveTarget(other.contacts[0].point);
                }*/
            }
        }

        
        GameObject explosion = Instantiate(expFactory);
        explosion.transform.position = other.contacts[0].point;
        gameObject.SetActive(false);

        /*rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;*/
    }

    
}
