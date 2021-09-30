using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPattern_IsA_Mono<GameManager>
{
    public string beforeScene = ""; //이전 씬 이름

    public void Awake()
    {
        DontDestroyInst(this);
    }

    public void LoadScene(string _scene)
    {
        GameManager.Instance.beforeScene = SceneManager.GetActiveScene().name;  //이전 씬 이름 저장
        SceneManager.LoadScene(_scene);
    }

    public void LoadBeforeScene()   //이전 씬 불러오기.
    {
        if (Instance.beforeScene == null || Instance.beforeScene == "")
        {
            Debug.LogWarning("beforeScene 값이 없습니다.");
            DebugManager.Instance.Log("beforeScene 값이 없습니다.", LogType.Warning);
            return;
        }
        SceneManager.LoadScene(Instance.beforeScene);
    }
}
