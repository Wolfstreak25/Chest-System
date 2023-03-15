using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private int Gold;
    [SerializeField] private int Gems;
    [SerializeField] private int ChestCost;
    private void Start() 
    {
        EventManagement.Instance.updateResource += UpdateCurrency;
        UIManagement.Instance.UpdateUI(Gold,Gems);
    }
    private void OnDisable() 
    {
        EventManagement.Instance.updateResource -= UpdateCurrency;
    }
   public void ChestSpawner()
   {
        if(Gold >= ChestCost)
        {
            ChestService.Instance.spawnChest();
            UpdateCurrency(-ChestCost,0);
        }
        else
        {
            EventManagement.Instance.EnablePopUp();
            PopUp.Instance.NoMoney();
        }
   }
   private void UpdateCurrency(int gold, int gems)
   {
        Gold += gold;
        Gems += gems;
        UIManagement.Instance.UpdateUI(Gold,Gems);
   }
}