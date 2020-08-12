using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProcedure2 : ProcedureBase
{
    delegate void Step(BaseControl cont);
    List<Step> steps;
    List<Step>.Enumerator currentStep;

    bool stepsFinished = false;
    bool wrongStep = false;

    bool greenTimerOn = false;
    const float greenTime = 3f;
    float greenTimer;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        ResetUI();
        stepsFinished = false;
        wrongStep = false;
        greenTimerOn = false;
        greenTimer = greenTime;

        steps = new List<Step>();
        steps.Add(StepOne);
        steps.Add(StepTwo);
        steps.Add(StepThree);
        steps.Add(StepFour);
        steps.Add(StepFive);
        steps.Add(StepSix);
        steps.Add(StepSeven);

        currentStep = steps.GetEnumerator();
        NextStep();
        controller.Controls.SubscribeToAllControls(UIHandler);
    }

    public override void RunUpdate()
    {
        if (greenTimerOn)
        {
            greenTimer -= Time.deltaTime;
            if (greenTimer <= 0)
            {
                controller.Controls.greenLight.Lit = true;
                greenTimerOn = false;
            }
        }
        if (wrongStep)
        {
            controller.Controls.UnsubscribeToAllControls(UIHandler);
            EndProcedure(false);
        }
        else
        {
            if (stepsFinished)
            {
                controller.Controls.UnsubscribeToAllControls(UIHandler);
                EndProcedure(true);
            }
        }
    }

    void StepOne(BaseControl control)
    {
        if (control != controller.Controls.setting1
            || !controller.Controls.setting1.option2.isOn)
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
        if (control != controller.Controls.setting2
            || !controller.Controls.setting2.option3.isOn)
        {
            wrongStep = true;
        }
        else
        {
            NextStep();
        }
    }

    void StepThree(BaseControl control)
    {
        if (control != controller.Controls.slider1)
        {
            if (control == controller.Controls.slider3
                && controller.Controls.slider1.Value == 1.0)
                NextStep();
            else
                wrongStep = true;
        }
    }

    void StepFour(BaseControl control)
    {
        if (control != controller.Controls.slider3)
        {
            if (control == controller.Controls.toggle1)
            {
                NextStep();
                StepFive(control);
            }
            else
                wrongStep = true;

        }
    }

    void StepFive(BaseControl control)
    {
        if (control == controller.Controls.toggle1)
            NextStep();
        else
            wrongStep = true;
    }

    void StepSix(BaseControl control)
    {
        if (control == controller.Controls.toggle4)
        {
            NextStep();
            //start timer for green light
            greenTimerOn = true;
        }
        else
            wrongStep = true;
    }

    void StepSeven(BaseControl control)
    {
        if (control == controller.Controls.button8
            && controller.Controls.greenLight.Lit)
            NextStep();
        else
            wrongStep = true;

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
        controller.Controls.toggle1.SetValue(true);
        controller.Controls.toggle2.SetValue(true);
        controller.Controls.toggle3.SetValue(true);
        controller.Controls.toggle4.SetValue(true);

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
