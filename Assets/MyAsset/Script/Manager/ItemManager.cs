using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ITEMTYPE
{
    FOOD,

    _MAX
}

[System.Serializable]
public class Item
{
    [SerializeField]
    string name;    //아이템 이름
    public string GetName() { return name; }
    [SerializeField]
    Sprite sprite;
    public Sprite GetSprite() { return sprite; }
    [SerializeField]
    int price;    //가격
    public int GetPrice() { return price; }
    [SerializeField]
    float exp;  //선물 시 얻는 기본 경험치
    public float GetExp() { return exp; }
    [SerializeField]
    string[] tag;   //아이템 분류(키워드)
    public string GetTag()
    {
        string tmp = "";
        if (tag.Length == 0)
            return null;
        for (int i = 0; i < tag.Length; i++)
        {
            tmp += tag[i];
            if (i != tag.Length - 1)
                tmp += "/";
        }

        return tmp;
    }
    public int count = 0;   //아이템 개수

    public Item() { }
    public Item(string _name, Sprite _sprite, int _price, float _exp, string _tag, int _count)
    {
        name = _name;
        sprite = _sprite;
        price = _price;
        exp = _exp;
        tag = _tag.Split('/');
        count = _count;
    }
}

public class ItemManager : SingletonPattern_IsA_Mono<ItemManager>
{
    public List<Item> food_lst = new List<Item>();

    void SortList(ref List<Item> _tmp_lst)  //이름 순 정렬.
    {
        List<Item> SortedList = _tmp_lst.OrderBy(x => x.GetName()).ToList();
        _tmp_lst = SortedList;
    }
    [ContextMenu("Sort_FoodList")]
    void SortFoodList()
    {
        SortList(ref Instance.food_lst);
    }
    [ContextMenu("SaveXML")]
    public void SaveXML()
    {
        SortFoodList();
        PlayerInfoXML.WriteItemInfo();
    }
    [ContextMenu("LoadXML")]
    void LoadXML()
    {
        PlayerInfoXML.ReadItemInfo();
    }

    public Item GetItem(ITEMTYPE _type, int _index)
    {
        switch (_type)
        {
            case ITEMTYPE.FOOD:
                return Instance.food_lst[_index];
            default:
                return null;
        }
    }

    public void Awake()
    {
        if (DontDestroyInst(this))
        {
            LoadXML();
        }
    }
}
