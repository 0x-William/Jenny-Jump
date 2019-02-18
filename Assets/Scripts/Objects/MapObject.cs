using UnityEngine;
using System.Collections;

public enum OBJECT_TYPE{NULL, BLOCK, THIN_BAR, OBSTACLE, POWERUP_JUMPONCE, ENDLESS_JUMP , DISABLE_BLOCK, DISABLE_BAR}

[System.Serializable]
public class MapObject{

	public OBJECT_TYPE object_type;
	public int p_row, p_col;
	public bool class_a;

	public MapObject(){
		object_type=OBJECT_TYPE.NULL;
		p_row=p_col=0;
		class_a=true;
	}

}
