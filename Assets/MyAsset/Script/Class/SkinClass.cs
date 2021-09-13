using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PARTSTYPE //부위 별 분류.
{
    EYEBLOW,  //눈썹
    FRONTHAIR,  //앞머리
    MOUTH,  //입
    EYELID,  //눈꺼풀
    EYEBALL,  //눈동자
    EYEWHITE, //눈 흰자
    CHEEK,  //볼
    HEAD,   //두상
    REARHAIR,   //뒷머리

    HAT,    //모자.
    OVERCOAT,   //코트.
    TOP,    //상의.
    BOTTOM, //하의.
    PANTS,  //바지, 치마 등 하의보다 위에 오는 상위 하의(코트랑 비슷).
    HAND,   //손(작업 과정에서 상의에 통합될 수 있으나 그 경우 해당 키워드는 손에 든 물건으로 대체).
    SHOES,  //신발(작업 과정에서 하의에 통합될 수 있음).

    _MAX //(마지막 값)
}

public class SkinParts
{
    public string name = null;
    [SerializeField]
    bool enable = true;    //기본 옷 켜짐/꺼짐(이벤트에서 사용).
    public Color color = new Color(255, 255, 255, 255);
    public Sprite sprite = null;

    public void ChangeParts(SkeletonAnimation _skeleton_ani, string[] _slot, string[] _key)
    {
        if (_slot.Length != _key.Length)
        {
            DebugManager.inst.Log("파츠 변경 중 변경할 슬롯과 키의 개수가 맞지 않습니다.", LogType.Error);
            return;
        }

        for (int i = 0; i < _slot.Length; i++)
        {
            //Debug.Log(i + "번째 슬롯 : " + _slot[i] + "\n파츠 : " + _key[i]);
            if (_slot[i] == null)
            {
                DebugManager.inst.Log("슬롯 이름이 존재하지 않습니다.\n이름: " + _slot[i], LogType.Error);
                return;
            }
            if (_key[i] == null || _key[i] == "")
            {
                //debugM.inst.Log("파츠 변경 : [" + _slot[i] + " <- [none] ]", LogType.Log);
                _skeleton_ani.skeleton.SetAttachment(_slot[i], null);
            }
            else
            {
                //debugM.inst.Log("파츠 변경 : [" + _slot[i] + " <- " + _key[i] + "]", LogType.Log);
                _skeleton_ani.skeleton.SetAttachment(_slot[i], _key[i]);
                //_skeleton_ani.skeleton.FindSlot(_slot[i]).SetColor(color);
            }
        }
        if (name != "nullskin" && name != "기본")
        {
            DebugManager.inst.Log("스킨 변경 : " + name, LogType.Log);
        }
    }
}

//**************머리**************//
//앞머리.
[System.Serializable]
public class Fronthair : SkinParts
{
    [SpineSlot] public string antennahairSlot;  //바보털
    [SpineAttachment] public string antennahairKey;
    [SpineSlot] public string fronthairSlot;
    [SpineAttachment] public string fronthairKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Fronthair _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        antennahairKey = _change.antennahairKey;
        fronthairKey = _change.fronthairKey;

        string[] slots = { antennahairSlot , fronthairSlot };
        string[] keys = { antennahairKey, fronthairKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}
//뒷머리.
[System.Serializable]
public class Rearhair : SkinParts
{
    [SpineSlot] public string rearhairSlot;
    [SpineAttachment] public string rearhairKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Rearhair _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        rearhairKey = _change.rearhairKey;

        string[] slots = { rearhairSlot };
        string[] keys = { rearhairKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

//모자.
[System.Serializable]
public class Hat : SkinParts
{

}
//**************얼굴**************//
//눈썹.
[System.Serializable]
public class Eyeblow : SkinParts
{
    [SpineSlot] public string eyeblowLSlot;
    [SpineAttachment] public string eyeblowLKey;
    [SpineSlot] public string eyeblowRSlot;
    [SpineAttachment] public string eyeblowRKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyeblow _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        eyeblowLKey = _change.eyeblowLKey;
        eyeblowRKey = _change.eyeblowRKey;
        //Debug.Log(eyeblowLSlot);
        //Debug.Log(eyeblowLKey);

        string[] slots = { eyeblowLSlot, eyeblowRSlot };
        string[] keys = { eyeblowLKey, eyeblowRKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

//눈꺼풀.
[System.Serializable]
public class Eyelid : SkinParts
{
    [SpineSlot] public string eyelidLUSlot;
    [SpineAttachment] public string eyelidLUKey;
    [SpineSlot] public string eyelidLDSlot;
    [SpineAttachment] public string eyelidLDKey;
    [SpineSlot] public string eyelidRUSlot;
    [SpineAttachment] public string eyelidRUKey;
    [SpineSlot] public string eyelidRDSlot;
    [SpineAttachment] public string eyelidRDKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyelid _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        eyelidLUKey = _change.eyelidLUKey;
        eyelidLDKey = _change.eyelidLDKey;
        eyelidRUKey = _change.eyelidRUKey;
        eyelidRDKey = _change.eyelidRDKey;

        string[] slots = { eyelidLUSlot, eyelidLDSlot, eyelidRUSlot, eyelidRDSlot };
        string[] keys = { eyelidLUKey, eyelidLDKey, eyelidRUKey, eyelidRDKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

//눈동자.
[System.Serializable]
public class Eyeball : SkinParts
{
    [SpineSlot] public string eyeballLSlot;
    [SpineAttachment] public string eyeballLKey;
    [SpineSlot] public string eyeballRSlot;
    [SpineAttachment] public string eyeballRKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyeball _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        eyeballLKey = _change.eyeballLKey;
        eyeballRKey = _change.eyeballRKey;

        string[] slots = { eyeballLSlot, eyeballRSlot };
        string[] keys = { eyeballLKey, eyeballRKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

//눈 흰자.
[System.Serializable]
public class Eyewhite : SkinParts
{
    [SpineSlot] public string eyewhiteLSlot;
    [SpineAttachment] public string eyewhiteLKey;
    [SpineSlot] public string eyewhiteRSlot;
    [SpineAttachment] public string eyewhiteRKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyewhite _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        eyewhiteLKey = _change.eyewhiteLKey;
        eyewhiteRKey = _change.eyewhiteRKey;

        string[] slots = { eyewhiteLSlot, eyewhiteRSlot };
        string[] keys = { eyewhiteLKey, eyewhiteRKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

//입.
[System.Serializable]
public class Mouth : SkinParts
{
    [SpineSlot] public string mouthSlot;
    [SpineAttachment] public string mouthKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Mouth _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        mouthKey = _change.mouthKey;

        string[] slots = { mouthSlot };
        string[] keys = { mouthKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

//볼.
[System.Serializable]
public class Cheek : SkinParts
{
    [SpineSlot] public string cheekSlot;
    [SpineAttachment] public string cheekKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Cheek _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        cheekKey = _change.cheekKey;

        string[] slots = { cheekSlot };
        string[] keys = { cheekKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}
//**************두상-기타**************//
//머리.
[System.Serializable]
public class Head : SkinParts
{
    [SpineSlot] public string headSlot;
    [SpineAttachment] public string headKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Head _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        headKey = _change.headKey;

        string[] slots = { headSlot };
        string[] keys = { headKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}
//**************상체**************//
//코트.
[System.Serializable]
public class Overcoat : SkinParts  //overcoat & L & R & B, armL & R(high, middle, low)
{
    //코트
    [SpineSlot] public string overcoatSlot; // 슬롯의 이름
    [SpineAttachment] public string overcoatKey; // 어테치먼트의 이름
    //코트(왼쪽)
    [SpineSlot] public string overcoatLSlot;
    [SpineAttachment] public string overcoatLKey;
    //코트(오른쪽)
    [SpineSlot] public string overcoatRSlot;
    [SpineAttachment] public string overcoatRKey;
    //코트(뒤)
    [SpineSlot] public string overcoatBSlot;
    [SpineAttachment] public string overcoatBKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Overcoat _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        overcoatKey = _change.overcoatKey; overcoatLKey = _change.overcoatLKey; overcoatRKey = _change.overcoatRKey; overcoatBKey = _change.overcoatBKey;

        string[] slots = { overcoatSlot, overcoatLSlot, overcoatRSlot, overcoatBSlot };
        string[] keys = { overcoatKey, overcoatLKey, overcoatRKey, overcoatBKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

[System.Serializable]
//상의
public class Top : SkinParts  //body & B, armL & R(high, middle, low)
{
    //상의
    [SpineSlot] public string bodySlot; // 슬롯의 이름
    [SpineAttachment] public string bodyKey; // 어테치먼트의 이름
    //상의(뒤)
    [SpineSlot] public string bodyBSlot;
    [SpineAttachment] public string bodyBKey;
    //왼팔(상)
    [SpineSlot] public string armL_highSlot;
    [SpineAttachment] public string armL_highKey;
    //왼팔(중)
    [SpineSlot] public string armL_middleSlot;
    [SpineAttachment] public string armL_middleKey;
    //왼팔(하)
    [SpineSlot] public string armL_lowSlot;
    [SpineAttachment] public string armL_lowKey;
    //오른팔(상)
    [SpineSlot] public string armR_highSlot;
    [SpineAttachment] public string armR_highKey;
    //오른팔(중)
    [SpineSlot] public string armR_middleSlot;
    [SpineAttachment] public string armR_middleKey;
    //오른팔(하)
    [SpineSlot] public string armR_lowSlot;
    [SpineAttachment] public string armR_lowKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Top _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        bodyKey = _change.bodyKey; bodyBKey = _change.bodyBKey;
        armL_highKey = _change.armL_highKey; armL_middleKey = _change.armL_middleKey; armL_lowKey = _change.armL_lowKey;
        armR_highKey = _change.armR_highKey; armR_middleKey = _change.armR_middleKey; armR_lowKey = _change.armR_lowKey;

        string[] slots = { bodySlot, bodyBSlot,
                            armL_highSlot, armL_middleSlot, armL_lowSlot,
                            armR_highSlot, armR_middleSlot, armR_lowSlot };
        string[] keys = { bodyKey, bodyBKey,
                            armL_highKey, armL_middleKey, armL_lowKey,
                            armR_highKey, armR_middleKey, armR_lowKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

[System.Serializable]
//**************하체**************//
//하의
public class Bottom : SkinParts  //waist, legL & R(high, middle, low)
{
    //상의
    [SpineSlot] public string waistSlot; // 슬롯의 이름
    [SpineAttachment] public string waistKey; // 어테치먼트의 이름
    //왼팔(상)
    [SpineSlot] public string legL_highSlot;
    [SpineAttachment] public string legL_highKey;
    //왼팔(중)
    [SpineSlot] public string legL_middleSlot;
    [SpineAttachment] public string legL_middleKey;
    //왼팔(하)
    [SpineSlot] public string legL_lowSlot;
    [SpineAttachment] public string legL_lowKey;
    //오른팔(상)
    [SpineSlot] public string legR_highSlot;
    [SpineAttachment] public string legR_highKey;
    //오른팔(중)
    [SpineSlot] public string legR_middleSlot;
    [SpineAttachment] public string legR_middleKey;
    //오른팔(하)
    [SpineSlot] public string legR_lowSlot;
    [SpineAttachment] public string legR_lowKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Bottom _change)
    {
        if (_change == null)
        {
            DebugManager.inst.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        waistKey = _change.waistKey;
        legL_highKey = _change.legL_highKey; legL_middleKey = _change.legL_middleKey; legL_lowKey = _change.legL_lowKey;
        legR_highKey = _change.legR_highKey; legR_middleKey = _change.legR_middleKey; legR_lowKey = _change.legR_lowKey;

        string[] slots = { waistSlot,
                            legL_highSlot, legL_middleSlot, legL_lowSlot,
                            legR_highSlot, legR_middleSlot, legR_lowSlot };
        string[] keys = { waistKey,
                           legL_highKey, legL_middleKey, legL_lowKey,
                            legR_highKey, legR_middleKey, legR_lowKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, this);
    }
}

[System.Serializable]
public class Skin
{
    public SkeletonAnimation skeleton_ani;
    [SpineSkin] public string baseSkinName; // 복사 할 스킨의 이름
    public Material baseMaterial; // 기본 머터리얼

    //스킨 목록.
    public Fronthair baseFronthair;
    public Rearhair baseRearhair;
    public Eyeblow baseEyeblow;
    public Eyelid baseEyelid;
    public Eyeball baseEyeball;
    public Eyewhite baseEyewhite;
    public Mouth baseMouth;
    public Cheek baseCheek;
    public Head baseHead;
    public Overcoat baseOvercoat;
    public Top baseTop;
    public Bottom baseBottom;

    public void ClearClothes()
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "nullskin");
        ChangeParts(PARTSTYPE.REARHAIR, "nullskin");
        ChangeParts(PARTSTYPE.EYEBLOW, "nullskin");
        ChangeParts(PARTSTYPE.EYELID, "nullskin");
        ChangeParts(PARTSTYPE.EYEBALL, "nullskin");
        ChangeParts(PARTSTYPE.EYEWHITE, "nullskin");
        ChangeParts(PARTSTYPE.MOUTH, "nullskin");
        ChangeParts(PARTSTYPE.CHEEK, "nullskin");
        ChangeParts(PARTSTYPE.HEAD, "nullskin");
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin");
        ChangeParts(PARTSTYPE.TOP, "nullskin");
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin");
    }

    public void DefaultCustom()
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "기본");
        ChangeParts(PARTSTYPE.REARHAIR, "기본");
        ChangeParts(PARTSTYPE.EYEBLOW, "기본");
        ChangeParts(PARTSTYPE.EYELID, "nullskin");
        ChangeParts(PARTSTYPE.EYEBALL, "기본");
        ChangeParts(PARTSTYPE.EYEWHITE, "nullskin");
        ChangeParts(PARTSTYPE.MOUTH, "기본");
        ChangeParts(PARTSTYPE.CHEEK, "기본");
        ChangeParts(PARTSTYPE.HEAD, "기본");
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin");
        ChangeParts(PARTSTYPE.TOP, "기본");
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin");
    }

    public void RefreshCustom(Skin _skin)
    {
        _skin.baseFronthair.RefreshSkin(skeleton_ani);
        _skin.baseRearhair.RefreshSkin(skeleton_ani);
        _skin.baseEyeblow.RefreshSkin(skeleton_ani);
        _skin.baseEyelid.RefreshSkin(skeleton_ani);
        _skin.baseEyeball.RefreshSkin(skeleton_ani);
        _skin.baseEyewhite.RefreshSkin(skeleton_ani);
        _skin.baseMouth.RefreshSkin(skeleton_ani);
        _skin.baseCheek.RefreshSkin(skeleton_ani);
        _skin.baseHead.RefreshSkin(skeleton_ani);
        _skin.baseOvercoat.RefreshSkin(skeleton_ani);
        _skin.baseTop.RefreshSkin(skeleton_ani);
        _skin.baseBottom.RefreshSkin(skeleton_ani);
    }

    public void ChangeParts(PARTSTYPE _type, string _clothes) //옷 변경.
    {
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                baseFronthair.ChangeSkin(skeleton_ani, SkinManager.inst.FindFronthairSkin(_clothes));
                break;
            case PARTSTYPE.REARHAIR:
                baseRearhair.ChangeSkin(skeleton_ani, SkinManager.inst.FindRearhairSkin(_clothes));
                break;
            case PARTSTYPE.EYEBLOW:
                baseEyeblow.ChangeSkin(skeleton_ani, SkinManager.inst.FindEyeblowSkin(_clothes));
                break;
            case PARTSTYPE.EYELID:
                baseEyelid.ChangeSkin(skeleton_ani, SkinManager.inst.FindEyelidSkin(_clothes));
                break;
            case PARTSTYPE.EYEBALL:
                baseEyeball.ChangeSkin(skeleton_ani, SkinManager.inst.FindEyeballSkin(_clothes));
                break;
            case PARTSTYPE.EYEWHITE:
                baseEyewhite.ChangeSkin(skeleton_ani, SkinManager.inst.FindEyewhiteSkin(_clothes));
                break;
            case PARTSTYPE.MOUTH:
                baseMouth.ChangeSkin(skeleton_ani, SkinManager.inst.FindMouthSkin(_clothes));
                break;
            case PARTSTYPE.CHEEK:
                baseCheek.ChangeSkin(skeleton_ani, SkinManager.inst.FindCheekSkin(_clothes));
                break;
            case PARTSTYPE.HEAD:
                baseHead.ChangeSkin(skeleton_ani, SkinManager.inst.FindHeadSkin(_clothes));
                break;
            case PARTSTYPE.OVERCOAT:
                baseOvercoat.ChangeSkin(skeleton_ani, SkinManager.inst.FindOvercoatSkin(_clothes));
                break;
            case PARTSTYPE.TOP:
                baseTop.ChangeSkin(skeleton_ani, SkinManager.inst.FindTopSkin(_clothes));
                break;
            case PARTSTYPE.BOTTOM:
                baseBottom.ChangeSkin(skeleton_ani, SkinManager.inst.FindBottomSkin(_clothes));
                break;
            default:
                DebugManager.inst.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
                break;
        }
    }
}
