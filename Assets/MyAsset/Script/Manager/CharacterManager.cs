using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PERSONA //성격 종류.
{
    EI,     //외향적(1) & 내향적(0)(애니메이션 속도에 영향).
    SN,     //상상력이 풍부(0) & 현실 직시(1).
    TF,     //논리적(0) & 감정적(1).
    JP,     //계획적(1) & 즉흥적(0.1).

    _MAX    //(마지막 값)
}

[System.Serializable]
public class personality   //캐릭터 성격 클래스(mbti 기반으로 음수<->양수 값으로 인게임에서 성격 처리).
{
    [SerializeField]
    float[] persona = new float[(int)(PERSONA._MAX)] { 0.5f, 0.5f, 0.5f, 0.5f };
    //생성자.
    public personality()
    {
        persona = new float[(int)(PERSONA._MAX)] { 0.5f, 0.5f, 0.5f, 0.5f };
    }
    public personality(float[] _persona)
    {
        substitutionP(_persona);
    }
    public personality(personality _persona)
    {
        substitutionP(_persona.persona);
    }
    //계산 - 대입.
    public void substitutionP(personality _persona)
    {
        if (_persona.persona.Length != (int)(PERSONA._MAX))
        {
            DebugManager.Instance.Log("_persona의 배열 개수가 PERSONA._MAX개가 아닙니다.\n개수 : " + _persona.persona.Length, LogType.Error);
            Debug.LogError("Error:_persona의 배열 개수가 PERSONA._MAX개가 아닙니다.\n개수 : " + _persona.persona.Length);
        }
        persona = _persona.persona;
    }
    public void substitutionP(float[] _persona)
    {
        if (_persona.Length != (int)(PERSONA._MAX))
        {
            DebugManager.Instance.Log("_persona의 배열 개수가 PERSONA._MAX개가 아닙니다.\n개수 : " + _persona.Length, LogType.Error);
            Debug.LogError("Error:_persona의 배열 개수가 PERSONA._MAX개가 아닙니다.\n개수 : " + _persona.Length);
        }
        persona = _persona;
    }
}

[System.Serializable]
public class character //캐릭터 정보 클래스.
{
    static int persona_size = 4;

    string name { get; set; }
    string nickname { get; set; }
    public personality persona;

    //생성자.
    public character()
    {
        name = "";
        nickname = "";
    }
    public character(string _name, string _nickname, personality _persona)
    {
        name = _name;
        nickname = _nickname;
        persona.substitutionP(_persona);
    }
}

public class CharacterManager : SingletonPattern_IsA_Mono<CharacterManager>
{
    public List<character> char_lst = new List<character>();
}
