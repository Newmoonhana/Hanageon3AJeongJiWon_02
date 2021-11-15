using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomScene : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        INVENTORY,
        GIFTING
    }
    STATE state = STATE.NONE;
    enum INVENTORY_STATE
    {
        FOOD,
        CLOTHES,

        MAX
    }
    INVENTORY_STATE inven_state = INVENTORY_STATE.FOOD;

    public Slider levelbar;
    public Text levelbar_txt;
    character roomchara;

    public GameObject DefaultUI_obj, InventoryUI_obj;   //state obj
    public Transform Inven_ItemLst_parent;
    public Image item_img;
    ROOMSCENE_component inven_selectItem = null;
    public class ItemList
    {
        List<GameObject> ItemList_obj = new List<GameObject>();
        List<Text> ItemListName_txt = new List<Text>();
        List<Text> ItemListCount_txt = new List<Text>();
        List<ROOMSCENE_component> ItemList_comp = new List<ROOMSCENE_component>();

        public void Clear()
        {
            ItemListName_txt.Clear();
            ItemListCount_txt.Clear();
            ItemList_comp.Clear();
        }
        public void Add(GameObject tmp)
        {
            ItemList_obj.Add(tmp);
            ItemListName_txt.Add(tmp.transform.GetChild(0).GetComponent<Text>());
            ItemListCount_txt.Add(tmp.transform.GetChild(1).GetComponent<Text>());
            ItemList_comp.Add(tmp.GetComponent<ROOMSCENE_component>());
        }
        public int Count() { return ItemList_obj.Count; }
        public void SetActive(bool enable, int index) { ItemList_obj[index].SetActive(enable); }
        public Text GetNameText(int index) { return ItemListName_txt[index]; }
        public Text GetCountText(int index) { return ItemListCount_txt[index]; }
        public ROOMSCENE_component GetComponent(int index) { return ItemList_comp[index]; }
    }
    ItemList item_lst = new ItemList();

    private void Start()
    {
        GameManager.Instance.Setting_Frame(false);
        character tmp_char = CharacterManager.Instance.FindCharacter(ApartManager.Instance.thisUnit);
        if (tmp_char == null)
            roomchara = new character();
        else
            roomchara = tmp_char;
        UIManager.Instance.SetActiveOKbutton(false);
        UIManager.Instance.SetActiveBackbutton<byte>(true, delegate { InputBackButton(); });
        SkinManager.Instance.RefreshSkeleAni(roomchara, 0);
        SkinManager.Instance.character[0].charaSetting.skin.RefreshCustom(SkinManager.Instance.character[0].charaSetting.skin, SkinManager.Instance.character[0].skeleton);
        AnimationManager.Instance.ChangeCharaAni(SkinManager.Instance.character[0], TRACKTYPE.BODY, "public/Idle", true, roomchara.persona.GetEIToAniSpeed());
        RefreshLevelVar();
        //최대 아이템 개수만큼 ItemButton 미리 생성.
        int tmp = 0; GameObject tmp_button = Inven_ItemLst_parent.GetChild(0).gameObject;
        if (ItemManager.Instance.food_lst.Count > tmp)
            tmp = ItemManager.Instance.food_lst.Count;
        item_lst.Clear();
        item_lst.Add(tmp_button);
        GameObject temp_obj;
        for (int i = 1; i < tmp; i++)
        {
            temp_obj = Instantiate(tmp_button, Inven_ItemLst_parent);
            item_lst.Add(temp_obj);
        }
        RefreshItemList();
    }

    public void InputBackButton()
    {
        if (state == STATE.NONE)
        {
            GameManager.Instance.LoadScene("ApartScene");
        }
        else if (state == STATE.INVENTORY)
        {
            DefaultUI_obj.SetActive(true);
            InventoryUI_obj.SetActive(false);
            state = STATE.NONE;
        }
    }

    public void InputGiftButton()
    {
        if (state != STATE.NONE)
            return;

        DefaultUI_obj.SetActive(false);
        RefreshItemList();
        InventoryUI_obj.SetActive(true);
        state = STATE.INVENTORY;
    }

    void RefreshLevelVar()
    {
        int lv = roomchara.exp.GetLevel();
        float exp = roomchara.exp.GetEXP();
        float exp_MAX = Experience_Setting.GetExpMAX(lv);
        levelbar.maxValue = exp_MAX;
        levelbar.value = exp;
        levelbar_txt.text = "Lv " + lv + " [ " + exp + " / " + exp_MAX + " ]";
    }

    void Input_SelectItemButton(ROOMSCENE_component _item)
    {
        if (_item == null)
            return;
        inven_selectItem = _item;   //아이템 정보 값 선물하기 버튼에 저장
        item_img.sprite = _item.item_spr;   //미리보기 이미지 표시
        for (int i = 0; i < item_lst.Count(); i++)  //기타 outline 끄기
            item_lst.GetComponent(i).this_outline.enabled = false;
        _item.this_outline.enabled = true;  //선택한 아이템만 outline 실행
    }

    public void Input_GiftSelectButton(ROOMSCENE_component _item)
    {
        if (state != STATE.INVENTORY)
            return;
        state = STATE.GIFTING;
        InventoryUI_obj.SetActive(false);
        
        AnimationManager.Instance.ChangeCharaAni(SkinManager.Instance.character[0], TRACKTYPE.BODY, "public/eat", false, roomchara.persona.GetEIToAniSpeed());
    }

    void RefreshItemList()
    {
        List<Item> tmp_lst;
        int size = 0;
        switch (inven_state)
        {
            case INVENTORY_STATE.FOOD:
                size = ItemManager.Instance.food_lst.Count;
                tmp_lst = ItemManager.Instance.food_lst;
                break;
            default:
                Debug.Log("존재하지 않는 분류입니다.");
                return;
        }
        for (int i = 0; i < item_lst.Count(); i++)
        {
            int idx = i;    //람다식 안에(델리게이트도 포함으로 보임) for문 i가 그대로 들어가면 클로저때문에 i값이 고정되므로 이렇게 써서 해결.
            ROOMSCENE_component tmp_comp = item_lst.GetComponent(idx);
            Button tmp_btn = tmp_comp.this_btn;
            tmp_btn.onClick.RemoveAllListeners();
            tmp_btn.onClick.AddListener(delegate { Input_SelectItemButton(tmp_comp); });
            if (idx < size)
            {
                item_lst.SetActive(true, idx);

                tmp_comp.item_name = tmp_lst[idx].GetName();
                tmp_comp.item_count = tmp_lst[idx].count;
                tmp_comp.item_spr = tmp_lst[idx].GetSprite();

                item_lst.GetNameText(idx).text = tmp_comp.item_name;
                string count = string.Format("{0}개", tmp_comp.item_count.ToString());
                item_lst.GetCountText(idx).text = count;
                item_img.sprite = item_lst.GetComponent(idx).item_spr;
                item_lst.GetComponent(i).this_outline.enabled = false;
            }
            else
                item_lst.SetActive(false, idx);
        }
    }
}
