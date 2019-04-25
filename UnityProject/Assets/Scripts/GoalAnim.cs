using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnim : MonoBehaviour {

    GameManger gm;

    public int fadeOutHieght;                   //Start and end position of the block.
    public int fadeInHieght;                    //Were the block will land befor fading out.                    
    [Space]
    public float totalFadeFrames;               //How many frames to fade in / out.
    public float totalFadeTime;                 //Time in secondes to fade in / out.

    private float fStep;                        //The value that need to be added to the position at each step.

    [Space]
    public float delayTimeRange;                
    private float delayTime;

    void Start(){
        gm = GameObject.Find("GameManger").GetComponent<GameManger>();
        transform.position = new Vector3(transform.parent.position.x, fadeOutHieght, transform.parent.position.z);
        delayTime = Random.Range(0, delayTimeRange);
        StartCoroutine(FadeIn());
        fStep = (fadeOutHieght - fadeInHieght) / totalFadeFrames;
    }


    IEnumerator FadeIn(){
        for (int i = 0; i < transform.childCount; i++)
        {
            Color col = transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(col.r, col.g, col.b, 0);
        }
        yield return new WaitForSeconds(delayTime + 1);
        for (int i = 0; i < totalFadeFrames; i++)
        {
            Fade(1/totalFadeFrames);
            Appearance(false, -fStep);
            yield return new WaitForSeconds(totalFadeTime / totalFadeFrames);
        }
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut(){
        yield return new WaitForSeconds(delayTime + gm.waitTime);
        StopCoroutine(FadeIn());
        for (int i = 0; i < totalFadeFrames; i++)
        {
            Fade(1 / totalFadeFrames);
            Appearance(true, fStep);
            yield return new WaitForSeconds(totalFadeTime / totalFadeFrames);
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
        if (isOut)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + valu, transform.position.z);
        } 
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + valu, transform.position.z);
        }
    }
}
