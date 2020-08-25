using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIControlCenter))]
public class ProcedureController : MonoBehaviour
{
    UIControlCenter controls;

    ProcedureBase[] procedures;
    const int procedureCount = 3;

    public List<MessageSequence> startSequences;
    public GameObject startScreen;
    public UIControlCenter Controls { get { return controls; } }
    
    int procIndex = -1;
    ProcedureBase currentProc;

    public void LoadProcedure(int index)
    {
        if (index < 0 || index >= procedureCount) return;

        //stop current or deal with start message
        if (currentProc != null && currentProc.Running)
        {
            //stop procedure
            currentProc.Stop();
        }
        else
        {
            if (procIndex + 1 >= 0 && procIndex + 1 < startSequences.Count)
            {
                startSequences[procIndex + 1].Finish();
            }
        }

        //ensure we disable start screen
        startScreen.SetActive(false);

        //prepare vars for new proc
        procIndex = index - 1;
        currentProc = null;

        ShowStartMessage();
    }

    void Awake()
    {
        controls = GetComponent<UIControlCenter>();
    }
    // Start is called before the first frame update
    void Start()
    {
        procedures = new ProcedureBase[procedureCount];
        ProcedureBase newProc;

        newProc = new SimpleProcedure2();
        procedures[0] = (newProc);

        newProc = new IntermediateProcedure2();
        procedures[1] = (newProc);

        newProc = new AdvancedProcedure2();
        procedures[2] = (newProc);

        //if (procedures != null && procedures.Count > 0)
        //    startScreen.SetActive(true);
        ShowStartMessage();
    }

    // Update is called once per frame
    void Update()
    {
        //if we have a current procedure
        if (currentProc != null)
        {
            if (currentProc.Running)
            {
                //wrap it up if it's finished
                if (currentProc.Finished)
                {
                    if (currentProc.Success)
                        Controls.Notifications.ShowNotification("Finished successfully!");
                    else
                        Controls.Notifications.ShowNotification("FAILED");

                    FinishedProcedure(currentProc.Success, currentProc.RestartOnFailure);
                    currentProc.Stop();
                } //call its update function if not
                else
                    currentProc.RunUpdate();
            }
        }
    }

    void StartProcedure()
    {
        if (procIndex >= 0 && procIndex < procedureCount)
        {
            currentProc = procedures[procIndex];
            currentProc.BeginProcedure(this);
        }
    }

    void ShowStartMessage()
    {
        int index = procIndex + 1;

        //show message if it exists, otherwise show start screen
        if (startSequences.Count > index && startSequences[index] != null)
        {
            startSequences[index].OnceSequenceFinished += StartSequenceFinished;
            startSequences[index].Begin();
        }
        else if (procedures != null && procedureCount > 0)
            startScreen.SetActive(true);
    }

    void FinishedProcedure(bool success, bool restarting)
    {
        //only do special message if
        //finishing last procedure and
        //either the procedure was successful,
        //or if restartOnFailure is not true
        if (procIndex + 1 == procedureCount
            && (success || (!restarting)))
        {
            string message = "";

            if (success)
                message += "Success! ";
            else
                message += "Failed. ";

            message += "Finished all procedures.";
            controls.Notifications.ShowNotification(message);
        }
        else
        {
            //revert to previous procIndex if restarting
            if (!success && restarting)
                --procIndex;

            //show next message
            ShowStartMessage();
        }
    }

    public void StartSequenceFinished()
    {
        startScreen.SetActive(true);
    }

    public void StartScreenPressed()
    {
        startScreen.SetActive(false);
        //if (startSequences.Count > procIndex && startSequences[procIndex] != null)
        //    startSequences[procIndex].Finish();
        controls.Notifications.DismissNotificationPressed();
        //increment for starting next proc
        ++procIndex;
        StartProcedure();
    }
}
