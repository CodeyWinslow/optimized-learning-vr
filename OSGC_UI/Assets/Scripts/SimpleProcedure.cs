using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProcedure : ProcedureBase
{
    const int stepsInProcedure = 5;
    int stepsPerformed = 0;
    bool wrongStep = false;

    BaseControl[] steps;
    System.Object[] values;
    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        steps = new BaseControl[stepsInProcedure]
        {
            controller.Controls.button4,
            controller.Controls.setting1,
            controller.Controls.setting2,
            controller.Controls.toggle2,
            controller.Controls.toggle4
        };
        values = new System.Object[stepsInProcedure]
        {
            null,
            1,
            2,
            false,
            false
        };
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
            if (stepsPerformed >= stepsInProcedure)
            {
                UnsetEvents();
                EndProcedure(true);
            }
        }
    }

    void CheckValidStep(BaseControl control)
    {
        if (control == steps[stepsPerformed])
            ++stepsPerformed;
        else
            wrongStep = true;
    }

    void CheckValidStep(BaseControl control, System.Object val)
    {
        if (control == steps[stepsPerformed] && val.Equals(values[stepsPerformed]))
            ++stepsPerformed;
        else
        {
            wrongStep = true;
        }
    }

    public void UIHandler(BaseControl c)
    {
        CheckValidStep(c);
    }

    public void UIHandler(BaseControl c, System.Object val)
    {
        CheckValidStep(c, val);
    }
}
