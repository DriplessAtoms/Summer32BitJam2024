using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchableItem : MonoBehaviour
{
    public QuestDataScript questData;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            questData.CollectedFetchItem(this.gameObject);
        }
    }
}
