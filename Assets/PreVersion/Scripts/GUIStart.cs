using UnityEngine;
using System.Collections;
using System.IO;

public class GUIStart : MonoBehaviour
{
    private GUISkin myskin;

    int selectNumber = -1;
    int StateOf = 0;


//    void OnGUI()
//    {
//        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
//        buttonStyle.fontSize = 25;

//        if (StateOf == 0)
//        {
//            int buttonleft = Screen.width / 10;
//            int buttonright = buttonleft * 8;
//            int bottonheight = (Screen.height - 30) / 6;
//            bool[] GUIState = new bool[13];
//            for (int i = 0; i < 12; i++)
//            {
//                if (i == selectNumber)
//                {
//                    GUIState[i] = false;
//                }
//                else
//                {
//                    GUIState[i] = true;
//                }
//            }
//            if (selectNumber >= 0)
//                GUIState[12] = true;
//            else
//                GUIState[12] = false;
//            if (myskin)
//                GUI.skin = myskin;
//            //GUI.enabled=GUIState[0];
//            //if(GUI.Button(new Rect(buttonleft, 10, buttonleft*3, bottonheight), "手勢 2D指引 0%",buttonStyle))
//            //{ 
//            //    selectNumber=0;			 

//            //}
//            GUI.enabled = GUIState[1];
//            if (GUI.Button(new Rect(buttonleft, 10 + bottonheight, buttonleft * 3, bottonheight), "手勢 2D指引 50%", buttonStyle))
//            {
//                selectNumber = 1;
//            }
//            //GUI.enabled=GUIState[2];
//            //if(GUI.Button(new Rect(buttonleft, 10+bottonheight*2, buttonleft*3, bottonheight), "手勢 2D指引 100%",buttonStyle))
//            //{
//            //    selectNumber=2;


//            //}
//            //GUI.enabled=GUIState[3];
//            //if(GUI.Button(new Rect(buttonleft, 20+bottonheight*3, buttonleft*3, bottonheight), "手勢 3D指引 0%",buttonStyle))
//            //{
//            //    selectNumber=3;


//            //}
//            GUI.enabled = GUIState[4];
//            if (GUI.Button(new Rect(buttonleft, 20 + bottonheight * 4, buttonleft * 3, bottonheight), "手勢 3D指引 50%", buttonStyle))
//            {
//                selectNumber = 4;
//            }
//            //GUI.enabled=GUIState[5];
//            //if(GUI.Button(new Rect(buttonleft, 20+bottonheight*5, buttonleft*3, bottonheight), "手勢 3D指引 100%",buttonStyle))
//            //{
//            //    selectNumber=5;
//            //}

//            //GUI.enabled=GUIState[6];
//            //if(GUI.Button(new Rect(buttonright-buttonleft*3, 10 , buttonleft*3, bottonheight), "體感 2D指引 0%",buttonStyle))
//            //{
//            //    selectNumber=6;

//            //}
//            GUI.enabled = GUIState[7];
//            if (GUI.Button(new Rect(buttonright - buttonleft * 3, 10 + bottonheight, buttonleft * 3, bottonheight), "體感 2D指引 50%", buttonStyle))
//            {
//                selectNumber = 7;
//            }
//            //GUI.enabled=GUIState[8];
//            //if(GUI.Button(new Rect(buttonright-buttonleft*3, 10+bottonheight*2, buttonleft*3, bottonheight), "體感 2D指引 100%",buttonStyle))
//            //{
//            //    selectNumber=8;				
//            //}
//            //GUI.enabled=GUIState[9];
//            //if(GUI.Button(new Rect(buttonright-buttonleft*3, 20+bottonheight*3, buttonleft*3, bottonheight), "體感 3D指引 0%",buttonStyle))
//            //{
//            //    selectNumber=9;
//            //}
//            GUI.enabled = GUIState[10];
//            if (GUI.Button(new Rect(buttonright - buttonleft * 3, 20 + bottonheight * 4, buttonleft * 3, bottonheight), "體感 3D指引 50%", buttonStyle))
//            {
//                selectNumber = 10;
//            }
//            //GUI.enabled=GUIState[11];
//            //if(GUI.Button(new Rect(buttonright-buttonleft*3, 20+bottonheight*5, buttonleft*3, bottonheight), "體感 3D指引 100%",buttonStyle))
//            //{
//            //    selectNumber=11;
//            //}
//            GUI.enabled = GUIState[12];
//            if (GUI.Button(new Rect(Screen.width - buttonleft * 2, Screen.height - bottonheight, buttonleft * 2, bottonheight), "Next", buttonStyle))
//            {
//                PlayerPrefs.SetInt("GameMode", selectNumber);
//                StateOf = 1;
//                selectNumber = -1;
//            }
//        }
//        else if (StateOf == 1)
//        {
//            int buttonleft = Screen.width / 4;
//            int bottonheight = (Screen.height - 20) / 6;
//            bool[] GUIState = new bool[7];
//            for (int i = 0; i < 6; i++)
//                if (i == selectNumber)
//                    GUIState[i] = false;
//                else
//                    GUIState[i] = true;

//            if (selectNumber >= 0)
//            {
//                GUIState[6] = true;
//            }
//            else
//            {
//                GUIState[6] = false;
//            }
//            GUI.enabled = GUIState[0];
//            if (GUI.Button(new Rect(buttonleft, 10, Screen.width / 2, bottonheight), "Practice Mode", buttonStyle))
//            {
//                selectNumber = 0;
//            }
//            GUI.enabled = GUIState[1];
//            if (GUI.Button(new Rect(buttonleft, 10 + bottonheight, Screen.width / 2, bottonheight), "First Task", buttonStyle))
//            {
//                selectNumber = 1;
//            }
//            GUI.enabled = GUIState[2];
//            if (GUI.Button(new Rect(buttonleft, 10 + bottonheight * 2, Screen.width / 2, bottonheight), "Second Task", buttonStyle))
//            {
//                selectNumber = 2;
//            }
//            GUI.enabled = GUIState[3];
//            if (GUI.Button(new Rect(buttonleft, 10 + bottonheight * 3, Screen.width / 2, bottonheight), "Third Task", buttonStyle))
//            {
//                selectNumber = 3;
//            }
//            GUI.enabled = GUIState[4];
//            if (GUI.Button(new Rect(buttonleft, 10 + bottonheight * 4, Screen.width / 2, bottonheight), "Forth Task", buttonStyle))
//            {
//                selectNumber = 4;
//            }
//            GUI.enabled = GUIState[5];
//            if (GUI.Button(new Rect(buttonleft, 10 + bottonheight * 5, Screen.width / 2, bottonheight), "Fifth Task", buttonStyle))
//            {
//                selectNumber = 5;

//            }
//            GUI.enabled = GUIState[6];
//            if (GUI.Button(new Rect(Screen.width - 300, Screen.height - bottonheight, 300, bottonheight), "Start", buttonStyle))
//            {
//                PlayerPrefs.SetInt("GameLevel", selectNumber);
//                StateOf = 3;
//            }
//            GUI.enabled = true;
//            if (GUI.Button(new Rect(0, Screen.height - bottonheight, 300, bottonheight), "Back", buttonStyle))
//            {
//                StateOf = 0;
//            }
//        }
//        else
//        {
//            GUI.Label(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 60, 200, 120), "Loading...", buttonStyle);
//            string levelname = "";
//#if UNITY_ANDROID
//            levelname = "sdcard/Download/GameData_level" + PlayerPrefs.GetInt("GameLevel") + "_mode" + PlayerPrefs.GetInt("GameMode") + ".csv";
//#endif
//#if UNITY_EDITOR
//            levelname = "GameData_level" + PlayerPrefs.GetInt("GameLevel") + "_mode" + PlayerPrefs.GetInt("GameMode") + ".csv";
//#endif
//            PlayerPrefs.SetString("GamePath", levelname);
//            Application.LoadLevel(selectNumber + 1);
//        }
//    }
}
