using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManger : MonoBehaviour {



    public GameObject resetPanel;
    public GameObject resetButt;

    [Space]

    public Image muteSButt;
    public Image muteMButt;
   
    [Space]

    public int worldsCount = 1;
    public GameObject[] worlds = new GameObject[98];

    public GameObject worldMenuUI;

    public GameObject adSoPre;
    public GameObject adSo;

    int myCurrentWorld = 0;


    public void Start(){
        Screen.orientation = ScreenOrientation.Landscape;
        UpdateWorlds();
        if (PlayerPrefs.GetInt("Sounds",1) == 0)
        {
            muteSButt.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("Music",1) == 0)
        {
            muteMButt.color = Color.gray;
        }
        if (GameObject.FindGameObjectWithTag("AudioSource") == null)
        {
            adSo = Instantiate(adSoPre);
            if (PlayerPrefs.GetInt("Music",1)  == 0)
            {
                adSo.GetComponent<AudioSource>().Stop();
            }
        } else
        {
            adSo = GameObject.FindGameObjectWithTag("AudioSource");
        }
    }



    public void NextWorld(){
        worlds [myCurrentWorld].SetActive(false);
        myCurrentWorld += 1 + worldsCount;
        myCurrentWorld = myCurrentWorld % worldsCount;
        worlds [myCurrentWorld].SetActive(true);
    }

    public void PreviousWorld(){
        worlds [myCurrentWorld].SetActive(false);
        myCurrentWorld += -1 + worldsCount;
        myCurrentWorld = myCurrentWorld % worldsCount;
        worlds [myCurrentWorld].SetActive(true);
    }

    public void UpdateWorlds(){
        for (int i = 0; i < PlayerPrefs.GetInt("ReachedWorld",1); i++)
        {
            worlds [i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            worlds [i].transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = true;
            Transform state = worlds [i].transform.GetChild(1);
            state.gameObject.SetActive(true);
            string name = state.GetChild(0).name;
            state.GetChild(1).GetComponent<Text>().text = "Levels : " + (PlayerPrefs.GetInt(name + "ReachedLevel",1)).ToString() + "/10";
            state.GetChild(2).GetComponent<Text>().text = "Stars : " + (PlayerPrefs.GetInt(name + "Stars",0)).ToString() + "/30";

        }
    }

    public void MuteSounds(Image butt){
        if (PlayerPrefs.GetInt("Sounds",1) == 1)
        {
            PlayerPrefs.SetInt("Sounds", 0);
            butt.color = Color.gray;

        } else
        {
            PlayerPrefs.SetInt("Sounds", 1);
            butt.color = Color.white;
        }
    }
    public void MuteMusic(Image butt){
        if (PlayerPrefs.GetInt("Music",1) == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
            adSo.GetComponent<AudioSource>().Stop();
            butt.color = Color.gray;
        } else
        {
            PlayerPrefs.SetInt("Music", 1);
            adSo.GetComponent<AudioSource>().Play();
            butt.color = Color.white;
        }    
    }

    public void Levels(){
    }
    public void Settings(){
    }
    public void Back(){
    }
    public void Exit(){
        Application.Quit();
    }

    public void LoadWorld(){
        DontDestroyOnLoad(adSo);
        SceneManager.LoadScene(worlds[myCurrentWorld].transform.GetChild(1).GetChild(0).name);
    }

    public void UnlockReset(){
        resetPanel.SetActive(true);
        resetButt.GetComponent<Button>().interactable = false;
    }

    public void Reset(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void CnacleReset(){
        resetPanel.SetActive(false);
        resetButt.GetComponent<Button>().interactable = true;
    }
}