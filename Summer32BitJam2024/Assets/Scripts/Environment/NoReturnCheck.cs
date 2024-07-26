using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoReturnCheck : MonoBehaviour
{
    public GameObject resetPos;

    public DialogueSystem ds;
    public TextAsset dialogueFile;
    public List<Texture> iconList;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ds.StartDialogue(dialogueFile, iconList, false);
        }
    }
}
