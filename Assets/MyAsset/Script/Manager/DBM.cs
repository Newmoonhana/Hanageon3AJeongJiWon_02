using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceList<T> where T : Object    //리소스 폴더 파일 캐싱용 클래스
{
    int i = 0;
    public List<T> r = new List<T>();
    public string path;

    public ResourceList(string _path)
    {
        path = _path;
    }

    public T FindR(string _name)
    {
        for (i = 0; i < r.Count; i++)
        {
            T tmp = r.Find(x => x.name == _name);
            if (tmp != null)
            {
                Debug.Log("Find it! : " + r[i]);
                return r[i];
            }
        }

        Debug.Log("Not Find");
        return null;
    }
    public T FindR(T _val)
    {
        for (i = 0; i < r.Count; i++)
        {
            if (r[i] == _val)
            {
                Debug.Log("Find it! : " + r[i]);
                return r[i];
            }
        }

        return null;
    }
    public void WriteRAll()
    {
        r.Clear();
        object[] temp = Resources.LoadAll(path);

        for (i = 0; i < temp.Length; i++)
        {
            T tmp = temp[i] as T;
            if (tmp != null)    //.meta 파일 제외
                r.Add(tmp);
        }
    }
    public void WriteRAll_SprToTxtu() //sprite -> texture
    {
        r.Clear();
        object[] temp = Resources.LoadAll(path);
        if (temp.Length == 0)   //파일이 아예 없는 경우 방지
        {
            Debug.LogError("파일이 없습니다.");
            return;
        }

        for (i = 0; i < temp.Length; i++)
        {
            Sprite tmp = temp[i] as Sprite;
            if (tmp != null)    //.meta 파일 제외
            {
                Texture2D tmp_txtu = Funtion.ConvertSpriteToTexture(tmp as Sprite);
                r.Add(tmp_txtu as T);
            }
        }
    }
}

public class DBM : SingletonPattern_IsA_Mono<DBM>
{
    //리소스 폴더 캐싱 변수
    public ResourceList<Sprite> spr_CharSpine_Icon = new ResourceList<Sprite>("Icon/CharSpine_Icon");
    public ResourceList<Texture2D> txtu_Food_Sprite = new ResourceList<Texture2D>("Sprite/Food_Sprite");

    public void Awake()
    {
        if (DontDestroyInst(this))
        {
            spr_CharSpine_Icon.WriteRAll();
            txtu_Food_Sprite.WriteRAll_SprToTxtu();
        }
    }
}
