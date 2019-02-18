using UnityEngine;
using System.Collections;

public enum BLOCKELEMENT_TYPE{BLOCK_LEFT,BLOCK_CENTER,BLOCK_RIGHT}
public enum BLOCK_TYPE{BLOCKOBJECT,LANDBLOCKOBJECT,OBSTACLEOBJECT}

public class BlockObject{

	protected int v_Index{get; set;}
	protected int BlockGroup_Type{get; set;}
	protected BLOCKELEMENT_TYPE BlockElement_Type{get; set;}

	protected BLOCK_TYPE Block_Type{get; set;}

	public BlockObject(){
		Block_Type=BLOCK_TYPE.BLOCKOBJECT;
	}

	public BlockObject(int v_Index, int BlockGroup_Type, BLOCKELEMENT_TYPE BlockElement_Type):this()
	{
		this.v_Index=v_Index;
		this.BlockGroup_Type=BlockGroup_Type;
		this.BlockElement_Type=BlockElement_Type;
	}

	public BlockObject(BlockObject block_object):this()
	{
		this.v_Index=block_object.v_Index;
		this.BlockGroup_Type=block_object.BlockGroup_Type;
		this.BlockElement_Type=block_object.BlockElement_Type;
	}

}
