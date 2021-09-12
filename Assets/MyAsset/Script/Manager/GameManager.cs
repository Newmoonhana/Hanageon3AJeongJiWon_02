using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst = null; //싱글톤.

    //매니저 스크립트.
    public CharacterManager characterM;
    public SkinManager skinM;
    public AnimationManager aniM;
    public DebugManager debugM;

    private void Awake()
    {
        if (inst != null)
            Destroy(this);
        inst = this;
    }
}
