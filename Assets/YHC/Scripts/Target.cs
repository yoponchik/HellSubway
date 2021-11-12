using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Color m_FlashDamageColor = Color.white;
    private MeshRenderer m_MeshRenderer = null;
    private Color m_OriginalColor = Color.white;

    private int m_MaxHealth = 100;
    private int m_curHealth = 0;

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_MeshRenderer.material.color;
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
            Damage();
    }

    private void Damage()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());

        RemoveHealth();
    }

    private IEnumerator Flash()
    {
        // 타겟을 맞출 때마다 하얀색으로 flash. 0.1초 기다린 후 다시 원래 색으로  
        m_MeshRenderer.material.color = m_FlashDamageColor;

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;

        m_MeshRenderer.material.color = m_OriginalColor;
    }

    private void RemoveHealth()
    {
        m_curHealth--;     // hp 얼마씩 줄이는지..? 20?
        CheckForDeath();

    }

    private void ResetHealth()
    {
        m_curHealth = m_MaxHealth;
    }

    private void CheckForDeath()
    {
        if (m_curHealth <= 0)
            Kill();

    }

    private void Kill()
    {
        gameObject.SetActive(false);
    }
}
