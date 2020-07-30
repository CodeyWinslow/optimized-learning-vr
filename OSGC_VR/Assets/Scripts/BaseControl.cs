using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseControlType
{
    Light,
    Meter,
    Button,
    Setting,
    Slider,
    Toggle
}

public abstract class BaseControl : MonoBehaviour
{
    public delegate void BaseControlEventHandler(BaseControl c);

    protected BaseControlType _controlType;

    public BaseControlType ControlType
    {
        get { return _controlType; }
    }

    public abstract System.Object ControlValue();
}
