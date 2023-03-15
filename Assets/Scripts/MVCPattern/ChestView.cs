using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChestView : MonoBehaviour
{
    [Header("Chest Image")]
    [SerializeField] private Image chestThumb;
    [Header("Locked State")]
    [SerializeField] private Transform Locked;
    [SerializeField] private TextMeshProUGUI chestTypeText;
    [SerializeField] private TextMeshProUGUI counterDuration;
    [Header("Unlocked State")]
    [SerializeField] private Transform Unlocked;
    [Header("CountDown State")]
    [SerializeField] private Transform countDown;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI unlockGem;
    private ChestController Controller;
    private Transform currentState;
    private Coroutine timer;
    private void Update() 
    {
        if(Controller.chestState == ChestState.CountDownState)
        {
            timerText.text = Controller.TimerText;
            unlockGem.text = $"{Controller.UnlockGems}";
        }
    }
    public void SetController(ChestController _controller)
    {
        this.Controller = _controller;
        chestThumb.sprite = Controller.Model.Thumb;
        chestTypeText.text = $"{Controller.Model.chestType}";
    }
    public void OnSelect ()
    {
        UIManagement.Instance.SetPopUp(this.Controller);
    }
    // Can moved to controller
    public void ChangeState(ChestState state)
    {
        
        switch(state)
        {
            case ChestState.LockedState :
                SetLocked();
                break;
            case ChestState.CountDownState :
                SetCountDown();
                break;
            case ChestState.UnlockedState :
                SetUnlocked();
                break;
            case ChestState.OpenedState:
                SetOpened();
                break;
        }
        currentState.gameObject.SetActive(true);
    }
    private void SetLocked()
    {
        if(currentState != null)    {   currentState.gameObject.SetActive(false);   }
        currentState = Locked;
        chestTypeText.text = $"{Controller.Model.chestType}";
        counterDuration.text = Controller.TimerText;
    }
    private void SetCountDown()
    {
        if(Controller.StartTimerRequest())
        {
            currentState.gameObject.SetActive(false);
            currentState = countDown;
            Controller.setTimerRunning(true);
            timerText.gameObject.SetActive(true);
            timer = StartCoroutine(CountDown());
        }
    }
    private void SetUnlocked()
    {
        Controller.StopUnlocking();
        if(timer != null)
        {
            StopCoroutine(timer);
        }
        Controller.setTimerRunning(false);
        currentState.gameObject.SetActive(false);
        currentState = Unlocked;
    }
    private void SetOpened()
    {
        currentState.gameObject.SetActive(false);
        Controller.ChestOpened();
    }
    
    private IEnumerator CountDown()
    {
        while(Controller.Model.UnlockDuration > 0) 
        {
            Controller.Model.UnlockDuration--;
            yield return new WaitForSeconds(1f);
        }
        Controller.SetState(ChestState.UnlockedState);
    }
    private void OnDisable() 
    {
        Locked.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);
        Unlocked.gameObject.SetActive(false);
        currentState.gameObject.SetActive(false);
    }
}