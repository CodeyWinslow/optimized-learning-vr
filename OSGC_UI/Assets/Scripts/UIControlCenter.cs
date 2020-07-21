using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake()
    {
        BackButton.onClick.AddListener(OnBackButton);
        ExitButton.onClick.AddListener(OnExitButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
        }
    }

    void OnBackButton()
    {
        pauseScreen.SetActive(false);
    }

    void OnExitButton()
    {
        Application.Quit();
    }

}
