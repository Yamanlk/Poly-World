using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour {

    GameManger gm;
    Controller con;

    public GoalAnim[] ga = new GoalAnim[4];

    public GameObject first;
    public GameObject second;
    public GameObject third;

    void Start(){
        StartCoroutine(TrainingFirst());
    }

    IEnumerator TrainingFirst(){
        yield return new WaitForSeconds(3);
        first.SetActive(true);
    }

    public void StartSecond(){
        StartCoroutine(TrainingSecond());
    }

    IEnumerator TrainingSecond(){
        foreach (var block in ga)
        {
            StartCoroutine(block.FadeOut());
        }
        yield return new WaitForSeconds(4);
        Debug.Log("F");
    }
	
}
