using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharSkin : MonoBehaviour
{
    public Skin baseskin;

    private void Awake()
    {
        baseskin.ClearClothes();
    }
}
