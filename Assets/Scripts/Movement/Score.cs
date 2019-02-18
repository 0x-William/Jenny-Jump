using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	//private bool is_dead=false;
	// Use this for initialization
	private int start_time;
	void Start () {
		start_time=(int)Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameObject.Find("Character").GetComponent<CharacterDead>().is_dead)
			transform.GetComponent<Text>().text=(int)((Time.fixedTime-start_time)*5.0f)+"";
	}
}
