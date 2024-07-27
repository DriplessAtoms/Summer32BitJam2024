using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public GameObject player;

    public GameObject resetPos;

    public GameObject transitionScreen;
    public RawImage transitionImage;

    private Color transitionImageColor;

    public float transitionSpeed;

    private bool teleport;
    private bool transition;
    private bool unTransition;

    private float transitionValue;

    private QuestDataScript scriptCall;

    private bool scriptCallWhenUnfadeStart;

    void Start()
    {
        teleport = false;
        transition = false;
        unTransition = false;
        transitionValue = 0;
        transitionScreen.SetActive(false);
        scriptCall = null;
    }

    void Update()
    {
        if (transition && transitionValue < 1)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.GetComponent<Rigidbody>().useGravity = false;

            transitionValue += Time.deltaTime * transitionSpeed;
            transitionImageColor = transitionImage.color;

            transitionImageColor.a = transitionValue;
            transitionImage.color = transitionImageColor;
        }
        else if (transition && transitionValue >= 1)
        {
            teleport = true;
            transition = false;
        }

        if (teleport && player.transform.position != resetPos.transform.position && player.transform.rotation != resetPos.transform.rotation)
        {
            player.transform.position = resetPos.transform.position;
            player.transform.rotation = resetPos.transform.rotation;
            player.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (teleport && player.transform.position == resetPos.transform.position && player.transform.rotation == resetPos.transform.rotation)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            player.GetComponent<Rigidbody>().useGravity = true;

            if (scriptCall != null)
            {
                scriptCall.EndOfTransitionRace();
                scriptCall = null;
            }

            unTransition = true;
            teleport = false;
        }

        if (unTransition && transitionValue > 0)
        {
            transitionValue -= Time.deltaTime * transitionSpeed;
            transitionImageColor = transitionImage.color;

            transitionImageColor.a = transitionValue;
            transitionImage.color = transitionImageColor;
        }
        else if (unTransition && transitionValue <= 0)
        {
            transitionScreen.SetActive(false);
            unTransition = false;
        }
    }
    public void StartTeleportationTransition(GameObject targetPos)
    {
        resetPos = targetPos;
        transitionScreen.SetActive(true);
        transition = true;
    }
    public void StartTeleportationTransitionWithEndTrigger(GameObject targetPos, QuestDataScript qds)
    {
        resetPos = targetPos;
        scriptCall = qds;
        transitionScreen.SetActive(true);
        transition = true;
    }
}
