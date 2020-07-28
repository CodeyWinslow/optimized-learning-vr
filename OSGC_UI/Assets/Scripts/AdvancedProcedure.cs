using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedProcedure : ProcedureBase
{
    float procedureTimer = 45f;
    float taskTimer = 0;
    const float timeToChange = 15f;

    float meter1Val = 500f;
    float meter2Val = 500f;
    float meter3Val = 500f;

    int meter1modifier = 0;
    int meter2modifier = 0;
    int meter3modifier = 0;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        meter1modifier = UnityEngine.Random.Range(-50, 51);
        meter2modifier = UnityEngine.Random.Range(-50, 51);
        meter3modifier = UnityEngine.Random.Range(-50, 51);
        taskTimer = timeToChange;
        SetupUI();
    }

    public override void RunUpdate()
    {
        //check if timer is up
        if (procedureTimer <= 0)
        {
                EndProcedure(true);
        }
        //challenge logic
        else
        {
            procedureTimer -= Time.deltaTime;
            taskTimer -= Time.deltaTime;

            if (taskTimer <= 0)
            {
                meter1modifier = UnityEngine.Random.Range(-50, 51);
                meter2modifier = UnityEngine.Random.Range(-50, 51);
                meter3modifier = UnityEngine.Random.Range(-50, 51);
                taskTimer = timeToChange;
            }

            int meter1adjust = Convert.ToInt32((controller.Controls.slider1.Value * 100) - 50);
            int meter2adjust = Convert.ToInt32((controller.Controls.slider2.Value * 100) - 50);
            int meter3adjust = Convert.ToInt32((controller.Controls.slider3.Value * 100) - 50);

            meter1Val += (meter1modifier + meter1adjust) * Time.deltaTime;
            meter2Val += (meter2modifier + meter2adjust) * Time.deltaTime;
            meter3Val += (meter3modifier + meter3adjust) * Time.deltaTime;

            controller.Controls.meter1.Value = Convert.ToInt32(meter1Val);
            controller.Controls.meter2.Value = Convert.ToInt32(meter2Val);
            controller.Controls.meter3.Value = Convert.ToInt32(meter3Val);

            if (meter1Val < 0 || meter1Val > 999
                || meter2Val < 0 || meter2Val > 999
                || meter3Val < 0 || meter3Val > 999)
            {
                EndProcedure(false);
            }
        }
    }

    void SetupUI()
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
