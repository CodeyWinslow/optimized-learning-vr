using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedProcedureTutorialHelper2 : MonoBehaviour
{
    public MessageSequence preamble;

    public GameObject ExplainMeter1a;
    public GameObject ExplainMeter2a;
    public GameObject ExplainMeter3;
    public GameObject Toggle2Hinta;
    public GameObject ExplainMeter1b;
    public GameObject ExplainMeter2b;
    public GameObject ExplainSetting1a;
    public GameObject ExplainSetting1b;
    public GameObject ExplainSetting2a;
    public GameObject ExplainSetting2b;
    public GameObject ExplainSetting2c;
    public GameObject Toggle2Hintb;
    public GameObject Toggle1Hinta;
    public GameObject RedLightHint;
    public GameObject YellowLightHint;
    public GameObject GreenLightHint;
    public GameObject ExplainMeter1c;
    public GameObject ExplainSetting1c;
    public GameObject Setting1Hint;
    public GameObject Setting2Hint;
    public GameObject Toggle1Hintb;
    public GameObject ExplainEnding;
    public GameObject ExplainEndingb;

    public void TurnAllOff()
    {
        preamble.Finish();
        ExplainMeter1a.SetActive(false);
        ExplainMeter2a.SetActive(false);
        ExplainMeter3.SetActive(false);
        Toggle2Hinta.SetActive(false);
        ExplainMeter1b.SetActive(false);
        ExplainMeter2b.SetActive(false);
        ExplainSetting1a.SetActive(false);
        ExplainSetting1b.SetActive(false);
        ExplainSetting2a.SetActive(false);
        ExplainSetting2b.SetActive(false);
        ExplainSetting2c.SetActive(false);
        ExplainSetting2c.GetComponentInChildren<Button>(true).gameObject.SetActive(false);
        Toggle2Hintb.SetActive(false);
        Toggle1Hinta.SetActive(false);
        RedLightHint.SetActive(false);
        YellowLightHint.SetActive(false);
        GreenLightHint.SetActive(false);
        ExplainMeter1c.SetActive(false);
        ExplainSetting1c.SetActive(false);
        Setting1Hint.SetActive(false);
        Setting2Hint.SetActive(false);
        Toggle1Hintb.SetActive(false);
        ExplainEnding.SetActive(false);
        ExplainEndingb.SetActive(false);
    }
}
