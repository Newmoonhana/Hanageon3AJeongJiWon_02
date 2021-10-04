using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        INVENTORY
    }
    STATE state;

    private void Start()
    {
        UIManager.Instance.SetActiveOKbutton(false);
        UIManager.Instance.SetActiveBackbutton<byte>(true, delegate { InputBackButton(); });
        SkinManager.Instance.RefreshSkeleAni(CharacterManager.Instance.FindCharacter(ApartManager.Instance.thisUnit), 0);
        SkinManager.Instance.character[0].charaSetting.skin.RefreshCustom(SkinManager.Instance.character[0].charaSetting.skin, SkinManager.Instance.character[0].skeleton);
    }

    public void InputBackButton()
    {
        if (state != STATE.NONE)
        {
            return;
        }
        GameManager.Instance.LoadScene("ApartScene");
    }
}
