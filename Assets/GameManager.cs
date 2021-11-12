using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ó���� ���ӿ���UI ������ �ʰ�
// �÷��̾� ������ ���ӿ���UI ���̰�
// ����� ��� 
// ���� ��� 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // ���ӿ��� UI
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ����� ���.
    public void OnClickRestart()
    {
        print("OnClickRestart");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // ���� ���
    public void OnClickQuit()
    {
        print("OnClickQuit");
        Application.Quit();
    }
}
