using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class Experience_Setting
{
    [SerializeField]
    static int level_MAX = 99;
    static Dictionary<int, float> exp_MAX = new Dictionary<int, float>(); //level , exp_MAX

    public static void ClearExpMax()
    {
        exp_MAX = new Dictionary<int, float>();
    }

    public static int GetLevelMAX()
    {
        return level_MAX;
    }
    public static void SetLevelMax(int _levelMAX)
    {
        level_MAX = _levelMAX;
    }
    public static float GetExpMAX(int _level)
    {
        return exp_MAX[_level];
    }

    public static void AddExpMax(int _level, float _max)
    {
        exp_MAX.Add(_level, _max);
    }
}

[System.Serializable]
public class Experience
{
    [SerializeField]
    int level = 1;
    [SerializeField]
    float exp = 0f;

    public override string ToString()
    {
        return level.ToString() + "/" + exp.ToString();
    }
    public Experience StringToEXP(string _exp)
    {
        Experience tmp = new Experience();
        string[] exp_tmp = _exp.Split('/');
        tmp.level = int.Parse(exp_tmp[0]);
        tmp.exp = float.Parse(exp_tmp[1]);
        return tmp;
    }

    public int GetLevel()
    {
        return level;
    }
    public float GetEXP()
    {
        return exp;
    }

    /// <summary>
    /// 경험치 계산
    /// </summary>
    /// <param name="_calExp">추가 경험치(+-)</param>
    public void CalExp(float _calExp)
    {
        float sum = exp + _calExp;
        if (sum < 0)    //경험치 최소치 이하
        {
            exp = 0;
        }
        else if (sum >= Experience_Setting.GetExpMAX(level))    //경험치 최대치 이상
        {
            exp = sum - Experience_Setting.GetExpMAX(level);
            LevelUp();
        }
        else    //경험치 추가
        {
            exp = sum;
        }
    }

    void LevelUp()
    {
        level += 1;
    }
}