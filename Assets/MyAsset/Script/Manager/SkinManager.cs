using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CLOTHESTYPE //옷 부위 별 분류.
{
    HAT,    //모자.
    OVERCOAT,   //코트.
    TOP,    //상의.
    BOTTOM, //하의.
    HAND,   //손(작업 과정에서 상의에 통합될 수 있으나 그 경우 해당 키워드는 손에 든 물건으로 대체).
    SHOES,  //신발(작업 과정에서 하의에 통합될 수 있음).
    MAX //(마지막 값)
}

[System.Serializable]
public class Clothes
{
    public string name = null;
    [SerializeField]
    bool enable = false;    //기본 켜짐/꺼짐.
    
    public void ChangeParts(SkeletonAnimation _skeleton_ani, string[] _slot, string[] _key)
    {
        if (_slot.Length != _key.Length)
        {
            GameManager.inst.debugM.Log("파츠 변경 중 변경할 슬롯과 키의 개수가 맞지 않습니다.", LogType.Error);
            return;
        }

        for (int i = 0; i < _slot.Length; i++)
        {
            if (_slot[i] == null || _slot[i] == "")
            {
                GameManager.inst.debugM.Log("슬롯 이름이 존재하지 않습니다.", LogType.Error);
                return;
            }
            else if (_key[i] == null || _key[i] == "")
            {
                GameManager.inst.debugM.Log("파츠 이름이 존재하지 않습니다.", LogType.Error);
                return;
            }
            else
            {
                GameManager.inst.debugM.Log("파츠 변경 : [" + _slot[i] + ", " + _key[i] + "]", LogType.Log);
            }
            _skeleton_ani.skeleton.SetAttachment(_slot[i], _key[i]);
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
public class Skin
{
    public SkeletonAnimation skeleton_ani;
    [SpineSkin] public string baseSkinName; // 복사 할 스킨의 이름
    public Material baseMaterial; // 기본 머터리얼

    //스킨 목록.
    public Overcoat baseovercoat;
    public Top basetop;

    public void ChangeClothes(CLOTHESTYPE _type, string _clothes) //옷 변경.
    {
        switch (_type)
        {
            case CLOTHESTYPE.OVERCOAT:
                {
                    baseovercoat.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindOvercoatSkin(_clothes));
                }
                break;
            case CLOTHESTYPE.TOP:
                {
                    basetop.ChangeSkin(skeleton_ani, GameManager.inst.skinM.FindTopSkin(_clothes));
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
}
