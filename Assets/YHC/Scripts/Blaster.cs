using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Blaster : MonoBehaviour
{
    /*[Header("Input")]
    public SteamVR_Action_Boolean m_FireAction = null;
    public SteamVR_Action_Boolean m_ReloadAction = null;

    [Header("Settings")]
    public int m_Force = 10;
    public int m_MaxProjectileCount = 6;     // how big the pool is going to be, how large the magazine is
    public float m_ReloadTime = 1.5f;    // how long the reload time to be  

    [Header("References")]
    public Transform m_Barrel = null;
    public GameObject m_ProjectilePrefab = null;
    public Text m_AmmoOutput = null;     // how much bullets left

    private bool m_IsReloading = false;
    private int m_FireCount = 0;

    public SteamVR_Behaviour_Pose m_Pose = null;
    private Animator m_Animator = null;
    private ProjectilePool m_ProjectilePool = null;

    private void Awake()
    {
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        m_Animator = GetComponent<Animator>();

        m_ProjectilePool = new ProjectilePool(m_ProjectilePrefab, m_MaxProjectileCount);
    }

    private void Start()
    {
        UpdateFiredCount(0);
    }

    private void Update()
    {
        if (m_IsReloading)
            return;

        if (m_FireAction.GetStateDown(m_Pose.inputSource))
        {
            m_Animator.SetBool("Fire", true);
            Fire();
        }

        if (m_FireAction.GetStateUp(m_Pose.inputSource))
        {
            m_Animator.SetBool("Fire", false);
        }

        if (m_ReloadAction.GetStateDown(m_Pose.inputSource))
            StartCoroutine(Reload());
    }

    private void Fire()
    {
        // check to see if we have ammo
        if (m_FireCount >= m_MaxProjectileCount)
            return;

        *//*Projectile targetProjectile = m_ProjectilePool.m_Projectiles[m_FireCount];
        targetProjectile.Launch(this);*//*

        UpdateFiredCount(m_FireCount + 1);
    }

    private IEnumerator Reload()
    {
        if (m_FireCount == 0)     // if we haven't fired any bullets yet => don't reload
            yield break;

        m_AmmoOutput.text = "-";
        m_IsReloading = true;

        m_ProjectilePool.SetAllProjectiles();     // disable all projectiles

        yield return new WaitForSeconds(m_ReloadTime);

        UpdateFiredCount(0);
        m_IsReloading = false;

    }

    private void UpdateFiredCount(int newValue)
    {
        m_FireCount = newValue;
        m_AmmoOutput.text = (m_MaxProjectileCount - m_FireCount).ToString();
    }*/
}
