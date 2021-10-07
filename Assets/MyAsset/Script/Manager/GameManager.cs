using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPattern_IsA_Mono<GameManager>
{
    public string beforeScene = ""; //이전 씬 이름

    [ContextMenu("SaveXML_LevelXML")]
    void SaveXML_LevelXML()
    {
        PlayerInfoXML.WriteLevelInfo();
    }
    [ContextMenu("LoadXML_LevelXML")]
    void LoadXML_LevelXML()
    {
        PlayerInfoXML.ReadLevelInfo();
    }

    public void Awake()
    {
        if (DontDestroyInst(this))
            LoadXML_LevelXML();
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
