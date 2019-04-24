using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GruopAndName : EditorWindow {

    float dis;
    List<GameObject> activeGO = new List<GameObject>();
    GameObject objGroup;


    [MenuItem("Window/Group")]
    public static void ShowWindow()
    {
        GetWindow<GruopAndName>("Group");
    }

    void OnGUI(){
        GUILayout.Label("GroupItems");
        if (GUILayout.Button("Group"))
        {
            Group();
        }
        if (GUILayout.Button("UnGroup"))
        {
            UnGroup();
        }
    }

    void Group(){
        activeGO.Clear();
        activeGO.AddRange(Selection.gameObjects);
        objGroup = new GameObject("Group");
        foreach (GameObject obj in activeGO)
        {
            obj.transform.SetParent(objGroup.transform);
        }
            
    }

    void UnGroup(){
        GameObject parent = Selection.activeGameObject;
        Debug.Log(parent.transform.childCount);
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(0).SetParent(null);
        }

    }

}
