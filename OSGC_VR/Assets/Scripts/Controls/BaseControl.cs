using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject label;

    [SerializeField]
    string defaultLabelString = "";

    protected BaseControlType _controlType;

    public BaseControlType ControlType
    {
        get { return _controlType; }
    }

    public void SetLabel()
    {
        if (label != null)
        {
            Text txt = label.GetComponent<Text>();
            if (txt != null) txt.text = defaultLabelString;
        }
    }

    public void SetLabel(string text)
    {
        if (label != null)
        {
            Text txt = label.GetComponent<Text>();
            if (txt != null) txt.text = text;
        }
    }

    public abstract System.Object ControlValue();
}
