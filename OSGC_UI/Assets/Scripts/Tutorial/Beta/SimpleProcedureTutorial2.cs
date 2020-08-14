using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProcedureTutorial2 : ProcedureBase
{
    delegate void Step(BaseControl cont);
    List<Step> steps;
    List<Step>.Enumerator currentStep;

    bool stepsFinished = false;

    bool greenTimerOn = false;
    const float greenTime = 3f;
    float greenTimer;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        ResetUI();
        stepsFinished = false;
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

        //show hint
        if (controller.Controls.simpleTutorialHelper2 != null)
            controller.Controls.simpleTutorialHelper2.HintOne.SetActive(true);
    }

    public override void Stop()
    {
        greenTimerOn = false;
        if (Running) controller.Controls.UnsubscribeToAllControls(UIHandler);
        controller.Controls.simpleTutorialHelper2.TurnAllOff();
        base.Stop();
    }

    public override void RunUpdate()
    {
        if (greenTimerOn)
        {
            greenTimer -= Time.deltaTime;
            if (greenTimer <= 0)
            {
                controller.Controls.greenLight.Lit = true;
                if (controller.Controls.simpleTutorialHelper2 != null)
                {
                    controller.Controls.simpleTutorialHelper2.HintSeven.SetActive(false);
                    controller.Controls.simpleTutorialHelper2.HintEight.SetActive(true);
                }
                greenTimerOn = false;
            }
        }
        if (stepsFinished)
        {
            controller.Controls.UnsubscribeToAllControls(UIHandler);
            EndProcedure(true);
        }
    }

    void StepOne(BaseControl control)
    {
        if (control == controller.Controls.setting1
            && controller.Controls.setting1.option2.isOn)
        {
            if (controller.Controls.simpleTutorialHelper2 != null)
            {
                controller.Controls.simpleTutorialHelper2.HintOne.SetActive(false);
                controller.Controls.simpleTutorialHelper2.HintTwo.SetActive(true);
            }
            NextStep();
        }
    }

    void StepTwo(BaseControl control)
    {
        if (control == controller.Controls.setting2
            && controller.Controls.setting2.option3.isOn)
        {
            if (controller.Controls.simpleTutorialHelper2 != null)
            {
                controller.Controls.simpleTutorialHelper2.HintTwo.SetActive(false);
                controller.Controls.simpleTutorialHelper2.HintThree.SetActive(true);
            }
            NextStep();
        }
    }

    void StepThree(BaseControl control)
    {
        if (control == controller.Controls.slider1)
            if (controller.Controls.simpleTutorialHelper2 != null
                && !controller.Controls.simpleTutorialHelper2.HintFour.activeSelf)
                controller.Controls.simpleTutorialHelper2.HintFour.SetActive(true);

        if (control == controller.Controls.slider3
            && controller.Controls.slider1.Value == 1.0)
            NextStep();
    }

    void StepFour(BaseControl control)
    {
        if (control == controller.Controls.slider3
            && controller.Controls.slider3.Value == 1f)
        {
            if (controller.Controls.simpleTutorialHelper2 != null)
            {
                controller.Controls.simpleTutorialHelper2.HintThree.SetActive(false);
                controller.Controls.simpleTutorialHelper2.HintFour.SetActive(false);
                controller.Controls.simpleTutorialHelper2.HintFive.SetActive(true);
                NextStep();
            }
        }
    }

    void StepFive(BaseControl control)
    {
        if (control == controller.Controls.toggle1)
        {
            if (controller.Controls.simpleTutorialHelper2 != null)
            {
                controller.Controls.simpleTutorialHelper2.HintFive.SetActive(false);
                controller.Controls.simpleTutorialHelper2.HintSix.SetActive(true);
            }
            NextStep();
        }
    }

    void StepSix(BaseControl control)
    {
        if (control == controller.Controls.toggle4)
        {
            if (controller.Controls.simpleTutorialHelper2 != null)
            {
                controller.Controls.simpleTutorialHelper2.HintSix.SetActive(false);
                controller.Controls.simpleTutorialHelper2.HintSeven.SetActive(true);
            }
            NextStep();
            greenTimerOn = true;
        }
    }

    void StepSeven(BaseControl control)
    {
        if (control == controller.Controls.button8
            && controller.Controls.greenLight.Lit)
        {
            if (controller.Controls.simpleTutorialHelper2 != null)
                controller.Controls.simpleTutorialHelper2.HintEight.SetActive(false);
            NextStep();
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
