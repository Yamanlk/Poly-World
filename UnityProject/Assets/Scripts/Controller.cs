using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Material greenMat;
    public Material redMat;

    public int width;
    public int lenght;
    public GameObject[,] buildChunksArry;
    public string[,] buildGoalArry;
    public string[,] buildDoneArry;

    public List<GameObject> buildBlocks = new List<GameObject>();

    float wrong;
    float right;

    int myPosX;
    int myPosY;

    public Vector3 buildBlockOffset;

    Image timerRight;
    Image timerLeft;
    float coolDown;
    float buildTimer;

    bool gameOver = false;

    GameManger gm;




    void Start () {
        if (PlayerPrefs.GetInt("Sounds",1) == 0)
        {
            this.GetComponent<AudioSource>().mute = true;
        }
        gm = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        buildChunksArry = new GameObject[width, lenght];
        buildGoalArry = new string[width,lenght];
        buildDoneArry = new string[width,lenght];
	}
	
	void FixedUpdate () {
        UpdatePosition();
        Timer();
        Movement();
    }

    void CalculateScore(){
        for (int i = 0; i < width; i++)
        {
            for (int z = 0; z < lenght; z++)
            {
                if (buildGoalArry [z, i] != "")
                {
                    if (buildDoneArry [z, i] == buildGoalArry [z, i])
                    {
                        buildChunksArry [z, i].GetComponent<MeshRenderer>().material = greenMat;
                        right++;

                    } else
                    {
                        buildChunksArry [z, i].GetComponent<MeshRenderer>().material = redMat;
                        wrong++;
                    }
                }
            }
        }

        gm.GameOver(right * 100 / (wrong + right));
    }
       

    public void Build(){
        this.GetComponent<AudioSource>().Play();
        buildDoneArry [myPosX, myPosY] = transform.GetChild(0).name;
        transform.GetChild(0).transform.position = new Vector3(buildChunksArry [myPosX, myPosY].transform.position.x,1,buildChunksArry [myPosX, myPosY].transform.position.z);
        transform.GetChild(0).SetParent(null);
        if (buildBlocks.Count == 0)
        {
            CalculateScore();
            gameOver = true;
            return;
        }
        int ran = Random.Range(0, buildBlocks.Count);
        GameObject myBlock = (GameObject)Instantiate(buildBlocks [ran], transform.position, buildBlocks[ran].transform.rotation,this.transform);
        myBlock.name = buildBlocks [ran].name;
        myBlock.transform.localPosition = buildBlockOffset;
        buildBlocks.RemoveAt(ran);
        buildTimer = coolDown;
        ChoseDir();

    }

    void UpdatePosition(){
        if (buildChunksArry [myPosX, myPosY] == null)
            return;
            transform.localPosition = Vector3.Lerp(transform.position, buildChunksArry [myPosX, myPosY].transform.position, 0.1f);
    }

    void ChoseDir(){ 
        for (int i = 0; i < width; i++)
        {
            bool found = false;
            for (int z = 0; z < lenght; z++) {
                if (buildDoneArry[i,z] == null)
                {
                    myPosX = i;
                    myPosY = z;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                break;
            }
        }
    }

    void Timer(){
        if (coolDown == 0 || gameOver)
            return;
        if (buildTimer > 0)
        {   
            buildTimer -= Time.deltaTime;
            timerRight.fillAmount = buildTimer  / coolDown;
            timerLeft.fillAmount = buildTimer  / coolDown;
        } else
        {
            Build();
            buildTimer = coolDown;
        }
    }

    public void StartGame(float cD, GameObject timerR, GameObject timerL){
        this.coolDown = cD;
        buildTimer = coolDown;
        timerRight = timerR.GetComponent<Image>();
        timerLeft = timerL.GetComponent<Image>();
        int ran = Random.Range(0, buildBlocks.Count);
        GameObject myBlock = (GameObject)Instantiate(buildBlocks [ran], transform.position, buildBlocks[ran].transform.rotation,this.transform);
        myBlock.name = buildBlocks [ran].name;
        myBlock.transform.localPosition = buildBlockOffset;
        buildBlocks.RemoveAt(ran);
    }

    void Movement(){
//        Input.GetTouch(0).position
//        if (coolDown == 0 || Input.touchCount == 0)
//            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag != "BuildChunk")
                return;
            Vector2 chunkID = hit.collider.gameObject.GetComponent<BuildChunk>().myID;
            if (buildDoneArry[(int)chunkID.x,(int)chunkID.y] == null)
            {
                myPosX = (int)chunkID.x;
                myPosY = (int)chunkID.y;
            }
        }
    }
}
