﻿using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HEADTYPE
{
    EYEBLOW_L,
    EYEBLOW_R,
    FRONTHAIR,
    MOUTH,
    EYELID_LU,
    EYELID_RU,
    EYELID_LD,
    EYELID_RD,
    EYEBALL_L,
    EYEBALL_R,
    EYEWHITE_L,
    EYEWHITE_R,
    HEAD,
    REARHAIR,

    _MAX
}

public enum CLOTHESTYPE //옷 부위 별 분류.
{
    HAT,    //모자.
    OVERCOAT,   //코트.
    TOP,    //상의.
    BOTTOM, //하의.
    PANTS,  //바지, 치마 등 하의보다 위에 오는 상위 하의(코트랑 비슷).
    HAND,   //손(작업 과정에서 상의에 통합될 수 있으나 그 경우 해당 키워드는 손에 든 물건으로 대체).
    SHOES,  //신발(작업 과정에서 하의에 통합될 수 있음).

    _MAX //(마지막 값)
}

[System.Serializable]
public class CharHead
{
    
}

[System.Serializable]
public class Clothes
{
    public string name = null;
    [SerializeField]
    bool enable = true;    //기본 옷 켜짐/꺼짐(이벤트에서 사용).
    
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
            else if (_key[i] == null)
            {
                GameManager.inst.debugM.Log("파츠 이름이 존재하지 않습니다.\n이름: " + _key[i], LogType.Error);
                return;
            }
            if (_key[i] == "")
            {
                GameManager.inst.debugM.Log("파츠 변경 : [" + _slot[i] + " <- [none] ]", LogType.Log);
                _skeleton_ani.skeleton.SetAttachment(_slot[i], null);
                return;
            }
            else
            {
                GameManager.inst.debugM.Log("파츠 변경 : [" + _slot[i] + " <- " + _key[i] + "]", LogType.Log);
                _skeleton_ani.skeleton.SetAttachment(_slot[i], _key[i]);
            }
        }
        GameManager.inst.debugM.Log("상의 스킨 변경 : " + name, LogType.Log);
    }   
}

public class Hat : Clothes
{
    
}

//코트.
[System.Serializable]
public class Overcoat : Clothes  //overcoat & L & R & B, armL & R(high, middle, low)
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
        string[] slots = { _change.overcoatSlot, _change.overcoatLSlot, _change.overcoatRSlot, _change.overcoatBSlot };
        string[] keys = { _change.overcoatKey, _change.overcoatLKey, _change.overcoatRKey, _change.overcoatBKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
}

[System.Serializable]
//상의
public class Top : Clothes  //body & B, armL & R(high, middle, low)
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
        string[] slots = { _change.bodySlot, _change.bodyBSlot,
                            _change.armL_highSlot, _change.armL_middleSlot, _change.armL_lowSlot,
                            _change.armR_highSlot, _change.armR_middleSlot, _change.armR_lowSlot };
        string[] keys = { _change.bodyKey, _change.bodyBKey,
                            _change.armL_highKey, _change.armL_middleKey, _change.armL_lowKey,
                            _change.armR_highKey, _change.armR_middleKey, _change.armR_lowKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
}

[System.Serializable]
//상의
public class Bottom : Clothes  //waist, legL & R(high, middle, low)
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
        string[] slots = { _change.waistSlot,
                            _change.legL_highSlot, _change.legL_middleSlot, _change.legL_lowSlot,
                            _change.legR_highSlot, _change.legR_middleSlot, _change.legR_lowSlot };
        string[] keys = { _change.waistKey,
                            _change.legL_highKey, _change.legL_middleKey, _change.legL_lowKey,
                            _change.legR_highKey, _change.legR_middleKey, _change.legR_lowKey };
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
    public Overcoat baseOvercoat;
    public Top baseTop;
    public Bottom baseBottom;

    public void ClearClothes()
    {
        ChangeClothes(CLOTHESTYPE.OVERCOAT, "nullskin");
        ChangeClothes(CLOTHESTYPE.TOP, "nullskin");
        ChangeClothes(CLOTHESTYPE.BOTTOM, "nullskin");
    }

    public void ChangeClothes(CLOTHESTYPE _type, string _clothes) //옷 변경.
    {
        switch (_type)
        {
            case CLOTHESTYPE.OVERCOAT:
                {
                    baseOvercoat.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindOvercoatSkin(_clothes));
                }
                break;
            case CLOTHESTYPE.TOP:
                {
                    baseTop.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindTopSkin(_clothes));
                }
                break;
            case CLOTHESTYPE.BOTTOM:
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

public class SkinManager : MonoBehaviour    //얼굴과 옷 둘다 담당할 예정이지만 일단 옷 코드만.
{
    public Overcoat[] Overcoat_Skin;  //코트 스킨 목록.
    public Top[] Top_Skin;  //상의 스킨 목록.
    public Bottom[] Bottom_Skin;  //하의 스킨 목록.

    public Overcoat FindOvercoatSkin(string _clothes)
    {
        for (int i = 0; i < Overcoat_Skin.Length; i++)
        {
            if (Overcoat_Skin[i].name == _clothes)   //이름이 같은 파츠 검색 성공.
            {
                return Overcoat_Skin[i];
            }
        }
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Top FindTopSkin(string _clothes)
    {
        for (int i = 0; i < Top_Skin.Length; i++)
        {
            if (Top_Skin[i].name == _clothes)   //이름이 같은 파츠 검색 성공.
            {
                return Top_Skin[i];
            }
        }
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Bottom FindBottomSkin(string _clothes)
    {
        for (int i = 0; i < Top_Skin.Length; i++)
        {
            if (Bottom_Skin[i].name == _clothes)   //이름이 같은 파츠 검색 성공.
            {
                return Bottom_Skin[i];
            }
        }
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
}
