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
    public ButtonControl btn;
    bool btnToggled = false;

    void Start()
    {
        setting.SettingChangedEvent += OnSettingChanged;
        slider.SliderChangedEvent += OnSliderChanged;
        btn.ButtonClickedEvent += OnButtonPress;
    }

    public void OnSettingChanged(BaseControl setting)
    {
        Debug.Log(setting.ToString() + "changed setting to option " + setting.ControlValue().ToString());
    }

    public void OnSliderChanged(BaseControl s)
    {
        Debug.Log(slider.ToString() + " changed to " + s.ControlValue().ToString());
    }

    public void OnButtonPress(BaseControl b)
    {
        ButtonControl btnCtrl = (ButtonControl)b;
        btnToggled = !btnToggled;
        btnCtrl.ToggleButton(btnToggled);
    }
}
