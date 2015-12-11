using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataCollection : MonoBehaviour
{
    public bool StopGame = false;
    public string[] endName;
    public int retryNumber = 0;
    public int returnNumber = 0;
    GameObject[] goals;
    [HideInInspector]
    public float sumOfPath = 0.0f;
    [HideInInspector]
    public float startTime = 0.0f;
    float targetLength = 5.0f;
    int countdown = 0;
    public GameObject twoDCompus;
    public GameObject threeDCompus;
    List<GameObject> redtarget = new List<GameObject>();
    GameObject player;
    public Vector3 lastPos;
    public int PathState = 0;
    static string levelname;
    public string seconedinit;

    private naviBase drawer;


    // Use this for initialization
    void Awake()
    {
        switch (PlayerPrefs.GetInt("GameMode"))
        {
            case 0:
                drawer = this.gameObject.AddComponent<twoD_fan_line>();
                threeDCompus.SetActive(false);
                drawer.mode = 0;
                break;
            case 1:
                drawer = this.gameObject.AddComponent<twoD_fan_line>();
                threeDCompus.SetActive(false);
                drawer.mode = 1;
                break;
            case 2:
                drawer = this.gameObject.AddComponent<twoD_fan_line>();
                threeDCompus.SetActive(false);
                drawer.mode = 2;
                break;
            case 3:
                drawer = this.gameObject.AddComponent<threeD_fan_line>();
                twoDCompus.SetActive(false);
                drawer.mode = 0;
                break;
            case 4:
                drawer = this.gameObject.AddComponent<threeD_fan_line>();
                twoDCompus.SetActive(false);
                drawer.mode = 1;
                break;
            case 5:
                drawer = this.gameObject.AddComponent<threeD_fan_line>();
                twoDCompus.SetActive(false);
                drawer.mode = 2;
                break;


            case 6:
                drawer = this.gameObject.AddComponent<twoD_fan_line>();
                threeDCompus.SetActive(false);
                drawer.mode = 0;

                break;
            case 7:
                drawer = this.gameObject.AddComponent<twoD_fan_line>();
                threeDCompus.SetActive(false);
                drawer.mode = 1;
                break;
            case 8:
                drawer = this.gameObject.AddComponent<twoD_fan_line>();
                threeDCompus.SetActive(false);
                drawer.mode = 2;
                break;
            case 9:
                drawer = this.gameObject.AddComponent<threeD_fan_line>();
                twoDCompus.SetActive(false);
                drawer.mode = 0;
                break;
            case 10:
                drawer = this.gameObject.AddComponent<threeD_fan_line>();
                twoDCompus.SetActive(false);
                drawer.mode = 1;
                break;
            case 11:
                drawer = this.gameObject.AddComponent<threeD_fan_line>();
                twoDCompus.SetActive(false);
                drawer.mode = 2;
                break;
        }

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPos = player.transform.position;
        goals = GameObject.FindGameObjectsWithTag("Finish");
        seconedinit = endName[0];
        levelname = PlayerPrefs.GetString("GamePath");

        File.WriteAllText(levelname, "Level," + (PlayerPrefs.GetInt("GameLevel")) + "\r\n");
        Debug.Log(PlayerPrefs.GetInt("GameLevel").ToString());

        for (int i = 0; i < goals.Length; i++)
        {
            redtarget.Add(Instantiate(Resources.Load("CylinderSinple"), goals[i].transform.position + new Vector3(0.0f, 0.1f, 0.0f), goals[i].transform.rotation) as GameObject);
        }


        switch (PlayerPrefs.GetInt("GameLevel"))
        {

            case 4:
                for (int i = 0; i < goals.Length; i++)
                {
                    if ((int.Parse(goals[i].name) - 29) >= 0)
                    {
                        drawer.goalenable[i] = false;
                        redtarget[i].GetComponent<Renderer>().enabled = false;
                        foreach (Transform tmpgg in goals[i].GetComponentsInChildren<Transform>())
                        {
                            tmpgg.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }
                break;
            case 5:
                for (int i = 0; i < goals.Length; i++)
                {
                    if ((int.Parse(goals[i].name) - 40) >= 0)
                    {
                        drawer.goalenable[i] = false;
                        redtarget[i].GetComponent<Renderer>().enabled = false;
                        foreach (Transform tmpgg in goals[i].GetComponentsInChildren<Transform>())
                        {
                            tmpgg.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }
                break;
        }

    }
    void FixedUpdate()
    {
        Vector3 nowPos = player.transform.position;
        if (!StopGame)
        {
            countdown--;
            if (lastPos != nowPos && countdown <= 0)
            {
                sumOfPath += Vector3.Distance(nowPos, lastPos) / 2.0f;
                File.AppendAllText(levelname, "" + nowPos.x + " , " + nowPos.z + "\r\n");
                countdown = 30;
                lastPos = nowPos;
            }
            switch (PlayerPrefs.GetInt("GameLevel"))
            {
                case 0:
                    mode_zero(nowPos);
                    break;
                case 1:
                    mode_two(nowPos);
                    //mode_one(nowPos);
                    break;
                case 2:
                    mode_five(nowPos);
                    //mode_one(nowPos);
                    break;
                case 3:
                    mode_five(nowPos);
                    //mode_two(nowPos);
                    break;
                case 4:
                    mode_three(nowPos);
                    //mode_five( nowPos);
                    break;
                case 5:
                    mode_three(nowPos);
                    //mode_five( nowPos);
                    break;
                /*case 6:
                    mode_three(nowPos);
                break;
                case 7:
                     mode_three(nowPos);
                break;
                case 8:
                    mode_four(nowPos);
                break;*/
            }


        }

    }
    void mode_zero(Vector3 nowPos)
    {
        foreach (GameObject theGaol in goals)
        {
            float length = Vector3.Distance(theGaol.transform.position, nowPos);
            if (length <= targetLength)
            {
                if (string.Compare(theGaol.name, endName[0].ToString()) == 0)
                {
                    StopGame = true;
                    Debug.Log("Finish Game");
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "retry Number," + retryNumber + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n");

                    StopGame = true;

                }
                else
                {
                    player.transform.position = GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 0);
                    File.AppendAllText(levelname, "sum of Path : " + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    retryNumber++;
                    sumOfPath = 0.0f;

                }

            }
        }
    }
    //0 to A
    void mode_one(Vector3 nowPos)
    {
        foreach (GameObject theGaol in goals)
        {
            float length = Vector3.Distance(theGaol.transform.position, nowPos);
            if (length <= targetLength)
            {
                if (string.Compare(theGaol.name, endName[0].ToString()) == 0)
                {
                    StopGame = true;
                    Debug.Log("Finish Game");
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "retry Number," + retryNumber + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n");

                    StopGame = true;

                }
                else
                {
                    player.transform.position = GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 0);
                    File.AppendAllText(levelname, "sum of Path : " + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    retryNumber++;
                    sumOfPath = 0.0f;

                }

            }
        }
    }
    // 0 to A+B
    void mode_two(Vector3 nowPos)
    {
        int k = 0;
        foreach (GameObject theGaol in goals)
        {
            float length = Vector3.Distance(theGaol.transform.position, nowPos);

            if (length <= targetLength && drawer.goalenable[k])
            {

                if (string.Compare(theGaol.name, endName[1].ToString()) == 0 && PathState == 1)
                {
                    StopGame = true;
                    Debug.Log("Finish Game");
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "retry Number," + retryNumber + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n");

                    StopGame = true;
                    break;

                }
                if (string.Compare(theGaol.name, endName[0].ToString()) == 0 && PathState == 0)
                {
                    //player.transform.position=GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    PathState = 1;
                    endName[0] = "";

                    drawer.goalenable[k] = false;
                    redtarget[k].GetComponent<Renderer>().enabled = false;
                    Transform[] tmpGame = goals[k].GetComponentsInChildren<Transform>();
                    foreach (Transform tmp in tmpGame)
                    {
                        tmp.GetComponent<Renderer>().enabled = !tmp.GetComponent<Renderer>().enabled;
                    }
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    sumOfPath = 0.0f;
                    break;

                }
                else
                {

                    player.transform.position = GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 1);
                    File.AppendAllText(levelname, "sum of Path : " + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    retryNumber++;
                    sumOfPath = 0.0f;

                }

            }
            k++;
        }
    }
    //0 to A to B
    void mode_three(Vector3 nowPos)
    {
        int k = 0;
        foreach (GameObject theGaol in goals)
        {
            float length = Vector3.Distance(theGaol.transform.position, nowPos);

            if (length <= targetLength && drawer.goalenable[k])
            {
                if (string.Compare(theGaol.name, endName[0].ToString()) == 0 && PathState == 0)
                {
                    endName[0] = "";
                    PathState++;
                    Debug.Log(PathState);
                    for (int j = 0; j < goals.Length; j++)
                    {
                        drawer.goalenable[j] = !drawer.goalenable[j];
                        redtarget[j].GetComponent<Renderer>().enabled = !redtarget[j].GetComponent<Renderer>().enabled;
                        Transform[] tmpGame = goals[j].GetComponentsInChildren<Transform>();
                        foreach (Transform tmp in tmpGame)
                        {
                            tmp.GetComponent<Renderer>().enabled = !tmp.GetComponent<Renderer>().enabled;
                        }

                    }
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    sumOfPath = 0.0f;
                    this.GetComponent<GUIController>().isGo = false;
                    break;

                }
                else if (string.Compare(theGaol.name, endName[1].ToString()) == 0 && PathState == 1)
                {
                    StopGame = true;
                    Debug.Log("Finish Game");
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "retry Number," + retryNumber + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n");

                    StopGame = true;
                    break;

                }
                else
                {
                    if (PathState == 0)
                        player.transform.position = GameObject.Find("00.Start").transform.position;
                    else
                        player.transform.position = GameObject.Find(seconedinit).transform.position;

                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 1);
                    File.AppendAllText(levelname, "sum of Path : " + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    retryNumber++;
                    sumOfPath = 0.0f;
                }
            }
            k++;
        }
    }
    void mode_four(Vector3 nowPos)
    {
        int k = 0;
        foreach (GameObject theGaol in goals)
        {
            float length = Vector3.Distance(theGaol.transform.position, nowPos);

            if (length <= targetLength && drawer.goalenable[k])
            {
                PathState++;
                drawer.goalenable[k] = false;
                redtarget[k].GetComponent<Renderer>().enabled = false;
                File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                sumOfPath = 0.0f;
                break;

            }

            k++;
        }
        if (PathState == 6)
        {
            StopGame = true;
            Debug.Log("Finish Game");
            File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
            File.AppendAllText(levelname, "retry Number," + retryNumber + "\r\n");
            File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n");


            StopGame = true;

        }
    }
    void mode_five(Vector3 nowPos)
    {
        int k = 0;
        foreach (GameObject theGaol in goals)
        {
            float length = Vector3.Distance(theGaol.transform.position, nowPos);

            if (length <= targetLength)
            {
                bool first = string.Compare(theGaol.name, endName[0].ToString()) == 0;
                bool second = string.Compare(theGaol.name, endName[1].ToString()) == 0;
                bool third = string.Compare(theGaol.name, endName[2].ToString()) == 0;
                if (string.Compare(theGaol.name, endName[3].ToString()) == 0 && PathState == 3)
                {
                    StopGame = true;
                    Debug.Log("Finish Game");
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "retry Number," + retryNumber + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n");

                    StopGame = true;
                    break;

                }
                else if ((first || second || third))
                {
                    player.transform.position = GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 0);
                    PathState++;
                    if (first)
                    {
                        endName[0] = "";
                        Debug.Log("1");
                    }
                    else if (second)
                    {
                        endName[1] = "";
                        Debug.Log("2");
                    }
                    else if (third)
                    {
                        endName[2] = "";
                        Debug.Log("3");
                    }
                    player.transform.position = GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 1);
                    File.AppendAllText(levelname, "Stage sum of Path," + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    sumOfPath = 0.0f;
                    break;

                }
                else
                {
                    player.transform.position = GameObject.Find("00.Start").transform.position;
                    lastPos = player.transform.position;
                    player.transform.rotation = new Quaternion(0, 0, 0, 1);
                    File.AppendAllText(levelname, "sum of Path : " + sumOfPath + "\r\n");
                    File.AppendAllText(levelname, "Toatal Time," + (int)(Time.time - startTime) + "\r\n \r\n \r\n");
                    retryNumber++;
                    sumOfPath = 0.0f;
                }
            }
            k++;
        }
    }

}
