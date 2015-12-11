using UnityEngine;
using System.Collections;

public class twoD_fan_line : naviBase {
	public float myweight;
	Vector2[] tmp_linePoints;
	Material[] insideMaterial;
void  OnPostRender (){

    
    if (!lineDrawOn || linePoints.Length < 2) {return;}
   

    Camera cam= GetComponent<Camera>();

    float nearClip= (float)cam.nearClipPlane+0.1f;

      
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
				GL.PushMatrix();
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
				GL.PopMatrix();
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
					GL.PushMatrix();
					GL.Begin(GL.TRIANGLES);
					
					Vector3 v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n].x, tmp_linePoints[3*n].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+1].x, tmp_linePoints[3*n+1].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+2].x, tmp_linePoints[3*n+2].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
									
					GL.End();
					GL.PopMatrix();
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
					GL.PushMatrix();
					GL.Begin(GL.TRIANGLES);
					
					Vector3 v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n].x, tmp_linePoints[3*n].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+1].x, tmp_linePoints[3*n+1].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					v = cam.ViewportToWorldPoint(new Vector3(tmp_linePoints[3*n+2].x, tmp_linePoints[3*n+2].y, nearClip));
					
					GL.Vertex3(v.x, v.y, v.z);
					
					GL.End();
					GL.PopMatrix();
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

 

void  Awake (){
	myweight=Screen.width/160;
	scale = 11.0f;
	line_scale = 1.0f;
	angle_scale = 0.15f;
	line_thickness =0.002f;
	leg_scale=0.95f;
	mode = 0;
	goal=GameObject.FindGameObjectsWithTag("Finish");
	goal_amount = goal.Length; 
	goalenable =new bool[ goal_amount] ; 
	lineMaterial = new Material[goal_amount];
	insideMaterial = new Material[goal_amount];
	for(int i=0;i<goal_amount;i++)
	{
		goalenable[i]=true;
		lineMaterial[i] = (Material)Resources.Load("MM"+(i+1));
		insideMaterial[i] = (Material)Resources.Load("M"+(i+1));
	}
	
    linePoints = new Vector2[12*goal_amount];    
	tmp_linePoints = new Vector2[3*goal_amount];
    lineDrawOn = true;
	controller = GameObject.FindGameObjectWithTag("Player");
		mDatac = this.gameObject.GetComponent<DataCollection>();

}

 

void  LateUpdate (){
	if(isFan)
		{	aa=-2;}
	else
		{aa=1;}
  	  for(int j=0;j<goal_amount;j++)
	  {	
	  	if(goalenable[j])
		{
		  Vector2 goalPoint;
		  Vector2 myPoint;
		  
		  goalPoint =new Vector2(goal[j].transform.position.x,goal[j].transform.position.z);
		 
		  myPoint =new Vector2(controller.transform.position.x,controller.transform.position.z);
		  
		  float rotateAngle= this.transform.rotation.eulerAngles.y*3.14f/180;
		  
		  
		  
		  Vector2 up_vector= new Vector2(0,1);
		  Vector2 right_vector=new Vector2(1,0);
		  
		  up_vector=new Vector2(up_vector.x*Mathf.Cos(-rotateAngle)-up_vector.y*Mathf.Sin(-rotateAngle),up_vector.x*Mathf.Sin(-rotateAngle)+up_vector.y*Mathf.Cos(-rotateAngle)); 
		  right_vector=new Vector2(right_vector.x*Mathf.Cos(-rotateAngle)-right_vector.y*Mathf.Sin(-rotateAngle),right_vector.x*Mathf.Sin(-rotateAngle)+right_vector.y*Mathf.Cos(-rotateAngle));
		  up_vector.Normalize();
		  right_vector.Normalize();
		  
		  
		  Vector2 up_point= myPoint+5.0f*up_vector*scale;
		  Vector2 down_point= myPoint-5.0f*up_vector*scale;
		  Vector2 right_point= myPoint+8.0f*right_vector*scale;
		  Vector2 left_point= myPoint-8.0f*right_vector*scale;
		  
		  Vector2[] boxConerPoints;
		  boxConerPoints =new Vector2[4];
		  boxConerPoints[0] = myPoint+5.0f*up_vector*scale-8.0f*right_vector*scale;//left_up_point
		  boxConerPoints[1] = myPoint+5.0f*up_vector*scale+8.0f*right_vector*scale;//right_up_point
		  boxConerPoints[2] = myPoint-5.0f*up_vector*scale+8.0f*right_vector*scale;//right_down_point
		  boxConerPoints[3] = myPoint-5.0f*up_vector*scale-8.0f*right_vector*scale;//left_down_point
	
		  
		  float dist= Vector2.Distance(up_point,goalPoint);
		  
		  		Vector2[] boxPoints; 
		 boxPoints = new Vector2[4];
		for(int ii=0;ii<4;ii++)
		 {
		 	boxPoints[ii].x = ( (myPoint.x*goalPoint.y-goalPoint.x*myPoint.y)*(boxConerPoints[ii%4].x-boxConerPoints[(ii+1)%4].x) - (myPoint.x-goalPoint.x)*(boxConerPoints[(ii)%4].x*boxConerPoints[(ii+1)%4].y - boxConerPoints[(ii)%4].y*boxConerPoints[(ii+1)%4].x))/((myPoint.x-goalPoint.x)*(boxConerPoints[(ii)%4].y-boxConerPoints[(ii+1)%4].y)-(myPoint.y-goalPoint.y)*(boxConerPoints[(ii)%4].x-boxConerPoints[(ii+1)%4].x));
		 	boxPoints[ii].y = ( (myPoint.x*goalPoint.y-goalPoint.x*myPoint.y)*(boxConerPoints[ii%4].y-boxConerPoints[(ii+1)%4].y) - (myPoint.y-goalPoint.y)*(boxConerPoints[(ii)%4].x*boxConerPoints[(ii+1)%4].y - boxConerPoints[(ii)%4].y*boxConerPoints[(ii+1)%4].x))/((myPoint.x-goalPoint.x)*(boxConerPoints[(ii)%4].y-boxConerPoints[(ii+1)%4].y)-(myPoint.y-goalPoint.y)*(boxConerPoints[(ii)%4].x-boxConerPoints[(ii+1)%4].x));
			
		 	
		 }
		 float tmp_dist;
		
		 for(int i=0;i<4;i++)
		 {
//		 	print("box"+i+" "+boxPoints[i%4]);
//		 	print("boxcor"+boxConerPoints[(i)%4]+" "+boxConerPoints[(i+1)%4]);
		 	if( boxConerPoints[(i+1)%4].x >=  boxConerPoints[(i)%4].x )
		 	{
		 		if( boxConerPoints[(i+1)%4].y >=  boxConerPoints[(i)%4].y )
			 	{
			 		//print(i+"in up");
			 		if(boxPoints[i].x<=boxConerPoints[(i+1)%4].x && boxPoints[i].x>=boxConerPoints[(i)%4].x && boxPoints[i].y<=boxConerPoints[(i+1)%4].y && boxPoints[i].y>=boxConerPoints[(i)%4].y)
			 		{
			 			
			 			tmp_dist =Vector2.Distance(boxPoints[i],goalPoint);
			 			if(tmp_dist<dist)
			 			{
//			 				print(i +" up ");
			 				dist = tmp_dist;
			 			}
			 		}
			 	}
			 	else
			 	{
			 		//print(i+"in down");
			 		if(boxPoints[i].x<=boxConerPoints[(i+1)%4].x && boxPoints[i].x>=boxConerPoints[(i)%4].x && boxPoints[i].y>=boxConerPoints[(i+1)%4].y && boxPoints[i].y<=boxConerPoints[(i)%4].y)
			 		{
//			 			print(i+"in down");
			 			tmp_dist =Vector2.Distance(boxPoints[i],goalPoint);
			 			
			 			if(tmp_dist<dist)
			 			{
//			 				print(i +" down ");
			 				dist = tmp_dist;
			 			}
			 		}
			 	}
		 	}
		 	else
		 	{
		 		if( boxConerPoints[(i+1)%4].y >=  boxConerPoints[(i)%4].y )
			 	{
//			 		print(i+"in right");
			 		if(boxPoints[i].x>=boxConerPoints[(i+1)%4].x && boxPoints[i].x<=boxConerPoints[(i)%4].x && boxPoints[i].y<=boxConerPoints[(i+1)%4].y && boxPoints[i].y>=boxConerPoints[(i)%4].y)
			 		{
			 			tmp_dist =Vector2.Distance(boxPoints[i],goalPoint);
			 			
			 			if(tmp_dist<dist)
			 			{
//			 				print(i +" rignt ");
			 				dist = tmp_dist;
			 			}
			 		}
			 	}
			 	else
			 	{
			 		//print(i+"in left");
			 		if(boxPoints[i].x>=boxConerPoints[(i+1)%4].x && boxPoints[i].x<=boxConerPoints[(i)%4].x && boxPoints[i].y>=boxConerPoints[(i+1)%4].y && boxPoints[i].y<=boxConerPoints[(i)%4].y)
			 		{
			 			tmp_dist =Vector2.Distance(boxPoints[i],goalPoint);
			 			
			 			if(tmp_dist<dist)
			 			{
//			 				print(i +" left ");
			 				dist = tmp_dist;
			 			}
			 		}
			 	}
		 	
		 	}
		 }
		  
		  
		  
		  dist = line_scale*dist;
		  ///////
		  float leg= dist + Mathf.Log((dist+20)/12.0f,2.71828182846f)*10.0f;
		  leg = leg * leg_scale;
		  float halfaperture=0.5f*( (5.0f+dist*0.3f)/leg)*angle_scale;
		
		  Vector2 v=myPoint-goalPoint;
		  Vector2 v1= new Vector2(v.x*Mathf.Cos(halfaperture)-v.y*Mathf.Sin(halfaperture),v.x*Mathf.Sin(halfaperture)+v.y*Mathf.Cos(halfaperture));
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
		  float myScreenW = Screen.width/myweight;
		  float myScreenH = Screen.height/myweight;
		  
	
		  point1=new Vector2(tmpv1.x*Mathf.Cos(rotateAngle)-tmpv1.y*Mathf.Sin(rotateAngle),tmpv1.x*Mathf.Sin(rotateAngle)+tmpv1.y*Mathf.Cos(rotateAngle))+myPoint;
		  point2=new Vector2(tmpv2.x*Mathf.Cos(rotateAngle)-tmpv2.y*Mathf.Sin(rotateAngle),tmpv2.x*Mathf.Sin(rotateAngle)+tmpv2.y*Mathf.Cos(rotateAngle))+myPoint;
		  goalPoint=new Vector2(tmpvg.x*Mathf.Cos(rotateAngle)-tmpvg.y*Mathf.Sin(rotateAngle),tmpvg.x*Mathf.Sin(rotateAngle)+tmpvg.y*Mathf.Cos(rotateAngle))+myPoint;
		    	    	    
		    Vector2 screen_goal=new Vector2((goalPoint.x-myPoint.x+0.5f*myScreenW)/myScreenW,(goalPoint.y-myPoint.y+0.5f*myScreenH)/myScreenH);
			Vector2 screen_p1=new Vector2((point1.x-myPoint.x+0.5f*myScreenW)/myScreenW,(point1.y-myPoint.y+0.5f*myScreenH)/myScreenH);
			Vector2 screen_p2=new Vector2((point2.x-myPoint.x+0.5f*myScreenW)/myScreenW,(point2.y-myPoint.y+0.5f*myScreenH)/myScreenH);
			
			
			Vector2 screen_goal_p = new Vector2(screen_goal.x*Screen.width,screen_goal.y*Screen.height);
			Vector2 screen_p1_p = new Vector2(screen_p1.x*Screen.width,screen_p1.y*Screen.height);
			Vector2 screen_p2_p = new Vector2(screen_p2.x*Screen.width,screen_p2.y*Screen.height);
				
			
		tmpv1 =  screen_p1_p-screen_goal_p;
		tmpv2 =  screen_p2_p-screen_goal_p;
		float screenDist=Vector2.Distance(screen_p1_p,screen_goal_p);
		float screenDist2=Vector2.Distance(screen_p2_p,screen_goal_p);
		//	print ("aa "+screenDist.ToString("F6") +"  " +screenDist2.ToString("F6"));	
		
		tmpv1.Normalize();
		tmpv2.Normalize();
				
		 float screenN1= screenDist/Mathf.Sqrt(tmpv1.x*tmpv1.x+tmpv1.y*tmpv1.y);
		 float screenN2= screenDist2/Mathf.Sqrt(tmpv2.x*tmpv2.x+tmpv2.y*tmpv2.y);
		float screenN = 0.5f*(screenN1+screenN2)		;
		float myangle=-Mathf.Acos(tmpv1.x*tmpv2.x+tmpv1.y*tmpv2.y)/6;
		 //print ("angle "+myangle);


			
			
			tmp_linePoints[0+3*j] = screen_p1;
		 	tmp_linePoints[1+3*j] = screen_p2;
			tmp_linePoints[2+3*j] = screen_goal;
		
			
			Vector2 tmp_vector;
			tmp_vector = screen_goal - screen_p1;
			tmp_vector =new Vector2(tmp_vector.y,-tmp_vector.x);
			tmp_vector.Normalize();
			
			linePoints[0+12*j] = screen_goal + line_thickness*tmp_vector;
			linePoints[1+12*j] = screen_p1 + line_thickness*tmp_vector;
			linePoints[2+12*j] = screen_p1 - line_thickness*tmp_vector;
			linePoints[3+12*j] = screen_goal - line_thickness*tmp_vector;	
			
			tmp_vector = screen_p1 - screen_p2;
			tmp_vector =new Vector2(tmp_vector.y,-tmp_vector.x);
			tmp_vector.Normalize();

			linePoints[4+12*j] = screen_p1 + line_thickness*tmp_vector;
			linePoints[5+12*j] = screen_p2 + line_thickness*tmp_vector;
			linePoints[6+12*j] = screen_p2 - line_thickness*tmp_vector;
			linePoints[7+12*j] = screen_p1 - line_thickness*tmp_vector; 


			tmp_vector = screen_goal - screen_p2;
			tmp_vector =new Vector2(tmp_vector.y,-tmp_vector.x);
			tmp_vector.Normalize();
			
			linePoints[8+12*j] = screen_goal + line_thickness*tmp_vector;
			linePoints[9+12*j] = screen_p2 + line_thickness*tmp_vector;
			linePoints[10+12*j] = screen_p2 - line_thickness*tmp_vector;
			linePoints[11+12*j] = screen_goal - line_thickness*tmp_vector;
			
		}	
	  }  
}
}