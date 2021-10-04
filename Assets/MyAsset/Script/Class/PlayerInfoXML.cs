using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerInfoXML
{
    static string skinInfo = "SkinInfo", skinInfo_add = "./Assets/Resources/XML/SkinInfo.xml";
    static string characterInfo = "CharacterInfo", characterInfo_add = "./Assets/Resources/XML/CharacterInfo.xml";

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
        return Color.red;
    }

    //CharacterInfo.xml 저장.
    public static void ReadCharacterInfo()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + characterInfo);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

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
                    innode.SelectSingleNode("Unit").InnerText);
                CharacterManager.Instance.AddCharacter(tmp);
            }
        }

        Debug.Log(characterInfo_add + " 불러오기 성공");
    }
    public static void WriteCharacterInfo()
    {
        XmlDocument Document = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = Document.CreateNode(XmlNodeType.Element, characterInfo, string.Empty);
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
        }

        Document.Save(characterInfo_add);
        Debug.Log(characterInfo_add + " 저장 완료");
    }

    //SkinInfo.xml 불러오기.
    static void ReadDefalutParts(SkinParts _skin, XmlNode innode)
    {
        _skin.name = innode.SelectSingleNode("Name").InnerText;
        _skin.skincolor = HexStringToColor(innode.SelectSingleNode("Color").InnerText);
        if (innode.SelectSingleNode("Sprite").InnerText != "")
            _skin.sprite = Resources.Load<Sprite>("Icon/CharSpine_Icon/" + innode.SelectSingleNode("Sprite").InnerText);
    }
    static public void ReadSkinInfo()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + skinInfo);
        Debug.Log("xml: " + textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        //앞머리
        XmlNodeList nodes = xmlDoc.SelectNodes("SkinInfo/Fronthair");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Fronthair_Skin, 0, SkinManager.Instance.Fronthair_Skin.Length);
            SkinManager.Instance.Fronthair_Skin = new Fronthair[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Fronthair_Skin[i] = new Fronthair();
                ReadDefalutParts(SkinManager.Instance.Fronthair_Skin[i], innode);
                SkinManager.Instance.Fronthair_Skin[i].antennahairKey = innode.SelectSingleNode("AntennahairKey").InnerText;
                SkinManager.Instance.Fronthair_Skin[i].fronthairKey = innode.SelectSingleNode("FronthairKey").InnerText;
                i++;
            }
        }
        //뒷머리
        nodes = xmlDoc.SelectNodes("SkinInfo/Rearhair");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Rearhair_Skin, 0, SkinManager.Instance.Rearhair_Skin.Length);
            SkinManager.Instance.Rearhair_Skin = new Rearhair[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Rearhair_Skin[i] = new Rearhair();
                ReadDefalutParts(SkinManager.Instance.Rearhair_Skin[i], innode);
                SkinManager.Instance.Rearhair_Skin[i].rearhairKey = innode.SelectSingleNode("RearhairKey").InnerText;
                i++;
            }
        }
        //눈썹
        nodes = xmlDoc.SelectNodes("SkinInfo/Eyeblow");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Eyeblow_Skin, 0, SkinManager.Instance.Eyeblow_Skin.Length);
            SkinManager.Instance.Eyeblow_Skin = new Eyeblow[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Eyeblow_Skin[i] = new Eyeblow();
                ReadDefalutParts(SkinManager.Instance.Eyeblow_Skin[i], innode);
                SkinManager.Instance.Eyeblow_Skin[i].eyeblowLKey = innode.SelectSingleNode("EyeblowLKey").InnerText;
                SkinManager.Instance.Eyeblow_Skin[i].eyeblowRKey = innode.SelectSingleNode("EyeblowRKey").InnerText;
                i++;
            }
        }
        //속눈썹
        nodes = xmlDoc.SelectNodes("SkinInfo/Eyelid");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Eyelid_Skin, 0, SkinManager.Instance.Eyelid_Skin.Length);
            SkinManager.Instance.Eyelid_Skin = new Eyelid[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Eyelid_Skin[i] = new Eyelid();
                ReadDefalutParts(SkinManager.Instance.Eyelid_Skin[i], innode);
                SkinManager.Instance.Eyelid_Skin[i].eyelidLUKey = innode.SelectSingleNode("EyelidLUKey").InnerText;
                SkinManager.Instance.Eyelid_Skin[i].eyelidLDKey = innode.SelectSingleNode("EyelidLDKey").InnerText;
                SkinManager.Instance.Eyelid_Skin[i].eyelidRUKey = innode.SelectSingleNode("EyelidRUKey").InnerText;
                SkinManager.Instance.Eyelid_Skin[i].eyelidRDKey = innode.SelectSingleNode("EyelidRDKey").InnerText;
                i++;
            }
        }
        //눈동자
        nodes = xmlDoc.SelectNodes("SkinInfo/Eyeball");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Eyeball_Skin, 0, SkinManager.Instance.Eyeball_Skin.Length);
            SkinManager.Instance.Eyeball_Skin = new Eyeball[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Eyeball_Skin[i] = new Eyeball();
                ReadDefalutParts(SkinManager.Instance.Eyeball_Skin[i], innode);
                SkinManager.Instance.Eyeball_Skin[i].eyeballLKey = innode.SelectSingleNode("EyeballLKey").InnerText;
                SkinManager.Instance.Eyeball_Skin[i].eyeballRKey = innode.SelectSingleNode("EyeballRKey").InnerText;
                i++;
            }
        }
        //눈 흰자
        nodes = xmlDoc.SelectNodes("SkinInfo/Eyewhite");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Eyewhite_Skin, 0, SkinManager.Instance.Eyewhite_Skin.Length);
            SkinManager.Instance.Eyewhite_Skin = new Eyewhite[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Eyewhite_Skin[i] = new Eyewhite();
                ReadDefalutParts(SkinManager.Instance.Eyewhite_Skin[i], innode);
                SkinManager.Instance.Eyewhite_Skin[i].eyewhiteLKey = innode.SelectSingleNode("EyewhiteLKey").InnerText;
                SkinManager.Instance.Eyewhite_Skin[i].eyewhiteRKey = innode.SelectSingleNode("EyewhiteRKey").InnerText;
                i++;
            }
        }
        //입
        nodes = xmlDoc.SelectNodes("SkinInfo/Mouth");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Mouth_Skin, 0, SkinManager.Instance.Mouth_Skin.Length);
            SkinManager.Instance.Mouth_Skin = new Mouth[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Mouth_Skin[i] = new Mouth();
                ReadDefalutParts(SkinManager.Instance.Mouth_Skin[i], innode);
                SkinManager.Instance.Mouth_Skin[i].mouthKey = innode.SelectSingleNode("MouthKey").InnerText;
                i++;
            }
        }
        //볼
        nodes = xmlDoc.SelectNodes("SkinInfo/Cheek");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Cheek_Skin, 0, SkinManager.Instance.Cheek_Skin.Length);
            SkinManager.Instance.Cheek_Skin = new Cheek[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Cheek_Skin[i] = new Cheek();
                ReadDefalutParts(SkinManager.Instance.Cheek_Skin[i], innode);
                SkinManager.Instance.Cheek_Skin[i].cheekKey = innode.SelectSingleNode("CheekKey").InnerText;
                i++;
            }
        }
        //두상
        nodes = xmlDoc.SelectNodes("SkinInfo/Head");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Head_Skin, 0, SkinManager.Instance.Head_Skin.Length);
            SkinManager.Instance.Head_Skin = new Head[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Head_Skin[i] = new Head();
                ReadDefalutParts(SkinManager.Instance.Head_Skin[i], innode);
                SkinManager.Instance.Head_Skin[i].headKey = innode.SelectSingleNode("HeadKey").InnerText;
                i++;
            }
        }
        //코트
        nodes = xmlDoc.SelectNodes("SkinInfo/Overcoat");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Overcoat_Skin, 0, SkinManager.Instance.Overcoat_Skin.Length);
            SkinManager.Instance.Overcoat_Skin = new Overcoat[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Overcoat_Skin[i] = new Overcoat();
                ReadDefalutParts(SkinManager.Instance.Overcoat_Skin[i], innode);
                SkinManager.Instance.Overcoat_Skin[i].overcoatKey = innode.SelectSingleNode("OvercoatKey").InnerText;
                SkinManager.Instance.Overcoat_Skin[i].overcoatBKey = innode.SelectSingleNode("OvercoatBKey").InnerText;
                SkinManager.Instance.Overcoat_Skin[i].overcoatLKey = innode.SelectSingleNode("OvercoatLKey").InnerText;
                SkinManager.Instance.Overcoat_Skin[i].overcoatRKey = innode.SelectSingleNode("OvercoatRKey").InnerText;
                i++;
            }
        }
        //상의
        nodes = xmlDoc.SelectNodes("SkinInfo/Top");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Top_Skin, 0, SkinManager.Instance.Top_Skin.Length);
            SkinManager.Instance.Top_Skin = new Top[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Top_Skin[i] = new Top();
                ReadDefalutParts(SkinManager.Instance.Top_Skin[i], innode);
                SkinManager.Instance.Top_Skin[i].bodyKey = innode.SelectSingleNode("BodyKey").InnerText;
                SkinManager.Instance.Top_Skin[i].bodyBKey = innode.SelectSingleNode("BodyBKey").InnerText;
                SkinManager.Instance.Top_Skin[i].armL_highKey = innode.SelectSingleNode("ArmL_highKey").InnerText;
                SkinManager.Instance.Top_Skin[i].armL_middleKey = innode.SelectSingleNode("ArmL_middleKey").InnerText;
                SkinManager.Instance.Top_Skin[i].armL_lowKey = innode.SelectSingleNode("ArmL_lowKey").InnerText;
                SkinManager.Instance.Top_Skin[i].armR_highKey = innode.SelectSingleNode("ArmR_highKey").InnerText;
                SkinManager.Instance.Top_Skin[i].armR_middleKey = innode.SelectSingleNode("ArmR_middleKey").InnerText;
                SkinManager.Instance.Top_Skin[i].armR_lowKey = innode.SelectSingleNode("ArmR_lowKey").InnerText;
                i++;
            }
        }
        //하의
        nodes = xmlDoc.SelectNodes("SkinInfo/Bottom");
        foreach (XmlNode node in nodes) //하나만 있어서 초기화를 안에 넣어도됨
        {
            Array.Clear(SkinManager.Instance.Bottom_Skin, 0, SkinManager.Instance.Bottom_Skin.Length);
            SkinManager.Instance.Bottom_Skin = new Bottom[node.ChildNodes.Count];
            int i = 0;
            foreach (XmlNode innode in node.ChildNodes)
            {
                SkinManager.Instance.Bottom_Skin[i] = new Bottom();
                ReadDefalutParts(SkinManager.Instance.Bottom_Skin[i], innode);
                SkinManager.Instance.Bottom_Skin[i].waistKey = innode.SelectSingleNode("WaistKey").InnerText;
                SkinManager.Instance.Bottom_Skin[i].legL_highKey = innode.SelectSingleNode("LegL_highKey").InnerText;
                SkinManager.Instance.Bottom_Skin[i].legL_middleKey = innode.SelectSingleNode("LegL_middleKey").InnerText;
                SkinManager.Instance.Bottom_Skin[i].legL_lowKey = innode.SelectSingleNode("LegL_lowKey").InnerText;
                SkinManager.Instance.Bottom_Skin[i].legR_highKey = innode.SelectSingleNode("LegR_highKey").InnerText;
                SkinManager.Instance.Bottom_Skin[i].legR_middleKey = innode.SelectSingleNode("LegR_middleKey").InnerText;
                SkinManager.Instance.Bottom_Skin[i].legR_lowKey = innode.SelectSingleNode("LegR_lowKey").InnerText;
                i++;
            }
        }

        Debug.Log(skinInfo_add + " 불러오기 성공");
    }
    //SkinInfo.xml 저장.
    static void WriteDefalutParts(int i, SkinParts[] _lst, XmlDocument _doc, XmlNode _node)
    {
        //이름
        XmlElement name = _doc.CreateElement("Name");
        name.InnerText = _lst[i].name;
        _node.AppendChild(name);

        //색(Hex string으로 저장)
        XmlElement color = _doc.CreateElement("Color");
        color.InnerText = ColorToHexString(_lst[i].skincolor);
        _node.AppendChild(color);

        //스프라이트
        XmlElement sprite = _doc.CreateElement("Sprite");
        if (_lst[i].sprite != null)
            sprite.InnerText = _lst[i].sprite.name;
        else
            sprite.InnerText = "";
        _node.AppendChild(sprite);
    }
    public static void WriteSkinInfo()
    {
        XmlDocument Document = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = Document.CreateNode(XmlNodeType.Element, skinInfo, string.Empty);
        Document.AppendChild(root);

        // 자식 노드 생성(앞머리)
        XmlNode fronthair = Document.CreateNode(XmlNodeType.Element, "Fronthair", string.Empty);
        root.AppendChild(fronthair);
        for (int i = 0; i < SkinManager.Instance.Fronthair_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Fronthair_Skin[i].name.Replace(" ", "_");
            XmlNode fronthair_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            fronthair.AppendChild(fronthair_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Fronthair_Skin, Document, fronthair_tmp);
            //키 값 저장
            XmlElement antennahairKey = Document.CreateElement("AntennahairKey");
            antennahairKey.InnerText = SkinManager.Instance.Fronthair_Skin[i].antennahairKey;
            fronthair_tmp.AppendChild(antennahairKey);
            XmlElement fronthairKey = Document.CreateElement("FronthairKey");
            fronthairKey.InnerText = SkinManager.Instance.Fronthair_Skin[i].fronthairKey;
            fronthair_tmp.AppendChild(fronthairKey);
        }
        // 자식 노드 생성(뒷머리)
        XmlNode rearhair = Document.CreateNode(XmlNodeType.Element, "Rearhair", string.Empty);
        root.AppendChild(rearhair);
        for (int i = 0; i < SkinManager.Instance.Rearhair_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Rearhair_Skin[i].name.Replace(" ", "_");
            XmlNode rearhair_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            rearhair.AppendChild(rearhair_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Rearhair_Skin, Document, rearhair_tmp);
            //키 값 저장
            XmlElement rearhairKey = Document.CreateElement("RearhairKey");
            rearhairKey.InnerText = SkinManager.Instance.Rearhair_Skin[i].rearhairKey;
            rearhair_tmp.AppendChild(rearhairKey);
        }
        // 자식 노드 생성(눈썹)
        XmlNode eyeblow = Document.CreateNode(XmlNodeType.Element, "Eyeblow", string.Empty);
        root.AppendChild(eyeblow);
        for (int i = 0; i < SkinManager.Instance.Eyeblow_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Eyeblow_Skin[i].name.Replace(" ", "_");
            XmlNode eyeblow_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            eyeblow.AppendChild(eyeblow_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Eyeblow_Skin, Document, eyeblow_tmp);
            //키 값 저장
            XmlElement eyeblowLKey = Document.CreateElement("EyeblowLKey");
            eyeblowLKey.InnerText = SkinManager.Instance.Eyeblow_Skin[i].eyeblowLKey;
            eyeblow_tmp.AppendChild(eyeblowLKey);
            XmlElement eyeblowRKey = Document.CreateElement("EyeblowRKey");
            eyeblowRKey.InnerText = SkinManager.Instance.Eyeblow_Skin[i].eyeblowRKey;
            eyeblow_tmp.AppendChild(eyeblowRKey);
        }
        // 자식 노드 생성(속눈썹)
        XmlNode eyelid = Document.CreateNode(XmlNodeType.Element, "Eyelid", string.Empty);
        root.AppendChild(eyelid);
        for (int i = 0; i < SkinManager.Instance.Eyelid_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Eyelid_Skin[i].name.Replace(" ", "_");
            XmlNode eyelid_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            eyelid.AppendChild(eyelid_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Eyelid_Skin, Document, eyelid_tmp);
            //키 값 저장
            XmlElement eyelidLUKey = Document.CreateElement("EyelidLUKey");
            eyelidLUKey.InnerText = SkinManager.Instance.Eyelid_Skin[i].eyelidLUKey;
            eyelid_tmp.AppendChild(eyelidLUKey);
            XmlElement eyelidLDKey = Document.CreateElement("EyelidLDKey");
            eyelidLDKey.InnerText = SkinManager.Instance.Eyelid_Skin[i].eyelidLDKey;
            eyelid_tmp.AppendChild(eyelidLDKey);
            XmlElement eyelidRUKey = Document.CreateElement("EyelidRUKey");
            eyelidRUKey.InnerText = SkinManager.Instance.Eyelid_Skin[i].eyelidRUKey;
            eyelid_tmp.AppendChild(eyelidRUKey);
            XmlElement eyelidRDKey = Document.CreateElement("EyelidRDKey");
            eyelidRDKey.InnerText = SkinManager.Instance.Eyelid_Skin[i].eyelidRDKey;
            eyelid_tmp.AppendChild(eyelidRDKey);
        }
        // 자식 노드 생성(눈알)
        XmlNode eyeball = Document.CreateNode(XmlNodeType.Element, "Eyeball", string.Empty);
        root.AppendChild(eyeball);
        for (int i = 0; i < SkinManager.Instance.Eyeball_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Eyeball_Skin[i].name.Replace(" ", "_");
            XmlNode eyeball_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            eyeball.AppendChild(eyeball_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Eyeball_Skin, Document, eyeball_tmp);
            //키 값 저장
            XmlElement eyeballLKey = Document.CreateElement("EyeballLKey");
            eyeballLKey.InnerText = SkinManager.Instance.Eyeball_Skin[i].eyeballLKey;
            eyeball_tmp.AppendChild(eyeballLKey);
            XmlElement eyeballRKey = Document.CreateElement("EyeballRKey");
            eyeballRKey.InnerText = SkinManager.Instance.Eyeball_Skin[i].eyeballRKey;
            eyeball_tmp.AppendChild(eyeballRKey);
        }
        // 자식 노드 생성(눈 흰자)
        XmlNode eyewhite = Document.CreateNode(XmlNodeType.Element, "Eyewhite", string.Empty);
        root.AppendChild(eyewhite);
        for (int i = 0; i < SkinManager.Instance.Eyewhite_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Eyewhite_Skin[i].name.Replace(" ", "_");
            XmlNode eyewhite_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            eyewhite.AppendChild(eyewhite_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Eyewhite_Skin, Document, eyewhite_tmp);
            //키 값 저장
            XmlElement eyewhiteLKey = Document.CreateElement("EyewhiteLKey");
            eyewhiteLKey.InnerText = SkinManager.Instance.Eyewhite_Skin[i].eyewhiteLKey;
            eyewhite_tmp.AppendChild(eyewhiteLKey);
            XmlElement eyewhiteRKey = Document.CreateElement("EyewhiteRKey");
            eyewhiteRKey.InnerText = SkinManager.Instance.Eyewhite_Skin[i].eyewhiteRKey;
            eyewhite_tmp.AppendChild(eyewhiteRKey);
        }
        // 자식 노드 생성(입)
        XmlNode mouth = Document.CreateNode(XmlNodeType.Element, "Mouth", string.Empty);
        root.AppendChild(mouth);
        for (int i = 0; i < SkinManager.Instance.Mouth_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Mouth_Skin[i].name.Replace(" ", "_");
            XmlNode mouth_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            mouth.AppendChild(mouth_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Mouth_Skin, Document, mouth_tmp);
            //키 값 저장
            XmlElement mouthKey = Document.CreateElement("MouthKey");
            mouthKey.InnerText = SkinManager.Instance.Mouth_Skin[i].mouthKey;
            mouth_tmp.AppendChild(mouthKey);
        }
        // 자식 노드 생성(볼)
        XmlNode cheek = Document.CreateNode(XmlNodeType.Element, "Cheek", string.Empty);
        root.AppendChild(cheek);
        for (int i = 0; i < SkinManager.Instance.Cheek_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Cheek_Skin[i].name.Replace(" ", "_");
            XmlNode cheek_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            cheek.AppendChild(cheek_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Cheek_Skin, Document, cheek_tmp);
            //키 값 저장
            XmlElement cheekKey = Document.CreateElement("CheekKey");
            cheekKey.InnerText = SkinManager.Instance.Cheek_Skin[i].cheekKey;
            cheek_tmp.AppendChild(cheekKey);
        }
        // 자식 노드 생성(두상)
        XmlNode head = Document.CreateNode(XmlNodeType.Element, "Head", string.Empty);
        root.AppendChild(head);
        for (int i = 0; i < SkinManager.Instance.Head_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Head_Skin[i].name.Replace(" ", "_");
            XmlNode head_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            head.AppendChild(head_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Head_Skin, Document, head_tmp);
            //키 값 저장
            XmlElement headKey = Document.CreateElement("HeadKey");
            headKey.InnerText = SkinManager.Instance.Head_Skin[i].headKey;
            head_tmp.AppendChild(headKey);
        }
        // 자식 노드 생성(코트)
        XmlNode overcoat = Document.CreateNode(XmlNodeType.Element, "Overcoat", string.Empty);
        root.AppendChild(overcoat);
        for (int i = 0; i < SkinManager.Instance.Overcoat_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Overcoat_Skin[i].name.Replace(" ", "_");
            XmlNode overcoat_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            overcoat.AppendChild(overcoat_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Overcoat_Skin, Document, overcoat_tmp);
            //키 값 저장
            XmlElement overcoatKey = Document.CreateElement("OvercoatKey");
            overcoatKey.InnerText = SkinManager.Instance.Overcoat_Skin[i].overcoatKey;
            overcoat_tmp.AppendChild(overcoatKey);
            XmlElement overcoatLKey = Document.CreateElement("OvercoatLKey");
            overcoatLKey.InnerText = SkinManager.Instance.Overcoat_Skin[i].overcoatLKey;
            overcoat_tmp.AppendChild(overcoatLKey);
            XmlElement overcoatRKey = Document.CreateElement("OvercoatRKey");
            overcoatRKey.InnerText = SkinManager.Instance.Overcoat_Skin[i].overcoatRKey;
            overcoat_tmp.AppendChild(overcoatRKey);
            XmlElement overcoatBKey = Document.CreateElement("OvercoatBKey");
            overcoatBKey.InnerText = SkinManager.Instance.Overcoat_Skin[i].overcoatBKey;
            overcoat_tmp.AppendChild(overcoatBKey);
        }
        // 자식 노드 생성(상의)
        XmlNode top = Document.CreateNode(XmlNodeType.Element, "Top", string.Empty);
        root.AppendChild(top);
        for (int i = 0; i < SkinManager.Instance.Top_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Top_Skin[i].name.Replace(" ", "_");
            XmlNode top_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            top.AppendChild(top_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Top_Skin, Document, top_tmp);
            //키 값 저장
            XmlElement bodyKey = Document.CreateElement("BodyKey");
            bodyKey.InnerText = SkinManager.Instance.Top_Skin[i].bodyKey;
            top_tmp.AppendChild(bodyKey);
            XmlElement bodyBKey = Document.CreateElement("BodyBKey");
            bodyBKey.InnerText = SkinManager.Instance.Top_Skin[i].bodyBKey;
            top_tmp.AppendChild(bodyBKey);
            XmlElement armL_highKey = Document.CreateElement("ArmL_highKey");
            armL_highKey.InnerText = SkinManager.Instance.Top_Skin[i].armL_highKey;
            top_tmp.AppendChild(armL_highKey);
            XmlElement armL_middleKey = Document.CreateElement("ArmL_middleKey");
            armL_middleKey.InnerText = SkinManager.Instance.Top_Skin[i].armL_middleKey;
            top_tmp.AppendChild(armL_middleKey);
            XmlElement armL_lowKey = Document.CreateElement("ArmL_lowKey");
            armL_lowKey.InnerText = SkinManager.Instance.Top_Skin[i].armL_lowKey;
            top_tmp.AppendChild(armL_lowKey);
            XmlElement armR_highKey = Document.CreateElement("ArmR_highKey");
            armR_highKey.InnerText = SkinManager.Instance.Top_Skin[i].armR_highKey;
            top_tmp.AppendChild(armR_highKey);
            XmlElement armR_middleKey = Document.CreateElement("ArmR_middleKey");
            armR_middleKey.InnerText = SkinManager.Instance.Top_Skin[i].armR_middleKey;
            top_tmp.AppendChild(armR_middleKey);
            XmlElement armR_lowKey = Document.CreateElement("ArmR_lowKey");
            armR_lowKey.InnerText = SkinManager.Instance.Top_Skin[i].armR_lowKey;
            top_tmp.AppendChild(armR_lowKey);
        }
        // 자식 노드 생성(하의)
        XmlNode bottom = Document.CreateNode(XmlNodeType.Element, "Bottom", string.Empty);
        root.AppendChild(bottom);
        for (int i = 0; i < SkinManager.Instance.Bottom_Skin.Length; i++)
        {
            string _name = SkinManager.Instance.Bottom_Skin[i].name.Replace(" ", "_");
            XmlNode bottom_tmp = Document.CreateNode(XmlNodeType.Element, _name, string.Empty);
            bottom.AppendChild(bottom_tmp);
            WriteDefalutParts(i, SkinManager.Instance.Bottom_Skin, Document, bottom_tmp);
            //키 값 저장
            XmlElement waistKey = Document.CreateElement("WaistKey");
            waistKey.InnerText = SkinManager.Instance.Bottom_Skin[i].waistKey;
            bottom_tmp.AppendChild(waistKey);
            XmlElement legL_highKey = Document.CreateElement("LegL_highKey");
            legL_highKey.InnerText = SkinManager.Instance.Bottom_Skin[i].legL_highKey;
            bottom_tmp.AppendChild(legL_highKey);
            XmlElement legL_middleKey = Document.CreateElement("LegL_middleKey");
            legL_middleKey.InnerText = SkinManager.Instance.Bottom_Skin[i].legL_middleKey;
            bottom_tmp.AppendChild(legL_middleKey);
            XmlElement legL_lowKey = Document.CreateElement("LegL_lowKey");
            legL_lowKey.InnerText = SkinManager.Instance.Bottom_Skin[i].legL_lowKey;
            bottom_tmp.AppendChild(legL_lowKey);
            XmlElement legR_highKey = Document.CreateElement("LegR_highKey");
            legR_highKey.InnerText = SkinManager.Instance.Bottom_Skin[i].legR_highKey;
            bottom_tmp.AppendChild(legR_highKey);
            XmlElement legR_middleKey = Document.CreateElement("LegR_middleKey");
            legR_middleKey.InnerText = SkinManager.Instance.Bottom_Skin[i].legR_middleKey;
            bottom_tmp.AppendChild(legR_middleKey);
            XmlElement legR_lowKey = Document.CreateElement("LegR_lowKey");
            legR_lowKey.InnerText = SkinManager.Instance.Bottom_Skin[i].legR_lowKey;
            bottom_tmp.AppendChild(legR_lowKey);
        }

        Document.Save(skinInfo_add);
        Debug.Log(skinInfo_add + " 저장 완료");
    }
}
