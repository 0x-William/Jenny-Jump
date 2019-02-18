using UnityEngine;
using System.Collections;

public class CharacterDead : MonoBehaviour {

	public bool is_dead;
	private int step=0;
	/*private float up_speed=-3.0f;
	private float up_accel=100.0f;
	private float left_speed=5.0f;*/
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && is_dead && step>25)
			Application.LoadLevel ("Play");
		if(!is_dead) return;
		step++;

		if(step<50 && step%20<10)
			GetComponent<CanvasGroup> ().alpha =1;
		if(step<50 && step%20>9)
			GetComponent<CanvasGroup> ().alpha =0;
		if(step>60)
			GetComponent<CanvasGroup> ().alpha -=0.03f;
		//up_speed-=up_accel*Time.deltaTime;
		//transform.Rotate (Vector3.forward,180.0f/25.0f);
		//transform.localPosition+=Vector3.up*up_speed+Vector3.left*left_speed;
		GetComponentInParent<Canvas> ().sortingOrder = 7;
		if(step>25)
		{
			//gameObject.SetActive(false);
		}
			
	}


}
