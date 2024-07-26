using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoReturnCheck : MonoBehaviour
{
    public GameObject player;
    public GameObject resetPos;

    public PlayerInteraction playerInteractionScript;
    public TextAsset dialogueFile;
    public List<Texture> iconList;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteractionScript.TriggerNoReturnDialogue(dialogueFile, iconList, this);
        }
    }
    public void ResetPos()
    {
        Debug.Log("Done");
        player.transform.position = resetPos.transform.position;
        player.transform.rotation = resetPos.transform.rotation;
    }
}
