using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

	[SerializeField]
	private string character_num;

	[SerializeField]
	private int min_index;

	[SerializeField]
	private int max_index;

	[SerializeField]
	private bool is_up;

	public bool is_changed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!is_changed) return;
		int index=((int)(Time.fixedTime*25.0f))%(max_index-min_index+1)+min_index;
		gameObject.GetComponent<Image>().sprite=Resources.Load<Sprite>("Characters/"+character_num+"/"+index+(is_up?"_up":"_down"));
	}
}
