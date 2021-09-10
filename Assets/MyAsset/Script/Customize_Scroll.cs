using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customize_Scroll : MonoBehaviour
{
    public CharSkin basechar;
    Skin basechar_skin;
    [Header("스크롤 파츠 오브젝트 목록")]
    public Transform fronthairlst_tns;
    public Transform rearhairlst_tns;
    public Transform otherhairlst_tns;

    public GameObject skinbuttom_pre;

    private void Awake()
    {
        basechar_skin = basechar.baseskin;

        RefreshSkinList(fronthairlst_tns, PARTSTYPE.FRONTHAIR);
    }

    public void RefreshSkinList(Transform _tns, PARTSTYPE _type)
    {
        int size = 0;
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                Debug.Log(GameManager.inst);
                size = GameManager.inst.skinM.Fronthair_Skin.Length;
                break;
            default:
                GameManager.inst.debugM.Log("없는 스킨 타입입니다.", LogType.Error);
                return;
        }
        for (int i = 0; i < size; i++)
        {
            GameManager.inst.debugM.Log(i + "슬롯 생성", LogType.Log);
            GameObject tmp = Instantiate<GameObject>(skinbuttom_pre);
            tmp.transform.SetParent(_tns.GetChild(0).GetChild(0));
            string _name = GameManager.inst.skinM.Fronthair_Skin[i].name;
            tmp.transform.GetChild(1).GetComponent<Text>().text = _name;
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManager.inst.skinM.Fronthair_Skin[i].sprite;
        }
    }

    //버튼 입력 시 스킨 변경.
    public void ChangeSkin_button(PARTSTYPE_Component _parts)
    {
        basechar_skin.ChangeParts(_parts.PARTSTYPE_cp, _parts.partsname);
    }
}
