using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class PlayerInfoXML
{
    static string skinInfo = "SkinInfo.xml";
    static string characterInfo = "CharacterInfo.xml";
    static string levelInfo = "LevelInfo", levelInfo_add = "./Assets/Resources/XML/LevelInfo.xml";
    static string itemInfo = "ItemInfo.xml";
    static string languageInfo = "LanguageInfo.xml";

    //색 변형
    public static string ColorToHexString(Color color)
    {
        string r = ((int)(color.r * 255)).ToString("X2");
        string g = ((int)(color.g * 255)).ToString("X2");
        string b = ((int)(color.b * 255)).ToString("X2");
        string a = ((int)(color.a * 255)).ToString("X2");
        return string.Format("{0}{1}{2}{3}", r, g, b, a);
    }
    public static Color HexStringToColor(string str)  //Hex로 저장된 색상 값 되찾기.
    {
        if (str != null)
        {
            str = str.ToLowerInvariant();
            if (str.Length == 8)
            {
                char[] arr = str.ToCharArray();
                char[] color_arr = new char[8];
                for (int i = 0; i < 8; i++)
                {
                    if (arr[i] >= '0' && arr[i] <= '9')
                        color_arr[i] = (char)(arr[i] - '0');
                    else if (arr[i] >= 'a' && arr[i] <= 'f')
                        color_arr[i] = (char)(10 + arr[i] - 'a');
                    else color_arr[i] = (char)0;
                }
                float red = (color_arr[0] * 16 + color_arr[1]) / 255.0f;
                float green = (color_arr[2] * 16 + color_arr[3]) / 255.0f;
                float blue = (color_arr[4] * 16 + color_arr[5]) / 255.0f;
                float alpha = (color_arr[6] * 16 + color_arr[7]) / 255.0f;
                return new Color(red, green, blue, alpha);
            }
        }
        
        return Color.white;
    }
    /// <summary>
    /// xml 불러오기.
    /// </summary>
    /// <typeparam name="T">.xml 파일 클래스</typeparam>
    /// <param name="path">파일 이름</param>
    /// <returns></returns>
    public static T DeserializeXML<T>(T data, string path) where T : class
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        string path_str = String.Format("{0}/{1}", Application.persistentDataPath, path);

        if (!System.IO.File.Exists(path_str))
            SerializeXML(data, path);

        Stream stream = new FileStream(path_str, FileMode.Open);
        data = (T)serializer.Deserialize(stream);
        stream.Close();

        Debug.Log(path_str + " 불러오기 성공");
        return data;
    }
    /// <summary>
    /// xml 저장.
    /// </summary>
    /// <typeparam name="T">.xml 파일 클래스</typeparam>
    /// <param name="path">파일 이름</param>
    /// <returns></returns>
    public static void SerializeXML<T>(T data, string path) where T : class
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        string path_str = String.Format("{0}/{1}", Application.persistentDataPath, path);
        TextWriter writer = new StreamWriter(path_str);
        serializer.Serialize(writer, data);
        writer.Close();
        Debug.Log(path_str + " 저장 완료");
    }

    //CharacterInfo.xml 불러오기.
    public static void ReadCharacterInfo()
    {
        string path_str = String.Format("{0}/{1}", Application.persistentDataPath, characterInfo);
        if (!System.IO.File.Exists(path_str))
            WriteCharacterInfo();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path_str);

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfo/Character");
        CharacterManager.Instance.char_lst.Clear();
        CharacterManager.Instance.char_lst = new List<character>();
        foreach (XmlNode node in nodes)
        {
            foreach (XmlNode innode in node.ChildNodes)
            {
                character tmp = new character(innode.SelectSingleNode("Name").InnerText,
                    innode.SelectSingleNode("Nickname").InnerText,
                    innode.SelectSingleNode("Skin").InnerText,
                    innode.SelectSingleNode("Persona").InnerText,
                    innode.SelectSingleNode("Unit").InnerText,
                    innode.SelectSingleNode("EXP").InnerText);
                CharacterManager.Instance.AddCharacter(tmp);
            }
        }

        Debug.Log(path_str + " 불러오기 성공");
    }
    //CharacterInfo.xml 저장.
    public static void WriteCharacterInfo()
    {
        string path_str = String.Format("{0}/{1}", Application.persistentDataPath, characterInfo);
        XmlDocument Document = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = Document.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        Document.AppendChild(root);

        // 자식 노드 생성
        XmlNode character = Document.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(character);
        for (int i = 0; i < CharacterManager.Instance.char_lst.Count; i++)
        {
            string _name = CharacterManager.Instance.char_lst[i].name.Replace(" ", "_");
            XmlNode chara = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            character.AppendChild(chara);
            //키 값 저장
            XmlElement name = Document.CreateElement("Name");
            name.InnerText = CharacterManager.Instance.char_lst[i].name;
            chara.AppendChild(name);
            XmlElement nickname = Document.CreateElement("Nickname");
            nickname.InnerText = CharacterManager.Instance.char_lst[i].nickname;
            chara.AppendChild(nickname);
            XmlElement persona = Document.CreateElement("Persona");
            persona.InnerText = CharacterManager.Instance.char_lst[i].persona.ToString();
            chara.AppendChild(persona);
            XmlElement skin = Document.CreateElement("Skin");
            skin.InnerText = CharacterManager.Instance.char_lst[i].skin.ToString();
            chara.AppendChild(skin);
            XmlElement unit = Document.CreateElement("Unit");
            unit.InnerText = CharacterManager.Instance.char_lst[i].unit.ToString();
            chara.AppendChild(unit);
            XmlElement exp = Document.CreateElement("EXP");
            exp.InnerText = CharacterManager.Instance.char_lst[i].exp.ToString();
            chara.AppendChild(exp);
        }

        Document.Save(path_str);
        Debug.Log(path_str + " 저장 완료");
    }

    //SkinInfo.xml 불러오기.
    static public XMLSkinData ReadSkinInfo()
    {
        return DeserializeXML<XMLSkinData>(SkinManager.Instance.xml, skinInfo);
    }
    //SkinInfo.xml 저장.
    public static void WriteSkinInfo(XMLSkinData xml)
    {
        SerializeXML<XMLSkinData>(xml, skinInfo);
    }

    //LevelInfo는 에디터 상에서의 편집 기능은 있지만 빌드 후 편집 기능은 없으니 Resource 유지
    //LevelInfo.xml 불러오기.
    public static void ReadLevelInfo()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + levelInfo);
        if (textAsset == null)
            return;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        Experience_Setting.ClearExpMax();   //기존 경험치 MAX 값 초기화

        XmlNodeList nodes = xmlDoc.SelectNodes("LevelInfo/EXPInfo/Info");
        int lv_max = 0;
        foreach (XmlNode node in nodes)
        {
            int lv = int.Parse(node.SelectSingleNode("Level").InnerText);
            Experience_Setting.AddExpMax(lv, float.Parse(node.SelectSingleNode("exp_MAX").InnerText));
            //Debug.Log("lv == " + lv + " / exp_MAX = " + node.SelectSingleNode("exp_MAX").InnerText);
            lv_max = lv;
        }
        Experience_Setting.SetLevelMax(lv_max);

        Debug.Log(levelInfo_add + " 불러오기 성공");
    }
    //LevelInfo.xml 저장.
    public static void WriteLevelInfo()
    {
        XmlDocument Document = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = Document.CreateNode(XmlNodeType.Element, levelInfo, string.Empty);
        Document.AppendChild(root);

        // 자식 노드 생성
        XmlNode exp = Document.CreateNode(XmlNodeType.Element, "EXPInfo", string.Empty);
        root.AppendChild(exp);
        
        for (int i = 1; i <= Experience_Setting.GetLevelMAX(); i++)
        {
            XmlElement info = Document.CreateElement("Info");
            exp.AppendChild(info);
            //키 값 저장
            XmlElement lv = Document.CreateElement("Level");
            lv.InnerText = i.ToString();
            info.AppendChild(lv);
            XmlElement exp_MAX = Document.CreateElement("exp_MAX");
            exp_MAX.InnerText = (1000 + (i - 1) * 100).ToString();
            info.AppendChild(exp_MAX);
        }

        Document.Save(levelInfo_add);
        Debug.Log(levelInfo_add + " 저장 완료");
    }

    //ItemInfo.xml 불러오기.
    public static void ReadItemInfo()
    {
        string path_str = String.Format("{0}/{1}", Application.persistentDataPath, itemInfo);
        if (!System.IO.File.Exists(path_str))
            WriteItemInfo();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path_str);

        //음식
        ItemManager.Instance.food_lst.Clear();
        XmlNodeList nodes = xmlDoc.SelectNodes("ItemInfo/FoodInfo");
        foreach (XmlNode node in nodes)
        {
            foreach (XmlNode info in node.ChildNodes)
            {
                string name = info.SelectSingleNode("Name").InnerText;
                Sprite sprite = null;
                if (info.SelectSingleNode("Sprite").InnerText != "")
                    sprite = Resources.Load<Sprite>("Sprite/Food_Sprite/" + info.SelectSingleNode("Sprite").InnerText);
                int price = int.Parse(info.SelectSingleNode("Price").InnerText);
                float exp = float.Parse(info.SelectSingleNode("Exp").InnerText);
                string tag = info.SelectSingleNode("Tag").InnerText;
                int count = int.Parse(info.SelectSingleNode("Count").InnerText);
                ItemManager.Instance.food_lst.Add(new Item(name, sprite, price, exp, tag, count));
            }
        }

        Debug.Log(path_str + " 불러오기 성공");
    }
    //ItemInfo.xml 저장.
    public static void WriteItemInfo()
    {
        string path_str = String.Format("{0}/{1}", Application.persistentDataPath, itemInfo);
        XmlDocument Document = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = Document.CreateNode(XmlNodeType.Element, "ItemInfo", string.Empty);
        Document.AppendChild(root);

        // 자식 노드 생성(음식 아이템)
        XmlNode food = Document.CreateNode(XmlNodeType.Element, "FoodInfo", string.Empty);
        root.AppendChild(food);
        int size = ItemManager.Instance.food_lst.Count;
        if (size != 0)
            for (int i = 0; i < size; i++)
            {
                XmlElement info = Document.CreateElement("Info");
                food.AppendChild(info);
                //키 값 저장
                Item tmp = ItemManager.Instance.GetItem(ITEMTYPE.FOOD, i);
                XmlElement name = Document.CreateElement("Name");
                name.InnerText = tmp.GetName();
                info.AppendChild(name);
                //스프라이트
                XmlElement sprite = Document.CreateElement("Sprite");
                Sprite tmp_spr = ItemManager.Instance.food_lst[i].GetSprite();
                if (tmp_spr != null)
                    sprite.InnerText = tmp_spr.name;
                else
                    sprite.InnerText = "";
                info.AppendChild(sprite);
                XmlElement price = Document.CreateElement("Price");
                price.InnerText = tmp.GetPrice().ToString();
                info.AppendChild(price);
                XmlElement exp = Document.CreateElement("Exp");
                exp.InnerText = tmp.GetExp().ToString();
                info.AppendChild(exp);
                XmlElement tag = Document.CreateElement("Tag");
                tag.InnerText = tmp.GetTag();
                info.AppendChild(tag);
                XmlElement count = Document.CreateElement("Count");
                count.InnerText = tmp.count.ToString();
                info.AppendChild(count);
            }

        Document.Save(path_str);
        Debug.Log(path_str + " 저장 완료");
    }

    //LanguageInfo.xml 불러오기.
    public static XMLLanguageData ReadLanguageInfo()
    {
        return DeserializeXML<XMLLanguageData>(LanguageManager.Instance.xml, languageInfo);
    }
    //LanguageInfo.xml 저장.
    public static void WriteLanguageInfo(XMLLanguageData xml)
    {
        SerializeXML<XMLLanguageData>(xml, languageInfo);
    }
}
