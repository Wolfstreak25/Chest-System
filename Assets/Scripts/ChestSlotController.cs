using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlotController : MonoBehaviour
{
    private bool isEmpty;
	public bool IsEmpty
	{get{return isEmpty;} private set{isEmpty = value;}}
	private ChestController chestController;
	public int Index{get;set;}
	private void OnEnable() 
	{
		isEmpty = true;
	}
	public void SetEmptySlot()
	{
		isEmpty = true;
		chestController = null;
	}

	public void SetChest(ChestController _chest)
	{
		isEmpty = false;
		chestController = _chest;
	}
}
