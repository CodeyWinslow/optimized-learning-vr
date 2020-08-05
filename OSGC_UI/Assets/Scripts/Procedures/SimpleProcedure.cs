using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class SimpleProcedure : ProcedureBase
{
    delegate void Step(BaseControl cont);
    List<Step> steps;
    List<Step>.Enumerator currentStep;

    bool stepsFinished = false;
    bool wrongStep = false;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        ResetUI();
        stepsFinished = false;
        wrongStep = false;
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
            wrongStep = true;
        }
        else
        {
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
            wrongStep = true;
        }
        else if (control == controller.Controls.button9)
        {
            if ((controller.Controls.slider2.Value < sliderValueMin)
               || (controller.Controls.slider2.Value > sliderValueMax))
            {
                wrongStep = true;
            }
            else
            {
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
