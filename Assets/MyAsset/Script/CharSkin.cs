using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharSkin : MonoBehaviour
{
    public SkeletonAnimation skeleton;
    public character charaSetting;

    private void Awake()
    {
        //AnimationManager.Instance.ChangeCharaAni(this, TRACKTYPE.BODY, "public/Idle", true);
    }
}
