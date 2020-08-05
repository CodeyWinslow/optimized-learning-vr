using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class SimpleProcedureTutorial : ProcedureBase
{
    delegate void Step(BaseControl cont);
    List<Step> steps;
    List<Step>.Enumerator currentStep;

    bool stepsFinished = false;
    bool wrongStep = false;

    public bool tutorial = false;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        ResetUI();
        if (controller.Controls.simpleTutorialHelper != null)
        {
            tutorial = true;
        }
        steps = new List<Step>();
        steps.Add(StepOne);
        steps.Add(StepTwo);
        currentStep = steps.GetEnumerator();
        NextStep();
        SetUpEvents();
    }

    void SetUpEvents()
    {
        controller.Controls.SubscribeToAllControls(UIHandler);
    }

    void UnsetEvents()
    {
        controller.Controls.UnsubscribeToAllControls(UIHandler);
    }

    public override void RunUpdate()
    {
        if (wrongStep)
        {
            UnsetEvents();
            EndProcedure(false);
        }
        else
        {
            if (stepsFinished)
            {
                UnsetEvents();
                controller.Controls.greenLight.Lit = true;
                EndProcedure(true);
            }
        }
    }

    void StepOne(BaseControl control)
    {
        if (control != controller.Controls.button4)
        {
            if (!tutorial)
                wrongStep = true;
        }
        else
        {
            if (tutorial)
                controller.Controls.simpleTutorialHelper.StepOneHint.SetActive(false);
            NextStep();
        }
    }

    void StepTwo(BaseControl control)
    {
        float sliderValueMin = 0.6f;
        float sliderValueMax = 0.7f;

        if (control != controller.Controls.slider2 &&
            control != controller.Controls.button9)
        {
            if (!tutorial)
                wrongStep = true;
        }
        else if (control == controller.Controls.slider2 && tutorial)
        {
            controller.Controls.simpleTutorialHelper.StepTwoAHint.SetActive(false);
            controller.Controls.simpleTutorialHelper.StepTwoBHint.SetActive(true);
        }
        else if (control == controller.Controls.button9)
        {
            if ((controller.Controls.slider2.Value < sliderValueMin)
               || (controller.Controls.slider2.Value > sliderValueMax))
            {
                if (!tutorial)
                    wrongStep = true;
            }
            else
            {
                if (tutorial)
                    controller.Controls.simpleTutorialHelper.StepTwoBHint.SetActive(false);
                NextStep();
            }
        }
    }

    void NextStep()
    {
        if (!currentStep.MoveNext())
        {
            stepsFinished = true;
        }
        else
        {
            if (tutorial)
            {
                if (currentStep.Current == StepOne)
                    controller.Controls.simpleTutorialHelper.StepOneHint.SetActive(true);
                else if (currentStep.Current == StepTwo)
                    controller.Controls.simpleTutorialHelper.StepTwoAHint.SetActive(true);
            }
        }
    }

    void ResetUI()
    {
        //lights
        controller.Controls.greenLight.Lit = false;
        controller.Controls.yellowLight.Lit = false;
        controller.Controls.redLight.Lit = false;

        //meters
        controller.Controls.meter1.Value = 0;
        controller.Controls.meter2.Value = 0;
        controller.Controls.meter3.Value = 0;

        //settings
        controller.Controls.setting1.option1.isOn = true;
        controller.Controls.setting2.option1.isOn = true;

        //toggles
        controller.Controls.toggle1.isOn = true;
        controller.Controls.toggle2.isOn = true;
        controller.Controls.toggle3.isOn = true;
        controller.Controls.toggle4.isOn = true;

        //sliders
        controller.Controls.slider1.Value = 0.5f;
        controller.Controls.slider2.Value = 0.5f;
        controller.Controls.slider3.Value = 0.5f;
    }

    public void UIHandler(BaseControl c)
    {
        currentStep.Current(c);
    }
}
