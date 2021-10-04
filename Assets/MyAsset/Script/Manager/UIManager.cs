using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonPattern_IsA_Mono<UIManager>
{
    //확인 버튼.
    GameObject defalutUICanvas_obj;
    Canvas defalutUICanvas_cv;
    GameObject OKButton_obj;
    Button OKButton_bt;
    GameObject backButton_obj;
    Button backButton_bt;

    public void Awake()
    {
        defalutUICanvas_obj = transform.GetChild(0).gameObject;
        defalutUICanvas_cv = defalutUICanvas_obj.GetComponent<Canvas>();
        defalutUICanvas_cv.worldCamera = Camera.main.GetComponent<Camera>();
        OKButton_obj = defalutUICanvas_obj.transform.GetChild(0).gameObject;
        OKButton_bt = OKButton_obj.GetComponent<Button>();
        backButton_obj = defalutUICanvas_obj.transform.GetChild(1).gameObject;
        backButton_bt = backButton_obj.GetComponent<Button>();
        DontDestroyInst(this);
    }

    public delegate void Listener<in T>(params T[] _val);
    public void SetActiveOKbutton(bool _enable)
    {
        OKButton_obj.SetActive(_enable);
    }
    public void SetActiveOKbutton<T>(bool _enable, Listener<T> _l, params T []_val)
    {
        if (_l != null)
            SetAddListenerOKbutton_SceneLoad(_l, _val);
        OKButton_obj.SetActive(_enable);
    }
    private void SetAddListenerOKbutton_SceneLoad<T>(Listener<T> _l, params T[] _val)
    {
        OKButton_bt.onClick.RemoveAllListeners();
        OKButton_bt.onClick.AddListener(delegate { _l(_val); } );
    }
    public void SetActiveBackbutton(bool _enable)
    {
        backButton_obj.SetActive(_enable);
    }
    public void SetActiveBackbutton<T>(bool _enable, Listener<T> _l, params T[] _val)
    {
        if (_l != null)
            SetAddListenerBackbutton_SceneLoad(_l, _val);
        backButton_obj.SetActive(_enable);
    }
    private void SetAddListenerBackbutton_SceneLoad<T>(Listener<T> _l, params T[] _val)
    {
        backButton_bt.onClick.RemoveAllListeners();
        backButton_bt.onClick.AddListener(delegate { _l(_val); });
    }

    public void IsntActiveAllUI()
    {
        SetActiveOKbutton<byte>(false, null);
        SetActiveBackbutton<byte>(false, null);
    }
}
