using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonControl : BaseControl
{
    public delegate void ButtonClickDelegate(BaseControl btn);
    public event ButtonClickDelegate ButtonClickedEvent;
    Button UI_Button;

    void Start()
    {
        UI_Button = GetComponent<Button>();
        UI_Button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (ButtonClickedEvent != null)
            ButtonClickedEvent(this);
    }
}
