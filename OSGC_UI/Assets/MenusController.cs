using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ProcedureController))]
public class MenusController : MonoBehaviour
{
    //pause screen buttons
    public Button BackButton;
    public Button ExitButton;
    public Button RestartButton;
    public Button ProceduresButton;

    //procedure select screen buttons
    public Button procedureBackButton;
    //public Button procedure1TutorialButton;
    public Button procedure1Button;
    //public Button procedure2TutorialButton;
    public Button procedure2Button;
    //public Button procedure3TutorialButton;
    public Button procedure3Button;

    //screens
    public GameObject pauseScreen;
    public GameObject proceduresScreen;

    //settings
    public Toggle cursorVisibleToggle;
    bool showCursor = true;
    bool paused = false;

    //proc controller
    ProcedureController procs;

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
        //procedure1TutorialButton.onClick.AddListener(OnProc1TutButton);
        procedure1Button.onClick.AddListener(OnProc1Button);
        //procedure2TutorialButton.onClick.AddListener(OnProc2TutButton);
        procedure2Button.onClick.AddListener(OnProc2Button);
        //procedure3TutorialButton.onClick.AddListener(OnProc3TutButton);
        procedure3Button.onClick.AddListener(OnProc3Button);
    }

    private void Start()
    {
        cursorVisibleToggle.isOn = showCursor;
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

        paused = false;
        CheckPaused();
    }

    void OnProc1Button()
    {

        if (procs != null)
            procs.LoadProcedure(0);

        paused = false;
        CheckPaused();
    }

    void OnProc2TutButton()
    {

        if (procs != null)
            procs.LoadProcedure(2);

        paused = false;
        CheckPaused();
    }

    void OnProc2Button()
    {
        if (procs != null)
            procs.LoadProcedure(1);

        paused = false;
        CheckPaused();
    }

    void OnProc3TutButton()
    {
        if (procs != null)
            procs.LoadProcedure(4);

        paused = false;
        CheckPaused();
    }

    void OnProc3Button()
    {
        if (procs != null)
            procs.LoadProcedure(2);

        paused = false;
        CheckPaused();
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

    void CheckPaused()
    {
        if (paused) pauseScreen.SetActive(true);
        else
        {
            pauseScreen.SetActive(false);
            proceduresScreen.SetActive(false);
        }
    }

    void PauseButtonPressed()
    {
        paused = !paused;

        CheckPaused();
    }
}
