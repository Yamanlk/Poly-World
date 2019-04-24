using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelsMenuManger : MonoBehaviour {

    public string myWorld;

    public GameObject adSoPre;
    public GameObject adSo;

    Button[] levelsButts = new Button[10];

	void Start () {	
        if (GameObject.FindGameObjectWithTag("AudioSource") == null)
        {
            adSo = Instantiate(adSoPre);
        } else
        {
            adSo = GameObject.FindGameObjectWithTag("AudioSource");
        }
        if (PlayerPrefs.GetInt("Music",1)  == 0)
        {
            adSo.GetComponent<AudioSource>().Stop();
        }
        AddButts();
        CheckLevels();
	}

    void AddButts(){
        for (int i = 0; i < transform.childCount; i++)
        {
            levelsButts [i] = transform.GetChild(i).GetComponent<Button>();
        }
    }

    void CheckLevels(){
        for (int i = 0; i < levelsButts.Length; i++){
            
            if (PlayerPrefs.GetInt("World" + myWorld + "ReachedLevel",1) > i)
            {
                levelsButts[i].interactable = true;
                switch (PlayerPrefs.GetInt("World" + myWorld + "Level" + levelsButts[i].name + "Stars",0))
                {
                    case 1:
                        {
                            levelsButts [i].transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                            break;
                        }
                    case 2:
                        {
                            levelsButts [i].transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                            levelsButts [i].transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255);
                            break;
                        }
                    case 3:
                        {
                            levelsButts [i].transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                            levelsButts [i].transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255);
                            levelsButts [i].transform.GetChild(2).GetComponent<Image>().color = new Color(255, 255, 255);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }

    public void LoadLevel(string level){
        SceneManager.LoadScene("World" + myWorld + "Level" + level);
        Destroy(GameObject.FindGameObjectWithTag("AudioSource"));
    }

    public void Back(){
        DontDestroyOnLoad(adSo);
        SceneManager.LoadScene("MainMenu");
    }

    public void Tot(){
        PlayerPrefs.DeleteAll();

    }
}
