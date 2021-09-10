using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour    //얼굴과 옷 둘다 담당할 예정이지만 일단 옷 코드만.
{
    [Header("머리 커스텀 파츠 목록")]
    public Fronthair[] Fronthair_Skin;  //앞머리 스킨 목록.
    public Rearhair[] Rearhair_Skin;  //앞머리 스킨 목록.
    [Space(25)]

    [Header("옷 커스텀 파츠 목록")]
    public Overcoat[] Overcoat_Skin;  //코트 스킨 목록.
    public Top[] Top_Skin;  //상의 스킨 목록.
    public Bottom[] Bottom_Skin;  //하의 스킨 목록.
    [Space(50)]

    public CharSkin []character;

    private void Awake()
    {
        
    }

    public Fronthair FindFronthairSkin(string _parts)
    {
        for (int i = 0; i < Fronthair_Skin.Length; i++)
        {
            if (Fronthair_Skin[i].name == _parts)   //이름이 같은 파츠 검색 성공.
            {
                return Fronthair_Skin[i];
            }
        }
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
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
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
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
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
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
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
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
        GameManager.inst.debugM.Log("해당 파츠는 존재하지 않습니다.", LogType.Warning);
        return null;
    }
}
