using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class UIManagement : Singleton<UIManagement>
{
    [SerializeField] private PopUp PopUp;
    [Header("Currencies")]
    [SerializeField] private TextMeshProUGUI goldCount;
    [SerializeField] private int Gold = 100;
    [SerializeField] private TextMeshProUGUI gemsCount;
    [SerializeField] private int Gems = 100;
    public bool TimerRunning{get;private set;}
    public int chestID{get;private set;}
    public void SetPopUp(ChestController chest)
    {
        PopUp.gameObject.SetActive(true);
        PopUp.Instance.ShowPopUp(chest);
    }
    private void Start() 
    {
        chestID = -1;
        UpdateUI();
    }
    private void UpdateUI() 
    {
        goldCount.text = $"{Gold}";
        gemsCount.text = $"{Gems}";
    }
    private void OnEnable() 
    {
        EventManagement.Instance.startTimer += StartTimer;
        EventManagement.Instance.stopTimer += StopTimer;
        EventManagement.Instance.updateResource += UpdateResource;
    }
    private void OnDisable() 
    {
        EventManagement.Instance.startTimer -= StartTimer;
        EventManagement.Instance.stopTimer -= StopTimer;
        EventManagement.Instance.updateResource -= UpdateResource;
    }
    private void StartTimer(ChestController Chest)
    {
        if(!TimerRunning)
        {
            StartCoroutine(CountDown(Chest));
        }
    }
    private void StopTimer(ChestController Chest)
    {
        if(chestID == Chest.ChestID)
        {
            StopCoroutine(CountDown(Chest));
            Chest.SetState(ChestState.UnlockedState);
            TimerRunning = false;
        }
    }

    private void UpdateResource(int gold, int gems)
    {
        //rewards and stuff
        Gold += gold;
        Gems += gems;
        UpdateUI();
    }
    private IEnumerator CountDown(ChestController Chest)
    {
        TimerRunning = true;
        chestID = Chest.ChestID;
        while(Chest.Model.CountDown > 0) 
        {
            Chest.Model.CountDown--;
            yield return new WaitForSeconds(1f);
        }
        TimerRunning = false;
        Chest.SetState(ChestState.UnlockedState);
        Debug.Log("timer ended");
    }
}
