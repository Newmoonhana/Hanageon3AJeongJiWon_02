using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LANGUAGE
{
    KOREAN,
    ENGLISH,

    _MAX
}

public class LanguageManager : SingletonPattern_IsA_Mono<LanguageManager>
{
    [SerializeField]
    public class Language
    {
        public LANGUAGE language = LANGUAGE.KOREAN;
        public string tmp = "기타 어쩌구 저쩌구";
    }

    [ContextMenu("SaveXML")]
    public void SaveXML()
    {
        PlayerInfoXML.WriteLanguageInfo();
    }
    [ContextMenu("LoadXML")]
    void LoadXML()
    {
        PlayerInfoXML.ReadLanguageInfo();
    }

    public void Awake()
    {
        if (DontDestroyInst(this))
        {
            //LoadXML();
        }
    }
}
