using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customize_Scroll : MonoBehaviour
{
    public CharSkin basechar;
    Skin basechar_skin;

    [Header("커스텀 카테고리 목록")]
    GameObject presentCategory;
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

    public GameObject skinbuttom_pre;

    private void Awake()
    {
        basechar_skin = basechar.baseskin;
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

    public void RefreshSkinList(Transform _tns, PARTSTYPE _type)
    {
        int size = 0;
        string[] partsName;
        Sprite[] partsSprite;
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                size = GameManager.inst.skinM.Fronthair_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Fronthair_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Fronthair_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.REARHAIR:
                size = GameManager.inst.skinM.Rearhair_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Rearhair_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Rearhair_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEBLOW:
                size = GameManager.inst.skinM.Eyeblow_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Eyeblow_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Eyeblow_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYELID:
                size = GameManager.inst.skinM.Eyelid_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Eyelid_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Eyelid_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEBALL:
                size = GameManager.inst.skinM.Eyeball_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Eyeball_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Eyeball_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.EYEWHITE:
                size = GameManager.inst.skinM.Eyewhite_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Eyewhite_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Eyewhite_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.MOUTH:
                size = GameManager.inst.skinM.Mouth_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Mouth_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Mouth_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.CHEEK:
                size = GameManager.inst.skinM.Cheek_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Cheek_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Cheek_Skin[i].sprite;
                }
                break;
            case PARTSTYPE.HEAD:
                size = GameManager.inst.skinM.Head_Skin.Length;
                partsName = new string[size]; partsSprite = new Sprite[size];
                for (int i = 0; i < size; i++)
                {
                    partsName[i] = GameManager.inst.skinM.Head_Skin[i].name;
                    partsSprite[i] = GameManager.inst.skinM.Head_Skin[i].sprite;
                }
                break;
            default:
                GameManager.inst.debugM.Log("없는 스킨 타입입니다.", LogType.Error);
                return;
        }
        for (int i = 0; i < size; i++)
        {
            GameManager.inst.debugM.Log(_type.ToString() + ' ' + i + "번 슬롯 생성", LogType.Log);
            GameObject tmp = Instantiate<GameObject>(skinbuttom_pre);
            tmp.transform.SetParent(_tns.GetChild(0).GetChild(0));
            //임시 변수 선언
            Button btn = tmp.transform.GetChild(0).GetComponent<Button>();
            Image img = tmp.transform.GetChild(0).GetChild(1).GetComponent<Image>();
            Text txt = tmp.transform.GetChild(1).GetComponent<Text>();
            PARTSTYPE_Component pc = tmp.AddComponent<PARTSTYPE_Component>();
            pc.PARTSTYPE_cp = _type;
            pc.partsname = partsName[i] == null ? null : partsName[i];
            //변경
            txt.text = partsName[i];
            img.sprite = partsSprite[i];
            btn.onClick.AddListener(delegate { ChangeSkin_button(pc); });
        }
    }

    //버튼 입력 시 예시 캐릭터의 스킨 변경.
    public void ChangeSkin_button(PARTSTYPE_Component _parts)
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
                GameManager.inst.debugM.Log("없는 스킨 카테고리입니다.", LogType.Error);
                return;
        }
    }
}
