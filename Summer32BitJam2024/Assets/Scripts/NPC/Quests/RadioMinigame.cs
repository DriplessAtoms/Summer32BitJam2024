using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMinigame : MonoBehaviour
{
    public GameObject leftDial;
    public GameObject rightDial;

    private GameObject selectedDial;

    public GameObject leftArrow;
    public GameObject rightArrow;

    public GameObject leftArrowLimitTop;
    public GameObject leftArrowLimitDown;

    public GameObject rightArrowLimitTop;
    public GameObject rightArrowLimitDown;

    public int targetAngleLeft;
    public int targetAngleRight;

    public float speed;

    public float xAxis;

    public AudioSource leftAudio;
    public AudioSource rightAudio;

    private float leftDialRot = 0;
    private float rightDialRot = 0;

    public float leftArrowLerp = 0;
    public float rightArrowLerp = 0;

    private bool isLeftDial = true;

    private float timer;

    private QuestDataScript questData;

    void Update()
    {
        if(Input.GetKeyDown("w") || Input.GetKeyDown("s"))
        {
            if(isLeftDial)
            {
                leftDialRot = xAxis;
                xAxis = rightDialRot;
                isLeftDial = false;
                selectedDial = rightDial;
            }
            else if(!isLeftDial)
            {
                rightDialRot = xAxis;
                xAxis = leftDialRot;
                isLeftDial = true;
                selectedDial = leftDial;
            }
        }
        leftArrowLerp = Mathf.Abs(leftDial.transform.rotation.eulerAngles.z / 360);
        leftArrow.transform.position = Vector3.Lerp(leftArrowLimitTop.transform.position, leftArrowLimitDown.transform.position, leftArrowLerp);

        if (leftDial.transform.rotation.eulerAngles.z == targetAngleLeft)
            leftAudio.volume = 1;
        else
            leftAudio.volume = 0;

        rightArrowLerp = Mathf.Abs(rightDial.transform.rotation.eulerAngles.z / 360);
        rightArrow.transform.position = Vector3.Lerp(rightArrowLimitTop.transform.position, rightArrowLimitDown.transform.position, rightArrowLerp);

        if (rightDial.transform.rotation.eulerAngles.z == targetAngleRight)
            rightAudio.volume = 1;
        else
            rightAudio.volume = 0;

        if (leftDial.transform.rotation.eulerAngles.z == targetAngleLeft && rightDial.transform.rotation.eulerAngles.z == targetAngleRight)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                EndMinigame();
        }
        else
            timer = 2;
    }
    void FixedUpdate()
    {
        xAxis += Input.GetAxisRaw("Horizontal") *speed;
        selectedDial.transform.rotation = Quaternion.Euler(0,0,xAxis);
    }
    public void StartMinigame(int startL, int startR, int targetL, int targetR, QuestDataScript qds)
    {
        isLeftDial = true;
        selectedDial = leftDial;

        xAxis = startL;

        leftDial.transform.rotation = Quaternion.Euler(0, 0, startL);
        rightDial.transform.rotation = Quaternion.Euler(0, 0, startR);

        targetAngleLeft = targetL;
        targetAngleRight = targetR;

        questData = qds;
    }
    private void EndMinigame()
    {
        questData.EndQuestRadioMinigame();
    }
}