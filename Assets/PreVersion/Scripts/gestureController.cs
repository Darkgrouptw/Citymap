// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class gestureController : MonoBehaviour {

	public int touch_reslut;
	float startTime;
	
	Vector2 startPos;
	
	bool  couldBeSwipe;
	
	float comfortZone;
	
	float minSwipeDist;
	
	float maxSwipeTime;
	 
	void  Start (){
	
		
	
	}
	void  Update (){
		touch_reslut=0;
	    if (Input.touchCount > 0) {
	
	        Touch touch= Input.touches[0];
	
	        
	
	        switch (touch.phase) {
	
	            case TouchPhase.Began:
	
	                couldBeSwipe = true;
	
	                startPos = touch.position;
	                
					
	                startTime = Time.time;
	
	                break;
	
	            
	
	            case TouchPhase.Moved:
	
	                if (Mathf.Abs(touch.position.y - startPos.y) > comfortZone) {
	
	                    couldBeSwipe = false;
						
	                }
	
	                break;
	
	            
	
	            case TouchPhase.Stationary:
	
	                couldBeSwipe = false;
	              
	
	                break;
	
	            
	
	            case TouchPhase.Ended:
	
	                float swipeTime= Time.time - startTime;
					float swipedist= (touch.position - startPos).magnitude;
	                Vector2 swipe_vector;
	                if(swipedist>5.0f)
	                {
		                swipe_vector = touch.position - startPos;
						swipe_vector.Normalize();
						
		                
		
		                float num;
		                num=-1000.0f;
		                Vector2 tmp_v;
		                int check = 0;
		                
		                
		                tmp_v = new Vector2(0,1);
		                if(Vector2.Dot(tmp_v,swipe_vector)>num)
		                {
		                	check=1;
		                	num=Vector2.Dot(tmp_v,swipe_vector);
		                }          
		
						tmp_v = new Vector2(0,-1);
		                if(Vector2.Dot(tmp_v,swipe_vector)>num)
		                {
		                	check=2;
							num=Vector2.Dot(tmp_v,swipe_vector);
		                }
		                
		                tmp_v = new Vector2(1,0);
		                if(Vector2.Dot(tmp_v,swipe_vector)>num)
		                {
		                	check=3;
		                	num=Vector2.Dot(tmp_v,swipe_vector);
		                }
		                
		                tmp_v = new Vector2(-1,0);
		                if(Vector2.Dot(tmp_v,swipe_vector)>num)
		                {
		                	check=4;
		                	num=Vector2.Dot(tmp_v,swipe_vector);
		                }
		                touch_reslut=check;
		                switch(check)
		                {
			                case 1:
			                	//swipeText.text = "up";
			                break;
			                case 2:
			                	// swipeText.text = "down";
			                break;
			                case 3:
			                	//swipeText.text = "right";
			                break;
			                case 4:
			                    // swipeText.text = "left";
			                break;
		                }
		            }
		            else
		            {
						touch_reslut=5;
		            	//swipeText.text = "click";
		            }
	                
	                break;
	
	        }
	
	    }
	
	}
}