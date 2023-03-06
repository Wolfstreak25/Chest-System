using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlot : Singleton<ChestSlot>
{
    private SlotPool slotPool;
    [SerializeField] private RectTransform content;
    [SerializeField] private ChestSlotController slotController;
    [SerializeField] private int maxSlot = 4;
    private List<ChestSlotController> chestSlot = new List<ChestSlotController>();
    private int emptySlotCount = 0;
    private void OnEnable() 
    {
        slotPool = this.gameObject.GetComponent<SlotPool>();
    }
    private void Start()
    {
        for(int i = 0; i < maxSlot; i++ )
        {
            SpawnSlot();
        }
    }
    private void Update()
    {
        //if last element of the list is not an empty slot spawn empty slot
        foreach (var Slot in chestSlot)
        {
            if(Slot.IsEmpty)
            {
                emptySlotCount++;
            }
        }
    }
    public void SpawnSlot ()
    {
        var Slot = slotPool.GetSlot(slotController,content);
        Slot.transform.SetParent(content);
        Slot.Index = chestSlot.Count;
        chestSlot.Add(Slot);
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
                //Debug.LogFormat($"{chestSlot.IndexOf(slot)}");;
                return slot;
            }
        }
        return null;
    }
}
