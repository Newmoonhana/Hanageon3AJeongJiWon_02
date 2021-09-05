using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharSkin : MonoBehaviour
{
    public Skin baseskin;

    private void Start()
    {
        //테스트용.
        baseskin.ChangeClothes(CLOTHESTYPE.TOP, GameManager.inst.skinM.Top_Skin[1].name);
        baseskin.ChangeClothes(CLOTHESTYPE.OVERCOAT, GameManager.inst.skinM.Overcoat_Skin[1].name);
    }
}
