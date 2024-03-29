﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeScene : MonoBehaviour
{
    character basechar;
    public GameObject skinbuttom_pre;

    GameObject customPresentCategory;
    [Header("메인 카테고리 오브젝트 목록")]
    public GameObject customHeadCategory;
    public GameObject customPersonaCategory;
    public GameObject customInfoCategory;

    GameObject presentCategory;
    [Header("커스텀 카테고리 목록")]
    public GameObject hairCategory;
    public GameObject faceCategory;
    public GameObject otherCategory;

    [Header("스크롤 파츠 오브젝트 목록")]
    public Transform fronthairlst_tns;
    public Transform rearhairlst_tns;
    public Transform eyeblowlst_tns;
    public Transform eyelidlst_tns;
    public Transform eyeballlst_tns;
    public Transform eyewhitelst_tns;
    public Transform mouthlst_tns;
    public Transform cheeklst_tns;
    public Transform headlst_tns;

    [Header("성격 스크롤바 목록")]
    public Slider EI_slider;
    public Slider SN_slider;
    public Slider TF_slider;
    public Slider JP_slider;

    [Header("정보 입력 목록")]
    public Text name_txt;
    public Text nickname_txt;

    private void Start()
    {
        GameManager.Instance.Setting_Frame(false);
        UIManager.Instance.SetActiveOKbutton(false);
        UIManager.Instance.SetActiveBackbutton<byte>(true, delegate { InputBackButton(); });

        basechar = SkinManager.Instance.character[0].charaSetting;
        basechar.persona = new personality();
        customPresentCategory = customHeadCategory;
        presentCategory = hairCategory;

        RefreshSkinList(fronthairlst_tns, PARTSTYPE.FRONTHAIR);
        RefreshSkinList(rearhairlst_tns, PARTSTYPE.REARHAIR);
        RefreshSkinList(eyeblowlst_tns, PARTSTYPE.EYEBLOW);
        RefreshSkinList(eyelidlst_tns, PARTSTYPE.EYELID);
        RefreshSkinList(eyeballlst_tns, PARTSTYPE.EYEBALL);
        RefreshSkinList(eyewhitelst_tns, PARTSTYPE.EYEWHITE);
        RefreshSkinList(mouthlst_tns, PARTSTYPE.MOUTH);
        RefreshSkinList(cheeklst_tns, PARTSTYPE.CHEEK);
        RefreshSkinList(headlst_tns, PARTSTYPE.HEAD);

        basechar.skin.DefaultCustom(SkinManager.Instance.character[0].skeleton, true);  //주민 생성 기준.
    }

    public void InputBackButton()
    {
        GameManager.Instance.LoadBeforeScene();
    }

    public void InputSaveButton()   //캐릭터 파일 저장.
    {
        //신규 주민 생성 기준.
        basechar.name = name_txt.text;
        basechar.nickname = nickname_txt.text;
        int _unit = ApartManager.Instance.FirstNullRoom();
        if (_unit == -1)    //빈 방 없음
        {
            DebugManager.Instance.Log("빈 방이 없습니다.", LogType.Log);
            return;
        }
        basechar.unit = _unit;
        ApartManager.Instance.EditUnitSetting(basechar.unit, basechar.name);
        CharacterManager.Instance.AddCharacter(basechar);
        CharacterManager.Instance.SaveXML();
        GameManager.Instance.LoadScene("MainScene");
    }

    //버튼 입력 시 메인 카테고리 UI 변경.
    public void ChangeMainCustomCategory_button(string _type)
    {
        AnimationManager.Instance.ChangeCharaAni(SkinManager.Instance.character[0], TRACKTYPE.FACE, null, false, SkinManager.Instance.character[0].charaSetting.persona.GetEIToAniSpeed());
        switch (_type)
        {
            case "head":    //커스터마이징(머리)
                customPresentCategory.SetActive(false);
                customHeadCategory.SetActive(true);
                customPresentCategory = customHeadCategory;
                break;
            case "persona": //성격
                customPresentCategory.SetActive(false);
                customPersonaCategory.SetActive(true);
                customPresentCategory = customPersonaCategory;
                break;
            case "info": //정보
                customPresentCategory.SetActive(false);
                customInfoCategory.SetActive(true);
                customPresentCategory = customInfoCategory;
                break;
            default:
                DebugManager.Instance.Log("없는 스킨 카테고리입니다.", LogType.Error);
                return;
        }
    }

    //스킨 목록 생성.
    public void RefreshSkinList(Transform _tns, PARTSTYPE _type)
    {
        int size = 0;
        string[] partsName;
        Sprite[] partsSprite;
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                size = SkinManager.Instance.xml.Fronthair_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Fronthair_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Fronthair_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.REARHAIR:
                size = SkinManager.Instance.xml.Rearhair_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Rearhair_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Rearhair_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEBLOW:
                size = SkinManager.Instance.xml.Eyeblow_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Eyeblow_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Eyeblow_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYELID:
                size = SkinManager.Instance.xml.Eyelid_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Eyelid_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Eyelid_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEBALL:
                size = SkinManager.Instance.xml.Eyeball_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Eyeball_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Eyeball_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEWHITE:
                size = SkinManager.Instance.xml.Eyewhite_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Eyewhite_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Eyewhite_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.MOUTH:
                size = SkinManager.Instance.xml.Mouth_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Mouth_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Mouth_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.CHEEK:
                size = SkinManager.Instance.xml.Cheek_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Cheek_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Cheek_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.HEAD:
                size = SkinManager.Instance.xml.Head_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.xml.Head_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.xml.Head_Skin[i].sprite;
                }
                break;
            default:
                DebugManager.Instance.Log("없는 스킨 타입입니다.", LogType.Error);
                return;
        }
        for (int i = 0; i < size; i++)
        {
            DebugManager.Instance.Log(_type.ToString() + ' ' + i + "번 슬롯 생성", LogType.Log);
            GameObject tmp = Instantiate<GameObject>(skinbuttom_pre);
            tmp.transform.SetParent(_tns.GetChild(0).GetChild(0));
            //임시 변수 선언
            Button btn = tmp.transform.GetChild(0).GetComponent<Button>();
            Image img = tmp.transform.GetChild(0).GetChild(1).GetComponent<Image>();
            Text txt = tmp.transform.GetChild(1).GetComponent<Text>();
            PARTSTYPE_component pc = tmp.AddComponent<PARTSTYPE_component>();
            pc.PARTSTYPE_cp = _type;
            pc.partsname = partsName[i] == null ? null : partsName[i];
            //변경
            txt.text = partsName[i];
            img.sprite = partsSprite[i];
            btn.onClick.AddListener(delegate { ChangeSkin_button(pc); });
        }
    }

    //버튼 입력 시 예시 캐릭터의 스킨 변경.
    public void ChangeSkin_button(PARTSTYPE_component _parts)
    {
        basechar.skin.ChangeParts(_parts.PARTSTYPE_cp, _parts.partsname, SkinManager.Instance.character[0].skeleton, true);
    }

    //버튼 입력 시 스킨 카테고리 리스트 UI 변경.
    public void ChangeCustomCategory_button(string _type)
    {
        switch (_type)
        {
            case "hair":    //머리카락
                presentCategory.SetActive(false);
                hairCategory.SetActive(true);
                presentCategory = hairCategory;
                break;
            case "face": //얼굴
                presentCategory.SetActive(false);
                faceCategory.SetActive(true);
                presentCategory = faceCategory;
                break;
            case "other":
                presentCategory.SetActive(false);
                otherCategory.SetActive(true);
                presentCategory = otherCategory;
                break;
            default:
                DebugManager.Instance.Log("없는 스킨 카테고리입니다.", LogType.Error);
                return;
        }
    }

    //스크롤바 조작해 변수 위치 분배.
    public void CheckPersonaScrollvar(PERSONA_component _type)
    {
        int center = 5;
        Slider tmp;
        switch (_type.persona)
        {
            case PERSONA.EI:
                tmp = EI_slider;
            break;
            case PERSONA.SN:
                tmp = SN_slider;
                break;
            case PERSONA.TF:
                tmp = TF_slider;
                break;
            case PERSONA.JP:
                tmp = JP_slider;
                break;
            default:
                DebugManager.Instance.Log("없는 성격 타입입니다.", LogType.Error);
                return;
        }
        basechar.persona.substitutionP(_type.persona, tmp.value);

        string tmp_string = null;   //정확히 가운데 값은 표정변화 x.
        switch (_type.persona)  //표정 예시 보여주기.
        {
            case PERSONA.EI:
                if (tmp.value > center) //E
                    tmp_string = "personality/E/smile";
                else if (tmp.value < center)   //I
                    tmp_string = "personality/I/smile";
                break;
            case PERSONA.SN:
                if (tmp.value < center) //S
                    tmp_string = "personality/S/thinking";
                else if (tmp.value > center)    //N
                    tmp_string = "personality/N/thinking";
                break;
            case PERSONA.TF:
                if (tmp.value < center) //T
                    tmp_string = "personality/T/reaction";
                else if (tmp.value > center)    //F
                    tmp_string = "personality/F/reaction";
                break;
            case PERSONA.JP:
                if (tmp.value > center) //J
                    tmp_string = "personality/J/success";
                else if (tmp.value < center)    //P
                    tmp_string = "personality/P/success";
                break;
        }
        AnimationManager.Instance.ChangeCharaAni(SkinManager.Instance.character[0], TRACKTYPE.FACE, tmp_string, false, SkinManager.Instance.character[0].charaSetting.persona.GetEIToAniSpeed());
    }
}
