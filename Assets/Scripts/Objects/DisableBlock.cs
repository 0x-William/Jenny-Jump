using UnityEngine;
using System.Collections;

public class DisableBlock : MonoBehaviour {

	public int row;
	public int col;

	public int p_col;
	public int p_row;

	public bool is_boost;

	// Use this for initialization
	void Start () {
		//is_boost=false;
	}
	
	// Update is called once per frame
	void Update () {

		if(is_boost)
		{
			float scale=(Mathf.Sin(Time.fixedTime*5)+14)/15;
			transform.localScale=new Vector3(scale,scale,scale);
		}
			

		if((p_col!=0 || p_row!=0) && Global.object_batches[p_col].mapobjects[p_row+20].object_type==OBJECT_TYPE.NULL)
			transform.GetComponent<CanvasGroup>().alpha-=0.1f;
		if(transform.GetComponent<CanvasGroup>().alpha<0.1f)
		{
			Global.object_batches[col].mapobjects[row].object_type=OBJECT_TYPE.NULL;
			Destroy (gameObject);
		}
			
	}
}
