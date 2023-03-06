using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService : Singleton<ChestService>
{
    private ChestPool chestPool;
    [SerializeField] private ChestList chestObject;
    private ChestController controller;
    private ChestSlotController chestSlot;
    public void spawnChest()
    {
        GetEmptySlot();
    }
    private void Start() 
    {
        chestPool = this.gameObject.GetComponent<ChestPool>();
    }
    private ChestController CreateChest(ChestSlotController slot)
    {
        controller = chestPool.GetChest(chestObject.Chest[Random.Range(0,chestObject.Chest.Count -1)],slot.transform);
        controller.SetActive(slot);
        return controller;
    }
    private void GetEmptySlot()
    {
        chestSlot = ChestSlot.Instance.emptySlot();
        if(chestSlot == null)
        {
            Debug.Log("No Slot Available");
            return;
        }
        else
        {
            chestSlot.SetChest(CreateChest(chestSlot));
        }
    }
}
