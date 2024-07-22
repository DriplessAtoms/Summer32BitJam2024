using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataScript : MonoBehaviour
{
    public GameObject player;
    public GameObject playerParent;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject borders;

    public float distanceFromPointB;

    private bool distanceCheckB;

    void Start()
    {
        pointA.SetActive(false);
        pointB.SetActive(false);
        distanceCheckB = false;
    }

    void Update()
    {
        if (distanceCheckB)
        {
            Debug.Log(Vector3.Distance(player.transform.position, pointB.transform.position));
            if (Vector3.Distance(player.transform.position, pointB.transform.position) <= distanceFromPointB)
            {
                Debug.Log("You did it!");
                EndQuest();
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
            default:
                {
                    break;
                }
        }
    }
    public void QuestAtoB()
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
    public void EndQuest()
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
}
