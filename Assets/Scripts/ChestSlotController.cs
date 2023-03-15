using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlotController : MonoBehaviour
{
    private bool isEmpty;
	private bool timerState;
	public bool IsEmpty {get{return isEmpty;}}
	private ChestController chestController;
	private ChestSlot ChestSlots;
	public int Index{get;private set;}
	public void GetSlotController(ChestSlot _chestSlots, int slotIndex)
	{
		ChestSlots = _chestSlots;
		Index = slotIndex;
	}
	private void OnEnable() 
	{
		isEmpty = true;
	}
	public void SetEmptySlot()
	{
		isEmpty = true;
		timerState = false;
		ChestPool.Instance.ReturnItem(chestController);
		chestController = null;
	}
	public bool SetTimerActive()
	{
		return chestController.TimerActive;
	}
	public bool StartTimerRequest()
	{
		return ChestSlots.StartUnlocking(chestController);
	}
	public void StopUnlocking(ChestController controller)
	{
		ChestSlots.StopUnlocking(controller);
	}
	public void SetChest(ChestController _chest)
	{
		isEmpty = false;
		chestController = _chest;
		chestController.SetActive(this);
	}
	public ChestController GetChest	{get{return chestController;}}
}
