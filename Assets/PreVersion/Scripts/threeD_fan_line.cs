using UnityEngine;
using System.Collections;

public class threeD_fan_line : naviBase {

 float round;
	Vector2[] tmp_linePoints;
	Material[] insideMaterial;
void  Awake (){
	
	line_scale = 0.9f;
	angle_scale = 0.15f;
	line_thickness =0.002f;
	leg_scale=1;
	round=20;
	aa =1;
		mode = 0;
	goal=GameObject.FindGameObjectsWithTag("Finish");
	goal_amount = goal.Length; 
	goalenable =new bool[ goal_amount] ; 
	lineMaterial = new Material[goal_amount];
	insideMaterial = new Material[goal_amount];
	for(int i=0;i<goal_amount;i++)
	{
		goalenable[i]=true;
		lineMaterial[i] = (Material) Resources.Load("MM"+(i+1));
		insideMaterial[i] = (Material) Resources.Load("M"+(i+1));
	}
	
    linePoints = new Vector2[12*goal_amount];    
	tmp_linePoints= new Vector2[3*goal_amount]; 
    lineDrawOn = true;
    
   controller = GameObject.FindGameObjectWithTag("Player");
		mDatac = this.gameObject.GetComponent<DataCollection>();
}
	
	
	
	
	
void  OnPostRender (){

    
    if (!lineDrawOn || linePoints.Length < 2) {return;}
   

    Camera cam= GetComponent<Camera>();

    float nearClip= cam.nearClipPlane+0.1f;

    //lineMaterial.SetPass(0);    

   int enableMount=0;
    for(int n=0;n<goal_amount;n++)
    {
	    if(goalenable[n])
		{
			if(PlayerPrefs.GetInt("GameLevel")==1)
			{
				lineMaterial[n].SetPass(0);
			}
			else
			{
					if(PlayerPrefs.GetInt("GameLevel")==4 && mDatac.PathState == 1)
					{
						lineMaterial[enableMount+1].SetPass(0);
					}
					else{
						lineMaterial[enableMount].SetPass(0);
					}
				
			}
		    for (int k= 0; k < 3; k++) {
				
				GL.Begin(GL.QUADS);
			    
				Vector3 vv= cam.ViewportToWorldPoint(new Vector3(linePoints[4*k+12*n].x, linePoints[4*k+12*n].y, nearClip));
				
				GL.Vertex3(vv.x, vv.y, vv.z);
				
				vv = cam.ViewportToWorldPoint(new Vector3(linePoints[4*k+12*n+1].x, linePoints[4*k+12*n+1].y, nearClip));
				
				GL.Vertex3(vv.x, vv.y, vv.z);
				
				vv = cam.ViewportToWorldPoint(new Vector3(linePoints[4*k+12*n+2].x, linePoints[4*k+12*n+2].y, nearClip));
				
				GL.Vertex3(vv.x, vv.y, vv.z);
				
				vv = cam.ViewportToWorldPoint(new Vector3(linePoints[4*k+12*n+3].x, linePoints[4*k+12*n+3].y, nearClip));
				
				GL.Vertex3(vv.x, vv.y, vv.z);	
			
			    GL.End();
			}
				if(mode==1){
					
					if(PlayerPrefs.GetInt("GameLevel")==1)
					{
						insideMaterial[n].SetPass(0);
					}
					else
					{
						if(PlayerPrefs.GetInt("GameLevel")==4 && mDatac.PathState == 1)
						{
							insideMaterial[enableMount+1].SetPass(0);
						}
						else{
							insideMaterial[enableMount].SetPass(0);
						}
						enableMount++;
					}
					//inside
					
					GL.Begin(GL.TRIANGLES);
					
					Vector3 v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n].x, tmp_linePoints[3*n].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+1].x, tmp_linePoints[3*n+1].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+2].x, tmp_linePoints[3*n+2].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					GL.End();

				}
				else if(mode==2){
					
					if(PlayerPrefs.GetInt("GameLevel")==1)
					{
						//insideMaterial[n].SetPass(0);
					}
					else
					{
						//insideMaterial[enableMount].SetPass(0);
						enableMount++;
					}
					//inside
					GL.Begin(GL.TRIANGLES);
					
					Vector3 v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n].x, tmp_linePoints[3*n].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+1].x, tmp_linePoints[3*n+1].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+2].x, tmp_linePoints[3*n+2].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					GL.End();

				}
				else{
					if(PlayerPrefs.GetInt("GameLevel")==1)
					{
						//insideMaterial[n].SetPass(0);
					}
					else
					{
						//insideMaterial[enableMount].SetPass(0);
						enableMount++;
					}
				}
		}
	}

}

 



 

void  LateUpdate (){
	if(isFan)
		{
			aa=1;
		}
		else
		{
			aa=10000;
		}
  	  for(int j=0;j<goal_amount;j++)
	  {	
	  	if(goalenable[j])
		{
		  Vector2 goalPoint;
		  Vector2 myPoint;
		  
		  goalPoint =new Vector2(goal[j].transform.position.x,goal[j].transform.position.z);
		 
		  myPoint =new Vector2(controller.transform.position.x,controller.transform.position.z);
		  
		  float rotateAngle= this.transform.rotation.eulerAngles.y*3.14f/180;
		  
		  
		  
		 
	
		  
		  float dist=Vector2.Distance(myPoint,goalPoint);
		  
		   dist = line_scale*dist-round;
		   if(dist<=0)
		   {
		   	dist = 0.0f;
		   }
		  ///////
		  float leg= dist + Mathf.Log((dist+20)/12.0f,2.71828182846f)*10.0f;
		  leg = leg * leg_scale;
		  float halfaperture=0.5f*( (5.0f+dist*0.3f)/leg)*angle_scale;
		
		  Vector2 v=myPoint-goalPoint;
		  Vector2 v1 = new Vector2(v.x*Mathf.Cos(halfaperture)-v.y*Mathf.Sin(halfaperture),v.x*Mathf.Sin(halfaperture)+v.y*Mathf.Cos(halfaperture));
		  v1.Normalize();
		  Vector2 v2=new Vector2(v.x*Mathf.Cos(-halfaperture)-v.y*Mathf.Sin(-halfaperture),v.x*Mathf.Sin(-halfaperture)+v.y*Mathf.Cos(-halfaperture));
		
		  
		  v2.Normalize();
		  float n1= leg/Mathf.Sqrt(v1.x*v1.x+v1.y*v1.y);
		  float n2= leg/Mathf.Sqrt(v2.x*v2.x+v2.y*v2.y);
		  Vector2 point1;
		  Vector2 point2;
		  point1 = goalPoint+v1*n1;
		  point2 = goalPoint+v2*n2;
		  
		  Vector2 tmpv1;
		  Vector2 tmpv2;
		  Vector2 tmpvg;
		  tmpv1 =  point1-myPoint;
		  tmpv2 =  point2-myPoint;
		  tmpvg =  goalPoint-myPoint;
		  
		  
	

			Vector3 vg =  GetComponent<Camera>().WorldToScreenPoint(goal[j].transform.position);
			//print(Screen.width+","+Screen.height);
			Vector2 screen_goal = new Vector2(vg.x/Screen.width ,vg.y/Screen.height);
			//print(goal[j].transform.position);
			//print(screen_goal);
			//print(vg);
			Vector3 vp1= GetComponent<Camera>().WorldToScreenPoint(new Vector3(point1.x,0,point1.y));
			Vector2 screen_p1 = new Vector2(vp1.x/GetComponent<Camera>().pixelWidth ,vp1.y/GetComponent<Camera>().pixelHeight);
			//print(screen_p1);
			//print(vp1);
			Vector3 vp2= GetComponent<Camera>().WorldToScreenPoint(new Vector3(point2.x,0,point2.y));
			Vector2 screen_p2 = new Vector2(vp2.x/GetComponent<Camera>().pixelWidth ,vp2.y/GetComponent<Camera>().pixelHeight);	
			//print(screen_p2);
			//print(vp2);
			//print("screen"+camera.pixelWidth+" "+camera.pixelHeight);
			tmpv1.Normalize();
			tmpv2.Normalize();
			float myangle=-halfaperture*2.0f/6;
			//print(myangle);
		   	 
		  	tmp_linePoints[0+3*j] = goalPoint;
		 	tmp_linePoints[1+3*j] = point1;
		  	tmp_linePoints[2+3*j] = point2;	
			for(int i=0;i<3;i++)
			{
				Vector3 tmpp= GetComponent<Camera>().WorldToScreenPoint(new Vector3(tmp_linePoints[i+3*j].x,0,tmp_linePoints[i+3*j].y));
				tmp_linePoints[i+3*j] = new Vector2(tmpp.x/GetComponent<Camera>().pixelWidth ,tmpp.y/GetComponent<Camera>().pixelHeight);
					//print ("p "+i+" " +tmp_linePoints[i].ToString("f6") );
			}
			//print ("sg "+screen_goal.ToString("f6"));
			//	print ("sp1 "+screen_p1.ToString("f6"));
			//	print ("sp2 "+screen_p2.ToString("f6"));
			if(vg.z<0&&vp1.z<0&&vp2.z<0 || dist==0 || screen_goal.x<0 || screen_goal.y<0 || screen_p1.x<0 || screen_p1.y<0 || screen_p2.x<0 || screen_p2.y<0|| screen_goal.x>1 || screen_goal.y>1 || screen_p1.x>1 || screen_p1.y>1 || screen_p2.x>1 || screen_p2.y>1)
			{
				for(int i=0;i<12;i++)
				{
					linePoints[i+12*j] = new Vector2(-1.0f,-1.0f);
				}
				for(int i=0;i<3;i++)
				{
					tmp_linePoints[i+3*j] = new Vector2(-1.0f,-1.0f);
				}
				
			}
			else
			{
				Vector2 tmp_vector;	
				for(int i=0;i<2;i++)
				{

						tmp_vector = tmp_linePoints[i+3*j] - tmp_linePoints[i+1+3*j];
						tmp_vector =new Vector2(tmp_vector.y,-tmp_vector.x);
						tmp_vector.Normalize();
						linePoints[0+4*i+12*j] =  tmp_linePoints[i+3*j] + line_thickness*tmp_vector;
						linePoints[1+4*i+12*j] =  tmp_linePoints[i+1+3*j] + line_thickness*tmp_vector;
						linePoints[2+4*i+12*j] =  tmp_linePoints[i+1+3*j] - line_thickness*tmp_vector;
						linePoints[3+4*i+12*j] =  tmp_linePoints[i+3*j] - line_thickness*tmp_vector;
				}
				
				tmp_vector = screen_goal - screen_p2;
				tmp_vector =new Vector2(tmp_vector.y,-tmp_vector.x);
				tmp_vector.Normalize();
				
				linePoints[8+12*j] = screen_goal + line_thickness*tmp_vector;
				linePoints[9+12*j] = screen_p2 + line_thickness*tmp_vector;
				linePoints[10+12*j] = screen_p2 - line_thickness*tmp_vector;
				linePoints[11+12*j] = screen_goal - line_thickness*tmp_vector;	
					
			}
		}	
			
	//		linePoints[3+4*j] = Vector2((goalPoint.x-myPoint.x+80)/160.0f,(goalPoint.y-myPoint.y+50)/100.0f);
	  }  
  //print(controller.transform.rotation.eulerAngles.y);

}
}
