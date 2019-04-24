using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnim : MonoBehaviour {

    public bool traniner = false;

    GameManger gm;

    public int fadeOutHieght;
    public int fadeInHieght;
    [Space]
    public float totalFadeFrames;
    public float totalFadeTime;

    float frameFadeValue;
    float frameFadeTime;
    [Space]
    public float delayTimeRange;
    float delayTime;


    float currValue = 1;

    void Start(){
        gm = GameObject.Find("GameManger").GetComponent<GameManger>();
        transform.position = new Vector3(transform.parent.position.x, fadeOutHieght, transform.parent.position.z);
        delayTime = Random.Range(0, delayTimeRange);
        frameFadeTime = totalFadeTime / totalFadeFrames;
        frameFadeValue = 1 / totalFadeFrames;
        StartCoroutine(FadeIn());
        if (!traniner)
        {
            StartCoroutine(FadeOut());
        }
    }


    IEnumerator FadeIn(){
        for (int i = 0; i < transform.childCount; i++)
        {
            Color col = transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(col.r, col.g, col.b, 0);
        }
        yield return new WaitForSeconds(delayTime + 1);
        while (transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.color.a < 1)
        {
            Fade(frameFadeValue);
            Appearance(false,-frameFadeValue);
            yield return new WaitForSeconds(frameFadeTime);
        }
    }

    public IEnumerator FadeOut(){
        yield return new WaitForSeconds(delayTime + gm.waitTime);
        StopCoroutine(FadeIn());
        while (transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().material.color.a > 0)
        {
            Fade(-frameFadeValue);
            Appearance(true,frameFadeValue);
            yield return new WaitForSeconds(frameFadeTime);
        }
        Destroy(this.gameObject,1);
    }

    void Fade(float valu){
        for (int i = 0; i < transform.childCount; i++)
        {
            Color col = transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(col.r, col.g, col.b, col.a + valu);
        }
    }

    void Appearance(bool isOut, float valu){
        currValue += valu;
        if (isOut)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(fadeInHieght,fadeOutHieght,currValue),transform.position.z);
        } 
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(fadeInHieght,fadeOutHieght,currValue),transform.position.z);
        }
    }
}
