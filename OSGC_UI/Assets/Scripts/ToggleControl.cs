using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleControl : BaseControl
{
    public event BaseControlEventHandler ToggleChangedEvent;

    public bool isOn;
    Button buttonComp;

    void Awake()
    {
        _controlType = BaseControlType.Toggle;
        buttonComp = GetComponent<Button>();
        buttonComp.onClick.AddListener(ToggleClicked);
    }

    public void ToggleClicked()
    {
        isOn = !isOn;
        if (ToggleChangedEvent != null)
            ToggleChangedEvent(this);
    }

    public override System.Object ControlValue()
    {
        return isOn;
    }
}
