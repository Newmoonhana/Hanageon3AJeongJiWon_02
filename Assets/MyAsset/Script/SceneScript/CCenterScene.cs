using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCenterScene : MonoBehaviour
{
    public void InputBackButton()   //뒤로 가기.
    {
        GameManager.Instance.LoadScene("MainScene");
    }

    public void InputAddCharaButton()   //주민 추가.
    {
        GameManager.Instance.LoadScene("Customize");
    }

    public void InputEditCharaButton()  //주민 정보.
    {

    }
}
