using UnityEngine;
using System.Collections;

public class BackgroundMove : MonoBehaviour {
	
	public bool is_moving;
	[SerializeField]
	private float position_x;
	[SerializeField]
	private float position_y;

	[SerializeField]
	private bool MoveLeft;

	[SerializeField]
	private bool MoveRight;

	[SerializeField]
	private bool MoveUp;

	[SerializeField]
	private bool MoveDown;


	// Use this for initialization
	void Start () {
		//is_moving=true;
	}
	
	// Update is called once per frame
	void Update () {
		Transform character_up=GameObject.Find("Character_Canvas_Up").transform.FindChild("Image");
		Transform character_down=GameObject.Find("Character_Canvas_Down").transform.FindChild("Image");
		if(character_up.GetComponent<CharacterDead>().is_dead)
			return;

		if(!is_moving) 
		{
			if(MoveLeft)
			{
				position_x+=1;
				MoveLeft=false;
			}
			if(MoveRight)
			{
				position_x-=1;
				MoveRight=false;
			}
			if(MoveUp)
			{
				position_y++;
				MoveUp=false;
			}
			if(MoveDown)
			{
				position_y--;
				MoveDown=false;
			}

			Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
			panel.localPosition=new Vector3(position_x*(-50.0f),panel.localPosition.y,panel.localPosition.z);

			character_up.localPosition=new Vector3(-200,position_y*50,0);
			character_down.localPosition=new Vector3(-200,position_y*50,0);
			return;
		}

		for(int i=0;i<4;i++)
		Move_BackgroundCanvas(i,Time.deltaTime);
		Move_BlockCanvas(Time.deltaTime);
	}

	void Move_BackgroundCanvas(int index,float deltaTime){
		float speed=(index+2)*(index+3)*450/20;

		Transform panel=GameObject.Find("Background_Canvas"+index).transform.FindChild("Panel");
		//panel.localPosition+=Vector3.left;
		panel.localPosition+=Vector3.left*speed*deltaTime;
	}

	void Move_BlockCanvas(float deltaTime){
		float speed=450;
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		panel.localPosition+=Vector3.left*deltaTime*speed;
	}
}
