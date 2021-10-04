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
    //public Color color = new Color(255, 255, 255, 255);
    public Color skincolor = new Color32(255, 255, 255, 255);
    public Sprite sprite = null;

    public void ChangeParts(SkeletonAnimation _skeleton, string[] _slot, string[] _key)
    {
        if (_slot.Length != _key.Length)
        {
            DebugManager.Instance.Log("파츠 변경 중 변경할 슬롯과 키의 개수가 맞지 않습니다.", LogType.Error);
            return;
        }

        for (int i = 0; i < _slot.Length; i++)
        {
            //Debug.Log(i + "번째 슬롯 : " + _slot[i] + "\n파츠 : " + _key[i]);
            if (_slot[i] == null)
            {
                DebugManager.Instance.Log("슬롯 이름이 존재하지 않습니다.\n이름: " + _slot[i], LogType.Error);
                return;
            }
            if (_key[i] == null || _key[i] == "")
            {
                DebugManager.Instance.Log("파츠 변경 : [" + _slot[i] + " <- [none] ]", LogType.Log);
                _skeleton.skeleton.SetAttachment(_slot[i], null);
            }
            else
            {
                DebugManager.Instance.Log("파츠 변경 : [" + _slot[i] + " <- " + _key[i] + "]", LogType.Log);
                _skeleton.skeleton.SetAttachment(_slot[i], _key[i]);
                _skeleton.skeleton.FindSlot(_slot[i]).SetColor(skincolor);
            }
        }
        if (name != "nullskin")
        {
            DebugManager.Instance.Log("스킨 변경 : " + name, LogType.Log);
        }
    }
}

//**************머리**************//
//앞머리.
[System.Serializable]
public class Fronthair : SkinParts
{
    [SpineSlot] string antennahairSlot = "antennahair";  //바보털
    [SpineAttachment] public string antennahairKey;
    [SpineSlot] string fronthairSlot = "fronthair";
    [SpineAttachment] public string fronthairKey;

    public Fronthair() { }
    public Fronthair(Fronthair _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.antennahairKey = _new.antennahairKey;
        this.fronthairKey = _new.fronthairKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Fronthair _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        antennahairKey = _change.antennahairKey;
        fronthairKey = _change.fronthairKey;

        string[] slots = { antennahairSlot , fronthairSlot };
        string[] keys = { antennahairKey, fronthairKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindFronthairSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Fronthair tmp = new Fronthair();
        tmp.antennahairKey = null;
        tmp.fronthairKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}
//뒷머리.
[System.Serializable]
public class Rearhair : SkinParts
{
    [SpineSlot] string rearhairSlot = "rearhair";
    [SpineAttachment] public string rearhairKey;

    public Rearhair() { }
    public Rearhair(Rearhair _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.rearhairKey = _new.rearhairKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Rearhair _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        rearhairKey = _change.rearhairKey;

        string[] slots = { rearhairSlot };
        string[] keys = { rearhairKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindRearhairSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Rearhair tmp = new Rearhair();
        tmp.rearhairKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
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
    [SpineSlot] string eyeblowLSlot = "eyeblowL";
    [SpineAttachment] public string eyeblowLKey;
    [SpineSlot] string eyeblowRSlot = "eyeblowR";
    [SpineAttachment] public string eyeblowRKey;

    public Eyeblow() { }
    public Eyeblow(Eyeblow _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.eyeblowLKey = _new.eyeblowLKey;
        this.eyeblowRKey = _new.eyeblowRKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyeblow _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

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
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindEyeblowSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Eyeblow tmp = new Eyeblow();
        tmp.eyeblowLKey = null;
        tmp.eyeblowRKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

//눈꺼풀.
[System.Serializable]
public class Eyelid : SkinParts
{
    [SpineSlot] string eyelidLUSlot = "eyelidLU";
    [SpineAttachment] public string eyelidLUKey;
    [SpineSlot] string eyelidLDSlot = "eyelidLD";
    [SpineAttachment] public string eyelidLDKey;
    [SpineSlot] string eyelidRUSlot = "eyelidRU";
    [SpineAttachment] public string eyelidRUKey;
    [SpineSlot] string eyelidRDSlot = "eyelidRD";
    [SpineAttachment] public string eyelidRDKey;

    public Eyelid() { }
    public Eyelid(Eyelid _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.eyelidLUKey = _new.eyelidLUKey;
        this.eyelidLDKey = _new.eyelidLDKey;
        this.eyelidRUKey = _new.eyelidRUKey;
        this.eyelidRDKey = _new.eyelidRDKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyelid _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

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
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindEyelidSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Eyelid tmp = new Eyelid();
        tmp.eyelidLUKey = null;
        tmp.eyelidLDKey = null;
        tmp.eyelidRUKey = null;
        tmp.eyelidRDKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

//눈동자.
[System.Serializable]
public class Eyeball : SkinParts
{
    [SpineSlot] string eyeballLSlot = "eyeballL";
    [SpineAttachment] public string eyeballLKey;
    [SpineSlot] string eyeballRSlot = "eyeballR";
    [SpineAttachment] public string eyeballRKey;

    public Eyeball() { }
    public Eyeball(Eyeball _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.eyeballLKey = _new.eyeballLKey;
        this.eyeballRKey = _new.eyeballRKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyeball _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        eyeballLKey = _change.eyeballLKey;
        eyeballRKey = _change.eyeballRKey;

        string[] slots = { eyeballLSlot, eyeballRSlot };
        string[] keys = { eyeballLKey, eyeballRKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindEyeballSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Eyeball tmp = new Eyeball();
        tmp.eyeballLKey = null;
        tmp.eyeballRKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

//눈 흰자.
[System.Serializable]
public class Eyewhite : SkinParts
{
    [SpineSlot] string eyewhiteLSlot = "eyewhiteL";
    [SpineAttachment] public string eyewhiteLKey;
    [SpineSlot] string eyewhiteRSlot = "eyewhiteR";
    [SpineAttachment] public string eyewhiteRKey;

    public Eyewhite() { }
    public Eyewhite(Eyewhite _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.eyewhiteLKey = _new.eyewhiteLKey;
        this.eyewhiteRKey = _new.eyewhiteRKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyewhite _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        eyewhiteLKey = _change.eyewhiteLKey;
        eyewhiteRKey = _change.eyewhiteRKey;

        string[] slots = { eyewhiteLSlot, eyewhiteRSlot };
        string[] keys = { eyewhiteLKey, eyewhiteRKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindEyewhiteSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Eyewhite tmp = new Eyewhite();
        tmp.eyewhiteLKey = null;
        tmp.eyewhiteRKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

//입.
[System.Serializable]
public class Mouth : SkinParts
{
    [SpineSlot] string mouthSlot = "mouth";
    [SpineAttachment] public string mouthKey;

    public Mouth() { }
    public Mouth(Mouth _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.mouthKey = _new.mouthKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Mouth _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        mouthKey = _change.mouthKey;

        string[] slots = { mouthSlot };
        string[] keys = { mouthKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindMouthSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Mouth tmp = new Mouth();
        tmp.mouthKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

//볼.
[System.Serializable]
public class Cheek : SkinParts
{
    [SpineSlot] string cheekSlot = "cheek";
    [SpineAttachment] public string cheekKey;

    public Cheek() { }
    public Cheek(Cheek _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.cheekKey = _new.cheekKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Cheek _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        cheekKey = _change.cheekKey;

        string[] slots = { cheekSlot };
        string[] keys = { cheekKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindCheekSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Cheek tmp = new Cheek();
        tmp.cheekKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}
//**************두상-기타**************//
//머리.
[System.Serializable]
public class Head : SkinParts
{
    [SpineSlot] string headSlot = "head";
    [SpineAttachment] public string headKey;

    public Head() { }
    public Head(Head _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.headKey = _new.headKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Head _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        headKey = _change.headKey;

        string[] slots = { headSlot };
        string[] keys = { headKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindHeadSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Head tmp = new Head();
        tmp.headKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}
//**************상체**************//
//코트.
[System.Serializable]
public class Overcoat : SkinParts  //overcoat & L & R & B, armL & R(high, middle, low)
{
    //코트
    [SpineSlot] string overcoatSlot = "overcoat"; // 슬롯의 이름
    [SpineAttachment] public string overcoatKey; // 어테치먼트의 이름
    //코트(왼쪽)
    [SpineSlot] string overcoatLSlot = "overcoatL";
    [SpineAttachment] public string overcoatLKey;
    //코트(오른쪽)
    [SpineSlot] string overcoatRSlot = "overcoatR";
    [SpineAttachment] public string overcoatRKey;
    //코트(뒤)
    [SpineSlot] string overcoatBSlot = "overcoatB";
    [SpineAttachment] public string overcoatBKey;

    public Overcoat() { }
    public Overcoat(Overcoat _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.overcoatKey = _new.overcoatKey;
        this.overcoatBKey = _new.overcoatBKey;
        this.overcoatLKey = _new.overcoatLKey;
        this.overcoatRKey = _new.overcoatRKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Overcoat _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

        overcoatKey = _change.overcoatKey; overcoatLKey = _change.overcoatLKey; overcoatRKey = _change.overcoatRKey; overcoatBKey = _change.overcoatBKey;

        string[] slots = { overcoatSlot, overcoatLSlot, overcoatRSlot, overcoatBSlot };
        string[] keys = { overcoatKey, overcoatLKey, overcoatRKey, overcoatBKey };
        base.ChangeParts(_skeleton_ani, slots, keys);
    }
    public void RefreshSkin(SkeletonAnimation _skeleton_ani)
    {
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindOvercoatSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Overcoat tmp = new Overcoat();
        tmp.overcoatKey = null;
        tmp.overcoatLKey = null;
        tmp.overcoatRKey = null;
        tmp.overcoatBKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

[System.Serializable]
//상의
public class Top : SkinParts  //body & B, armL & R(high, middle, low)
{
    //상의
    [SpineSlot] string bodySlot = "body"; // 슬롯의 이름
    [SpineAttachment] public string bodyKey; // 어테치먼트의 이름
    //상의(뒤)
    [SpineSlot] string bodyBSlot = "bodyB";
    [SpineAttachment] public string bodyBKey;
    //왼팔(상)
    [SpineSlot] string armL_highSlot = "armL_high";
    [SpineAttachment] public string armL_highKey;
    //왼팔(중)
    [SpineSlot] string armL_middleSlot = "armL_middle";
    [SpineAttachment] public string armL_middleKey;
    //왼팔(하)
    [SpineSlot] string armL_lowSlot = "armL_low";
    [SpineAttachment] public string armL_lowKey;
    //오른팔(상)
    [SpineSlot] string armR_highSlot = "armR_high";
    [SpineAttachment] public string armR_highKey;
    //오른팔(중)
    [SpineSlot] string armR_middleSlot = "armR_middle";
    [SpineAttachment] public string armR_middleKey;
    //오른팔(하)
    [SpineSlot] string armR_lowSlot = "armR_low";
    [SpineAttachment] public string armR_lowKey;

    public Top() { }
    public Top(Top _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.bodyKey = _new.bodyKey;
        this.bodyBKey = _new.bodyBKey;
        this.armL_highKey = _new.armL_highKey;
        this.armL_middleKey = _new.armL_middleKey;
        this.armL_lowKey = _new.armL_lowKey;
        this.armR_highKey = _new.armR_highKey;
        this.armR_middleKey = _new.armR_middleKey;
        this.armR_lowKey = _new.armR_lowKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Top _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

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
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindTopSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Top tmp = new Top();
        tmp.bodyKey = null;
        tmp.bodyBKey = null;
        tmp.armL_highKey = null;
        tmp.armL_middleKey = null;
        tmp.armL_lowKey = null;
        tmp.armR_highKey = null;
        tmp.armR_middleKey = null;
        tmp.armR_lowKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

[System.Serializable]
//**************하체**************//
//하의
public class Bottom : SkinParts  //waist, legL & R(high, middle, low)
{
    //상의
    [SpineSlot] string waistSlot = "waist"; // 슬롯의 이름
    [SpineAttachment] public string waistKey; // 어테치먼트의 이름
    //왼팔(상)
    [SpineSlot] string legL_highSlot = "legL_high";
    [SpineAttachment] public string legL_highKey;
    //왼팔(중)
    [SpineSlot] string legL_middleSlot = "legL_middle";
    [SpineAttachment] public string legL_middleKey;
    //왼팔(하)
    [SpineSlot] string legL_lowSlot = "legL_low";
    [SpineAttachment] public string legL_lowKey;
    //오른팔(상)
    [SpineSlot] string legR_highSlot = "legR_high";
    [SpineAttachment] public string legR_highKey;
    //오른팔(중)
    [SpineSlot] string legR_middleSlot = "legR_middle";
    [SpineAttachment] public string legR_middleKey;
    //오른팔(하)
    [SpineSlot] string legR_lowSlot = "legR_low";
    [SpineAttachment] public string legR_lowKey;

    public Bottom() { }
    public Bottom(Bottom _new)
    {
        base.name = _new.name;
        base.skincolor = _new.skincolor;
        base.sprite = _new.sprite;
        this.waistKey = _new.waistKey;
        this.legL_highKey = _new.legL_highKey;
        this.legL_middleKey = _new.legL_middleKey;
        this.legL_lowKey = _new.legL_lowKey;
        this.legR_highKey = _new.legR_highKey;
        this.legR_middleKey = _new.legR_middleKey;
        this.legR_lowKey = _new.legR_lowKey;
    }

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Bottom _change, bool _ischange)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
            return;
        }

        if (_ischange)
        {
            base.name = _change.name;
            base.sprite = _change.sprite;
        }

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
        ChangeSkin(_skeleton_ani, SkinManager.Instance.FindBottomSkin(base.name), false);
    }
    public void NullSkin(SkeletonAnimation _skeleton_ani)
    {
        Bottom tmp = new Bottom();
        tmp.waistKey = null;
        tmp.legL_highKey = null;
        tmp.legL_middleKey = null;
        tmp.legL_lowKey = null;
        tmp.legR_highKey = null;
        tmp.legR_middleKey = null;
        tmp.legR_lowKey = null;
        ChangeSkin(_skeleton_ani, tmp, false);
    }
}

[System.Serializable]
public class Skin
{
    [SpineSkin] public string baseSkinName; // 복사 할 스킨의 이름
    public Material baseMaterial; // 기본 머터리얼

    //스킨 목록.
    public Fronthair baseFronthair = new Fronthair();
    public Rearhair baseRearhair = new Rearhair();
    public Eyeblow baseEyeblow = new Eyeblow();
    public Eyelid baseEyelid = new Eyelid();
    public Eyeball baseEyeball = new Eyeball();
    public Eyewhite baseEyewhite = new Eyewhite();
    public Mouth baseMouth = new Mouth();
    public Cheek baseCheek = new Cheek();
    public Head baseHead = new Head();
    public Overcoat baseOvercoat = new Overcoat();
    public Top baseTop = new Top();
    public Bottom baseBottom = new Bottom();

    public override string ToString()
    {
        return "Fronthair:" + (baseFronthair.name == null ? "*" : baseFronthair.name) + ":" + PlayerInfoXML.ColorToHexString(baseFronthair.skincolor) + "/" +
                "Rearhair:" + (baseRearhair.name == null ? "*" : baseRearhair.name) + ":" + PlayerInfoXML.ColorToHexString(baseRearhair.skincolor) + "/" +
                "Eyeblow:" + (baseEyeblow.name == null ? "*" : baseEyeblow.name) + ":" + PlayerInfoXML.ColorToHexString(baseEyeblow.skincolor) + "/" +
                "Eyelid:" + (baseEyelid.name == null ? "*" : baseEyelid.name) + ":" + PlayerInfoXML.ColorToHexString(baseEyelid.skincolor) + "/" +
                "Eyeball:" + (baseEyeball.name == null ? "*" : baseEyeball.name) + ":" + PlayerInfoXML.ColorToHexString(baseEyeball.skincolor) + "/" +
                "Eyewhite:" + (baseEyewhite.name == null ? "*" : baseEyewhite.name) + ":" + PlayerInfoXML.ColorToHexString(baseEyewhite.skincolor) + "/" +
                "Mouth:" + (baseMouth.name == null ? "*" : baseMouth.name) + ":" + PlayerInfoXML.ColorToHexString(baseMouth.skincolor) + "/" +
                "Cheek:" + (baseCheek.name == null ? "*" : baseCheek.name) + ":" + PlayerInfoXML.ColorToHexString(baseCheek.skincolor) + "/" +
                "Head:" + (baseHead.name == null ? "*" : baseHead.name) + ":" + PlayerInfoXML.ColorToHexString(baseHead.skincolor) + "/" +
                "Overcoat:" + (baseOvercoat.name == null ? "*" : baseOvercoat.name) + ":" + PlayerInfoXML.ColorToHexString(baseOvercoat.skincolor) + "/" +
                "Top:" + (baseTop.name == null ? "*" : baseTop.name) + ":" + PlayerInfoXML.ColorToHexString(baseTop.skincolor) + "/" +
                "Bottom:" + (baseBottom.name == null ? "*" : baseBottom.name) + ":" + PlayerInfoXML.ColorToHexString(baseBottom.skincolor);
    }
    public void StringToSkin(string _skin)
    {
        string[] skin_tmp = _skin.Split('/');
        foreach (var item in skin_tmp)
        {
            string[] part_tmp = item.Split(':');
            switch (part_tmp[0])
            {
                case "Fronthair":
                    baseFronthair = SkinManager.Instance.FindFronthairSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseFronthair.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Rearhair":
                    baseRearhair = SkinManager.Instance.FindRearhairSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseRearhair.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyeblow":
                    baseEyeblow = SkinManager.Instance.FindEyeblowSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseEyeblow.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyelid":
                    baseEyelid = SkinManager.Instance.FindEyelidSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseEyelid.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyeball":
                    baseEyeball = SkinManager.Instance.FindEyeballSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseEyeball.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyewhite":
                    baseEyewhite = SkinManager.Instance.FindEyewhiteSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseEyewhite.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Mouth":
                    baseMouth = SkinManager.Instance.FindMouthSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseMouth.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Cheek":
                    baseCheek = SkinManager.Instance.FindCheekSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseCheek.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Head":
                    baseHead = SkinManager.Instance.FindHeadSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseHead.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Overcoat":
                    baseOvercoat = SkinManager.Instance.FindOvercoatSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseOvercoat.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Top":
                    baseTop = SkinManager.Instance.FindTopSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseTop.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Bottom":
                    baseBottom = SkinManager.Instance.FindBottomSkin(part_tmp[1] == "*" ? null : part_tmp[1]);
                    baseBottom.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
            }
        }
    }

    public void ClearClothes(SkeletonAnimation _skeleton)
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.REARHAIR, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.EYEBLOW, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.EYELID, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.EYEBALL, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.EYEWHITE, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.MOUTH, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.CHEEK, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.HEAD, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.TOP, "nullskin", _skeleton, false);
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin", _skeleton, false);
    }

    public void DefaultCustom(SkeletonAnimation _skeleton, bool _ischange)
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.REARHAIR, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.EYEBLOW, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.EYELID, "nullskin", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.EYEBALL, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.EYEWHITE, "nullskin", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.MOUTH, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.CHEEK, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.HEAD, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.TOP, "기본", _skeleton, _ischange);
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin", _skeleton, _ischange);
    }

    public void RefreshCustom(Skin _skin, SkeletonAnimation _skeleton)
    {
        _skin.baseFronthair.RefreshSkin(_skeleton);
        _skin.baseRearhair.RefreshSkin(_skeleton);
        _skin.baseEyeblow.RefreshSkin(_skeleton);
        _skin.baseEyelid.RefreshSkin(_skeleton);
        _skin.baseEyeball.RefreshSkin(_skeleton);
        _skin.baseEyewhite.RefreshSkin(_skeleton);
        _skin.baseMouth.RefreshSkin(_skeleton);
        _skin.baseCheek.RefreshSkin(_skeleton);
        _skin.baseHead.RefreshSkin(_skeleton);
        _skin.baseOvercoat.RefreshSkin(_skeleton);
        _skin.baseTop.RefreshSkin(_skeleton);
        _skin.baseBottom.RefreshSkin(_skeleton);
    }

    public void OnlyHead(Skin _skin, SkeletonAnimation _skeleton)
    {
        _skin.baseFronthair.RefreshSkin(_skeleton);
        _skin.baseRearhair.RefreshSkin(_skeleton);
        _skin.baseEyeblow.RefreshSkin(_skeleton);
        _skin.baseEyelid.RefreshSkin(_skeleton);
        _skin.baseEyeball.RefreshSkin(_skeleton);
        _skin.baseEyewhite.RefreshSkin(_skeleton);
        _skin.baseMouth.RefreshSkin(_skeleton);
        _skin.baseCheek.RefreshSkin(_skeleton);
        _skin.baseHead.RefreshSkin(_skeleton);

        _skin.baseOvercoat.NullSkin(_skeleton);
        _skin.baseTop.NullSkin(_skeleton);
        _skin.baseBottom.NullSkin(_skeleton);
    }

    public void ChangeParts(PARTSTYPE _type, string _clothes, SkeletonAnimation _skeleton, bool _ischange) //옷 변경.
    {
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                baseFronthair.ChangeSkin(_skeleton, SkinManager.Instance.FindFronthairSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.REARHAIR:
                baseRearhair.ChangeSkin(_skeleton, SkinManager.Instance.FindRearhairSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.EYEBLOW:
                baseEyeblow.ChangeSkin(_skeleton, SkinManager.Instance.FindEyeblowSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.EYELID:
                baseEyelid.ChangeSkin(_skeleton, SkinManager.Instance.FindEyelidSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.EYEBALL:
                baseEyeball.ChangeSkin(_skeleton, SkinManager.Instance.FindEyeballSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.EYEWHITE:
                baseEyewhite.ChangeSkin(_skeleton, SkinManager.Instance.FindEyewhiteSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.MOUTH:
                baseMouth.ChangeSkin(_skeleton, SkinManager.Instance.FindMouthSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.CHEEK:
                baseCheek.ChangeSkin(_skeleton, SkinManager.Instance.FindCheekSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.HEAD:
                baseHead.ChangeSkin(_skeleton, SkinManager.Instance.FindHeadSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.OVERCOAT:
                baseOvercoat.ChangeSkin(_skeleton, SkinManager.Instance.FindOvercoatSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.TOP:
                baseTop.ChangeSkin(_skeleton, SkinManager.Instance.FindTopSkin(_clothes), _ischange);
                break;
            case PARTSTYPE.BOTTOM:
                baseBottom.ChangeSkin(_skeleton, SkinManager.Instance.FindBottomSkin(_clothes), _ischange);
                break;
            default:
                DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
                break;
        }
    }
}
