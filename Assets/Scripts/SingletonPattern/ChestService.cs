using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService : Singleton<ChestService>
{
    private ChestPool chestPool;
    [SerializeField] private ChestList chestObject;
    private ChestSlot chestSlot;
    public bool IsUnlocking{get{return chestSlot.isUnlocking;}}
    public void spawnChest()
    {
        GetEmptySlot();
    }
    private void Start() 
    {
        chestPool = this.gameObject.GetComponent<ChestPool>();
    }
    private void GetEmptySlot()
    {
        ChestSlotController chestSlot = ChestSlot.Instance.emptySlot();
        if(chestSlot == null)
        {
            EventManagement.Instance.EnablePopUp();
            PopUp.Instance.NoSlots();
            return;
        }
        else
        {
            chestSlot.SetChest(CreateChest(chestSlot));
        }
    }
    private ChestController CreateChest(ChestSlotController slot)
    {
        return chestPool.GetChest ( chestObject.Chest[Random.Range(0,chestObject.Chest.Count)], slot.transform );
    }
}
