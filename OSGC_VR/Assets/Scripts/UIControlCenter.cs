using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(NotificationController))]
[RequireComponent(typeof(AudioSource))]
public class UIControlCenter : MonoBehaviour
{
    //controls
    //-outputs
    public LightControl greenLight;
    public LightControl yellowLight;
    public LightControl redLight;
    public MeterControl meter1;
    public MeterControl meter2;
    public MeterControl meter3;

    //-inputs
    public SettingControl setting1;
    public SettingControl setting2;
    public ButtonControl button1;
    public ButtonControl button2;
    public ButtonControl button3;
    public ButtonControl button4;
    public ButtonControl button5;
    public ButtonControl button6;
    public ButtonControl button7;
    public ButtonControl button8;
    public ButtonControl button9;
    public JoystickControl joystick;
    public ToggleControl toggle1;
    public ToggleControl toggle2;
    public ToggleControl toggle3;
    public ToggleControl toggle4;
    public SliderControl slider1;
    public SliderControl slider2;
    public SliderControl slider3;

    //sound
    AudioSource controlSound;

    //notification
    NotificationController notifications;
    public NotificationController Notifications
    {
        get { return notifications; }
    }

    //tutorials
    public SimpleProcedureTutorialHelper simpleTutorialHelper;
    public IntermediateProcedureTutorialHelper intermediateTutorialHelper;
    public AdvancedProcedureTutorialHelper advancedTutorialHelper;

    public SimpleProcedureTutorialHelper2 simpleTutorialHelper2;
    public IntermediateProcedureTutorialHelper2 intermediateTutorialHelper2;
    public AdvancedProcedureTutorialHelper2 advancedTutorialHelper2;

    void Awake()
    {
        
        notifications = GetComponent<NotificationController>();
        simpleTutorialHelper = GetComponent<SimpleProcedureTutorialHelper>();
        intermediateTutorialHelper = GetComponent<IntermediateProcedureTutorialHelper>();
        advancedTutorialHelper = GetComponent<AdvancedProcedureTutorialHelper>();
        simpleTutorialHelper2 = GetComponent<SimpleProcedureTutorialHelper2>();
        intermediateTutorialHelper2 = GetComponent<IntermediateProcedureTutorialHelper2>();
        advancedTutorialHelper2 = GetComponent<AdvancedProcedureTutorialHelper2>();
        controlSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        SubscribeToAllControls(ControlSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to subscripe to all UI control events
    public void SubscribeToAllControls(BaseControl.BaseControlEventHandler handler)
    {
        //buttons
        button1.ButtonClickedEvent += handler;
        button2.ButtonClickedEvent += handler;
        button3.ButtonClickedEvent += handler;
        button4.ButtonClickedEvent += handler;
        button5.ButtonClickedEvent += handler;
        button6.ButtonClickedEvent += handler;
        button7.ButtonClickedEvent += handler;
        button8.ButtonClickedEvent += handler;
        button9.ButtonClickedEvent += handler;

        //toggles
        toggle1.ToggleChangedEvent += handler;
        toggle2.ToggleChangedEvent += handler;
        toggle3.ToggleChangedEvent += handler;
        toggle4.ToggleChangedEvent += handler;

        //sliders
        slider1.SliderChangedEvent += handler;
        slider2.SliderChangedEvent += handler;
        slider3.SliderChangedEvent += handler;

        //settings
        setting1.SettingChangedEvent += handler;
        setting2.SettingChangedEvent += handler;
    }

    public void UnsubscribeToAllControls(BaseControl.BaseControlEventHandler handler)
    {
        //buttons
        button1.ButtonClickedEvent -= handler;
        button2.ButtonClickedEvent -= handler;
        button3.ButtonClickedEvent -= handler;
        button4.ButtonClickedEvent -= handler;
        button5.ButtonClickedEvent -= handler;
        button6.ButtonClickedEvent -= handler;
        button7.ButtonClickedEvent -= handler;
        button8.ButtonClickedEvent -= handler;
        button9.ButtonClickedEvent -= handler;

        //toggles
        toggle1.ToggleChangedEvent -= handler;
        toggle2.ToggleChangedEvent -= handler;
        toggle3.ToggleChangedEvent -= handler;
        toggle4.ToggleChangedEvent -= handler;

        //sliders
        slider1.SliderChangedEvent -= handler;
        slider2.SliderChangedEvent -= handler;
        slider3.SliderChangedEvent -= handler;

        //settings
        setting1.SettingChangedEvent -= handler;
        setting2.SettingChangedEvent -= handler;
    }

    

    void ControlSound(BaseControl control)
    {
        if (control.ControlType != BaseControlType.Slider)
        {
            controlSound.Play();
        }
    }

}
