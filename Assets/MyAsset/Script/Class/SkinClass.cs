﻿using Spine.Unity;
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
    public Color color = new Color(255f, 255f, 255f, 255f);
    public Sprite sprite = null;

    public void ChangeParts(SkeletonAnimation _skeleton_ani, string[] _slot, string[] _key)
    {
        if (_slot.Length != _key.Length)
        {
            GameManager.inst.debugM.Log("파츠 변경 중 변경할 슬롯과 키의 개수가 맞지 않습니다.", LogType.Error);
            return;
        }

        for (int i = 0; i < _slot.Length; i++)
        {
            if (_slot[i] == null)
            {
                GameManager.inst.debugM.Log("슬롯 이름이 존재하지 않습니다.\n이름: " + _slot[i], LogType.Error);
                return;
            }
            if (_key[i] == null || _key[i] == "")
            {
                //GameManager.inst.debugM.Log("파츠 변경 : [" + _slot[i] + " <- [none] ]", LogType.Log);
                _skeleton_ani.skeleton.SetAttachment(_slot[i], null);
            }
            else
            {
                //GameManager.inst.debugM.Log("파츠 변경 : [" + _slot[i] + " <- " + _key[i] + "]", LogType.Log);
                _skeleton_ani.skeleton.SetAttachment(_slot[i], _key[i]);
            }
        }
        GameManager.inst.debugM.Log("스킨 변경 : " + name, LogType.Log);
    }
}

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
            GameManager.inst.debugM.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
            GameManager.inst.debugM.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        rearhairKey = _change.rearhairKey;

        string[] slots = { rearhairSlot };
        string[] keys = { rearhairKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
}

//모자.
[System.Serializable]
public class Hat : SkinParts
{

}

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
            GameManager.inst.debugM.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        base.name = _change.name;
        base.sprite = _change.sprite;

        overcoatKey = _change.overcoatKey; overcoatLKey = _change.overcoatLKey; overcoatRKey = _change.overcoatRKey; overcoatBKey = _change.overcoatBKey;

        string[] slots = { overcoatSlot, overcoatLSlot, overcoatRSlot, overcoatBSlot };
        string[] keys = { overcoatKey, overcoatLKey, overcoatRKey, overcoatBKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
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
            GameManager.inst.debugM.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
}

[System.Serializable]
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
            GameManager.inst.debugM.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    public Overcoat baseOvercoat;
    public Top baseTop;
    public Bottom baseBottom;

    public void ClearClothes()
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "nullskin");
        ChangeParts(PARTSTYPE.REARHAIR, "nullskin");
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin");
        ChangeParts(PARTSTYPE.TOP, "nullskin");
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin");
    }

    public void ChangeParts(PARTSTYPE _type, string _clothes) //옷 변경.
    {
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                {
                    baseFronthair.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindFronthairSkin(_clothes));
                }
                break;
            case PARTSTYPE.REARHAIR:
                {
                    baseRearhair.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindRearhairSkin(_clothes));
                }
                break;
            case PARTSTYPE.OVERCOAT:
                {
                    baseOvercoat.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindOvercoatSkin(_clothes));
                }
                break;
            case PARTSTYPE.TOP:
                {
                    baseTop.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindTopSkin(_clothes));
                }
                break;
            case PARTSTYPE.BOTTOM:
                {
                    baseBottom.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindBottomSkin(_clothes));
                }
                break;
            default:
                {
                    GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
                }
                break;
        }
    }
}
