using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleControl : BaseControl
{
    public delegate void ToggleChangedDelegate(BaseControl t, System.Object v);
    public event ToggleChangedDelegate ToggleChangedEvent;

    public bool isOn;
    Button buttonComp;

    void Awake()
    {
        buttonComp = GetComponent<Button>();
        buttonComp.onClick.AddListener(ToggleClicked);
    }

    public void ToggleClicked()
    {
        isOn = !isOn;
        if (ToggleChangedEvent != null)
            ToggleChangedEvent(this, isOn);
    }
}
