using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.IO;

public class StartSceneEvenet : MonoBehaviour 
{
    private int SelectNumber = -1;
    private int StateIndex = 0;            //0 在選 Mode， 1 在選 Level

    private Color NomralColor = new Color(1, 1, 1, 1);
    private Color PressColor = new Color(0.7f, 0.7f, 1, 1);//new Color(200, 200, 200, 255);

    public GameObject GameModePresent;
    public GameObject GameLevelPresent;
    public Image[] GameMode_Button;
    public Image[] GameLevel_Button;
    public GameObject Next_Button;
    public GameObject Back_Button;

    public GameObject LoadingText;
    private int LoadingState = 0;
    private float LoadingTime = 0;
    private const float LoadingChange = 0.2f;

    void Start()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                string str = "Width: " + Screen.width + " Height: " + Screen.height;
                System.IO.File.WriteAllText("sdcard/CityMap/ScreenInfo.txt", str);
                break;
        }
    }
    void Update()
    {
        // For loading Anim
        switch(StateIndex)
        {
            case 2:
                switch(LoadingState)
                {
                    case 0:
                        LoadingText.GetComponentInChildren<Text>().text  = "Loading";
                        break;
                    case 1:
                        LoadingText.GetComponentInChildren<Text>().text  = "Loading.";
                        break;
                    case 2:
                        LoadingText.GetComponentInChildren<Text>().text  = "Loading..";
                        break;
                    case 3:
                        LoadingText.GetComponentInChildren<Text>().text = "Loading...";
                        break;
                }
                LoadingTime += Time.deltaTime;
                if(LoadingTime >= LoadingChange)
                {
                    LoadingTime -= LoadingChange;
                    LoadingState = (LoadingState + 1) % 4;
                }
                break;
        }
    }


    public void GameModePress(int Number)
    {
        SelectNumber = Number;
        SetGameModeColor(Number);
        Next_Button.SetActive(true);
    }

    public void GameLevelPress(int Number)
    {
        SelectNumber = Number;
        SetGameLevelColor(Number);
        Next_Button.SetActive(true);
    }

    public void NextPress()
    {
        switch (StateIndex)
        {
            case 0:
                PlayerPrefs.SetInt("GameMode", SelectNumber);
                StateIndex = 1;
                SelectNumber = -1;


                SetGameLevelColor(-1);

                GameModePresent.SetActive(false);
                GameLevelPresent.SetActive(true);
                Next_Button.SetActive(false);
                Back_Button.SetActive(true);
                break;
            case 1:
                PlayerPrefs.SetInt("GameLevel", SelectNumber);
                StateIndex = 2;

                LoadingText.SetActive(true);
                GameLevelPresent.SetActive(false);
                Next_Button.SetActive(false);
                Back_Button.SetActive(false);

                string levelname = "";
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        levelname = "sdcard/CityMap/GameData_Level";
                        break;
                    case RuntimePlatform.WindowsEditor:
                        levelname = "GameData_Level";
                        break;
                }
                levelname += PlayerPrefs.GetInt("GameLevel") + "_mode" + PlayerPrefs.GetInt("GameMode") + ".csv";
                
                PlayerPrefs.SetString("GamePath", levelname);
                Application.LoadLevelAsync(SelectNumber + 1);
                break;
        }
    }
    public void BackPress()
    {
        StateIndex = 0;
        SelectNumber = -1;

        SetGameModeColor(-1);
        GameModePresent.SetActive(true);
        GameLevelPresent.SetActive(false);
        Next_Button.SetActive(false);
        Back_Button.SetActive(false);
    }

    //Set Color Method
    private void SetGameModeColor(int index)
    {
        for (int i = 0; i < GameMode_Button.Length; i++)
            if (i == index)
                GameMode_Button[i].color = PressColor;
            else
                GameMode_Button[i].color = NomralColor;
    }
    private void SetGameLevelColor(int index)
    {
        for (int i = 0; i < GameLevel_Button.Length; i++)
            if (i == index)
                GameLevel_Button[i].color = PressColor;
            else
                GameLevel_Button[i].color = NomralColor;
    }
}
