using System.Collections;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManger : MonoBehaviour {

    public bool trainer = false;

    public GameObject playingMusic;
    GameObject playedMusic;
    [Space]
    public float waitTime;
    [Space]
    public string myWorld;
    public string myLevel;
    public int nextLevelInt;
    public string nextLevel;
    public int myWorldInt;
    public bool isLastLevel;
    [Space]
    public GameObject buildButt;
    public GameObject timingBar;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject backGroundPanel;
    public Text text;

    [Space]
    bool seenAd;
    public float coolDown;

    [Space]


    [Range(0,100)]
    public float levelPassRequirement;

    Controller controller;

    public Material mat;

    void Start(){
        seenAd = false;
        if (PlayerPrefs.GetInt("Music",1)  == 1)
        {
            playedMusic = Instantiate(playingMusic);
        }
        controller = GameObject.Find("Controller").GetComponent<Controller>();
        if (!trainer)
        {
            StartCoroutine(StartGame());
        }
    }

    void Update(){
    }

    IEnumerator StartGame(){
        yield return new WaitForSeconds(waitTime + 1);
        text.text = 3.ToString();
        yield return new WaitForSeconds(1);
        text.text = 2.ToString();
        yield return new WaitForSeconds(1);
        text.text = 1.ToString();
        yield return new WaitForSeconds(1);
        text.text = 0.ToString();
        Destroy(text, 0.5f);

        buildButt.SetActive(true); 
        timingBar.SetActive(true);
        controller.StartGame(coolDown,timingBar.transform.GetChild(0).GetChild(0).gameObject,timingBar.transform.GetChild(1).GetChild(0).gameObject);
    }

    public void Replay(){
        if (!seenAd)
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home(){
        if (!seenAd)
            return;
        SceneManager.LoadScene("World" + myWorld);
    }
    public void NextLevel(){
        if (!seenAd)
            return;
        SceneManager.LoadScene("World" + myWorld + "Level" + nextLevel); 
    }

    IEnumerator StartAd(){
        if (trainer)
        {
            seenAd = true;
        } else
        {
            int ran = Random.Range(0, 100);
            if (ran > 25)
            {
                seenAd = true;  
            } else
            {
                yield return new WaitForSeconds(5f);
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                    yield return new WaitForSeconds(1);
                    seenAd = true;
                } else
                {
                    seenAd = true;
                }
            }
        }
    }
        
    public void GameOver(float score){
        StartCoroutine(StartAd());
        if (playedMusic != null)
        {
            playedMusic.GetComponent<AudioSource>().volume = 0.5f;
        }
        backGroundPanel.SetActive(true);
        if (score >= levelPassRequirement)
        {
            if (isLastLevel && PlayerPrefs.GetInt("ReachedWorld",1) == myWorldInt)
            {
                PlayerPrefs.SetInt("ReachedWorld", myWorldInt + 1);
            }
            if (PlayerPrefs.GetInt("World" + myWorld + "ReachedLevel", 1) < nextLevelInt && !isLastLevel)
            {
                PlayerPrefs.SetInt("World" + myWorld + "ReachedLevel", nextLevelInt);
            }
            winPanel.SetActive(true);
            if (score >= 40)
            {
                winPanel.transform.GetChild(5).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("World" + myWorld + "Level" + myLevel + "Stars",0) < 1)
                {
                    PlayerPrefs.SetInt("World" + myWorld + "Level" + myLevel + "Stars",1);
                    PlayerPrefs.SetInt("World" + myWorld + "Stars",PlayerPrefs.GetInt("World" + myWorld + "Stars",0) + 1);

                }
            }
            if (score >= 60)
            {
                winPanel.transform.GetChild(6).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("World" + myWorld + "Level" + myLevel + "Stars",0) < 2)
                {
                    PlayerPrefs.SetInt("World" + myWorld + "Level" + myLevel + "Stars",2);
                    PlayerPrefs.SetInt("World" + myWorld + "Stars", PlayerPrefs.GetInt("World" + myWorld + "Stars", 0) + 1);
                }
            }
            if (score == 100)
            {
                winPanel.transform.GetChild(7).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("World" + myWorld + "Level" + myLevel + "Stars",0) < 3)
                {
                    PlayerPrefs.SetInt("World" + myWorld + "Level" + myLevel + "Stars",3);
                    PlayerPrefs.SetInt("World" + myWorld + "Stars", PlayerPrefs.GetInt("World" + myWorld + "Stars", 0) + 1);
                }
            }
        } else
        {
            gameOverPanel.SetActive(true);
            if (score >= 40)
            {
                winPanel.transform.GetChild(5).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("World" + myWorld + "Level" + myLevel + "Stars",0) < 1)
                {
                    PlayerPrefs.SetInt("World" + myWorld + "Level" + myLevel + "Stars",1);
                }
            }
            if (score >= 60)
            {
                winPanel.transform.GetChild(6).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("World" + myWorld + "Level" + myLevel + "Stars",0) < 2)
                {
                    PlayerPrefs.SetInt("World" + myWorld + "Level" + myLevel + "Stars",2);
                }
            }
        }
    }
}
