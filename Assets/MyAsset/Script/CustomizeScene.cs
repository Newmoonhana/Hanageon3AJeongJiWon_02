using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeScene : MonoBehaviour
{
    public CharSkin basechar;
    Skin basechar_skin;
    public GameObject skinbuttom_pre;

    GameObject customPresentCategory;
    [Header("메인 카테고리 오브젝트 목록")]
    public GameObject customHeadCategory;
    public GameObject customPersonaCategory;

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

    private void Awake()
    {
        basechar_skin = basechar.baseskin;
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

        basechar_skin.DefaultCustom();
    }

    //버튼 입력 시 메인 카테고리 UI 변경.
    public void ChangeMainCustomCategory_button(string _type)
    {
        switch (_type)
        {
            case "head":    //머리카락
                customPresentCategory.SetActive(false);
                customHeadCategory.SetActive(true);
                customPresentCategory = customHeadCategory;
                break;
            case "persona": //얼굴
                customPresentCategory.SetActive(false);
                customPersonaCategory.SetActive(true);
                customPresentCategory = customPersonaCategory;
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
                size = SkinManager.Instance.Fronthair_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Fronthair_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Fronthair_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.REARHAIR:
                size = SkinManager.Instance.Rearhair_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Rearhair_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Rearhair_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEBLOW:
                size = SkinManager.Instance.Eyeblow_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Eyeblow_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Eyeblow_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYELID:
                size = SkinManager.Instance.Eyelid_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Eyelid_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Eyelid_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEBALL:
                size = SkinManager.Instance.Eyeball_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Eyeball_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Eyeball_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEWHITE:
                size = SkinManager.Instance.Eyewhite_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Eyewhite_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Eyewhite_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.MOUTH:
                size = SkinManager.Instance.Mouth_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Mouth_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Mouth_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.CHEEK:
                size = SkinManager.Instance.Cheek_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Cheek_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Cheek_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.HEAD:
                size = SkinManager.Instance.Head_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = SkinManager.Instance.Head_Skin[i].name;
                    partsSprite[i] = SkinManager.Instance.Head_Skin[i].sprite;
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
        basechar_skin.ChangeParts(_parts.PARTSTYPE_cp, _parts.partsname);
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

        string tmp_string = null;   //정확히 가운데 값은 표정변화 x.
        switch (_type.persona)  //표정 예시 보여주기.
        {
            case PERSONA.EI:
                if (tmp.value > center) //E
                    tmp_string = "personality/E/smile_E";
                else if (tmp.value < center)   //I
                    tmp_string = "personality/I/smile_I";
                break;
            case PERSONA.SN:
                if (tmp.value < center) //S
                    tmp_string = "personality/S/thinking_S";
                else if (tmp.value > center)    //N
                    tmp_string = "personality/N/thinking_N";
                break;
            case PERSONA.TF:
                if (tmp.value < center) //T
                    tmp_string = "personality/T/reaction_T";
                else if (tmp.value > center)    //F
                    tmp_string = "personality/F/reaction_F";
                break;
            case PERSONA.JP:
                if (tmp.value > center) //J
                    tmp_string = "personality/J/success_J";
                else if (tmp.value < center)    //P
                    tmp_string = "personality/P/success_P";
                break;
        }
        AnimationManager.Instance.ChangeCharaAni(SkinManager.Instance.character[0].baseskin, TRACKTYPE.FACE, tmp_string, false);
    }

    public void CustomColor(Scrollbar _this)
    {
        PARTSTYPE type = _this.transform.parent.GetComponent<PARTSTYPE_component>().PARTSTYPE_cp;
        Image color_img = _this.transform.parent.GetChild(1).GetComponent<Image>();
        Image scroll_img = _this.GetComponent<Image>();
        Color c = color_img.color;
        Color scroll = Color.black;
        switch (_this.name)
        {
            case "R":
                c.r = _this.value;
                scroll.r = _this.value;
                break;
            case "G":
                c.g = _this.value;
                scroll.g = _this.value;
                break;
            case "B":
                c.b = _this.value;
                scroll.b = _this.value;
                break;
            case "A":
                c.a = _this.value;
                break;
        }
        color_img.color = c;
        scroll_img.color = scroll;

        //미리보기 스킨 색상 변경(머리만 커스텀하므로 옷은 제외).
        switch (type)
        {
            case PARTSTYPE.FRONTHAIR:
                SkinManager.Instance.character[0].baseskin.baseFronthair.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseFronthair.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.REARHAIR:
                SkinManager.Instance.character[0].baseskin.baseRearhair.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseRearhair.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.EYEBLOW:
                SkinManager.Instance.character[0].baseskin.baseEyeblow.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseEyeblow.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.EYELID:
                SkinManager.Instance.character[0].baseskin.baseEyelid.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseEyelid.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.EYEBALL:
                SkinManager.Instance.character[0].baseskin.baseEyeball.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseEyeball.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.EYEWHITE:
                SkinManager.Instance.character[0].baseskin.baseEyewhite.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseEyewhite.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.MOUTH:
                SkinManager.Instance.character[0].baseskin.baseMouth.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseMouth.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.HEAD:
                SkinManager.Instance.character[0].baseskin.baseHead.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseHead.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            case PARTSTYPE.CHEEK:
                SkinManager.Instance.character[0].baseskin.baseCheek.skincolor = c;
                SkinManager.Instance.character[0].baseskin.baseCheek.RefreshSkin(SkinManager.Instance.character[0].baseskin.skeleton_ani);
                break;
            default:
                DebugManager.Instance.Log("없는 스킨 타입입니다.", LogType.Error);
                return;
        }
    }
}
