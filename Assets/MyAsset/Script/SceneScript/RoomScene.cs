﻿using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        INVENTORY
    }
    STATE state;

    private void Start()
    {
        SkinManager.Instance.RefreshSkeleAni(CharacterManager.Instance.FindCharacter(ApartManager.Instance.thisUnit), 0);
    }

    public void InputBackButton()
    {
        if (state != STATE.NONE)
        {
            return;
        }
        GameManager.Instance.LoadScene("ApartScene");
    }
}
