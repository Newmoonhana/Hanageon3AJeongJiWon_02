using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomScene : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        INVENTORY
    }
    STATE state;

    public Slider levelbar;
    public Text levelbar_txt;
    character roomchara;

    private void Start()
    {
        GameManager.Instance.Setting_Frame(false);
        roomchara = CharacterManager.Instance.FindCharacter(ApartManager.Instance.thisUnit);
        UIManager.Instance.SetActiveOKbutton(false);
        UIManager.Instance.SetActiveBackbutton<byte>(true, delegate { InputBackButton(); });
        SkinManager.Instance.RefreshSkeleAni(roomchara, 0);
        SkinManager.Instance.character[0].charaSetting.skin.RefreshCustom(SkinManager.Instance.character[0].charaSetting.skin, SkinManager.Instance.character[0].skeleton);
        AnimationManager.Instance.ChangeCharaAni(SkinManager.Instance.character[0], TRACKTYPE.BODY, "public/Idle", true, roomchara.persona.GetEIToAniSpeed());
        RefreshLevelVar();
    }

    public void InputBackButton()
    {
        if (state != STATE.NONE)
        {
            return;
        }
        GameManager.Instance.LoadScene("ApartScene");
    }

    void RefreshLevelVar()
    {
        int lv = roomchara.exp.GetLevel();
        float exp = roomchara.exp.GetEXP();
        float exp_MAX = Experience_Setting.GetExpMAX(lv);
        levelbar.maxValue = exp_MAX;
        levelbar.value = exp;
        levelbar_txt.text = "Lv " + lv + " [ " + exp + " / " + exp_MAX + " ]";
    }
}
