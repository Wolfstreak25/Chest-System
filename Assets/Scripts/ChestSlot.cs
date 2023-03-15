using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlot : Singleton<ChestSlot>
{
    [SerializeField] private RectTransform content;
    [SerializeField] private ChestSlotController slotController;
    [SerializeField] private int maxSlot = 4;
    private ChestSlotController[] chestSlot;
    public Queue<ChestController> Unlocking = new Queue<ChestController>();
    public bool isUnlocking{get;private set;}
    private void Start()
    {
        chestSlot = new ChestSlotController[maxSlot];
        isUnlocking = false;
        EventManagement.Instance.TimerState(false);
        for(int i = 0; i < maxSlot; i++ )
        {
            SpawnSlot(i);
        }
    }
    // private void Update()
    // {
    //     foreach (ChestSlotController Slot in chestSlot)
    //     {
    //         if(Slot.GetChest.TimerActive)
    //         {
    //            StartUnlocking(Slot.GetChest);
    //         }
    //     }
    // }
    public bool StartUnlocking(ChestController slot)
    {
        if(Unlocking.Count == 0)
        {
            Unlocking.Enqueue(slot);
            isUnlocking = true;
            EventManagement.Instance.TimerState(true);
            return true;
        }
        return false;
    }
    public void StopUnlocking(ChestController requester)
    {
        if(requester.TimerActive && Unlocking.Count >= 1)
        {
            Unlocking.Dequeue();
            EventManagement.Instance.TimerState(false);
            isUnlocking = false;
        }
    }
    public void SpawnSlot ( int index)
    {
        ChestSlotController Slot = GameObject.Instantiate<ChestSlotController>(slotController);
        Slot.transform.SetParent(content);
        Slot.GetSlotController(this, index);
        chestSlot[index] = Slot;
    }
    public void EmptySlot(int index)
    {
        chestSlot[index].SetEmptySlot();
    }
    public ChestSlotController emptySlot()
    {
        foreach (var slot in chestSlot)
        {
            if (slot.IsEmpty)
            {
                return slot;
            }
        }
        return null;
    }
}
