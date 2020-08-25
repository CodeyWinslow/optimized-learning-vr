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
    [Header("Controls")]
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

    [Header("Colliders")]
    //colliders for objects to hide when not in focus
    public BoxCollider Setting1Option1Collider;
    public BoxCollider Setting1Option2Collider;
    public BoxCollider Setting1Option3Collider;

    public BoxCollider Setting2Option1Collider;
    public BoxCollider Setting2Option2Collider;
    public BoxCollider Setting2Option3Collider;

    public BoxCollider Toggle1Collider;
    public BoxCollider Toggle2Collider;
    public BoxCollider Toggle3Collider;
    public BoxCollider Toggle4Collider;

    public BoxCollider Button1Collider;
    public BoxCollider Button2Collider;
    public BoxCollider Button3Collider;
    public BoxCollider Button4Collider;
    public BoxCollider Button5Collider;
    public BoxCollider Button6Collider;
    public BoxCollider Button7Collider;
    public BoxCollider Button8Collider;
    public BoxCollider Button9Collider;

    public BoxCollider Slider1Collider;
    public BoxCollider Slider2Collider;
    public BoxCollider Slider3Collider;


    [Header("Click Sound")]
    //sound
    AudioSource controlSound;

    [Header("Notifications")]
    //notification
    NotificationController notifications;
    public NotificationController Notifications
    {
        get { return notifications; }
    }

    [Header("Tutorials")]
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

    public void DisableAllColliders()
    {
        Setting1Option1Collider.enabled = false;
        Setting1Option2Collider.enabled = false;
        Setting1Option3Collider.enabled = false;

        Setting2Option1Collider.enabled = false;
        Setting2Option2Collider.enabled = false;
        Setting2Option3Collider.enabled = false;

        Toggle1Collider.enabled = false;
        Toggle2Collider.enabled = false;
        Toggle3Collider.enabled = false;
        Toggle4Collider.enabled = false;

        Button1Collider.enabled = false;
        Button2Collider.enabled = false;
        Button3Collider.enabled = false;
        Button4Collider.enabled = false;
        Button5Collider.enabled = false;
        Button6Collider.enabled = false;
        Button7Collider.enabled = false;
        Button8Collider.enabled = false;
        Button9Collider.enabled = false;

        Slider1Collider.enabled = false;
        Slider2Collider.enabled = false;
        Slider3Collider.enabled = false;
    }

    public void EnableAllColliders()
    {
        Setting1Option1Collider.enabled = true;
        Setting1Option2Collider.enabled = true;
        Setting1Option3Collider.enabled = true;

        Setting2Option1Collider.enabled = true;
        Setting2Option2Collider.enabled = true;
        Setting2Option3Collider.enabled = true;

        Toggle1Collider.enabled = true;
        Toggle2Collider.enabled = true;
        Toggle3Collider.enabled = true;
        Toggle4Collider.enabled = true;

        Button1Collider.enabled = true;
        Button2Collider.enabled = true;
        Button3Collider.enabled = true;
        Button4Collider.enabled = true;
        Button5Collider.enabled = true;
        Button6Collider.enabled = true;
        Button7Collider.enabled = true;
        Button8Collider.enabled = true;
        Button9Collider.enabled = true;

        Slider1Collider.enabled = true;
        Slider2Collider.enabled = true;
        Slider3Collider.enabled = true;
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
