using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
        {
            PlayerInfoXML.Instance();
            LoadXML_LevelXML();
        }
    }

    /// <summary>
    /// 최적화 - 프레임 제약(높은 프레임이 필요하지 않는 경우 프레임 수 낮추기)
    /// </summary>
    /// <param name="_isdown">프레임 수 낮추기</param>
    public void Setting_Frame(bool _isdown)
    {
        Application.targetFrameRate = 60;   //기본 fps
        int _interval = 1;  // 60 / 1 = 기본 fps
        if (_isdown)
            _interval = 3;  //낮출 fps
        OnDemandRendering.renderFrameInterval = _interval;  // Application.targetFrameRate / _interval fps
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
