using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingControl : BaseControl
{
    //event for setting change
    public event BaseControlEventHandler SettingChangedEvent;

    //class variables
    public Toggle option1;
    public Toggle option2;
    public Toggle option3;

    void Awake()
    {
        _controlType = BaseControlType.Setting;
    }

    void Start()
    {
        option1.onValueChanged.AddListener(OnChangedSetting);
        option2.onValueChanged.AddListener(OnChangedSetting);
        option3.onValueChanged.AddListener(OnChangedSetting);
    }

    public int SelectedOption
    {
        get
        {
            if (option1.isOn) return 0;
            else if (option2.isOn) return 1;
            else if (option3.isOn) return 2;

            return -1;
        }
        set
        {
            switch (value)
            {
                case 0:
                    option1.isOn = true;
                    break;
                case 1:
                    option2.isOn = true;
                    break;
                case 2:
                    option3.isOn = true;
                    break;
            }
        }
    }

    public void OnChangedSetting(bool e)
    {
        //notify subscribers
        if (SettingChangedEvent != null && e == true)
        {
            SettingChangedEvent(this);
        }
    }

    public override object ControlValue()
    {
        return SelectedOption;
    }
}
