using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Data : MonoBehaviour
{
    public bool canBeInteractedWith = false;
    public bool disableInteractionWhenDone = false;
    public List<TextAsset> dialogueList = new List<TextAsset>();
    public List<Texture> icons = new List<Texture>();
    public int dialogueNumber = 0;
    public bool hasQuest = false;
    public string questType;
    public QuestDataScript quest;
    public bool hasChain = false;
    public NPC_Data chain;
    //NO!!! WHY'D YOU PUT THEM IN BOTH!!!???
    public bool hasChainQuest;
    public QuestDataScript chainNewQuest;
    public string chainQuestName;
    //Distance Tracking
    public GameObject player;
    public AudioSource radioSound;
    public bool important = false;
    public float distanceFromNPC;

    private float calculatedDistance;

    void Update()
    {
        if(important)
        {
            calculatedDistance = Mathf.Clamp(distanceFromNPC / Vector3.Distance(player.transform.position, transform.position), 0, 1);
            radioSound.volume = calculatedDistance;
        }
    }
}
