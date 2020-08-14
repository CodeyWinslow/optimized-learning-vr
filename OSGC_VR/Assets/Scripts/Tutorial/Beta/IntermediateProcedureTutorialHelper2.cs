using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateProcedureTutorialHelper2 : MonoBehaviour
{
    //Green
    public GameObject WhenGreenHint;
    public GameObject WhenGreenButton;
    public GameObject GreenHintOne;
    public GameObject GreenHintTwo;

    //Yellow
    public GameObject WhenYellowHint;
    public GameObject WhenYellowButton;
    public GameObject YellowHintOne;
    public GameObject YellowHintTwo;
    public GameObject YellowHintThree;

    //Red
    public GameObject WhenRedHint;
    public GameObject WhenRedButton;
    public GameObject RedHintOne;
    public GameObject RedHintTwo;
    public GameObject RedHintThree;
    public GameObject RedHintFour;
    public GameObject RedHintFive;

    //Repeat
    public GameObject RepeatMessage;

    public void TurnAllOff()
    {
        WhenGreenHint.SetActive(false);
        WhenGreenButton.SetActive(false);
        GreenHintOne.SetActive(false);
        GreenHintTwo.SetActive(false);


        WhenYellowHint.SetActive(false);
        WhenYellowButton.SetActive(false);
        YellowHintOne.SetActive(false);
        YellowHintTwo.SetActive(false);
        YellowHintThree.SetActive(false);


        WhenRedHint.SetActive(false);
        WhenRedButton.SetActive(false);
        RedHintOne.SetActive(false);
        RedHintTwo.SetActive(false);
        RedHintThree.SetActive(false);
        RedHintFour.SetActive(false);
        RedHintFive.SetActive(false);
    }
}
