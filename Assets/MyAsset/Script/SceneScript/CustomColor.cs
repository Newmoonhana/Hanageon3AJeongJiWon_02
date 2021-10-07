using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomColor : MonoBehaviour
{
    public PARTSTYPE_component partstype;
    public Image color_img;
    public Image scroll_img;
    public Scrollbar this_scroll;

    Color color = Color.black;

    public void SetCustomColor()
    {
        PARTSTYPE type = partstype.PARTSTYPE_cp;
        color_img = this.transform.parent.GetChild(1).GetComponent<Image>();
        scroll_img = this.GetComponent<Image>();
        Color sumColor = color_img.color;
        switch (this.name)
        {
            case "R":
                sumColor.r = color.r = this_scroll.value;
                break;
            case "G":
                sumColor.g = color.g = this_scroll.value;
                break;
            case "B":
                sumColor.b = color.b = this_scroll.value;
                break;
            case "A":
                sumColor.a = color.a = this_scroll.value;
                break;
        }
        color_img.color = sumColor;
        scroll_img.color = color;

        //미리보기 스킨 색상 변경('일단' 머리만 커스텀하므로 옷은 제외).
        switch (type)
        {
            case PARTSTYPE.FRONTHAIR:
                SkinManager.Instance.character[0].charaSetting.skin.baseFronthair.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseFronthair.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.REARHAIR:
                SkinManager.Instance.character[0].charaSetting.skin.baseRearhair.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseRearhair.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.EYEBLOW:
                SkinManager.Instance.character[0].charaSetting.skin.baseEyeblow.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseEyeblow.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.EYELID:
                SkinManager.Instance.character[0].charaSetting.skin.baseEyelid.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseEyelid.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.EYEBALL:
                SkinManager.Instance.character[0].charaSetting.skin.baseEyeball.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseEyeball.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.EYEWHITE:
                SkinManager.Instance.character[0].charaSetting.skin.baseEyewhite.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseEyewhite.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.MOUTH:
                SkinManager.Instance.character[0].charaSetting.skin.baseMouth.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseMouth.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.HEAD:
                SkinManager.Instance.character[0].charaSetting.skin.baseHead.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseHead.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            case PARTSTYPE.CHEEK:
                SkinManager.Instance.character[0].charaSetting.skin.baseCheek.skincolor = sumColor;
                SkinManager.Instance.character[0].charaSetting.skin.baseCheek.RefreshSkin(SkinManager.Instance.character[0].skeleton);
                break;
            default:
                DebugManager.Instance.Log("없는 스킨 타입입니다.", LogType.Error);
                return;
        }
    }
}
