using UnityEngine;
using System.Collections;

public class campus: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles =new Vector3(0,-Camera.main.transform.eulerAngles.y,0);
		print ("cam"+Camera.main.transform.rotation);
		print (this.gameObject.name+" "+ this.gameObject.transform.rotation);
	}
}
