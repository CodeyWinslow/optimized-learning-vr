using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterControl : BaseControl
{
    public Text Hundreds;
    public Text Tens;
    public Text Ones;

    int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            UpdateText();
        }
    }

    void Awake()
    {
        _controlType = BaseControlType.Meter;
    }

    void UpdateText()
    {
        int numToText = 0;

        numToText = (_value % 1000 / 100);
        Hundreds.text = numToText.ToString();

        numToText = (_value % 100 / 10);
        Tens.text = numToText.ToString();

        numToText = (_value % 10);
        Ones.text = numToText.ToString();
    }

    public override object ControlValue()
    {
        return null;
    }
}
