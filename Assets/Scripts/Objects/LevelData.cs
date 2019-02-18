using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelData {

	[TextArea(5,5)]
	[SerializeField]
	public string block_positions_string;
	[TextArea(5,5)]
	[SerializeField]
	public string obstacle_positions_string;
	[SerializeField]
	public string thinbar_positions_string;
	[SerializeField]
	public string powerupjumponce_positions_string;
	[SerializeField]
	public string powerupendlessjump_positions_string;
	
	[SerializeField]
	public string disableblockhorizontal_positions_string;
	
	[SerializeField]
	public string disableblockvertical_positions_string;

	[SerializeField]
	public Sprite[] Block_Sprite;

	[SerializeField]
	public Sprite Obstacle_Sprite;


	[SerializeField]
	public Sprite[] ThinBar_Sprite;

	[SerializeField]
	public Sprite ExtraJump_Sprite;

	[SerializeField]
	public Sprite EndlessJump_Sprite;

	[SerializeField]
	public Sprite DisableBlock_Sprite;

	[SerializeField]
	public Sprite DisableBar_Sprite;

	[SerializeField]
	public Sprite[] Background_Sprite;

	[SerializeField]
	public Sprite Background0_Sprite;

	[SerializeField]
	public Sprite[] Block_Sprites;
}
