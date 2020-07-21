using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderControl : BaseControl
{
    public delegate void SliderChangedDelegate(BaseControl slider, System.Object value);
    public event SliderChangedDelegate SliderChangedEvent;

    private Slider sliderComp;

    void Awake()
    {
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
            SliderChangedEvent(this, val);
    }
}
