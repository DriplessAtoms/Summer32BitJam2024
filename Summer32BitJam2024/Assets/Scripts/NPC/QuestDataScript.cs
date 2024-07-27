using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestDataScript : MonoBehaviour
{
    public GameObject player;
    public PlayerInteraction playerInteractionScript;
    public FadeTransition fadeTransitionScript;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject borders;
    public GameObject timerScreen;
    public GameObject radioMinigameScreen;

    //Move Chain Shit PLEASEEEEEEEEEEE!!!!
    public NPC_Data chain;
    public QuestDataScript chainNewQuest;
    public string chainQuestName;

    public NPC_Data actualChain;

    public List<GameObject> fetchableItems;

    public float distanceFromPointB;

    public float timeForTimedQuest;

    private bool distanceCheckB;

    private int itemsCollected;

    public int leftDialStartPosition;
    public int rightDialStartPosition;
    public int leftDialTargetPosition;
    public int rightDialTargetPosition;

    public GameObject noReturn;

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
            case "RadioMinigame":
                {
                    QuestRadioMinigame();
                    break;
                }
            case "Credits":
                {
                    SceneManager.LoadScene("Credits", LoadSceneMode.Single);
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

        if (chain != null)
        {
            chain.important = true;
            chain.dialogueNumber++;
            chain.quest = chainNewQuest;
            chain.questType = chainQuestName;
            if (chain.quest == null)
            {
                chain.hasQuest = false;
            }
            else if (chain.quest != null)
            {
                chain.hasQuest = true;
            }
        }

        if (borders != null)
        {
            borders.SetActive(false);
        }
    }
    private void QuestTimedRace()
    {
        pointA.SetActive(true);
        pointB.SetActive(true);

        playerInteractionScript.enabled = false;

        fadeTransitionScript.StartTeleportationTransitionWithEndTrigger(pointA,this);
    }
    public void EndOfTransitionRace()
    {
        playerInteractionScript.enabled = true;
        timerScreen.SetActive(true);
        timerScreen.GetComponent<Timer>().StartCountdownTimer(timeForTimedQuest, this);
        if (borders != null)
        {
            borders.SetActive(true);
        }
    }
    public void EndQuestTimedRaceFail()
    {
        fadeTransitionScript.StartTeleportationTransition(pointA);

        timerScreen.SetActive(false);
        //pointA.SetActive(false);
        pointB.SetActive(false);
        if (borders != null)
        {
            borders.SetActive(false);
        }
        Debug.Log("Failure");
    }
    public void EndQuestTimedRaceWin()
    {
        fadeTransitionScript.StartTeleportationTransition(pointA);

        timerScreen.SetActive(false);
        //pointA.SetActive(false);
        pointB.SetActive(false);
        timerScreen.GetComponent<Timer>().StopTimer();
        if (borders != null)
        {
            borders.SetActive(false);
        }

        if (chain != null)
        {
            chain.dialogueNumber++;
            chain.quest = chainNewQuest;
            chain.questType = chainQuestName;
            if (chain.quest == null)
            {
                chain.hasQuest = false;
            }
            else if (chain.quest != null)
            {
                chain.hasQuest = true;
            }
        }
    }
    private void QuestRadioMinigame()
    {
        radioMinigameScreen.SetActive(true);
        player.SetActive(false);
        playerInteractionScript.enabled = false;

        radioMinigameScreen.GetComponent<RadioMinigame>().StartMinigame(leftDialStartPosition, rightDialStartPosition, leftDialTargetPosition, rightDialTargetPosition, this);
    }
    public void EndQuestRadioMinigame()
    {
        radioMinigameScreen.SetActive(false);
        player.SetActive(true);
        playerInteractionScript.enabled = true;

        if (chain != null)
        {
            chain.dialogueNumber++;
            chain.quest = chainNewQuest;
            chain.questType = chainQuestName;

            if (chain.quest == null)
            {
                chain.hasQuest = false;
            }
            else if (chain.quest != null)
            {
                chain.hasQuest = true;
            }

            playerInteractionScript.TriggerDialogue(chain.dialogueList[chain.dialogueNumber], chain.icons);
            playerInteractionScript.disableNPC();
        }
        if(actualChain != null)
        {
            actualChain.canBeInteractedWith = true;
        }
        if(noReturn != null)
        {
            noReturn.SetActive(false);
        }
    }
}