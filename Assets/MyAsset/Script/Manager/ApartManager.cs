using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSetting
{
    public int unit;
    public string chara_name;

    public UnitSetting(int _unit, string _chara_name)
    {
        unit = _unit;
        chara_name = _chara_name;
    }
}

public class ApartManager : SingletonPattern_IsA_Mono<ApartManager>
{
    public int floor_max = 9;
    public int unit_max = 10;
    [SerializeField]
    List<UnitSetting> room_chara = new List<UnitSetting>();
    public int thisUnit;    //현재 방.

    public void Awake()
    {
        bool isInst = DontDestroyInst(this);
        for (int i = 1; i <= floor_max; i++)
            for (int j = 1; j <= unit_max; j++)
            {
                if (isInst)
                    Instance.room_chara.Add(new UnitSetting(i * 100 + j, null));
            }
    }

    //호 수 정보 수정(호 수 기준)
    public void EditUnitSetting(int _unit, string _charaName)
    {
        for (int i = 0; i < Instance.room_chara.Count; i++)
        {
            if (Instance.room_chara[i].unit == _unit)
            {
                Instance.room_chara[i].chara_name = _charaName;
                break;
            }
        }
    }

    //빈 방 찾기.
    public int FirstNullRoom()
    {
        int nullroom = -1;

        foreach (var item in Instance.room_chara)
        {
            if (item.chara_name == null)
            {
                nullroom = item.unit;
                break;
            }
        }

        return nullroom;
    }

    //unit로 찾기.
    public UnitSetting FindUnitSetting(int _unit)
    {
        foreach (var item in Instance.room_chara)
        {
            if (item.unit == _unit)
            {
                return item;
            }
        }

        return null;
    }
}
