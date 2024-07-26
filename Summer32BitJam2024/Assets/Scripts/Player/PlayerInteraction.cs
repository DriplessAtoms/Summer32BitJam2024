using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float distance;
    public Transform interactionDirection;
    public DialogueSystem ds;
    public GameObject buttonPromptScreen;
    public TMP_Text buttonString;
    public bool busy;

    private NPC_Data npc;

    //Might want to add disable player command here instead of dialogue system <-NO!!!

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(interactionDirection.position, interactionDirection.TransformDirection(Vector3.forward), out hit, distance))
        {
            if (hit.collider.tag == "NPC" && hit.collider.GetComponent<NPC_Data>().canBeInteractedWith)
            {
                npc = hit.collider.GetComponent<NPC_Data>();
                buttonPromptScreen.SetActive(true);
                buttonString.text = "E";
                if (Input.GetKeyDown("e"))
                {
                    TriggerDialogue(npc.dialogueList[npc.dialogueNumber], npc.icons);
                }
            }
        }
        if((hit.collider == null || hit.collider.tag != "NPC") && buttonPromptScreen.activeInHierarchy)
        {
            buttonPromptScreen.SetActive(false);
        }
    }
    void TriggerDialogue(TextAsset dialogue, List<Texture> icon)
    {
        ds.StartDialogue(dialogue, icon);
    }
    public void EndDialogueCheck() //Gets called after dialogue has finished
    {
        if (npc.hasQuest)
        {
            npc.quest.StartQuest(npc.questType); //Starts quest based off type
        }
        if(npc.hasChain)
        {
            if(!npc.chain.canBeInteractedWith)
                npc.chain.canBeInteractedWith = true;
            if(npc.hasChainQuest)
            {
                npc.chain.dialogueNumber++;
                npc.chain.quest = npc.chainNewQuest;
                npc.chain.questType = npc.chainQuestName;
                npc.chain.hasQuest = true;
            }
        }
        if(npc.disableInteractionWhenDone)
        {
            npc.canBeInteractedWith = false;
        }
    }
}