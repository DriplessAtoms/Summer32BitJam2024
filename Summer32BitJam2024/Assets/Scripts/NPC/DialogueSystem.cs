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
    public PlayerInteraction interactionSystem;

    private string dialogueScript;
    private string dialogueLine;

    private string nameStr;
    private string line;

    private int currentPos = 0;

    private bool dialogueTriggered = false;

    private bool endOfDialogue = false;
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
    public void StartDialogue(TextAsset dialogueFile, List<Texture> iconList) //add parameters for icons and dialogue
    {
        dialogueScript = dialogueFile.text;
        icons = iconList;

        dialogueScreen.SetActive(true);
        hm.enabled = false;
        interactionSystem.enabled = false;

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
        hm.enabled = true; ;
        interactionSystem.enabled = true;
        endOfDialogue = false;
        dialogueTriggered = false;
        dialogueScreen.SetActive(false);
        //End dialogue
    }
}
