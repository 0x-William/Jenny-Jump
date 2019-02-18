using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum BLOCK_EVENT{NOBUMP,SIDEBUMP,TOPDOWNBUMP}

public class Jump : MonoBehaviour {

	private float up_speed=0.0f;
	private float up_accel=80.0f;
	private bool flyable;
	private bool endless_fly;
	private bool is_touch;
	private float highest_y=200.0f;
	private float fly_delaytime=1.0f;
	private float flyable_delaytime=0.1f;
	private float flyable_num = 3;
	private bool fly_cache = false;
	private bool enabled=true;


	private Vector2 previous_pos;
	private Vector2 current_pos;
	// Use this for initialization
	void Start () {
		flyable=true;
		endless_fly=false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(1))
			GameObject.Find ("Main Camera").GetComponent<BackgroundMove>().is_moving=!(GameObject.Find ("Main Camera").GetComponent<BackgroundMove>().is_moving);

		if(!enabled || !(GameObject.Find ("Main Camera").GetComponent<BackgroundMove>().is_moving)) return;
		previous_pos=transform.localPosition;
		if((Input.GetMouseButtonDown(0)&&(flyable || endless_fly)) || (Input.GetMouseButton(0) && flyable && fly_cache))
		{
			flyable=false;
			is_touch=true;
			fly_cache=false;
			fly_delaytime=0.25f;
		} else if(Input.GetMouseButtonDown(0)){
			fly_cache=true;
		}
		if(is_touch){
			up_speed=12.0f;
			fly_delaytime-=Time.deltaTime;
		}else{
			//up_speed=-10.0f;
		}
		if(Input.GetMouseButtonUp(0) || fly_delaytime<0.0f)
			is_touch=false;

		flyable_num--;
		if (flyable_num == 0)
			flyable = false;

		//if(up_speed==0.0f)
		//	return;
		//else
		up_speed-=up_accel*Time.deltaTime;

		UpdatePosition();

		int batch_left=(int)GetCurrentCol();
		int batch_right=batch_left+1;
		int batch_top=(int)GetCurrentRow();
		int batch_bottom=batch_top+1;


		/*if(CheckIfExist(batch_left,batch_bottom) && Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.OBSTACLE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_bottom),Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type);
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				EndLevel();
			}
		}
			
		if(CheckIfExist(batch_right,batch_bottom) && Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.OBSTACLE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_bottom),Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type);
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				EndLevel();
			}
		}
		if(CheckIfExist(batch_left,batch_top) && Global.object_batches[batch_left].mapobjects[batch_top+20].object_type==OBJECT_TYPE.OBSTACLE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_top),Global.object_batches[batch_left].mapobjects[batch_top+20].object_type);
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				EndLevel();
			}
		}
		if(CheckIfExist(batch_right,batch_top) && Global.object_batches[batch_right].mapobjects[batch_top+20].object_type==OBJECT_TYPE.OBSTACLE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_top),Global.object_batches[batch_right].mapobjects[batch_top+20].object_type);
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				EndLevel();
			}
		}*/

		if (CheckIfExist(batch_left,batch_bottom) && Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.POWERUP_JUMPONCE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_bottom),Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpJumpOnce(batch_left,batch_bottom);
			}
		}

		if (CheckIfExist(batch_left,batch_top) && Global.object_batches[batch_left].mapobjects[batch_top+20].object_type==OBJECT_TYPE.POWERUP_JUMPONCE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_top),Global.object_batches[batch_left].mapobjects[batch_top+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpJumpOnce(batch_left,batch_top);
			}
		}

		if (CheckIfExist(batch_right,batch_bottom) && Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.POWERUP_JUMPONCE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_bottom),Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpJumpOnce(batch_right,batch_bottom);
			}
		}

		if (CheckIfExist(batch_right,batch_top) && Global.object_batches[batch_right].mapobjects[batch_top+20].object_type==OBJECT_TYPE.POWERUP_JUMPONCE)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_top),Global.object_batches[batch_right].mapobjects[batch_top+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpJumpOnce(batch_right,batch_top);
			}
		}

		if (CheckIfExist(batch_left,batch_bottom) && Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.ENDLESS_JUMP)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_bottom),Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpEndlessJump(batch_left,batch_bottom);
			}
		}
		
		if (CheckIfExist(batch_left,batch_top) && Global.object_batches[batch_left].mapobjects[batch_top+20].object_type==OBJECT_TYPE.ENDLESS_JUMP)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_top),Global.object_batches[batch_left].mapobjects[batch_top+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpEndlessJump(batch_left,batch_top);
			}
		}
		
		if (CheckIfExist(batch_right,batch_bottom) && Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.ENDLESS_JUMP)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_bottom),Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpEndlessJump(batch_right,batch_bottom);
			}
		}
		
		if (CheckIfExist(batch_right,batch_top) && Global.object_batches[batch_right].mapobjects[batch_top+20].object_type==OBJECT_TYPE.ENDLESS_JUMP)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_top),Global.object_batches[batch_right].mapobjects[batch_top+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpEndlessJump(batch_right,batch_top);
			}
		}

		if (CheckIfExist(batch_left,batch_bottom) && Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.DISABLE_BLOCK)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_bottom),Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpDisableBlock(batch_left,batch_bottom);
			}
		}
		
		if (CheckIfExist(batch_left,batch_top) && Global.object_batches[batch_left].mapobjects[batch_top+20].object_type==OBJECT_TYPE.DISABLE_BLOCK)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_top),Global.object_batches[batch_left].mapobjects[batch_top+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpDisableBlock(batch_left,batch_top);
			}
		}
		
		if (CheckIfExist(batch_right,batch_bottom) && Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type==OBJECT_TYPE.DISABLE_BLOCK)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_bottom),Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpDisableBlock(batch_right,batch_bottom);
			}
		}
		
		if (CheckIfExist(batch_right,batch_top) && Global.object_batches[batch_right].mapobjects[batch_top+20].object_type==OBJECT_TYPE.DISABLE_BLOCK)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_top),Global.object_batches[batch_right].mapobjects[batch_top+20].object_type);
			
			if(be!=BLOCK_EVENT.NOBUMP)
			{
				PowerUpDisableBlock(batch_right,batch_top);
			}
		}

	

		if (CheckIfExist(batch_left,batch_bottom) && Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type!=OBJECT_TYPE.NULL)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_bottom),Global.object_batches[batch_left].mapobjects[batch_bottom+20].object_type);
			
			if(be==BLOCK_EVENT.TOPDOWNBUMP)
			{
				StopCharacter(batch_left,batch_bottom);
				return;
			}
		}
		
		if (CheckIfExist(batch_left,batch_top) && Global.object_batches[batch_left].mapobjects[batch_top+20].object_type!=OBJECT_TYPE.NULL)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,batch_top),Global.object_batches[batch_left].mapobjects[batch_top+20].object_type);
			
			if(be==BLOCK_EVENT.TOPDOWNBUMP)
			{
				StopCharacter(batch_left,batch_top);
				return;
			}
		}

		if (CheckIfExist(batch_right,batch_bottom))
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_bottom),Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type);
			
			if(be==BLOCK_EVENT.SIDEBUMP)
			{
				print (current_pos);
				EndLevel();
			}
		}

		if (CheckIfExist(batch_right,batch_top) )
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_top),Global.object_batches[batch_right].mapobjects[batch_top+20].object_type);
			print (batch_right+":"+batch_top);
			if(be==BLOCK_EVENT.SIDEBUMP)
			{
				EndLevel();
			}
		}


		if (CheckIfExist(batch_right,batch_bottom) && Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type!=OBJECT_TYPE.NULL)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_bottom),Global.object_batches[batch_right].mapobjects[batch_bottom+20].object_type);
			
			if(be==BLOCK_EVENT.TOPDOWNBUMP)
			{
				StopCharacter(batch_right,batch_bottom);
			}
		}



		if (CheckIfExist(batch_right,batch_top) && Global.object_batches[batch_right].mapobjects[batch_top+20].object_type!=OBJECT_TYPE.NULL)
		{
			BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,batch_top),Global.object_batches[batch_right].mapobjects[batch_top+20].object_type);
			
			if(be==BLOCK_EVENT.TOPDOWNBUMP)
			{
				StopCharacter(batch_right,batch_top);
			}
		}

		if (current_pos.y > 400) {
			current_pos.y = 399;
			up_speed = 0;
		}



		/*if (CheckIfExist(batch_left,batch_bottom))
		{

			Transform block=panel1.FindChild("block"+batch_left+"_"+batch_bottom);
			block.GetComponent<Image>().sprite=Resources.Load<Sprite>("Images/obstacle");
		}*/

		/*for(int i=19;i>-21;i--)
		{


			if (CheckIfExist(batch_left,i))
			{

				BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_left,i),Global.object_batches[batch_left].mapobjects[i+20].object_type);
				if(be!=BLOCK_EVENT.NOBUMP)
				{

					switch(Global.object_batches[batch_left].mapobjects[i+20].object_type)
					{
					case OBJECT_TYPE.OBSTACLE:
						EndLevel();
						break;
					case OBJECT_TYPE.POWERUP_JUMPONCE:
						PowerUpJumpOnce(batch_left,i);
						break;
					case OBJECT_TYPE.BLOCK:
					case OBJECT_TYPE.THIN_BAR:
						StopCharacter(batch_left,i);
						break;
					}
				}
			}

			if (CheckIfExist(batch_right,i))
			{
				print (current_pos.x);

				BLOCK_EVENT be=getBumpWay(getBlockPosition(batch_right,i),Global.object_batches[batch_right].mapobjects[i+20].object_type);
				if(be==BLOCK_EVENT.NOBUMP)
				{
					continue;
				}

				switch(Global.object_batches[batch_right].mapobjects[i+20].object_type)
				{
				case OBJECT_TYPE.OBSTACLE:

					EndLevel();
					break;
				case OBJECT_TYPE.POWERUP_JUMPONCE:

					PowerUpJumpOnce(batch_right,i);

					break;
				case OBJECT_TYPE.BLOCK:
				case OBJECT_TYPE.THIN_BAR:

					if(be==BLOCK_EVENT.TOPDOWNBUMP)
						StopCharacter(batch_right,i);
					else if(be==BLOCK_EVENT.SIDEBUMP)
						EndLevel();
					break;
				}
			}

		}*/
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		transform.localPosition=new Vector3(current_pos.x+panel.localPosition.x-200,current_pos.y,0);

		Transform transform_down=GameObject.Find("Character_Canvas_Down").transform.FindChild("Image");
		transform_down.localPosition=new Vector3(current_pos.x+panel.localPosition.x-200,current_pos.y,0);

		if(current_pos.y<-150)
			EndLevel();

		previous_pos=current_pos;
		//for(int i=-20;i<20;i++)
	}

	private void PowerUpJumpOnce(int col, int row){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		Global.object_batches[col].mapobjects[row+20].object_type=OBJECT_TYPE.NULL;
		Transform block=panel.FindChild("block"+col+"_"+row);
		//Destroy(block.gameObject);
		flyable=true;
	}

	private void PowerUpEndlessJump(int col, int row){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		Global.object_batches[col].mapobjects[row+20].object_type=OBJECT_TYPE.NULL;
		Transform block=panel.FindChild("block"+col+"_"+row);
		Destroy(block.gameObject);
		endless_fly=true;
	}

	private void PowerUpDisableBlock(int col, int row){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		Global.object_batches[col].mapobjects[row+20].object_type=OBJECT_TYPE.NULL;
		Transform block=panel.FindChild("block"+col+"_"+row);
		Destroy(block.gameObject);
		Global.object_batches[col].mapobjects[row].object_type=OBJECT_TYPE.NULL;
	}
	private void EndLevel(){
		//print("rightsidebump");
		GameObject camera=GameObject.Find ("Main Camera");
		(camera.GetComponent<BackgroundMove>()).is_moving=false;
		gameObject.GetComponent<CharacterAnimation>().is_changed=false;
		//up_speed=0;
		gameObject.GetComponent<CharacterDead>().is_dead=true;


		Transform character_down=GameObject.Find("Character_Canvas_Down").transform.FindChild("Image");
		character_down.GetComponent<CharacterDead>().is_dead=true;
		character_down.GetComponent<CharacterAnimation>().is_changed=false;
		enabled=false;
	}

	private void StopCharacter(int col, int row){
		OBJECT_TYPE object_type=Global.object_batches[col].mapobjects[row+20].object_type;
		Vector2 object_pos=getBlockPosition(col,row);
		float block_height=50.0f;
		switch(object_type)
		{
		case OBJECT_TYPE.BLOCK:
			block_height=50.0f;
			break;
		case OBJECT_TYPE.THIN_BAR:
			block_height=50.0f;
			break;
		}
	
		//print("rightsidebump");
		up_speed=0.0f;
		if(object_pos.y<current_pos.y)
		{
			flyable=true;
			flyable_num=3;
			endless_fly=false;
			current_pos.y=object_pos.y+block_height;
		}
			
		else
		{
			current_pos.y=object_pos.y-block_height;
			fly_delaytime=0;
		}
			

	}

	private Vector2 getBlockPosition(int col, int row){
		float block_width=50.0f;
		float block_height=50.0f;
		float position_x = block_width*(col-1);
		float position_y = -50-block_height*row;
		
		return new Vector2(position_x,position_y);
	}

	private BLOCK_EVENT getBumpWay(Vector2 block_pos, OBJECT_TYPE object_type){
		float block_width=50.0f;
		float block_height=50.0f;
		switch(object_type)
		{
		case OBJECT_TYPE.BLOCK:
			block_width=50.0f;
			block_height=50.0f;
			break;
		case OBJECT_TYPE.OBSTACLE:
			block_width=30.0f;
			block_height=35.0f;
			break;
		case OBJECT_TYPE.THIN_BAR:
			block_width=50.0f;
			block_height=50.0f;
			break;
		case OBJECT_TYPE.POWERUP_JUMPONCE:
			block_width=70.0f;
			block_height=70.0f;
			break;
		}

		if(Mathf.Abs(current_pos.y-block_pos.y)>=block_height || Mathf.Abs(current_pos.x-block_pos.x)>=block_width/5.0f*3.0f)
			return BLOCK_EVENT.NOBUMP;
		if(Mathf.Abs(current_pos.x-block_pos.x)-block_width<Mathf.Abs(current_pos.y-block_pos.y)-block_height)
			return BLOCK_EVENT.TOPDOWNBUMP;
		return BLOCK_EVENT.SIDEBUMP;
	}

	private void UpdatePosition(){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		current_pos=new Vector2(-panel.localPosition.x, previous_pos.y+up_speed*Time.deltaTime*50.0f);
	}

	private float GetCurrentCol(){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		return -panel.localPosition.x/50.0f+1;
	}

	private float GetCurrentRow(){
		return -current_pos.y/50.0f-2;
	}

	private bool CheckIfExist(int col, int row){
		return Global.object_batches[col].mapobjects[row+20].object_type!=OBJECT_TYPE.NULL;
	}

	private int onLand(float previous_position_y,int index){
		float block_height=50.0f/3.0f*4.0f;
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		for(int i=-19;i<21;i++){
			Transform block=panel.FindChild("block"+index+"_"+i);
			if(block==null)
				continue;
			if(transform.localPosition.y<block_height*(-i) && previous_position_y>=block_height*(-i))
				return i;
		}
		return -100;
	}/*

	private bool onBlocked(float previous_position_y, int index)
	{
		float block_height=50.0f/3.0f*4.0f;
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		for(int i=-20;i<20;i++)
		{
			Transform block=panel.FindChild("block"+index+"_"+i);
			if(block==null)
				continue;
			if(transform.localPosition.y<block_height*(-i) && transform.localPosition.y>block_height*(-i-1) && 
			   previous_position_y<block_height*(-i) && previous_position_y>block_height*(-i-1))
				return true;
		}
		return false;
	}*/
}
