﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Slider))]
public class SliderControl : BaseControl
{
    public event BaseControlEventHandler SliderChangedEvent;

    private Slider sliderComp;

    void Awake()
    {
        _controlType = BaseControlType.Slider;
        sliderComp = GetComponent<Slider>();
        sliderComp.onValueChanged.AddListener(OnValueChanged);
        SetLabel();
    }

    public float Value
    {
        get { return sliderComp.value; }
        set {
            sliderComp.value = value;
            LinearMapping map = GetComponent<LinearMapping>();
            if (map != null)
            {
                map.value = value;
            }
        }
    }

    public void OnValueChanged(float val)
    {
        if (SliderChangedEvent != null)
            SliderChangedEvent(this);
    }

    public override object ControlValue()
    {
        return Value;
    }
}
