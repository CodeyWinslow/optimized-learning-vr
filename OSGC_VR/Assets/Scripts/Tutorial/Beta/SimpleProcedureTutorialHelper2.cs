using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProcedureTutorialHelper2 : MonoBehaviour
{
    public GameObject HintOne;
    public GameObject HintTwo;
    public GameObject HintThree;
    public GameObject HintFour;
    public GameObject HintFive;
    public GameObject HintSix;
    public GameObject HintSeven;
    public GameObject HintEight;

    public void TurnAllOff()
    {
        HintOne.SetActive(false);
        HintTwo.SetActive(false);
        HintThree.SetActive(false);
        HintFour.SetActive(false);
        HintFive.SetActive(false);
        HintSix.SetActive(false);
        HintSeven.SetActive(false);
        HintEight.SetActive(false);
    }
}
