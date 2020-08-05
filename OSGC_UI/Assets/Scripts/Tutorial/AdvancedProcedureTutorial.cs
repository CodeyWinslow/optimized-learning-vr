using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedProcedureTutorial : ProcedureBase
{
    float taskTimer = 0;
    const float timeToChange = 15f;

    float meter1Val = 500f;
    float meter2Val = 500f;
    float meter3Val = 500f;

    int meter1modifier = 0;
    int meter2modifier = 0;
    int meter3modifier = 0;

    bool pauseMeters = true;
    bool meter1on = false;
    bool meter2on = false;
    bool meter3on = false;
    bool taskTimerOn = false;

    enum TutState
    {
        Explain1,
        Explain2,
        Explain3,
        Explain4,
        Fail
    };
    TutState currentState;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        meter1Val = meter2Val = meter3Val = 500f;
        pauseMeters = true;
        meter1on = meter2on = meter3on = true;
        taskTimerOn = false;

        RefreshMeters();

        taskTimer = timeToChange;

        currentState = TutState.Explain1;
        controller.Controls.slider1.SliderChangedEvent += OnSliderChanged;
        controller.Controls.slider2.SliderChangedEvent += OnSliderChanged;
        controller.Controls.slider3.SliderChangedEvent += OnSliderChanged;
        controller.Controls.advancedTutorialHelper.Explain1.SetActive(true);
        controller.Controls.advancedTutorialHelper.OKButton.SetActive(true);
        controller.Controls.advancedTutorialHelper.OKButton.GetComponent<Button>().onClick.AddListener(()=>{
            controller.Controls.advancedTutorialHelper.Explain2.SetActive(true);
            controller.Controls.advancedTutorialHelper.Explain1.SetActive(false);
            controller.Controls.advancedTutorialHelper.OKButton.SetActive(false);
            currentState = TutState.Explain2;
        });

        ResetUI();
    }

    void RefreshMeters()
    {
        meter1modifier = UnityEngine.Random.Range(-50, 51);
        meter2modifier = UnityEngine.Random.Range(-50, 51);
        meter3modifier = UnityEngine.Random.Range(-50, 51);
    }

    public void OnSliderChanged(BaseControl c)
    {
        if (currentState == TutState.Explain2)
        {
            controller.Controls.advancedTutorialHelper.Explain3.SetActive(true);
            controller.Controls.advancedTutorialHelper.Explain2.SetActive(false);
            pauseMeters = false;
            taskTimerOn = true;
            currentState = TutState.Explain3;
            controller.Controls.slider1.SliderChangedEvent -= OnSliderChanged;
            controller.Controls.slider2.SliderChangedEvent -= OnSliderChanged;
            controller.Controls.slider3.SliderChangedEvent -= OnSliderChanged;
        }
    }

    void Failed()
    {
        pauseMeters = true;
        taskTimerOn = false;
        controller.Controls.advancedTutorialHelper.Failure.SetActive(true);
        controller.Controls.advancedTutorialHelper.RestartButton.SetActive(true);
        controller.Controls.advancedTutorialHelper.RestartButton.GetComponent<Button>().onClick.AddListener(() => {
            controller.Controls.advancedTutorialHelper.Explain1.SetActive(false);
            controller.Controls.advancedTutorialHelper.OKButton.SetActive(false);
            controller.Controls.advancedTutorialHelper.Explain2.SetActive(false);
            controller.Controls.advancedTutorialHelper.Explain3.SetActive(false);
            controller.Controls.advancedTutorialHelper.Explain4.SetActive(false);
            controller.Controls.advancedTutorialHelper.Failure.SetActive(false);
            controller.Controls.advancedTutorialHelper.RestartButton.SetActive(false);
            BeginProcedure(controller);
        });
    }

    public override void RunUpdate()
    {
        //challenge logic
        if (taskTimerOn)
            taskTimer -= Time.deltaTime;

        if (taskTimer <= 0)
        {
            if (currentState == TutState.Explain3)
            {
                controller.Controls.advancedTutorialHelper.Explain3.SetActive(false);
                controller.Controls.advancedTutorialHelper.Explain4.SetActive(true);
                currentState = TutState.Explain4;
                RefreshMeters();
                taskTimer = timeToChange;
            }
            else if (currentState == TutState.Explain4)
            {
                controller.Controls.advancedTutorialHelper.Explain4.SetActive(false);
                EndProcedure(true);
            }
        }

        int meter1adjust = Convert.ToInt32((controller.Controls.slider1.Value * 100) - 50);
        int meter2adjust = Convert.ToInt32((controller.Controls.slider2.Value * 100) - 50);
        int meter3adjust = Convert.ToInt32((controller.Controls.slider3.Value * 100) - 50);

        if (!pauseMeters)
        {
            if (meter1on)
                meter1Val += (meter1modifier + meter1adjust) * Time.deltaTime;
            if (meter2on)
                meter2Val += (meter2modifier + meter2adjust) * Time.deltaTime;
            if (meter3on)
                meter3Val += (meter3modifier + meter3adjust) * Time.deltaTime;
        }

        controller.Controls.meter1.Value = Convert.ToInt32(meter1Val);
        controller.Controls.meter2.Value = Convert.ToInt32(meter2Val);
        controller.Controls.meter3.Value = Convert.ToInt32(meter3Val);

        if ((meter1Val < 0 && (meter1modifier + meter1adjust < 0))
            || (meter1Val > 999 && (meter1modifier + meter1adjust > 0)))
        {
            Failed();
        }

        if ((meter2Val < 0 && (meter2modifier + meter2adjust < 0))
            || (meter2Val > 999 && (meter2modifier + meter2adjust > 0)))
        {
            Failed();
        }

        if ((meter3Val < 0 && (meter3modifier + meter3adjust < 0))
            || (meter3Val > 999 && (meter3modifier + meter3adjust > 0)))
        {
            Failed();
        }
    }

    void ResetUI()
    {
        //lights
        controller.Controls.redLight.Lit = false;
        controller.Controls.yellowLight.Lit = false;
        controller.Controls.greenLight.Lit = false;

        //switches
        controller.Controls.toggle1.SetValue(false);
        controller.Controls.toggle2.SetValue(false);
        controller.Controls.toggle3.SetValue(false);
        controller.Controls.toggle4.SetValue(false);

        //sliders
        controller.Controls.slider1.Value = 0.5f;
        controller.Controls.slider2.Value = 0.5f;
        controller.Controls.slider3.Value = 0.5f;

        //settings
        controller.Controls.setting1.SelectedOption = 0;
        controller.Controls.setting2.SelectedOption = 0;
    }
}
