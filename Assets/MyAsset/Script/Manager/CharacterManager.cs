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
    float center = 5f;
    [SerializeField]
    float[] persona = new float[(int)(PERSONA._MAX)] { 5f, 5f, 5f, 5f };
    
    
    //생성자.
    public personality()
    {
        persona = new float[(int)(PERSONA._MAX)] { center, center, center, center };
    }
    public personality(float[] _persona)
    {
        substitutionP(_persona);
    }
    public personality(personality _persona)
    {
        substitutionP(_persona.persona);
    }
    public override string ToString()
    {
        string tmp = "";
        for (int i = 0; i < (int)PERSONA._MAX; i++)
        {
            tmp += persona[i].ToString();
            if (i != (int)PERSONA._MAX)
                tmp += "/";
        }
            
        return tmp;
    }
    public personality StringToPersona(string _persona)
    {
        personality tmp = new personality();
        string[] persona_tmp = _persona.Split('/');
        for (int i = 0; i < (int)PERSONA._MAX; i++)
        {
            tmp.persona[i] = float.Parse(persona_tmp[i]);
        }
        return tmp;
    }

    //get
    public float GetEIToAniSpeed()
    {
        return 1f + (persona[0] - center) * 0.1f;
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
    public void substitutionP(float _EI, float _SN, float _TF, float _JP)
    {
        persona[0] = _EI;
        persona[1] = _SN;
        persona[2] = _TF;
        persona[3] = _JP;
    }
    public void substitutionP(PERSONA _type, float _value)
    {
        persona[(int)_type] = _value;
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
    public string name;
    public string nickname;
    public personality persona = new personality();
    public Skin skin = new Skin();
    public int unit = 0;    //아파트 호 수.
    public Experience exp = new Experience();

    //생성자.
    public character()
    {
        name = "";
        nickname = "";
    }
    public character(string _name, string _nickname, Skin _skin, personality _persona, int _unit, Experience _exp)
    {
        this.name = _name;
        this.nickname = _nickname;
        this.skin = _skin;
        this.persona = _persona;
        this.unit = _unit;
        this.exp = _exp;
    }
    public character(string _name, string _nickname, string _skin, string _persona, string _unit, string _exp)
    {
        this.name = _name;
        this.nickname = _nickname;
        this.skin.SetStringToSkin(_skin);
        this.persona = this.persona.StringToPersona(_persona);
        this.unit = int.Parse(_unit);
        this.exp = this.exp.StringToEXP(_exp);
    }
    public character(character _chara)
    {
        this.name = _chara.name;
        this.nickname = _chara.nickname;
        this.skin = _chara.skin;
        this.persona = _chara.persona;
        this.unit = _chara.unit;
        this.exp = _chara.exp;
    }
}

public class CharacterManager : SingletonPattern_IsA_Mono<CharacterManager>
{
    public List<character> char_lst = new List<character>();

    [ContextMenu("SaveXML")]
    public void SaveXML()
    {
        PlayerInfoXML.WriteCharacterInfo();
    }
    [ContextMenu("LoadXML")]
    void LoadXML()
    {
        PlayerInfoXML.ReadCharacterInfo();
    }

    public void Awake()
    {
        if (DontDestroyInst(this))
        {
            LoadXML();
        }
    }

    public void AddCharacter(character _chara)
    {
        Instance.char_lst.Add(_chara);
        ApartManager.Instance.EditUnitSetting(_chara.unit, _chara.name);
    }

    public character FindCharacter(int _unit)
    {
        foreach (var item in char_lst)
        {
            if (item.unit == _unit)
                return item;
        }
        return null;
    }
}
