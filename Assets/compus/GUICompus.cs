using UnityEngine;
using System.Collections;

public class GUICompus : MonoBehaviour {
	public Texture RoundImage;
	public Texture arrorImage;
	float rotationAngle;
	int textureSize;
	// Use this for initialization
	void Start () {
		rotationAngle = 0;
		textureSize = Screen.width/5;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnGUI(){
		GUI.DrawTexture(new Rect(0, 2*Screen.height/3, textureSize, textureSize), RoundImage);


		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		GUIUtility.RotateAroundPivot(-Camera.main.transform.eulerAngles.y , new Vector2(textureSize/2, textureSize/2 + 2*Screen.height/3));
		GUI.DrawTexture(new Rect(0, 2*Screen.height/3, textureSize, textureSize), arrorImage);
		GUI.EndGroup();
	}
}
