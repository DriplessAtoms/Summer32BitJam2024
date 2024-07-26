using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoReturnCheck : MonoBehaviour
{
    public GameObject player;
    public GameObject resetPos;

    public PlayerInteraction playerInteractionScript;
    public TextAsset dialogueFile;
    public List<Texture> iconList;

    public FadeTransition fadeTransitionScript;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteractionScript.TriggerNoReturnDialogue(dialogueFile, iconList, this);
        }
    }
    public void ResetPos()
    {
        fadeTransitionScript.StartTeleportationTransition(resetPos);
    }
}
