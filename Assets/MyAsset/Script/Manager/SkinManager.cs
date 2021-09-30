using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : SingletonPattern_IsA_Mono<SkinManager>    //얼굴과 옷 둘다 담당할 예정이지만 일단 옷 코드만.
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
    public Bottom[] Bottom_Skin;  //하의 스킨 목록.
    [Space(50)]

    public CharSkin []character;

    public void Awake()
    {
        Instance.character = character;

        DontDestroyInst(this);
    }

    //skeletonAni 재설정
    public void RefreshSkeleAni(character _chara, int _index)
    {
        if (_chara == null)
        {
            return;
        }
        SkeletonAnimation sketmp = Instance.character[_index].charaSetting.skin.skeleton_ani;
        Instance.character[_index].charaSetting = _chara;
        Instance.character[_index].charaSetting.skin.skeleton_ani = sketmp;
        Instance.character[_index].charaSetting.skin.RefreshCustom(Instance.character[_index].charaSetting.skin);
    }

    //스킨 찾기 함수.
    public Fronthair FindFronthairSkin(string _parts)
    {
        for (int i = 0; i < Fronthair_Skin.Length; i++)
        {
            if (Fronthair_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Fronthair_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Rearhair FindRearhairSkin(string _parts)
    {
        for (int i = 0; i < Rearhair_Skin.Length; i++)
        {
            if (Rearhair_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Rearhair_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyeblow FindEyeblowSkin(string _parts)
    {
        for (int i = 0; i < Eyeblow_Skin.Length; i++)
        {
            if (Eyeblow_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Eyeblow_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyelid FindEyelidSkin(string _parts)
    {
        for (int i = 0; i < Eyelid_Skin.Length; i++)
        {
            if (Eyelid_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Eyelid_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyeball FindEyeballSkin(string _parts)
    {
        for (int i = 0; i < Eyeball_Skin.Length; i++)
        {
            if (Eyeball_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Eyeball_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Eyewhite FindEyewhiteSkin(string _parts)
    {
        for (int i = 0; i < Eyewhite_Skin.Length; i++)
        {
            if (Eyewhite_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Eyewhite_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Mouth FindMouthSkin(string _parts)
    {
        for (int i = 0; i <Mouth_Skin.Length; i++)
        {
            if (Mouth_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Mouth_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Cheek FindCheekSkin(string _parts)
    {
        for (int i = 0; i < Cheek_Skin.Length; i++)
        {
            if (Cheek_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Cheek_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Head FindHeadSkin(string _parts)
    {
        for (int i = 0; i < Head_Skin.Length; i++)
        {
            if (Head_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Head_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Overcoat FindOvercoatSkin(string _parts)
    {
        for (int i = 0; i < Overcoat_Skin.Length; i++)
        {
            if (Overcoat_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Overcoat_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Top FindTopSkin(string _parts)
    {
        for (int i = 0; i < Top_Skin.Length; i++)
        {
            if (Top_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Top_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
    public Bottom FindBottomSkin(string _parts)
    {
        for (int i = 0; i < Top_Skin.Length; i++)
        {
            if (Bottom_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Bottom_Skin[i];
            }
        }
        DebugManager.Instance.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
}
