using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : Singleton<PopUp>
{
    [Header("Main Panel")]
    [SerializeField] private GameObject mainBG;
    [Header("Header")]
    [SerializeField] private Image chestThumb;
    [SerializeField] private TextMeshProUGUI chestType;
    [Header("Rewards")]
    [SerializeField] private TextMeshProUGUI goldReward;
    [SerializeField] private TextMeshProUGUI gemReward;
    [Header("Timer")]
    [SerializeField] private GameObject startTimerButton;
    [SerializeField] private GameObject stopCountDown;
    [SerializeField] private GameObject countDown;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI gemCost;
    [Header("Footer")]
    [SerializeField] private TextMeshProUGUI chestStatus;
    [Header("Open Chest")]
    [SerializeField] private Image openChest;
    [SerializeField] private TextMeshProUGUI rewardGold;
    [SerializeField] private TextMeshProUGUI rewardGem;
    private ChestController Chest;
    private int unlockGems;
    public bool isTimerActive = false;
    //public bool ProcessStatus { get; private set;}
    public void ShowPopUp(ChestController _chest)
    {
        Chest = _chest;
        InitializePopUp();
    }
    private void Update() 
    {
        CheckUnlockStatus();
    }
    private void CheckUnlockStatus()
    {
        if(UIManagement.Instance.TimerRunning && UIManagement.Instance.chestID == Chest.ChestID)
        {
            chestStatus.text = "In Progress";
            startTimerButton.SetActive(false);
            unlockGems = Chest.Model.CountDown / 10;
            gemCost.text = $"{unlockGems}";
            countDown.SetActive(true);
            TimerText(Chest.Model.CountDown);
        }
        else if(UIManagement.Instance.TimerRunning)
        {
            chestStatus.text = "Another in Progress";
            startTimerButton.SetActive(false);
            unlockGems = Chest.Model.CountDown / 10;
            gemCost.text = $"{unlockGems}";
            //countDown.SetActive(false);
            stopCountDown.SetActive(true);
            Timer.gameObject.SetActive(false);
        }
        else
        {
            countDown.SetActive(false);
            startTimerButton.SetActive(true);
            chestStatus.text = "No Operation";
        }
        if(Chest.chestState == ChestState.UnlockedState)
        {
            mainBG.SetActive(false);
        }
    }
    private void InitializePopUp()
    {
        this.gameObject.GetComponent<Button>().enabled = true;
        if (Chest.chestState == ChestState.UnlockedState)
        {
            mainBG.SetActive(false);
            openChest.gameObject.SetActive(true);
            int gems = Random.Range(Chest.Model.minGems,Chest.Model.maxGems);
            int gold = Random.Range(Chest.Model.minGold,Chest.Model.maxGold);
            EventManagement.Instance.UpdateResource(gold,gems);
            rewardGem.text = $"{gems}";
            rewardGold.text = $"{gold}";
            Chest.SetState(ChestState.OpenedState);
            return;
        }
        else
        {
            mainBG.SetActive(true);
            openChest.gameObject.SetActive(false);
        }
        chestThumb.sprite = Chest.Model.Thumb;
        chestType.text = $"{Chest.Model.type}";
        goldReward.text = $"{Chest.Model.minGold}-{Chest.Model.maxGold}";
        gemReward.text = $"{Chest.Model.minGems}-{Chest.Model.maxGems}";
    }
    private void TimerText(int duration)
    {
        int hr = (duration/60)/60;
        int min = (duration/60)%60;
        int sec = (duration%60)%60;
        Timer.text = (hr > 0) ?  $"{hr:00}Hr {min:00}Min" : $"{min:00}Min {sec:00}Sec";
        if(duration == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void StartCountDown()
    {
        Chest.SetState(ChestState.CountDownState);
        EventManagement.Instance.StartTimer(Chest);
        countDown.gameObject.SetActive(true);
        startTimerButton.gameObject.SetActive(false);
    }
    public void StopCountDown()
    {
        EventManagement.Instance.StopTimer(Chest);
        EventManagement.Instance.UpdateResource(0,-unlockGems);
        countDown.SetActive(false);
        Chest.SetState(ChestState.UnlockedState);
    }

}
public enum PopUpState
{
    StartTimer,
    CountDown,
    CountFinished,
    Open
}