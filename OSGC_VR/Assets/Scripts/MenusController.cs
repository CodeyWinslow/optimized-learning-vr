using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ProcedureController))]
[RequireComponent(typeof(UIControlCenter))]
public class MenusController : MonoBehaviour
{
    //pause screen buttons
    public Button BackButton;
    public Button ExitButton;
    public Button RestartButton;
    public Button ProceduresButton;

    //procedure select screen buttons
    public Button procedureBackButton;
    public Button procedure1TutorialButton;
    public Button procedure1Button;
    public Button procedure2TutorialButton;
    public Button procedure2Button;
    public Button procedure3TutorialButton;
    public Button procedure3Button;

    //screens
    public GameObject pauseScreen;
    public GameObject proceduresScreen;

    //settings
    public Toggle cursorVisibleToggle;
    bool showCursor = true;

    //proc controller
    ProcedureController procs;
    bool paused;

    // Start is called before the first frame update
    void Awake()
    {
        procs = GetComponent<ProcedureController>();

        BackButton.onClick.AddListener(OnBackButton);
        ExitButton.onClick.AddListener(OnExitButton);
        RestartButton.onClick.AddListener(OnRestartButton);
        ProceduresButton.onClick.AddListener(OnProceduresButton);
        cursorVisibleToggle.onValueChanged.AddListener(OnShowCursorToggle);

        procedureBackButton.onClick.AddListener(OnProcBackButton);
        procedure1TutorialButton.onClick.AddListener(OnProc1TutButton);
        procedure1Button.onClick.AddListener(OnProc1Button);
        procedure2TutorialButton.onClick.AddListener(OnProc2TutButton);
        procedure2Button.onClick.AddListener(OnProc2Button);
        procedure3TutorialButton.onClick.AddListener(OnProc3TutButton);
        procedure3Button.onClick.AddListener(OnProc3Button);

        paused = false;
    }

    private void Start()
    {
        cursorVisibleToggle.isOn = showCursor;
        pauseScreen.SetActive(false);
    }

    public void PauseButtonPressed()
    {
        SetPaused(!paused);
    }

    void SetPaused(bool isPaused)
    {
        paused = isPaused;
        CheckPauseScreen();
    }

    void CheckPauseScreen()
    {
        if (paused)
        {
            pauseScreen.SetActive(true);
            GetComponent<UIControlCenter>().DisableAllColliders();
        }
        else
        {
            pauseScreen.SetActive(false);
            proceduresScreen.SetActive(false);
            GetComponent<UIControlCenter>().EnableAllColliders();
        }
    }

    //pause screen logic

    void OnBackButton()
    {
        paused = false;
        CheckPauseScreen();
    }

    void OnExitButton()
    {
        Application.Quit();
    }

    void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnProceduresButton()
    {
        pauseScreen.SetActive(false);
        proceduresScreen.SetActive(true);
    }

    //procedures

    void OnProcBackButton()
    {
        proceduresScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    void OnProc1TutButton()
    {
        if (procs != null)
            procs.LoadProcedure(0);

        SetPaused(false);
    }

    void OnProc1Button()
    {

        if (procs != null)
            procs.LoadProcedure(1);

        SetPaused(false);
    }

    void OnProc2TutButton()
    {

        if (procs != null)
            procs.LoadProcedure(2);

        SetPaused(false);
    }

    void OnProc2Button()
    {
        if (procs != null)
            procs.LoadProcedure(3);

        SetPaused(false);
    }

    void OnProc3TutButton()
    {
        if (procs != null)
            procs.LoadProcedure(4);

        SetPaused(false);
    }

    void OnProc3Button()
    {
        if (procs != null)
            procs.LoadProcedure(5);

        SetPaused(false);
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonPressed();
        }
    }
}
