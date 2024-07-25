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
}
