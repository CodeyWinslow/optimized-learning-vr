using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public float Value
    {
        get { return sliderComp.value; }
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
