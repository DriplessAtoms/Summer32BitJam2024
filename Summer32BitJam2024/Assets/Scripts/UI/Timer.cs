using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;

    private QuestDataScript questData;
    private float timeNumber;
    private bool countDown;

    void Update()
    {
        if(countDown && timeNumber > 0)
        {
            timeText.text = ((int)timeNumber).ToString();
            timeNumber -= Time.deltaTime;
        }
        else if(countDown && timeNumber <= 0)
        {
            EndCountdownTimer();
        }
    }

    public void StartCountdownTimer(float timeGiven, QuestDataScript qds)
    {
        questData = qds;
        timeNumber = timeGiven;
        countDown = true;
    }
    public void StopTimer()
    {
        countDown = false;
    }
    public void EndCountdownTimer()
    {
        countDown = false;
        questData.EndQuestTimedRaceFail();
    }
}