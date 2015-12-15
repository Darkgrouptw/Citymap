using UnityEngine;
using System.Collections;
using System.IO;

public class GUIController : MonoBehaviour {
	public float Vertical;
	public float Horizontal;
	public bool runOrWalk;   //true:Run  false:Walk
	public bool GameStart;
	DataCollection collector;
	Texture2D taskContext;
	Texture2D gogo;
	Texture2D[] finishTag;
	Texture2D[] controltext;
	private  Animation _animation;
	GameObject player;
	int pressState=0;
	public bool isGo = false;
	float exitpresstime=0.0f;
	ThirdPersonController ThirdController;

	// Use this for initialization
	void Start () {	
		player=GameObject.FindGameObjectWithTag("Player");
		ThirdController=player.GetComponent<ThirdPersonController>();
		_animation=player.GetComponent<Animation>();
		ThirdController.enabled=false;
		collector = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DataCollection>();
		finishTag = new Texture2D[4];
		finishTag[0] = (Texture2D)Resources.Load("map_55_1_2");
		finishTag[1] = (Texture2D)Resources.Load("map_55_1_4");
		finishTag[2] = (Texture2D)Resources.Load("map_55_2_4");
		finishTag[3] = (Texture2D)Resources.Load("map_55_3_4");

		gogo = (Texture2D)Resources.Load("map_GO");

		switch(PlayerPrefs.GetInt("GameLevel"))
		{
		    case 0:
				switch(PlayerPrefs.GetInt("GameMode")%6)
				{
					case 0:		
						taskContext=(Texture2D)Resources.Load("mission_touch_wedge01_1");
						break;	
					case 1:						
					case 2:
						taskContext=(Texture2D)Resources.Load("mission_touch_wedge02_1");
					break;
					case 3:	
						taskContext=(Texture2D)Resources.Load("mission_touch_wedge04_1");
						break;	
					case 4:						
					case 5:
						taskContext=(Texture2D)Resources.Load("mission_touch_wedge03_1");
						break;					
				}
		    break;
		    case 1:
		    	taskContext = (Texture2D)Resources.Load("mission_m1");
		    break;
		    case 2:
		    	taskContext = (Texture2D)Resources.Load("mission_m2");
		    break;
		    case 3:
		        taskContext = (Texture2D)Resources.Load("mission_m3");
		    break;
			case 4:
				taskContext = (Texture2D)Resources.Load("mission_m4");
		    break;
			case 5:
				taskContext = (Texture2D)Resources.Load("mission_m5");
		    break;
		}
		runOrWalk = true;	
		GameStart=false;
		Vertical=0.0f;
		
	}	
	void Update() {
		
		if(Vertical>0.0f)
		{
			Vertical--;
		}
		if(Horizontal>0.0f)
		{
			Horizontal--;
		}
		if(Horizontal<0.0f)
		{
			Horizontal++;
		}
		
		if(!collector.StopGame)
		{
			if(GameStart)
			{
				#if UNITY_ANDROID
                bool isMoving = false;
                if (Input.acceleration.y > -0.85f && isGo)
                {
                    Vector3 tmpfroward = this.transform.forward;
                    CharacterController controller = player.GetComponent<CharacterController>();
                    controller.SimpleMove(tmpfroward * 10);
                    player.GetComponent<Animation>()["walk"].speed = 3.0f;
                    _animation.Play("walk", PlayMode.StopAll);
                    isMoving = true;

                }
                if (Input.acceleration.x > 0.1f)
                {
                    isMoving = true;
                    player.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
                }
                if (Input.acceleration.x < -0.1f)
                {
                    isMoving = true;
                    player.transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f));
                }
                if (!isMoving)
                {
                    _animation.Play("idle1", PlayMode.StopAll);
                }			 
				#endif

				#if UNITY_EDITOR					
					if(Input.GetKey(KeyCode.W) && isGo)
					{					
						//Vertical=1.0f;
						Vector3 tmpfroward=this.transform.forward;
						CharacterController controller  = player.GetComponent<CharacterController>();
						controller.SimpleMove(tmpfroward*10 );
						player.GetComponent<Animation>()["walk"].speed=3.0f;
						_animation.Play("walk", PlayMode.StopAll);
						
					}
					else if(Input.GetKey(KeyCode.D))
					{
						player.transform.Rotate(new Vector3(0.0f,3.0f,0.0f));
					}
					else if(Input.GetKey(KeyCode.A))
					{
						player.transform.Rotate(new Vector3(0.0f,-3.0f,0.0f));
					}
					else
					{
						_animation.Play("idle1", PlayMode.StopAll);
					}
				#endif
			
			}
		}
	}
	
	 void OnGUI() {		
		if(!GameStart)
		{
			GUIStyle style=new GUIStyle();
			if(PlayerPrefs.GetInt("GameLevel")==0)
			{
				
				style.normal.background= taskContext;
				style.hover.background = taskContext;
				switch(pressState)
				{
					case 0:				
						if (GUI.Button(new Rect(10, 10, Screen.width-20.0f, Screen.height-20.0f), "",style))
						{
							switch(PlayerPrefs.GetInt("GameMode")/6)
							{
								case 0:
									switch(PlayerPrefs.GetInt("GameMode")%3)
									{
										case 0:
											taskContext=(Texture2D)Resources.Load("mission_touch_wedge01_2");
											break;
										case 1:
										case 2:
											taskContext=(Texture2D)Resources.Load("mission_touch_wedge02_2");
											break;
									}
								break;
								case 1:
									taskContext=(Texture2D)Resources.Load("mission_body_control_2");
								break;
							}
							pressState=1;
						}
						break;
					case 1:
						if (GUI.Button(new Rect(10, 10, Screen.width-20.0f, Screen.height-20.0f), "",style))
						{
							switch(PlayerPrefs.GetInt("GameMode")%3)
							{
								case 0:
									taskContext=(Texture2D)Resources.Load("mission_touch_wedge01_3");
									break;
								case 1:
								case 2:
									taskContext=(Texture2D)Resources.Load("mission_touch_wedge02_3");
									break;								
							}
							pressState=2;
							
						}
						break;
					case 2:
						if (GUI.Button(new Rect(10, 10, Screen.width-20.0f, Screen.height-20.0f), "",style))
						{
							GameStart=true;	
							this.GetComponent<DataCollection>().startTime=Time.time;
							
						}
						break;
				}
				
				
			}
			else
			{
				style.normal.background=taskContext;
				style.hover.background = taskContext;
				if (GUI.Button(new Rect(10, 10, Screen.width-20.0f, Screen.height-20.0f), "",style))
				{
					 GameStart=true;	
					this.GetComponent<DataCollection>().startTime=Time.time;
					
				}
			}
			
		}
		else
		{
			GUI.Label(new Rect(10, 10, 80, 50),""+(int) collector.PathState);
			if (GUI.RepeatButton(new Rect(Screen.width- 150, Screen.height- 100, 120, 70), "",GUIStyle.none))
			{
				exitpresstime += Time.deltaTime;
				if(exitpresstime>5.0f)
					Application.LoadLevel(0);
			}
			if(Input.touchCount==0)
			{
				exitpresstime = 0.0f;
			}
			if((Time.time - collector.startTime) > 2 && !isGo)
			{
				if (GUI.Button(new Rect(Screen.width/2- 185, Screen.height/2+50, 370, 100), gogo))
				{
					isGo = true;
					File.AppendAllText(PlayerPrefs.GetString("GamePath"),"GO Time,"+(Time.time - collector.startTime)+"\r\n\r\n");	


				}
			}
		
			if(!collector.StopGame)
			{
				if(collector.PathState>0)
				{
					if(PlayerPrefs.GetInt("GameLevel")==2 || PlayerPrefs.GetInt("GameLevel")==3)
						GUI.Label(new Rect(Screen.width- 100, Screen.height- 50, 100, 50),finishTag[collector.PathState]);
					else
						GUI.Label(new Rect(Screen.width- 100, Screen.height- 50, 100, 50),finishTag[0]);
				}
                GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.fontSize = 25;
                if (GUI.Button(new Rect(Screen.width * 0.9f, 10, Screen.width * 0.1f, Screen.height * 0.1f), "Return", buttonStyle))
		        {
					collector.returnNumber++;
					if(collector.PathState==0)
						player.transform.position=GameObject.Find("00.Start").transform.position;
					else 
					{
						if(PlayerPrefs.GetInt("GameLevel")!=1)
						{
							player.transform.position=GameObject.Find(collector.seconedinit).transform.position;
						}
						else
						{
							player.transform.position=GameObject.Find("00.Start").transform.position;
						}
					}
					
					collector.lastPos=player.transform.position;
					player.transform.rotation=new Quaternion(0,0,0,1);
					File.AppendAllText(PlayerPrefs.GetString("GamePath"),"Return sum of Path : "+collector.sumOfPath+"\r\n");	
					File.AppendAllText(PlayerPrefs.GetString("GamePath"),"Toatal Time : "+(int)(Time.time-collector.startTime)+"\r\n \r\n \r\n");
					collector.sumOfPath=0.0f;
					collector.retryNumber++;
					
				}
				
				
			}
			else
			{
				GUIStyle style=new GUIStyle();
				Texture2D btnTexture=(Texture2D)Resources.Load("map_55");
				style.normal.background=btnTexture;
				style.hover.background = btnTexture;
				if(GUI.Button(new Rect((Screen.width/2)-150, (Screen.height/2)-50, 300, 100),"",style))
				{					
						Application.LoadLevel(0);				
				}	
			}
			
			
			GUI.Label(new Rect(10, Screen.height-50, 80, 50),""+(int) collector.sumOfPath);
			GUIStyle mystyle=new GUIStyle();
			mystyle.fontSize=60;
			mystyle.normal.textColor=Color.blue;
			int delataTime=(int)(Time.time-collector.startTime);
			GUI.Label(new Rect(Screen.width/2-40, 10, 80, 50),delataTime/60+" : "+string.Format("{0:00}",delataTime%60) ,mystyle);        
			
			if(PlayerPrefs.GetInt("GameLevel")==0)
			{
				if(delataTime>=20.0f)
				{
					GUIStyle buttonStyle=new GUIStyle(GUI.skin.button);
					buttonStyle.fontSize=25;
					if(GUI.Button(new Rect(Screen.width*0.85f, Screen.height*0.85f, Screen.width * 0.15f, Screen.height * 0.15f), "Practice Finish",buttonStyle))
					{
						
						Application.LoadLevel(0);
					}
				}
			}
			
		}
		
		
    }
}
