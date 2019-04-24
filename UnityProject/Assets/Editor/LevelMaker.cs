using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelMaker : EditorWindow {

    Object block;

    Controller con;

    int fadeOutHieght;
    int fadeInHieght;
   
    float totalFadeFrames;
    float totalFadeTime;

    float delayTimeRange;

    [MenuItem("Window/LevelMaker")]
    public static void ShowWindow()
    {
        GetWindow<LevelMaker>("LevelMaker");
    }

    void OnGUI(){
        GUILayout.Label("Block Prefab");
        block = EditorGUILayout.ObjectField(block, typeof(GameObject));
        GUILayout.Label("Fade In Hieght");
        fadeInHieght = EditorGUILayout.IntField(fadeInHieght);
        GUILayout.Label("Fade Out Hieght");
        fadeOutHieght = EditorGUILayout.IntField(fadeOutHieght);
        GUILayout.Label("Fade Frames");
        totalFadeFrames = EditorGUILayout.FloatField(totalFadeFrames);
        GUILayout.Label("Fade Time");
        totalFadeTime = EditorGUILayout.FloatField(totalFadeTime);
        GUILayout.Label("Delay Time");
        delayTimeRange = EditorGUILayout.FloatField(delayTimeRange);
        GUILayout.Space(10);
        if (GUILayout.Button("BUILD"))
        {
            Build();
        }
        if (GUILayout.Button("Clear"))
        {
            Clear();
        }

    }

    void Build(){
        con = GameObject.Find("Controller").GetComponent<Controller>();
        foreach (GameObject obj in Selection.gameObjects)
        {
            GameObject createdBlock = (GameObject)Instantiate(block, obj.transform);
            createdBlock.transform.localScale = new Vector3(0.2f, 1f, 0.2f);
            createdBlock.name = block.name;
            createdBlock.AddComponent(typeof(GoalAnim));
            createdBlock.GetComponent<GoalAnim>().fadeInHieght = fadeInHieght;
            createdBlock.GetComponent<GoalAnim>().fadeOutHieght = fadeOutHieght;
            createdBlock.GetComponent<GoalAnim>().totalFadeFrames = totalFadeFrames;
            createdBlock.GetComponent<GoalAnim>().totalFadeTime = totalFadeTime;
            createdBlock.GetComponent<GoalAnim>().delayTimeRange = delayTimeRange;
            con.buildBlocks.Add((GameObject)block);   
        }
    }

    void Clear(){
        con = GameObject.Find("Controller").GetComponent<Controller>();
        con.buildBlocks.Clear();
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.transform.childCount != 0)
            {   
                DestroyImmediate(obj.transform.GetChild(0).gameObject);
            }
        }
    }
}
