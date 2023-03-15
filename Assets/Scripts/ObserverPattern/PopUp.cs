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
    [SerializeField] private GameObject TimerText;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI gemCost;
    [Header("Footer")]
    [SerializeField] private TextMeshProUGUI chestStatus;
    [Header("Open Chest")]
    [SerializeField] private Image openChest;
    [SerializeField] private TextMeshProUGUI rewardGold;
    [SerializeField] private TextMeshProUGUI rewardGem;
    [Header("No Empty Slot")]
    [SerializeField] private Transform NoSlotsPanel;
    [Header("No Money")]
    [SerializeField] private Transform NoMoneyPanel;
    private ChestController Chest;
    public bool isTimerActive = false;
    private void OnEnable() 
    {
        this.gameObject.GetComponent<Button>().enabled = true;
        EventManagement.Instance.timerState += TimerActive;
        EventManagement.Instance.updateResource += SetRewards;
    }
    private void Update() 
    {
        if(Chest != null)
        {
            CheckUnlockStatus();
        }
    }
    public void ShowPopUp(ChestController _chest)
    {
        Chest = _chest;
        InitializePopUp();
    }
    private void CheckUnlockStatus()
    {
        // Timer Running On current chest
        if(isTimerActive && Chest.TimerActive)
        {
            UnlockingState();
        }
        // Timer Running On Other chest
        else if(isTimerActive && !Chest.TimerActive && Chest.chestState == ChestState.LockedState)
        {
            UnlockWaitState();
        }
        // No Timer Running and Is locked
        else if(Chest.chestState == ChestState.LockedState && !isTimerActive)
        {
            LockedState();
        }
    }
    private void InitializePopUp()
    {
        chestThumb.sprite = Chest.Model.Thumb;
        chestType.text = $"{Chest.Model.chestType}";
        goldReward.text = $"{Chest.Model.minGold}-{Chest.Model.maxGold}";
        gemReward.text = $"{Chest.Model.minGems}-{Chest.Model.maxGems}";
        if (Chest.chestState == ChestState.UnlockedState)
        {
            OpenChest();
        }
        else
        {
           ChestData();
        }
    }
    public void ChestData()
    {
        mainBG.SetActive(true);
    }
    public void OpenChest()
    {
        // Moved to controller
        openChest.gameObject.SetActive(true);
        Chest.SetState(ChestState.OpenedState);
        return;
    }
    // UI Pannels Error
    public void NoSlots()
    {
        NoSlotsPanel.gameObject.SetActive(true);
    }
    public void NoMoney()
    {
        NoMoneyPanel.gameObject.SetActive(true);
    }
    // Events
    private void TimerActive(bool state)
    {
        isTimerActive = state;
    }
    private void SetRewards(int gold, int gems)
    {
        rewardGem.text = $"{gems}";
        rewardGold.text = $"{gold}";
    }
    // UI Button Events
    public void StartCountDown()
    {
        Chest.SetState(ChestState.CountDownState);
        countDown.gameObject.SetActive(true);
        startTimerButton.gameObject.SetActive(false);
    }
    public void StopCountDown()
    {
        EventManagement.Instance.UpdateResource(0,-Chest.UnlockGems);
        Chest.SetState(ChestState.UnlockedState);
        this.gameObject.SetActive(false);
    }
    // PopUp states
    private void UnlockingState()
    {
        startTimerButton.SetActive(false);
        chestStatus.text = "Unlocking Shortly";
        countDown.SetActive(true);
        TimerText.SetActive(true);
        Timer.text = Chest.TimerText;
        gemCost.text = $"{Chest.UnlockGems}";
    }
    private void UnlockWaitState()
    {
        startTimerButton.SetActive(false);
        chestStatus.text = "Unlocking Unavailable";
        TimerText.SetActive(false);
        gemCost.text = $"{Chest.UnlockGems}";
    }
    private void LockedState()
    {
        startTimerButton.SetActive(true);
        chestStatus.text = "Unlock Timer";
        countDown.SetActive(false);
    }
    private void OnDisable() 
    {
        NoSlotsPanel.gameObject.SetActive(false);
        mainBG.SetActive(false);
        openChest.gameObject.SetActive(false);
        NoMoneyPanel.gameObject.SetActive(false);
        EventManagement.Instance.timerState -= TimerActive;
        EventManagement.Instance.updateResource -= SetRewards;
    }
} 