using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestView View{get;private set;}
    public ChestModel Model{get;private set;}
    public ChestState chestState{get;private set;}
    public bool TimerActive{get;private set;}
    private ChestSlotController slot;
    public ChestController(ChestModel _model, ChestView _view, Transform spawn)
    {
        Model = _model;
        View = _view;
        View = GameObject.Instantiate<ChestView>(_view);
        View.SetController(this);
        Model.SetController(this);
    }
    public void SetActive(ChestSlotController _slot)
    {
        this.slot = _slot;
        this.View.gameObject.SetActive(true);
        Model.ResetTimer();
        ChestTransform.transform.SetParent(slot.transform);
        ChestTransform.transform.localPosition = Vector3.zero ;
        ChestTransform.transform.localScale = Vector3.one;
        SetState(ChestState.LockedState);
    }
    public RectTransform ChestTransform     {   get {    return View.GetComponent<RectTransform>();  } }
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
    public string TimerText
    {
        get
        {
            int duration = Model.UnlockDuration;
            int hr = (duration/60)/60;
            int min = (duration/60)%60;
            int sec = (duration%60)%60;
            string text = (hr > 0) ?  $"{hr:0}Hr {min:0}Min" : $"{min:0}Min {sec:0}Sec";
            return text;
        }
    }
    public int UnlockGems
    {
        get
        {
            int count = 0;
            int duration = Model.UnlockDuration;
            int hr = (duration/60)/60;
            int min = (duration/60)%60;
            int sec = (duration%60)%60;

            if(hr > 1)  {   count += hr*6;  }
            if(min > 0) {   count += min/10;    if(min%10 > 0)  {count += 1;} }
            if(sec>0 && min == 0 && hr == 0)    {   count = 1;  }
            return count;
        }
    }
    public void setTimerRunning(bool status)
    {
        TimerActive = status;
    }
    // Requests
    public bool StartTimerRequest()
    {
        return slot.StartTimerRequest();
    }
    public void StopUnlocking()
    {
        slot.StopUnlocking(this);
    }
    public void ChestOpened()
    {
        Rewards();
        ChestTransform.transform.SetParent(null);
        this.View.gameObject.SetActive(false);
        slot.SetEmptySlot();
    }
}