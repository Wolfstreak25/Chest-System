using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChestView : MonoBehaviour
{
    [SerializeField] private Transform Locked;
    [SerializeField] private Transform Unlocked;
    [SerializeField] private Transform countDown;
    [SerializeField] private Image chestThumb;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI chestTypeText;
    
    private ChestController Controller;
    private ChestState currentState;
    private void Update() 
    {
        TimerText(Controller.Model.CountDown);
    }
    public void SetController(ChestController _controller)
    {
        this.Controller = _controller;
        chestThumb.sprite = Controller.Model.Thumb;
        chestTypeText.text = $"{Controller.Model.type}";
    }
    public void OnSelect ()
    {
        UIManagement.Instance.SetPopUp(this.Controller);
    }
    public void ChangeState(ChestState state)
    {
        switch(state)
        {
            case ChestState.LockedState :
                Locked.gameObject.SetActive(true);
                countDown.gameObject.SetActive(false);
                Unlocked.gameObject.SetActive(false);
                break;
            case ChestState.CountDownState :
                Locked.gameObject.SetActive(false);
                countDown.gameObject.SetActive(true);
                break;
            case ChestState.UnlockedState :
                countDown.gameObject.SetActive(false);
                Locked.gameObject.SetActive(false);
                Unlocked.gameObject.SetActive(true);
                break;
            case ChestState.OpenedState:
                Controller.DeActive();
                break;
        }
    }
    private void TimerText(int duration)
    {
        int hr = (duration/60)/60;
        int min = (duration/60)%60;
        int sec = (duration%60)%60;
        timerText.text = (hr > 0) ?  $"{hr:00}Hr {min:00}Min" : $"{min:00}Min {sec:00}Sec";
    }
}