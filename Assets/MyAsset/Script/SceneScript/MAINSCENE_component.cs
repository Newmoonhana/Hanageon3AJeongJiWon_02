using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MAINSCENEBUTTONTYPE
{
    APART,  //아파트
    CCENTER,    //주민센터
    SHOP_SKIN,  //상점 - 옷

    _MAX
}

public class MAINSCENE_component : MonoBehaviour
{
    public MAINSCENEBUTTONTYPE btype;
    public string scene_name;
    public Sprite zoom_spr;
}
