using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LogType
{
    None,
    Log,
    Warning,
    Error
}

public class DebugManager : SingletonPattern_IsA_Mono<DebugManager>
{
    GameObject debugWindow_small;
    ScrollRect scrollview_sr;
    Scrollbar scrollY_sb;
    Text logtext_txt;

    private void Awake()
    {
        debugWindow_small = transform.GetChild(0).GetChild(0).gameObject;
        scrollview_sr = debugWindow_small.GetComponent<ScrollRect>();
        scrollY_sb = debugWindow_small.transform.GetChild(1).GetComponent<Scrollbar>();
        logtext_txt = debugWindow_small.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        DontDestroyInst(this);
    }

    private void Start()
    {
        Log("DebugManager is enable", LogType.None);
        //for (int i = 0; i < 20; i++)
        //    Log(i.ToString(), LogType.Log);
    }

    public void Log(String _log, LogType _type)
    {
        switch (_type)
        {
            case LogType.None:    logtext_txt.text += "<color=#ffffff>>>";  break;
            case LogType.Log:     logtext_txt.text += "<color=#cccccc>>>";  break;
            case LogType.Warning: logtext_txt.text += "<color=#ffff00>>>Warning:";  break;
            case LogType.Error:   logtext_txt.text += "<color=#ff0000>>>Error:";    break;
        }
        logtext_txt.text += "\t" + _log + " </color>\n";
        //scrollY_sb.value = 0;
        //scrollview_sr.verticalNormalizedPosition = 0 / (scrollview_sr.content.rect.height - debugWindow_small.transform.GetChild(0).GetComponent<RectTransform>().rect.height);
        Vector2 tmp = scrollview_sr.normalizedPosition;
        tmp.y = 0.5f;
        scrollview_sr.normalizedPosition = tmp;
    }
}
