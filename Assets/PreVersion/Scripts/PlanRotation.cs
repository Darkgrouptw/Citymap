using UnityEngine;
using System.Collections;

public class PlanRotation : MonoBehaviour {
	Quaternion preRotation;
	GameObject camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");		
	}
	
	// Update is called once per frame
	void Update () {		
		Vector3 tmp=camera.transform.position-this.transform.position;
		transform.rotation = Quaternion.LookRotation(tmp);
		this.transform.Rotate(new Vector3(90.0f,0.0f,0.0f)) ;		
	}
}
