using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float distance;
    public Transform interactionDirection;
    public DialogueSystem ds;

    //Might want to add disable player command here instead of dialogue system

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(interactionDirection.position, interactionDirection.TransformDirection(Vector3.forward), out hit, distance))
        {
            if (hit.collider.tag == "NPC")
            {
                NPC_Data npc = hit.collider.GetComponent<NPC_Data>();
                if (Input.GetKeyDown("e"))
                {
                    TriggerDialogue(npc.dialogueList[0]);
                }
            }
        }
    }
    void TriggerDialogue(TextAsset dialogue)
    {
        ds.StartDialogue(dialogue);
    }
}
