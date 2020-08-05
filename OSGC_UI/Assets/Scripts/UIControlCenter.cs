using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NotificationController))]
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

    //pausing
    public Button BackButton;
    public Button ExitButton;
    public GameObject pauseScreen;

    //settings
    public Toggle cursorVisibleToggle;
    bool showCursor = true;

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

    void Awake()
    {
        BackButton.onClick.AddListener(OnBackButton);
        ExitButton.onClick.AddListener(OnExitButton);
        cursorVisibleToggle.onValueChanged.AddListener(OnShowCursorToggle);
        notifications = GetComponent<NotificationController>();
        simpleTutorialHelper = GetComponent<SimpleProcedureTutorialHelper>();
        intermediateTutorialHelper = GetComponent<IntermediateProcedureTutorialHelper>();
        advancedTutorialHelper = GetComponent<AdvancedProcedureTutorialHelper>();
    }

    void Start()
    {
        cursorVisibleToggle.isOn = showCursor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
        }
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

    //pause screen logic

    void OnBackButton()
    {
        pauseScreen.SetActive(false);
    }

    void OnExitButton()
    {
        Application.Quit();
    }

    void OnShowCursorToggle(bool show)
    {
        showCursor = show;
        SetCursorVisibility();
    }

    void SetCursorVisibility()
    {
        Cursor.visible = showCursor;
    }

}
