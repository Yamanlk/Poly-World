using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;


public class LevelEditor : EditorWindow {


    public GameObject playingMusic;
    public float waitTime;
    public string myWorld;
    public string myLevel;
    public int nextLevelInt;
    public string nextLevel;
    public int myWorldInt;
    public float coolDown;
    public float levelPassRequirement;

    public bool isLastLevel;

    [MenuItem("Window/LevelEditor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>("LevelEditor");
    }

    void OnGUI(){
        GUILayout.Label("WorldString");
        myWorld = GUILayout.TextField(myWorld);
        GUILayout.Label("LevelString");
        myLevel = GUILayout.TextField(myLevel);
        GUILayout.Label("NextLevelString");
        nextLevel = GUILayout.TextField(nextLevel);
        GUILayout.Space(25);
        GUILayout.Label("WaitTime");
        waitTime = EditorGUILayout.FloatField(waitTime);
        GUILayout.Label("CoolDown");
        coolDown = EditorGUILayout.FloatField(coolDown);
        GUILayout.Label("LPR");
        levelPassRequirement = EditorGUILayout.FloatField(levelPassRequirement);
        GUILayout.Space(25);
        GUILayout.Label("MyWorldInt");
        myWorldInt = EditorGUILayout.IntField(myWorldInt);
        GUILayout.Label("NextLevelInt");
        nextLevelInt = EditorGUILayout.IntField(nextLevelInt);
        isLastLevel = EditorGUILayout.Toggle(isLastLevel);
        GUILayout.Space(10);



        if (GUILayout.Button("Submit"))
        {
            GameManger Gm = GameObject.Find("GameManger").GetComponent<GameManger>();
            Gm.myWorld = myWorld;
            Gm.myLevel = myLevel;
            Gm.nextLevel = nextLevel;
            Gm.waitTime = waitTime;
            Gm.coolDown = coolDown;
            Gm.levelPassRequirement = levelPassRequirement;
            Gm.myWorldInt = myWorldInt;
            Gm.nextLevelInt = nextLevelInt;
            Gm.isLastLevel = isLastLevel;
        }
        if (GUILayout.Button("ImportCurrent"))
        {
            GameManger Gm = GameObject.Find("GameManger").GetComponent<GameManger>();
            myWorld = Gm.myWorld;
            myLevel = Gm.myLevel;
            nextLevel = Gm.nextLevel;
            waitTime = Gm.waitTime;
            coolDown = Gm.coolDown;
            levelPassRequirement = Gm.levelPassRequirement;
            myWorldInt = Gm.myWorldInt;
            nextLevelInt = Gm.nextLevelInt;
            isLastLevel = Gm.isLastLevel;
        }

    }
}
