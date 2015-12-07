using UnityEngine;
using System.Collections;

public class naviBase : MonoBehaviour {
	protected Vector2[] linePoints;
	protected Vector2[] m_linePoints;
	public GameObject[] goal; 
	
	public bool[ ] goalenable;
	protected bool lineDrawOn= false;

	public GameObject controller;
	
	public float scale;
	public float line_scale;
	
	public float angle_scale;
	public float line_thickness;
	public float leg_scale;
	protected Camera cam;
	
	protected int goal_amount;
	
	 protected Material[] lineMaterial;
	 protected int GameState =0;
	 public float aa;
	public bool isFan = true;
	public int mode;
	public DataCollection mDatac;
	
	// Use this for initialization
	
}
