using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public float dialogueSpeed;

    //public TextAsset dialogueScriptFile; //clean up what doesn't need to be seen
    public List<Texture> icons = new List<Texture>();

    public TMP_Text nameText;
    public TMP_Text lineText;

    public GameObject dialogueScreen;
    public HoverboardMovement hm;
    public PlayerInteraction interactionSystem;

    [SerializeField] private string dialogueScript;
    [SerializeField] private string dialogueLine;

    [SerializeField] private string nameStr;
    [SerializeField] private string line;

    [SerializeField] private int currentPos = 0;

    [SerializeField] private bool dialogueTriggered = false;

    [SerializeField] private bool endOfDialogue = false;
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
            if (endOfDialogue)
                EndDialogue();
            else
                NextLine();
        }
    }
    public void StartDialogue(TextAsset dialogueFile) //add parameters for icons and dialogue
    {
        dialogueScript = dialogueFile.text;

        dialogueScreen.SetActive(true);
        hm.enabled = false;
        interactionSystem.enabled = false;

        dialogueTriggered = true;

        NextLine();
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
        //ChangeIcon
        lineText.text = "";
        while (lineText.text.Length < line.Length)
        {
            lineText.text += line.Substring(lineText.text.Length, 1);
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
    void EndDialogue()
    {
        currentPos = 0;
        hm.enabled = true; ;
        interactionSystem.enabled = true;
        endOfDialogue = false;
        dialogueTriggered = false;
        dialogueScreen.SetActive(false);
        //End dialogue
    }
}
