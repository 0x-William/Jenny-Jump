using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackInitiator : MonoBehaviour {

	[SerializeField]
	private int level;

	private const int backobject_num=10;

	// Use this for initialization
	void Start () {
		Init_Background();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Init_s the background.
	/// </summary>
	void Init_Background()
	{
	//	Init_BackgroundCanvas ();
	//	for(int i=0;i<4;i++)
	//		Init_BackgroundCanvas(i);
		Init_BlockCanvas ();
	}

	/// <summary>
	/// Init_s the background canvas.
	/// </summary>
	void Init_BackgroundCanvas()
	{
		LevelData level_data=GetComponent<BlockInitiator>().level_dataset[GetComponent<BlockInitiator>().level_num-1];
		GameObject background_1000=GameObject.Find ("Background_Canvas").transform.FindChild("Image").gameObject;
		background_1000.GetComponent<Image>().sprite=level_data.Background0_Sprite;
	}

	/// <summary>
	/// Init_s the background canvas500.
	/// </summary>
	void Init_BackgroundCanvas(int index)
	{
		LevelData level_data=GetComponent<BlockInitiator>().level_dataset[GetComponent<BlockInitiator>().level_num-1];

		Transform panel=GameObject.Find("Background_Canvas"+index).transform.FindChild("Panel");
		for(int i=0;i<(index+1)*(index+1)*5;i++)
		{
			float scale = 1;
			float position_x = i*800-200;
			float position_y = 0;

			GameObject backobject=GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BackObject")) as GameObject;
			backobject.GetComponent<Image>().sprite=level_data.Background_Sprite[index];
			backobject.transform.SetParent(panel);
			backobject.transform.localPosition=new Vector3(position_x,position_y,0.0f);
			backobject.transform.localEulerAngles=new Vector3(0.0f,0.0f,0.0f);
			backobject.transform.localScale=new Vector3(scale,scale,scale);
		}
	}

	/// <summary>
	/// Init_s the block canvas.
	/// </summary>
	void Init_BlockCanvas()
	{
		float width = 954.73f;
		LevelData level_data=GetComponent<BlockInitiator>().level_dataset[GetComponent<BlockInitiator>().level_num-1];
		
		Transform panel=GameObject.Find("Block_Canvas").transform.FindChild("Panel");
		for(int i=0;i<level_data.Block_Sprites.Length;i++)
		{
			float scale = 1;
			float position_x = i*width;
			float position_y = 0;
			
			GameObject backobject=GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BackObject")) as GameObject;
			backobject.GetComponent<Image>().sprite=level_data.Block_Sprites[i];
			backobject.transform.SetParent(panel);
			backobject.transform.localPosition=new Vector3(position_x,position_y,0.0f);
			backobject.transform.localEulerAngles=new Vector3(0.0f,0.0f,0.0f);
			backobject.transform.localScale=new Vector3(scale*width/800,scale,scale);
		}
	}



}
