using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public float dialogueSpeed;

    //public TextAsset dialogueScriptFile; //clean up what doesn't need to be seen
    public List<Texture> icons = new List<Texture>();

    public TMP_Text nameText;
    public TMP_Text lineText;
    public RawImage characterIcon;

    public GameObject dialogueScreen;
    public HoverboardMovement hm;
    public HoverboardDirection hd;
    public HoverboardPhysics hp;
    public PlayerInteraction interactionSystem;

    public GameObject player;

    public AudioSource clickSound;

    private string dialogueScript;
    private string dialogueLine;

    private string nameStr;
    private string line;

    private int currentPos = 0;

    private bool dialogueTriggered = false;

    private bool endOfDialogue = false;

    private bool isAnNPC;

    public 
    // Start is called before the first frame update
    void Start()
    {
        //dialogueScript = dialogueScriptFile.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && dialogueTriggered)
        {
            clickSound.Play();
            if (endOfDialogue)
                EndDialogue();
            else
                NextLine();
        }
    }
    public void StartDialogue(TextAsset dialogueFile, List<Texture> iconList, bool fromNPC) //add parameters for icons and dialogue
    {
        dialogueScript = dialogueFile.text;
        icons = iconList;

        isAnNPC = fromNPC;

        dialogueScreen.SetActive(true);
        hm.enabled = false;
        hd.enabled = false;
        hp.enabled = false;
        interactionSystem.busy = true;
        interactionSystem.enabled = false;

        //Disables All Movement From Player
        player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        NextLine();

        dialogueTriggered = true;
    }
    void NextLine()
    {
        if (dialogueScript.IndexOf("\n", currentPos) < 0)
        {
            dialogueLine = dialogueScript.Substring(currentPos, dialogueScript.Length - currentPos);

            nameStr = dialogueLine.Substring(0, dialogueLine.IndexOf(":"));
            line = dialogueLine.Substring(dialogueLine.IndexOf(":") + 1, dialogueLine.Length - nameStr.Length - 1);

            StartCoroutine(ReadLine());

            endOfDialogue = true;
            return;
        }

        dialogueLine = dialogueScript.Substring(currentPos, dialogueScript.IndexOf("\n", currentPos) - currentPos);
        nameStr = dialogueLine.Substring(0, dialogueLine.IndexOf(":"));
        line = dialogueLine.Substring(dialogueLine.IndexOf(":") + 1, dialogueLine.Length - nameStr.Length - 1);

        currentPos = dialogueScript.IndexOf("\n", currentPos) + 1;

        StartCoroutine(ReadLine());
    }
    IEnumerator ReadLine()
    {
        //Display all dialogue with name and line displayed and with their corresponding photo
        nameText.text = nameStr;
        ChangeIcon(nameStr);
        lineText.text = "";
        while (lineText.text.Length < line.Length)
        {
            lineText.text += line.Substring(lineText.text.Length, 1);
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
    void ChangeIcon(string nameOfIcon)
    {
        for(int i = 0; i < icons.Count; i++)
        {
            Debug.Log(icons[i].name);
            if(nameOfIcon == icons[i].name)
            {
                characterIcon.texture = icons[i];
                return;
            }
        }
    }
    void EndDialogue()
    {
        currentPos = 0;

        interactionSystem.enabled = true;
        interactionSystem.busy = false;
        
        interactionSystem.EndDialogueCheck(isAnNPC); //Interaction System checks if there's any action needed before giving control back to the player

        hm.enabled = true;
        hd.enabled = true;
        hp.enabled = true;

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        endOfDialogue = false;
        dialogueTriggered = false;
        dialogueScreen.SetActive(false);
        //End dialogue
    }
}
