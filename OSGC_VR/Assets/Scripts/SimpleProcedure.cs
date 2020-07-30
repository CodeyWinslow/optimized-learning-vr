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
        steps = new List<Step>();
        steps.Add(StepOne);
        steps.Add(StepTwo);
        currentStep = steps.GetEnumerator();
        currentStep.MoveNext();
        SetUpEvents();
    }

    void SetUpEvents()
    {
        //settings
        controller.Controls.setting1.SettingChangedEvent += UIHandler;
        controller.Controls.setting2.SettingChangedEvent += UIHandler;
        //buttons
        controller.Controls.button1.ButtonClickedEvent += UIHandler;
        controller.Controls.button2.ButtonClickedEvent += UIHandler;
        controller.Controls.button3.ButtonClickedEvent += UIHandler;
        controller.Controls.button4.ButtonClickedEvent += UIHandler;
        controller.Controls.button5.ButtonClickedEvent += UIHandler;
        controller.Controls.button6.ButtonClickedEvent += UIHandler;
        controller.Controls.button7.ButtonClickedEvent += UIHandler;
        controller.Controls.button8.ButtonClickedEvent += UIHandler;
        controller.Controls.button9.ButtonClickedEvent += UIHandler;
        //toggles
        controller.Controls.toggle1.ToggleChangedEvent += UIHandler;
        controller.Controls.toggle2.ToggleChangedEvent += UIHandler;
        controller.Controls.toggle3.ToggleChangedEvent += UIHandler;
        controller.Controls.toggle4.ToggleChangedEvent += UIHandler;
        //sliders
        controller.Controls.slider1.SliderChangedEvent += UIHandler;
        controller.Controls.slider2.SliderChangedEvent += UIHandler;
        controller.Controls.slider3.SliderChangedEvent += UIHandler;
    }

    void UnsetEvents()
    {
        //settings
        controller.Controls.setting1.SettingChangedEvent -= UIHandler;
        controller.Controls.setting2.SettingChangedEvent -= UIHandler;
        //buttons
        controller.Controls.button1.ButtonClickedEvent -= UIHandler;
        controller.Controls.button2.ButtonClickedEvent -= UIHandler;
        controller.Controls.button3.ButtonClickedEvent -= UIHandler;
        controller.Controls.button4.ButtonClickedEvent -= UIHandler;
        controller.Controls.button5.ButtonClickedEvent -= UIHandler;
        controller.Controls.button6.ButtonClickedEvent -= UIHandler;
        controller.Controls.button7.ButtonClickedEvent -= UIHandler;
        controller.Controls.button8.ButtonClickedEvent -= UIHandler;
        controller.Controls.button9.ButtonClickedEvent -= UIHandler;
        //toggles
        controller.Controls.toggle1.ToggleChangedEvent -= UIHandler;
        controller.Controls.toggle2.ToggleChangedEvent -= UIHandler;
        controller.Controls.toggle3.ToggleChangedEvent -= UIHandler;
        controller.Controls.toggle4.ToggleChangedEvent -= UIHandler;
        //sliders
        controller.Controls.slider1.SliderChangedEvent -= UIHandler;
        controller.Controls.slider2.SliderChangedEvent -= UIHandler;
        controller.Controls.slider3.SliderChangedEvent -= UIHandler;
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
            wrongStep = true;
        else
            NextStep();
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
             if((controller.Controls.slider2.Value < sliderValueMin)
                || (controller.Controls.slider2.Value > sliderValueMax))
                wrongStep = true;
            else
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

    public void UIHandler(BaseControl c)
    {
        currentStep.Current(c);
    }
}
