using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class UIManagement : Singleton<UIManagement>
{
    [SerializeField] private PopUp PopUp;
    [Header("Currencies")]
    [SerializeField] private TextMeshProUGUI goldCount;
    [SerializeField] private TextMeshProUGUI gemsCount;
    private void OnEnable() 
    {
        EventManagement.Instance.showPopUp += SetPopUp;
        EventManagement.Instance.enablePopUp += EnablePopUp;
    }
    private void OnDisable() 
    {
        EventManagement.Instance.showPopUp += SetPopUp;
        EventManagement.Instance.enablePopUp += EnablePopUp;
    }

    public void SetPopUp(ChestController chest)
    {
        PopUp.gameObject.SetActive(true);
        PopUp.Instance.ShowPopUp(chest);
    }
    private void EnablePopUp()
    {
        PopUp.gameObject.SetActive(true);
    }
    public void UpdateUI(int Gold, int Gems) 
    {
        goldCount.text = $"{Gold}";
        gemsCount.text = $"{Gems}";
    }   
}
