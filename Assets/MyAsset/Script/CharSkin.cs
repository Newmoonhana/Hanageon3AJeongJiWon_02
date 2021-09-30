using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharSkin : MonoBehaviour
{
    public character charaSetting;

    private void Awake()
    {
        charaSetting.skin.ClearClothes();
        AnimationManager.Instance.ChangeCharaAni(charaSetting.skin, TRACKTYPE.BODY, "public/Idle", true);
    }
}
