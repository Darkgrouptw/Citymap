using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class MiniMapSetting : MonoBehaviour 
{
    private GameObject      Male;


    //For Case 1 & 3
    private GameObject      MiniMap;                //地圖
    //For Case All
    private GameObject      MiniPerson;             //小地圖裡的方向
    //For Case 5
    private GameObject      MapBorder;          //地圖邊界

    private Vector3         MapSize = new Vector3(1134, 0, 1120);
    private Vector3         MiniMapSize;
    private const float     MiniMapScale = 2f;


    //地圖 2D UI
    private float UIArrowScale = 0.9f;
    void Awake()
    {
        //拿人物的座標
        Male = GameObject.Find("Male");

        MiniMap = GameObject.Find("MiniMap-Map");
        MiniPerson = GameObject.Find("MiniMap-Person");
        MapBorder = GameObject.Find("Map Border");
        switch(PlayerPrefs.GetInt("GameMode"))
        {
            case 0:
            case 2:
                MiniMap.SetActive(true);
                Vector2 TempPos = MiniMap.GetComponent<RectTransform>().rect.size;
                MiniMapSize = new Vector3(TempPos.x, 0, TempPos.y);
                break;
            case 1:
            case 3:
            case 4:
                MiniMap.SetActive(false);
                break;
            case 5:
                MapBorder.SetActive(false);
                MiniMap.SetActive(false);
                MiniPerson.SetActive(false);
                GameObject.Find("Map Area").GetComponent<Image>().enabled = false;
                this.enabled = false;
                break;
        }
    }

    void Update()
    {
        switch (PlayerPrefs.GetInt("GameMode"))
        {
            case 0:
            case 2:
                //位置地圖
                MiniMap.GetComponent<RectTransform>().localPosition =
                    new Vector3(CountToMiniMapX(Male.transform.position.x),
                        CountToMiniMapZ(Male.transform.position.z), 0);
                break;
        }
        //小地圖裡的人物要動
        MiniPerson.transform.localRotation = Quaternion.Euler(0, 0, -Male.transform.localEulerAngles.y);
    }

    // X,Z 座標轉到 小地圖的座標
    private float CountToMiniMapX(float PosX)                   
    {
        return (0.5f - PosX / MapSize.x) * MiniMapSize.x * MiniMapScale;
    }
    private float CountToMiniMapZ(float PosZ)
    {
        return (0.5f - PosZ / MapSize.z) * MiniMapSize.z * MiniMapScale;
    }
}
