using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApartScene : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        UNITCANVAS
    }
    STATE state = STATE.NONE;
    //state obj
    public GameObject unitstate_obj;

    public GameObject apart_content;
    List<GameObject> floor_lst; //층 리스트
    List<Button> unitButton_lst;    //호 수 버튼 리스트
    public Image room_img;  //인테리어 이미지
    public Text unit_txt;   //호 수 표시
    public GameObject unitButton_pre;

    private void Start()
    {
        UIManager.Instance.SetActiveOKbutton(false);
        UIManager.Instance.SetActiveBackbutton<byte>(true, delegate { InputBackButton(); });

        int size = apart_content.transform.childCount;
        floor_lst = new List<GameObject>();
        unitButton_lst = new List<Button>();
        for (int i = 1; i <= ApartManager.Instance.floor_max; i++)  //아파트 로비와 꼭대기 층을 제외한 호수가 있는 층만 카운트.
        {
            Transform tmp = apart_content.transform.GetChild(i);
            floor_lst.Add(tmp.gameObject);  //리스트에 층 추가.
            int unit_size = ApartManager.Instance.unit_max;
            for (int j = 0; j < unit_size; j++) //리스트에 호 수 버튼 추가.
            {
                GameObject button_obj;
                button_obj = Instantiate(unitButton_pre, tmp);  //반복해서 생성/제거되는 옵젝이 아니라 오브젝트 풀링 x
                Button unit_button = button_obj.GetComponent<Button>();
                APARTSCENE_component unit_button_as = unit_button.GetComponent<APARTSCENE_component>();
                unit_button_as.unit = i * 100 + j + 1;  //호 수
                unit_button.onClick.AddListener(delegate { InputUnitButton(unit_button_as); } );    //버튼 입력 추가
                unitButton_lst.Add(unit_button);
            }
        }
    }

    public void InputUnitButton(APARTSCENE_component _type)
    {
        if (state != STATE.NONE)
        {
            return;
        }
        unitstate_obj.SetActive(true);
        room_img.sprite = _type.room_spr;
        UnitSetting tmp = ApartManager.Instance.FindUnitSetting(_type.unit);
        if (tmp == null)
        {
            unit_txt.text = string.Format("{0:###}동", _type.unit);
        }
        else
        {
            unit_txt.text = string.Format("{0:###}동 {1}", _type.unit, tmp.chara_name);
        }
        ApartManager.Instance.thisUnit = tmp.unit;

        state = STATE.UNITCANVAS;
        UIManager.Instance.SetActiveOKbutton<byte>(true, delegate { InputInButton(); });
    }

    public void InputInButton()
    {
        if (state != STATE.UNITCANVAS)
        {
            DebugManager.Instance.Log("state가 UNITCANVAS가 아닙니다.\nstate = " + state.ToString(), LogType.Warning);
            return;
        }
        GameManager.Instance.LoadScene("RoomScene");
    }

    public void InputBackButton()
    {
        switch (state)
        {
            case STATE.NONE:
                GameManager.Instance.LoadScene("MainScene");
                break;
            case STATE.UNITCANVAS:
                ApartManager.Instance.thisUnit = -1;
                unitstate_obj.SetActive(false);
                UIManager.Instance.SetActiveOKbutton(false);
                state = STATE.NONE;
                break;
        }
    }
}
