using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestView View{get;private set;}
    public ChestModel Model{get;private set;}
    public ChestState chestState{get;private set;}
    public int ChestID{get;private set;}
    private ChestSlotController slot;
    public ChestController(ChestModel _model, ChestView _view, Transform spawn)
    {
        Model = _model;
        View = _view;
        View = GameObject.Instantiate<ChestView>(_view);
        View.SetController(this);
        Model.SetController(this);
    }
    public RectTransform ChestTransform
    {
        get
        {
            return View.GetComponent<RectTransform>();
        }
    }
    public void SetState(ChestState state)
    {
        this.chestState = state;
        View.ChangeState(state);
    }
    private void Rewards()
    {
        int goldReward = Random.Range(Model.minGold,Model.maxGold);
        int gemsReward = Random.Range(Model.minGems,Model.maxGems);
        EventManagement.Instance.UpdateResource(goldReward,gemsReward);
    }
    private void OpenedChest()
    {
        DeActive();
    }
    public void SetActive(ChestSlotController _slot)
    {
        SetState(ChestState.LockedState);
        this.slot = _slot;
        this.View.gameObject.SetActive(true);
        Model.ResetTimer();
        ChestID = slot.Index;
        ChestTransform.transform.SetParent(slot.transform);
        ChestTransform.transform.localPosition = Vector3.zero ;
        ChestTransform.transform.localScale = Vector3.one;
        chestState = ChestState.SpawnedState;
    }
    public void DeActive()
    {
        ChestPool.Instance.ReturnItem(this);
        ChestTransform.transform.SetParent(null);
        this.View.gameObject.SetActive(false);
        slot.SetEmptySlot();
    }
}
public enum ChestState 
{
    SpawnedState,
    LockedState,
    UnlockedState,
    CountDownState,
    OpenedState
}