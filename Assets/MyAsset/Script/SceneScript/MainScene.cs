using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        ZOOM
    }
    STATE state = STATE.NONE;
    //state obj
    public GameObject in_obj;   //zoom 상태일 시 enable=true

    public GameObject zoomButton_obj;
    public Image zoomImage_img;
    string scenename = null;

    public void InputZoomButton(MAINSCENE_component _type)
    {
        if (state != STATE.NONE)
        {
            return;
        }
        zoomButton_obj.SetActive(false);
        zoomImage_img.sprite = _type.zoom_spr;
        in_obj.SetActive(true);
        scenename = _type.scene_name;
        state = STATE.ZOOM;
    }

    public void InputInButton()
    {
        if (state != STATE.ZOOM)
        {
            return;
        }

        GameManager.Instance.LoadScene(scenename);
    }

    public void InputBackButton()
    {
        zoomButton_obj.SetActive(true);
        in_obj.SetActive(false);

        scenename = null;
        state = STATE.NONE;
    }
}
