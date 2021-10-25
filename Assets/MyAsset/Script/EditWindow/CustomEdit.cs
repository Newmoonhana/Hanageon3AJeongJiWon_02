#if UNITY_EDITOR
using Spine.Unity;
using UnityEditor;
using UnityEngine;

public class CustomEdit : EditorWindow
{
    public CharSkin customdata;

    [SpineSlot] public string bodyslot;
    [SpineAttachment] public string bodykey;

    [MenuItem("Edit_Custom/Edit Parts")]
    static void init()
    {
        CustomEdit window = (CustomEdit)EditorWindow.GetWindow(typeof(CustomEdit));
    }

    private void OnGUI()
    {
        //EditorGUILayout.BeginHorizontal();
        customdata = (CharSkin)EditorGUILayout.ObjectField("커스텀 파일", customdata, typeof(CharSkin), true);

        if (customdata == null)
        {
            //EditorGUILayout.EndHorizontal();
            return;
        }
        //EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("************");
        GUILayout.Label("얼굴 커스텀 파츠", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("************");
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("************");
        GUILayout.Label("의상 커스텀 파츠", EditorStyles.boldLabel);

        //EditorGUILayout.ObjectField("상의", bodykey, typeof(SpineAttachment), true);
        //bodykey = EditorGUILayout.EnumPopup("몬스터 종류", bodyslot.dataField);
        //EditorGUILayout.EnumPopup("몬스터 종류", );
        EditorGUILayout.LabelField("************");
    }
}
#endif