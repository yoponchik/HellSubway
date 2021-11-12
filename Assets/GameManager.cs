using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 처음에 게임오버UI 보이지 않게
// 플레이어 죽으면 게임오버UI 보이게
// 재시작 기능 
// 종료 기능 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // 게임오버 UI
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

    // 재시작 기능.
    public void OnClickRestart()
    {
        print("OnClickRestart");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // 종료 기능
    public void OnClickQuit()
    {
        print("OnClickQuit");
        Application.Quit();
    }
}
