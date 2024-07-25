using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDataScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject borders;
    public GameObject timerScreen;
    public NPC_Data chain;

    public List<GameObject> fetchableItems;

    public float distanceFromPointB;

    public float timeForTimedQuest;

    private bool distanceCheckB;

    private int itemsCollected;

    void Start()
    {
        if(pointA != null)
            pointA.SetActive(false);
        if (pointB != null)
            pointB.SetActive(false);
        distanceCheckB = false;

        for (int i = 0; i < fetchableItems.Count; i++)
        {
            fetchableItems[i].GetComponent<FetchableItem>().questData = this;
            fetchableItems[i].SetActive(false);
        }

        if (borders != null)
        {
            borders.SetActive(false);
        }
    }

    void Update()
    {
        //AtoB
        if (distanceCheckB)
        {
            Debug.Log(Vector3.Distance(player.transform.position, pointB.transform.position));
            if (Vector3.Distance(player.transform.position, pointB.transform.position) <= distanceFromPointB)
            {
                Debug.Log("You did it!");
                EndQuestAtoB();
            }
        }
        if (Input.GetKeyDown("g"))
        {
            StartQuest("AtoB");
        }
    }

    public void StartQuest(string typeOfQuest)
    {
        switch(typeOfQuest)
        {
            case "AtoB":
                {
                    distanceCheckB = true;
                    QuestAtoB();
                    break;
                }
            case "Fetch":
                {
                    QuestFetch();
                    break;
                }
            case "TimedRace":
                {
                    QuestTimedRace();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    private void QuestAtoB()
    {
        pointA.SetActive(true);
        pointB.SetActive(true);

        player.transform.position = pointA.transform.position;
        player.transform.rotation = pointA.transform.rotation;

        distanceCheckB = true;
        if(borders != null)
        {
            borders.SetActive(true);
        }
    }
    private void EndQuestAtoB()
    {
        player.transform.position = pointA.transform.position;
        player.transform.rotation = pointA.transform.rotation;

        pointA.SetActive(false);
        pointB.SetActive(false);
        distanceCheckB = false;
        if (borders != null)
        {
            borders.SetActive(false);
        }
    }
    private void QuestFetch()
    {
        itemsCollected = 0;

        for(int i = 0; i < fetchableItems.Count; i++)
        {
            fetchableItems[i].SetActive(true);
        }
        if (borders != null)
        {
            borders.SetActive(true);
        }
    }
    public void CollectedFetchItem(GameObject itemFetched)
    {
        itemFetched.SetActive(false);

        Debug.Log("Collected Item!");

        itemsCollected++;
        if(itemsCollected == fetchableItems.Count)
        {
            EndQuestFetch();
        }
    }
    private void EndQuestFetch()
    {
        itemsCollected = 0;

        Debug.Log("Finished Fetch Quest");

        chain.dialogueNumber++;

        if (borders != null)
        {
            borders.SetActive(false);
        }
    }
    private void QuestTimedRace()
    {
        timerScreen.SetActive(true);
        pointA.SetActive(true);
        pointB.SetActive(true);

        player.transform.position = pointA.transform.position;
        player.transform.rotation = pointA.transform.rotation;

        timerScreen.GetComponent<Timer>().StartCountdownTimer(timeForTimedQuest, this);
        if (borders != null)
        {
            borders.SetActive(true);
        }
    }
    public void EndQuestTimedRaceFail()
    {
        player.transform.position = pointA.transform.position;
        player.transform.rotation = pointA.transform.rotation;

        timerScreen.SetActive(false);
        pointA.SetActive(false);
        pointB.SetActive(false);
        if (borders != null)
        {
            borders.SetActive(false);
        }
        Debug.Log("Failure");
    }
    public void EndQuestTimedRaceWin()
    {
        player.transform.position = pointA.transform.position;
        player.transform.rotation = pointA.transform.rotation;

        timerScreen.SetActive(false);
        pointA.SetActive(false);
        pointB.SetActive(false);
        timerScreen.GetComponent<Timer>().StopTimer();
        if (borders != null)
        {
            borders.SetActive(false);
        }
        Debug.Log("Win!");
    }
}
