using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildChunk : MonoBehaviour {

    public Vector2 myID;
    public string myTag;

    public Controller con;

	void Start () {
        StartCoroutine(AddMe(con));
	}
	
	void Update () {
		
	}
        

    public IEnumerator AddMe(Controller con){
        yield return new WaitForSeconds(2);
        if (transform.childCount > 0)
        {
            myTag = transform.GetChild(0).name;
        }
        con.buildChunksArry [(int)myID.x, (int)myID.y] = this.gameObject;
        con.buildGoalArry [(int)myID.x, (int)myID.y] = this.myTag;
    }
}
