using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PERSONA //성격 종류.
{
    EI,    //외향 & 내향.
    SN, 
    TF,  //사고 & 감정.
    JP,

    _MAX    //(마지막 값)
}

public class character //캐릭터 정보 클래스.
{
    static int persona_size = 4;
    public class personality   //캐릭터 성격 클래스(mbti 기반으로 음수<->양수 값으로 인게임에서 성격 처리).
    {
        float[] persona;
        //생성자.
        public personality()
        {
            persona = new float[(int)(PERSONA._MAX)] { 0, 0, 0, 0 };
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
        public void substitutionP(float[] _persona)
        {
            if (_persona.Length != (int)(PERSONA._MAX))
            {
                GameManager.inst.debugM.Log("_persona의 배열 개수가 PERSONA._MAX개가 아닙니다.\n개수 : " + _persona.Length, LogType.Error);
                Debug.LogError("Error:_persona의 배열 개수가 PERSONA._MAX개가 아닙니다.\n개수 : " + _persona.Length);
            }
            persona = _persona;
        }
    }

    string name { get; set; }
    string nickname { get; set; }

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

    }
}

public class CharacterManager : MonoBehaviour
{
    public List<character> char_lst = new List<character>();
}
