using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ToggleUIScript))]
public class ToggleControl : BaseControl
{
    public event BaseControlEventHandler ToggleChangedEvent;

    public bool isOn;
    Button buttonComp;
    ToggleUIScript ui;

    void Awake()
    {
        _controlType = BaseControlType.Toggle;
        buttonComp = GetComponent<Button>();
        ui = GetComponent<ToggleUIScript>();
        buttonComp.onClick.AddListener(ToggleClicked);
    }

    public void ToggleClicked()
    {
        isOn = !isOn;
        if (ToggleChangedEvent != null)
            ToggleChangedEvent(this);
    }

    public void SetValue(bool on)
    {
        isOn = on;
        ui.CheckToggle(this);
    }

    public override System.Object ControlValue()
    {
        return isOn;
    }
}
