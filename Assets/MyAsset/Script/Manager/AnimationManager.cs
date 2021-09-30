using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TRACKTYPE
{
    BODY,
    FACE,

    MAX
}

public class AnimationManager : SingletonPattern_IsA_Mono<AnimationManager>
{
    public void Awake()
    {
        DontDestroyInst(this);
    }

    public void ChangeCharaAni(Skin _chara, TRACKTYPE _type, string _ani, bool _isloop)
    {
        _chara.skeleton_ani.AnimationState.ClearTrack((int)_type);
        _chara.RefreshCustom(_chara);
        if (_ani != null)
        {
            _chara.skeleton_ani.AnimationState.SetAnimation((int)_type, _ani, _isloop);
            DebugManager.Instance.Log(_ani + " 애니메이션 실행", LogType.Log);
        }
        else
        {
            _chara.skeleton_ani.AnimationState.SetEmptyAnimation((int)_type, 0);
            DebugManager.Instance.Log(_type.ToString() + "타입 애니메이션 해제", LogType.Log);
        }
    }
}
