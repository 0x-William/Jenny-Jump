using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockInitiator : MonoBehaviour {

	/*should be removed*/

	[SerializeField]
	private bool UpdateScreenFromArray;

	[SerializeField]
	private bool UpdateScreenFromString;

	[SerializeField]
	public LevelData[] level_dataset;

	[SerializeField]
	private int level;

	[SerializeField]
	private ObjectBatch[] object_batches;



	//[SerializeField]
	//private bool UpdateScreenFromArray;

	/**/

	private int current_blocknum;
	private int block_num_for_batch=3;

	private float potential_block_hide;

	[SerializeField]
	public int level_num;

	float block_width;
	float block_height;

	// Use this for initialization
	void Start () {
		//LoadBlocks();
		current_blocknum=40;
		block_width=50;
		block_height=block_width/3.0f*4.0f;
		Init_Blocks();
		if(UpdateScreenFromArray)
		{
			EncodeIntoText();
			Init_Blocks();
			UpdateScreenFromArray=false;
		}
		if(UpdateScreenFromString)
		{
			DecodeFromText();
			Init_Blocks();
			UpdateScreenFromString=false;
		}
		AddAndRemoveBlock ();
	}
	
	// Update is called once per frame
	void Update () {
		if(UpdateScreenFromArray)
		{
			EncodeIntoText();
			Init_Blocks();
			UpdateScreenFromArray=false;
		}
		if(UpdateScreenFromString)
		{
			DecodeFromText();
			Init_Blocks();
			UpdateScreenFromString=false;
		}
		AddAndRemoveBlock ();

	}

	void Init_Blocks(){
		Init_LandBlock ();
		Init_Obstacles();
		Init_Boosts();

	}

	void Remove_AllBlocks()
	{
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		for(int i=0;i<panel.childCount;i++)
			Destroy(panel.GetChild(i).gameObject);
	}

	void AddBlock(int index){
		if (index == 1) {
			for(int i=-10;i<1;i++)
				AddBlock(i);
		}
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		float position_x,position_y;

		if(index<=object_batches.Length)
		{
			position_x = (index-1)*block_width;
			if(index<1)
				index=1;
			LevelData level_data = level_dataset [level_num-1];
			for(int i=39;i>-1;i--)
			//for(int i=20;i>15;i--)
			{
				if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.BLOCK || object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.OBSTACLE ||
				   object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.NULL)
					continue;
				int y_index=i-20;
				position_y = -60-block_height/4.0f*3.0f*y_index;
				GameObject backobject=GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BackObject")) as GameObject;

				if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.BLOCK)
				{
					if(object_batches[index-1].mapobjects[i].object_type==OBJECT_TYPE.BLOCK && 
					   object_batches[index+1].mapobjects[i].object_type==OBJECT_TYPE.BLOCK)
						backobject.GetComponent<Image>().sprite=level_data.Block_Sprite[1];
					else if(object_batches[index-1].mapobjects[i].object_type!=OBJECT_TYPE.BLOCK && 
					        object_batches[index+1].mapobjects[i].object_type!=OBJECT_TYPE.BLOCK){
						backobject.GetComponent<Image>().sprite=level_data.Block_Sprite[1];
					}
					else if(object_batches[index-1].mapobjects[i].object_type==OBJECT_TYPE.BLOCK)
						backobject.GetComponent<Image>().sprite=level_data.Block_Sprite[2];
					else 
						backobject.GetComponent<Image>().sprite=level_data.Block_Sprite[0];
				}
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.OBSTACLE)
					backobject.GetComponent<Image>().sprite=level_data.Obstacle_Sprite;
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.THIN_BAR)
				{
					if(object_batches[index-1].mapobjects[i].object_type==OBJECT_TYPE.THIN_BAR && 
					   object_batches[index+1].mapobjects[i].object_type==OBJECT_TYPE.THIN_BAR)
						backobject.GetComponent<Image>().sprite=level_data.ThinBar_Sprite[1];
					else if(object_batches[index-1].mapobjects[i].object_type!=OBJECT_TYPE.THIN_BAR && 
					        object_batches[index+1].mapobjects[i].object_type!=OBJECT_TYPE.THIN_BAR){
						backobject.GetComponent<Image>().sprite=level_data.ThinBar_Sprite[1];
					}
					else if(object_batches[index-1].mapobjects[i].object_type==OBJECT_TYPE.THIN_BAR)
						backobject.GetComponent<Image>().sprite=level_data.ThinBar_Sprite[2];
					else 
						backobject.GetComponent<Image>().sprite=level_data.ThinBar_Sprite[0];
				}
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.POWERUP_JUMPONCE)
					backobject.GetComponent<Image>().sprite=level_data.ExtraJump_Sprite;
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.ENDLESS_JUMP)
					backobject.GetComponent<Image>().sprite=level_data.EndlessJump_Sprite;
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.DISABLE_BLOCK)
					backobject.GetComponent<Image>().sprite=level_data.DisableBlock_Sprite;
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.DISABLE_BAR)
				{
					backobject.GetComponent<Image>().sprite=level_data.ThinBar_Sprite[1];
				}

				if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.ENDLESS_JUMP || object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.POWERUP_JUMPONCE
				   || object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.DISABLE_BLOCK)
					backobject.GetComponent<DisableBlock>().is_boost=true;
					

				backobject.transform.SetParent(panel);
				if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.BLOCK)
					backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_height);
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.OBSTACLE)
				{

					backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_height/4.0f*3.0f);
					//if(object_batches[index].mapobjects[i+1].object_type!=OBJECT_TYPE.NULL)
					//	backobject.transform.localEulerAngles=new Vector3(180.0f,0.0f,0.0f);
				}
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.THIN_BAR)
					backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_height/4.0f);
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.POWERUP_JUMPONCE)
					backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_width);
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.ENDLESS_JUMP)
					backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_width);
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.DISABLE_BLOCK)
				{
					backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_width);
				}
					
				else if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.DISABLE_BAR)
				{
					backobject.GetComponent<DisableBlock>().col=index;
					backobject.GetComponent<DisableBlock>().row=i;
					backobject.GetComponent<DisableBlock>().p_col=object_batches[index].mapobjects[i].p_col;
					print (object_batches[index].mapobjects[i].p_col);
					backobject.GetComponent<DisableBlock>().p_row=object_batches[index].mapobjects[i].p_row;
					if (object_batches[index].mapobjects[i].class_a)
						backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width,block_width/4.0f);
					else
						backobject.GetComponent<RectTransform>().sizeDelta=new Vector3(block_width/4.0f,block_width);
				}

				backobject.transform.localPosition=new Vector3(position_x-200,position_y,0.0f);
				if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.THIN_BAR ||
				   object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.DISABLE_BAR && object_batches[index].mapobjects[i].class_a)
					backobject.transform.localPosition=new Vector3(position_x-200,position_y+block_height/8.0f*3.0f,0.0f);
				backobject.transform.localEulerAngles=new Vector3(0.0f,0.0f,0.0f);
				if(object_batches[index].mapobjects[i].object_type==OBJECT_TYPE.OBSTACLE)
					if(object_batches[index].mapobjects[i-1].object_type==OBJECT_TYPE.BLOCK)
						backobject.transform.localEulerAngles=new Vector3(180.0f,0.0f,0.0f);
				backobject.transform.localScale=new Vector3(1,1,1);
				backobject.name="block"+index+"_"+y_index;
			}
			return;
		}
	}

	void Init_LandBlock(){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		float position_x=panel.localPosition.x;
		current_blocknum=(int)(-position_x/50.0f);
		/*for(int i=current_blocknum-40;i<current_blocknum+40;i++)
			AddBlock(i);*/
	}

	void Init_Obstacles(){

	}

	void Init_Boosts(){

	}

	void AddAndRemoveBlock(){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		float position_x=panel.localPosition.x;
		if(-position_x>block_width*(current_blocknum-30))
		{
			RemoveBlock (current_blocknum-40);
			AddBlock (++current_blocknum);
		}
		//print (position_x+"");
	}

	void RemoveBlock(int index){
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		float position_x=panel.localPosition.x;
		Transform block_transform=panel.FindChild("block"+index+"_0");
		if(block_transform==null)
			return;
		for(int i=0;i<block_num_for_batch;i++)
		{
			block_transform=panel.FindChild("block"+index+"_"+i);
			if(block_transform!=null)
				Destroy(block_transform.gameObject);
		}
		Transform obstacle_transform=panel.FindChild("obstacle"+index);
		if(obstacle_transform==null)
			return;
		Destroy (obstacle_transform.gameObject);

	}

	void EncodeIntoText()
	{
		//block_positions_string=string.Join ("|",block_positions);

	}

	void DecodeFromText()
	{
		LevelData level_data = level_dataset [level_num-1];

		/**BLOCK POSITIONS**/
		string block_positions_string = level_data.block_positions_string;
		string[] block_positions;
		block_positions=block_positions_string.Split('|');
		object_batches=new ObjectBatch[block_positions.Length];
		for(int i=0;i<object_batches.Length;i++)
		{
			object_batches[i]=new ObjectBatch();

			string [] block_pos_list=block_positions[i].Split(',');
			object_batches[i].mapobjects=new MapObject[40];
			for(int j=0;j<40;j++)
			{
				object_batches[i].mapobjects[j]=new MapObject();
			}
			if(block_positions[i]!="")
			{
				for(int j=0;j<block_pos_list.Length;j++)
				{
					int y_index=int.Parse(block_pos_list[j]);
					object_batches[i].mapobjects[y_index+20].object_type=OBJECT_TYPE.BLOCK;
				}
			}
		}

		/**END**/

		/**OBSTACLE POSITIONS**/
		string obstacle_positions_string = level_data.obstacle_positions_string;
		string[] obstacle_positions;
		if(obstacle_positions_string!="")
		{
			obstacle_positions=obstacle_positions_string.Split('|');
			for(int i=0;i<obstacle_positions.Length;i++){
				string[] obstacle_position=obstacle_positions[i].Split(',');
				int x_pos=int.Parse(obstacle_position[0]);
				int y_pos=int.Parse(obstacle_position[1]);
				object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.OBSTACLE;
			}
		}


		/**THINBAR POSITIONS**/
		string thinbar_positions_string = level_data.thinbar_positions_string;
		string[] thinbar_positions;
		if(thinbar_positions_string!="")
		{
			thinbar_positions=thinbar_positions_string.Split('|');
			for(int i=0;i<thinbar_positions.Length;i++){
				string[] thinbar_position=thinbar_positions[i].Split(',');
				int x_pos=int.Parse(thinbar_position[0]);
				int y_pos=int.Parse(thinbar_position[1]);
				object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.THIN_BAR;
			}
		}


		/**JUMPONCE POSITIONS**/
		string powerupjumponce_positions_string = level_data.powerupjumponce_positions_string;
		string[] powerupjumponce_positions;
		if(powerupjumponce_positions_string!="")
		{
			powerupjumponce_positions=powerupjumponce_positions_string.Split('|');
			for(int i=0;i<powerupjumponce_positions.Length;i++){
				string[] powerupjumponce_position=powerupjumponce_positions[i].Split(',');
				int x_pos=int.Parse(powerupjumponce_position[0]);
				int y_pos=int.Parse(powerupjumponce_position[1]);
				object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.POWERUP_JUMPONCE;
			}

		}

		/**ENDLESS POSITIONS**/
		string powerupendlessjump_positions_string = level_data.powerupendlessjump_positions_string;
		string[] powerupendlessjump_positions;
		if(powerupendlessjump_positions_string!="")
		{
			powerupendlessjump_positions=powerupendlessjump_positions_string.Split('|');
			for(int i=0;i<powerupendlessjump_positions.Length;i++){
				string[] powerupendlessjump_position=powerupendlessjump_positions[i].Split(',');
				int x_pos=int.Parse(powerupendlessjump_position[0]);
				int y_pos=int.Parse(powerupendlessjump_position[1]);
				object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.ENDLESS_JUMP;
			}
			
		}
		/**ENDLESS POSITIONS**/
		string disableblockhorizontal_positions_string = level_data.disableblockhorizontal_positions_string;
		string disableblockvertical_positions_string = level_data.disableblockvertical_positions_string;
		string[] disableblock_positions;
		if(disableblockhorizontal_positions_string!="")
		{
			disableblock_positions=disableblockhorizontal_positions_string.Split('*');
			for(int i=0;i<disableblock_positions.Length;i++){
				string[] disableblock_position=disableblock_positions[i].Split('|');
				int p_row=0,p_col=0;
				for(int j=0;j<disableblock_position.Length;j++)
				{
					string[] disablebar_position=disableblock_position[j].Split(',');
					int x_pos=int.Parse(disablebar_position[0]);
					int y_pos=int.Parse(disablebar_position[1]);
					if(j==0)
					{
						object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.DISABLE_BLOCK;
						p_row=y_pos;
						p_col=x_pos;
					}
					else
					{
						object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.DISABLE_BAR;
						object_batches[x_pos].mapobjects[y_pos+20].p_col=p_col;
						object_batches[x_pos].mapobjects[y_pos+20].p_row=p_row;
						object_batches[x_pos].mapobjects[y_pos+20].class_a=true;
					}
				}
			}
		}

		if(disableblockvertical_positions_string!="")
		{
			disableblock_positions=disableblockvertical_positions_string.Split('*');
			for(int i=0;i<disableblock_positions.Length;i++){
				string[] disableblock_position=disableblock_positions[i].Split('|');
				int p_row=0,p_col=0;
				for(int j=0;j<disableblock_position.Length;j++)
				{
					string[] disablebar_position=disableblock_position[j].Split(',');
					int x_pos=int.Parse(disablebar_position[0]);
					int y_pos=int.Parse(disablebar_position[1]);
					if(j==0)
					{
						object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.DISABLE_BLOCK;
						p_row=y_pos;
						p_col=x_pos;
					}
					else
					{
						object_batches[x_pos].mapobjects[y_pos+20].object_type=OBJECT_TYPE.DISABLE_BAR;
						object_batches[x_pos].mapobjects[y_pos+20].p_col=p_col;
						object_batches[x_pos].mapobjects[y_pos+20].p_row=p_row;
						object_batches[x_pos].mapobjects[y_pos+20].class_a=false;
					}
				}
			}
		}
		
		Global.object_batches=object_batches;
	}
}
