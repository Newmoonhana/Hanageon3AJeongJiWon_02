using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharSkin : MonoBehaviour
{
    public Skin baseskin;
    public character charaSetting;

    private void Awake()
    {
        baseskin.ClearClothes();
        GameManager.inst.aniM.ChangeCharaAni(baseskin, TRACKTYPE.BODY, "public/Idle", true);
    }
}
