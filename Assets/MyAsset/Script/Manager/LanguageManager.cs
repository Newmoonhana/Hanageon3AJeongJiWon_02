using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LANGUAGE
{
    KOREAN,
    ENGLISH,

    _MAX
}

[System.Serializable]
public class XMLLanguageData
{
    public LANGUAGE language = LANGUAGE.KOREAN;
    public string tmp = "기타 어쩌구 저쩌구";
}

public class LanguageManager : SingletonPattern_IsA_Mono<LanguageManager>
{
    public XMLLanguageData xml = null;
    [ContextMenu("SaveXML")]
    public void SaveXML()
    {
        PlayerInfoXML.WriteLanguageInfo(xml);
    }
    [ContextMenu("LoadXML")]
    void LoadXML()
    {
        xml = PlayerInfoXML.ReadLanguageInfo();
    }

    public void Awake()
    {
        if (DontDestroyInst(this))
        {
            //LoadXML();
        }
    }
}
