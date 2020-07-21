using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingController : MonoBehaviour
{
    public MeterControl meter;
    public int meterNum;

    public LightControl uiLight;
    public SettingControl setting;
    public SliderControl slider;

    void Start()
    {
        setting.SettingChangedEvent += OnSettingChanged;
        slider.SliderChangedEvent += OnSliderChanged;
    }

    public void OnSettingChanged(BaseControl setting, System.Object option)
    {
        Debug.Log(setting.ToString() + "changed setting to option " + option.ToString());
    }

    public void OnSliderChanged(BaseControl s, System.Object v)
    {
        Debug.Log(slider.ToString() + " changed to " + v.ToString());
    }
}
