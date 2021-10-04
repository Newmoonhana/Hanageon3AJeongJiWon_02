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

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Fronthair _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string rearhairSlot = "rearhair";
    [SpineAttachment] public string rearhairKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Rearhair _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string eyeblowLSlot = "eyeblowL";
    [SpineAttachment] public string eyeblowLKey;
    [SpineSlot] string eyeblowRSlot = "eyeblowR";
    [SpineAttachment] public string eyeblowRKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyeblow _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string eyelidLUSlot = "eyelidLU";
    [SpineAttachment] public string eyelidLUKey;
    [SpineSlot] string eyelidLDSlot = "eyelidLD";
    [SpineAttachment] public string eyelidLDKey;
    [SpineSlot] string eyelidRUSlot = "eyelidRU";
    [SpineAttachment] public string eyelidRUKey;
    [SpineSlot] string eyelidRDSlot = "eyelidRD";
    [SpineAttachment] public string eyelidRDKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyelid _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string eyeballLSlot = "eyeballL";
    [SpineAttachment] public string eyeballLKey;
    [SpineSlot] string eyeballRSlot = "eyeballR";
    [SpineAttachment] public string eyeballRKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyeball _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string eyewhiteLSlot = "eyewhiteL";
    [SpineAttachment] public string eyewhiteLKey;
    [SpineSlot] string eyewhiteRSlot = "eyewhiteR";
    [SpineAttachment] public string eyewhiteRKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Eyewhite _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string mouthSlot = "mouth";
    [SpineAttachment] public string mouthKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Mouth _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string cheekSlot = "cheek";
    [SpineAttachment] public string cheekKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Cheek _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
    [SpineSlot] string headSlot = "head";
    [SpineAttachment] public string headKey;

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Head _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Overcoat _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Top _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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

    public void ChangeSkin(SkeletonAnimation _skeleton_ani, Bottom _change)
    {
        if (_change == null)
        {
            DebugManager.Instance.Log("해당 스킨은 존재하지 않습니다.", LogType.Warning);
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
        return "Fronthair:" + baseFronthair.name + ":" + PlayerInfoXML.ColorToHexString(baseFronthair.skincolor) + "/" +
                "Rearhair:" + baseRearhair.name + ":" + PlayerInfoXML.ColorToHexString(baseRearhair.skincolor) + "/" +
                "Eyeblow:" + baseEyeblow.name + ":" + PlayerInfoXML.ColorToHexString(baseEyeblow.skincolor) + "/" +
                "Eyelid:" + baseEyelid.name + ":" + PlayerInfoXML.ColorToHexString(baseEyelid.skincolor) + "/" +
                "Eyeball:" + baseEyeball.name + ":" + PlayerInfoXML.ColorToHexString(baseEyeball.skincolor) + "/" +
                "Eyewhite:" + baseEyewhite.name + ":" + PlayerInfoXML.ColorToHexString(baseEyewhite.skincolor) + "/" +
                "Mouth:" + baseMouth.name + ":" + PlayerInfoXML.ColorToHexString(baseMouth.skincolor) + "/" +
                "Cheek:" + baseCheek.name + ":" + PlayerInfoXML.ColorToHexString(baseCheek.skincolor) + "/" +
                "Head:" + baseHead.name + ":" + PlayerInfoXML.ColorToHexString(baseHead.skincolor) + "/" +
                "Overcoat:" + baseOvercoat.name + ":" + PlayerInfoXML.ColorToHexString(baseOvercoat.skincolor) + "/" +
                "Top:" + baseTop.name + ":" + PlayerInfoXML.ColorToHexString(baseTop.skincolor) + "/" +
                "Bottom:" + baseBottom.name + ":" + PlayerInfoXML.ColorToHexString(baseBottom.skincolor);
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
                    baseFronthair = SkinManager.Instance.FindFronthairSkin(part_tmp[1]);
                    baseFronthair.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Rearhair":
                    baseRearhair = SkinManager.Instance.FindRearhairSkin(part_tmp[1]);
                    baseRearhair.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyeblow":
                    baseEyeblow = SkinManager.Instance.FindEyeblowSkin(part_tmp[1]);
                    baseEyeblow.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyelid":
                    baseEyelid = SkinManager.Instance.FindEyelidSkin(part_tmp[1]);
                    baseEyelid.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyeball":
                    baseEyeball = SkinManager.Instance.FindEyeballSkin(part_tmp[1]);
                    baseEyeball.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Eyewhite":
                    baseEyewhite = SkinManager.Instance.FindEyewhiteSkin(part_tmp[1]);
                    baseEyewhite.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Mouth":
                    baseMouth = SkinManager.Instance.FindMouthSkin(part_tmp[1]);
                    baseMouth.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Cheek":
                    baseCheek = SkinManager.Instance.FindCheekSkin(part_tmp[1]);
                    baseCheek.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Head":
                    baseHead = SkinManager.Instance.FindHeadSkin(part_tmp[1]);
                    baseHead.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Overcoat":
                    baseOvercoat = SkinManager.Instance.FindOvercoatSkin(part_tmp[1]);
                    baseOvercoat.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Top":
                    baseTop = SkinManager.Instance.FindTopSkin(part_tmp[1]);
                    baseTop.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
                case "Bottom":
                    baseBottom = SkinManager.Instance.FindBottomSkin(part_tmp[1]);
                    baseBottom.skincolor = PlayerInfoXML.HexStringToColor(part_tmp[2]);
                    break;
            }
        }
    }

    public void ClearClothes(SkeletonAnimation _skeleton)
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.REARHAIR, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.EYEBLOW, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.EYELID, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.EYEBALL, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.EYEWHITE, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.MOUTH, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.CHEEK, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.HEAD, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.TOP, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin", _skeleton);
    }

    public void DefaultCustom(SkeletonAnimation _skeleton)
    {
        ChangeParts(PARTSTYPE.FRONTHAIR, "기본", _skeleton);
        ChangeParts(PARTSTYPE.REARHAIR, "기본", _skeleton);
        ChangeParts(PARTSTYPE.EYEBLOW, "기본", _skeleton);
        ChangeParts(PARTSTYPE.EYELID, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.EYEBALL, "기본", _skeleton);
        ChangeParts(PARTSTYPE.EYEWHITE, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.MOUTH, "기본", _skeleton);
        ChangeParts(PARTSTYPE.CHEEK, "기본", _skeleton);
        ChangeParts(PARTSTYPE.HEAD, "기본", _skeleton);
        ChangeParts(PARTSTYPE.OVERCOAT, "nullskin", _skeleton);
        ChangeParts(PARTSTYPE.TOP, "기본", _skeleton);
        ChangeParts(PARTSTYPE.BOTTOM, "nullskin", _skeleton);
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

    public void ChangeParts(PARTSTYPE _type, string _clothes, SkeletonAnimation _skeleton) //옷 변경.
    {
        switch (_type)
        {
            case PARTSTYPE.FRONTHAIR:
                baseFronthair.ChangeSkin(_skeleton, SkinManager.Instance.FindFronthairSkin(_clothes));
                break;
            case PARTSTYPE.REARHAIR:
                baseRearhair.ChangeSkin(_skeleton, SkinManager.Instance.FindRearhairSkin(_clothes));
                break;
            case PARTSTYPE.EYEBLOW:
                baseEyeblow.ChangeSkin(_skeleton, SkinManager.Instance.FindEyeblowSkin(_clothes));
                break;
            case PARTSTYPE.EYELID:
                baseEyelid.ChangeSkin(_skeleton, SkinManager.Instance.FindEyelidSkin(_clothes));
                break;
            case PARTSTYPE.EYEBALL:
                baseEyeball.ChangeSkin(_skeleton, SkinManager.Instance.FindEyeballSkin(_clothes));
                break;
            case PARTSTYPE.EYEWHITE:
                baseEyewhite.ChangeSkin(_skeleton, SkinManager.Instance.FindEyewhiteSkin(_clothes));
                break;
            case PARTSTYPE.MOUTH:
                baseMouth.ChangeSkin(_skeleton, SkinManager.Instance.FindMouthSkin(_clothes));
                break;
            case PARTSTYPE.CHEEK:
                baseCheek.ChangeSkin(_skeleton, SkinManager.Instance.FindCheekSkin(_clothes));
                break;
            case PARTSTYPE.HEAD:
                baseHead.ChangeSkin(_skeleton, SkinManager.Instance.FindHeadSkin(_clothes));
                break;
            case PARTSTYPE.OVERCOAT:
                baseOvercoat.ChangeSkin(_skeleton, SkinManager.Instance.FindOvercoatSkin(_clothes));
                break;
            case PARTSTYPE.TOP:
                baseTop.ChangeSkin(_skeleton, SkinManager.Instance.FindTopSkin(_clothes));
                break;
            case PARTSTYPE.BOTTOM:
                baseBottom.ChangeSkin(_skeleton, SkinManager.Instance.FindBottomSkin(_clothes));
                break;
            default:
                DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
                break;
        }
    }
}
