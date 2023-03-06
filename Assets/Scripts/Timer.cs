using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private int duration = 5000;
    [SerializeField] private TextMeshProUGUI textTimer;
    public void Start()
    {
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while(duration>=0)
        {
            int hr = (duration/3600);
            int min = (duration/60)%60;
            int sec = (duration%60)%60;
            textTimer.text = $"{hr:00}Hr {min:00}Min {sec:00}Sec";
            duration--;
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnEnd()
    {
        //set timer status inActive
    }
}
