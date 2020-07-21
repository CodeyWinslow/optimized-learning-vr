using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonControl : BaseControl
{
    public event BaseControlEventHandler ButtonClickedEvent;

    public Sprite normalImage;
    public Sprite toggledImage;

    Button UI_Button;

    void Awake()
    {
        _controlType = BaseControlType.Button;
    }

    void Start()
    {
        UI_Button = GetComponent<Button>();
        UI_Button.onClick.AddListener(OnButtonClick);
    }

    public void ToggleButton(bool toggled)
    {
        if (toggled)
        {
            UI_Button.GetComponent<Image>().sprite = toggledImage;
        }
        else
        {
            UI_Button.GetComponent<Image>().sprite = normalImage;
        }
    }

    public void OnButtonClick()
    {
        if (ButtonClickedEvent != null)
            ButtonClickedEvent(this);
    }

    public override object ControlValue()
    {
        return null;
    }
}
