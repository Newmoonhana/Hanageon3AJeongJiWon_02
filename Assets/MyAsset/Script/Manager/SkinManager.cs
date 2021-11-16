using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XMLSkinData
{
    [Header("머리 커스텀 파츠 목록")]
    public Fronthair[] Fronthair_Skin;  //앞머리 스킨 목록.
    public Rearhair[] Rearhair_Skin;  //뒷머리 스킨 목록.
    public Eyeblow[] Eyeblow_Skin;  //눈썹 스킨 목록.
    public Eyelid[] Eyelid_Skin;    //눈꺼풀 스킨 목록.
    public Eyeball[] Eyeball_Skin;  //눈동자 스킨 목록.
    public Eyewhite[] Eyewhite_Skin;    //눈 흰자 스킨 목록.
    public Mouth[] Mouth_Skin;  //입 스킨 목록.
    public Cheek[] Cheek_Skin;  //볼 스킨 목록.
    public Head[] Head_Skin;    //머리 스킨 목록.
    [Space(25)]

    [Header("옷 커스텀 파츠 목록")]
    public Overcoat[] Overcoat_Skin;  //코트 스킨 목록.
    public Top[] Top_Skin;  //상의 스킨 목록.
    public Hand[] Hand_Skin;  //손 스킨 목록.
    public Bottom[] Bottom_Skin;  //하의 스킨 목록.
    public Pants[] Pants_Skin;  //바지 스킨 목록.
    public Foot[] Foot_Skin;  //신발 스킨 목록.
}

public class SkinManager : SingletonPattern_IsA_Mono<SkinManager>    //얼굴과 옷 둘다 담당할 예정이지만 일단 옷 코드만.
{
    public XMLSkinData xml;
    [Space(50)]
    public CharSkin []character;
    public Material baseMaterial;

    [ContextMenu("SaveXML")]
    void SaveXML()
    {
        PlayerInfoXML.WriteSkinInfo(xml);
    }
    [ContextMenu("LoadXML")]
    void LoadXML()
    {
        xml = PlayerInfoXML.ReadSkinInfo();
    }

    public void Awake()
    {
        Instance.character = character;

        if(DontDestroyInst(this))
            LoadXML();
    }

    /// <summary>
    /// 씬 변경 후 skeletonAni 재설정
    /// </summary>
    /// <param name="_chara">_chara 값으로 SkinManager.character[_index].charaSetting 값을 변경</param>
    /// <param name="_index">SkinManager.character의 인덱스</param>
    public void RefreshSkeleAni(character _chara, int _index)
    {
        if (_chara == null)
        {
            return;
        }
        Instance.character[_index].charaSetting = _chara;
        Instance.character[_index].charaSetting.skin.RefreshCustom(Instance.character[_index].charaSetting.skin, Instance.character[_index].skeleton);
    }

    public Fronthair FindFronthairSkin(string _parts)
    {
        for (int i = 0; i < xml.Fronthair_Skin.Length; i++)
        {
            if (xml.Fronthair_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Fronthair(xml.Fronthair_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Rearhair FindRearhairSkin(string _parts)
    {
        for (int i = 0; i < xml.Rearhair_Skin.Length; i++)
        {
            if (xml.Rearhair_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Rearhair(xml.Rearhair_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyeblow FindEyeblowSkin(string _parts)
    {
        for (int i = 0; i < xml.Eyeblow_Skin.Length; i++)
        {
            if (xml.Eyeblow_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Eyeblow(xml.Eyeblow_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyelid FindEyelidSkin(string _parts)
    {
        for (int i = 0; i < xml.Eyelid_Skin.Length; i++)
        {
            if (xml.Eyelid_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Eyelid(xml.Eyelid_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyeball FindEyeballSkin(string _parts)
    {
        for (int i = 0; i < xml.Eyeball_Skin.Length; i++)
        {
            if (xml.Eyeball_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Eyeball(xml.Eyeball_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyewhite FindEyewhiteSkin(string _parts)
    {
        for (int i = 0; i < xml.Eyewhite_Skin.Length; i++)
        {
            if (xml.Eyewhite_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Eyewhite(xml.Eyewhite_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Mouth FindMouthSkin(string _parts)
    {
        for (int i = 0; i < xml.Mouth_Skin.Length; i++)
        {
            if (xml.Mouth_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Mouth(xml.Mouth_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Cheek FindCheekSkin(string _parts)
    {
        for (int i = 0; i < xml.Cheek_Skin.Length; i++)
        {
            if (xml.Cheek_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return new Cheek(xml.Cheek_Skin[i]);
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Head FindHeadSkin(string _parts)
    {
        for (int i = 0; i < xml.Head_Skin.Length; i++)
        {
            if (xml.Head_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Head(xml.Head_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Overcoat FindOvercoatSkin(string _parts)
    {
        for (int i = 0; i < xml.Overcoat_Skin.Length; i++)
        {
            if (xml.Overcoat_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Overcoat(xml.Overcoat_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Top FindTopSkin(string _parts)
    {
        for (int i = 0; i < xml.Top_Skin.Length; i++)
        {
            if (xml.Top_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Top(xml.Top_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Hand FindHandSkin(string _parts)
    {
        for (int i = 0; i < xml.Hand_Skin.Length; i++)
        {
            if (xml.Hand_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Hand(xml.Hand_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Bottom FindBottomSkin(string _parts)
    {
        for (int i = 0; i < xml.Bottom_Skin.Length; i++)
        {
            if (xml.Bottom_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Bottom(xml.Bottom_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Pants FindPantsSkin(string _parts)
    {
        for (int i = 0; i < xml.Pants_Skin.Length; i++)
        {
            if (xml.Pants_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Pants(xml.Pants_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Foot FindFootSkin(string _parts)
    {
        for (int i = 0; i < xml.Foot_Skin.Length; i++)
        {
            if (xml.Foot_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
                return new Foot(xml.Foot_Skin[i]);
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
}
