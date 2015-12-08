using UnityEngine;
using System.Collections;

public class threeD_fan_full : naviBase {
	int round;
	DataCollection mDatac;
void  OnPostRender (){

    
    if (!lineDrawOn || linePoints.Length < 2) {return;}
   

    Camera cam= GetComponent<Camera>();

    float nearClip= cam.nearClipPlane+0.01f;
  

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
				lineMaterial[enableMount].SetPass(0);
				enableMount++;
			}
				if(PlayerPrefs.GetInt("GameLevel")==4 && mDatac.PathState==1)
			{
				lineMaterial[n+1].SetPass(0);
			}
			   for (int a=1;a<6;a++)
			  {
				   	GL.Begin(GL.TRIANGLES);
				   	Vector3 v= cam.ViewportToWorldPoint(new Vector3(m_linePoints[0+n*8].x,m_linePoints[0+n*8].y, nearClip));
					GL.Vertex3(v.x, v.y, v.z);
					v = cam.ViewportToWorldPoint(new Vector3(m_linePoints[a+n*8].x,m_linePoints[a+n*8].y, nearClip));
					GL.Vertex3(v.x, v.y, v.z);
					v = cam.ViewportToWorldPoint(new Vector3(m_linePoints[a+1+n*8].x,m_linePoints[a+1+n*8].y, nearClip));
					GL.Vertex3(v.x, v.y, v.z);
					GL.End();
			  } 

			
			   
			
		}
	}

}

 


void  Awake (){
	aa=100.0f;
	line_scale = 0.9f;
	angle_scale = 0.15f;
	line_thickness =0.002f;
	leg_scale=1;
	round=20;

	goal=GameObject.FindGameObjectsWithTag("Finish");
	goal_amount = goal.Length; 
	goalenable =new bool[ goal_amount] ; 
	lineMaterial = new Material[goal_amount];
	for(int i=0;i<goal_amount;i++)
	{
		goalenable[i]=true;
		lineMaterial[i] = (Material) Resources.Load("M"+(i+1));
	}
	
    linePoints = new Vector2[12*goal_amount];    
    m_linePoints = new Vector2[8*goal_amount];  
    lineDrawOn = true;
    
   controller = GameObject.FindGameObjectWithTag("Player");
		mDatac = this.gameObject.GetComponent<DataCollection>();
}

 

void  LateUpdate (){
	
		if(isFan)
		{aa=100.0f;}
		else
		{aa=1000000;}
  	  for(int j=0;j<goal_amount;j++)
	  {	
	  	if(goalenable[j])
		{
		  Vector2 goalPoint;
		  Vector2 myPoint;
		  
		  goalPoint =new Vector2(goal[j].transform.position.x,goal[j].transform.position.z);
		 
		  myPoint =new Vector2(controller.transform.position.x,controller.transform.position.z);
		  
		  float rotateAngle= this.transform.rotation.eulerAngles.y*3.14f/180;
		  
		  
		  
		 
	
		  
		  float dist= Vector2.Distance(myPoint,goalPoint);
		  
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
		  Vector2 v1=new Vector2(v.x*Mathf.Cos(halfaperture)-v.y*Mathf.Sin(halfaperture),v.x*Mathf.Sin(halfaperture)+v.y*Mathf.Cos(halfaperture));
		  v1.Normalize();
		  Vector2 v2= new Vector2(v.x*Mathf.Cos(-halfaperture)-v.y*Mathf.Sin(-halfaperture),v.x*Mathf.Sin(-halfaperture)+v.y*Mathf.Cos(-halfaperture));
		 
		  
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
		  tmpv1 =  point1-goalPoint;
		  tmpv2 =  point2-goalPoint;
		 
		  
		  Vector2 ntmpv1= tmpv1;
		  tmpv1.Normalize();
		 
		  float myangle= -(halfaperture*2.0f)/8.0f;

		   Vector2[] tmp_linePoints= new Vector2[8*goal_amount];  
		  tmp_linePoints[0] = goalPoint;
		  tmp_linePoints[1] = point1;
		  for(int i=2;i<7;i++)
		  {
		  	if(i<4)
		  	{
				Vector2 tmpp =new  Vector2( tmpv1.x*Mathf.Cos(myangle*(i-1)) - tmpv1.y*Mathf.Sin(myangle*(i-1)) , tmpv1.y*Mathf.Cos(myangle*(i-1)) + tmpv1.x*Mathf.Sin(myangle*(i-1)) );
		  		tmp_linePoints[i] =goalPoint +(n1+(i%4)*(dist/(dist+aa))) * tmpp;
		  	}
		  	else
		  	{
				Vector2 tmpp =new Vector2( tmpv1.x*Mathf.Cos(myangle*(i-1)) - tmpv1.y*Mathf.Sin(myangle*(i-1)) , tmpv1.y*Mathf.Cos(myangle*(i-1)) + tmpv1.x*Mathf.Sin(myangle*(i-1)) );
		  		tmp_linePoints[i] =goalPoint +(n1+(7-i)*(dist/(dist+aa))) * tmpp;
		  	}
		  	//tmp_linePoints[i] = point1;
		  }
		  tmp_linePoints[7] = point2;
		  
			//print(Screen.width+","+Screen.height);
			 
		  
		  Vector3 vg =  GetComponent<Camera>().WorldToScreenPoint(goal[j].transform.position);
		  m_linePoints[j*8] =new Vector2(vg.x/Screen.width ,vg.y/Screen.height);
		   //print(Mathf.Cos(180.0f));
		 // print("0 "+tmp_linePoints[j*8]);
				Vector3 vp1;
		  for(int jj=1;jj<8;jj++)
		  {
		  //	print(jj + " " +tmp_linePoints[jj]);
		  	vp1= GetComponent<Camera>().WorldToScreenPoint(new Vector3(tmp_linePoints[jj].x,0,tmp_linePoints[jj].y));
			m_linePoints[j*8+jj] =new Vector2(vp1.x/GetComponent<Camera>().pixelWidth ,vp1.y/GetComponent<Camera>().pixelHeight);
			
		  }
	

			vg =  GetComponent<Camera>().WorldToScreenPoint(goal[j].transform.position);
			//print(Screen.width+","+Screen.height);
			Vector2 screen_goal = new Vector2(vg.x/Screen.width ,vg.y/Screen.height);
			//print(goal[j].transform.position);
			//print(screen_goal);
			//print(vg);
			vp1 = GetComponent<Camera>().WorldToScreenPoint(new Vector3(point1.x,0,point1.y));
			Vector2 screen_p1 = new Vector2(vp1.x/GetComponent<Camera>().pixelWidth ,vp1.y/GetComponent<Camera>().pixelHeight);
			//print(screen_p1);
			//print(vp1);
			Vector3 vp2= GetComponent<Camera>().WorldToScreenPoint(new Vector3(point2.x,0,point2.y));
			Vector2 screen_p2 = new Vector2(vp2.x/GetComponent<Camera>().pixelWidth ,vp2.y/GetComponent<Camera>().pixelHeight);	
			//print(screen_p2);
			//print(vp2);
			//print("screen"+camera.pixelWidth+" "+camera.pixelHeight);
			
			if(vg.z<0&&vp1.z<0&&vp2.z<0 || dist==0 || screen_goal.x<0 || screen_goal.y<0 || screen_p1.x<0 || screen_p1.y<0 || screen_p2.x<0 || screen_p2.y<0|| screen_goal.x>1 || screen_goal.y>1 || screen_p1.x>1 || screen_p1.y>1 || screen_p2.x>1 || screen_p2.y>1)
			{
				for(int i=0;i<12;i++)
				{
					linePoints[i+12*j] = new Vector2(-1.0f,-1.0f);
				}
				for(int i=0;i<8;i++)
				{
					m_linePoints[i+8*j] = new Vector2(-1.0f,-1.0f);
				}
			}
		}	
	  }  
	}

}
