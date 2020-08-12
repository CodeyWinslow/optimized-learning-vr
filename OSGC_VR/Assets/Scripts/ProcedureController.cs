using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIControlCenter))]
public class ProcedureController : MonoBehaviour
{
    UIControlCenter controls;
    List<ProcedureBase> procedures;
    public List<MessageSequence> startSequences;
    public GameObject startScreen;
    public UIControlCenter Controls { get { return controls; } }
    
    int procIndex = 0;
    ProcedureBase currentProc;

    void Awake()
    {
        controls = GetComponent<UIControlCenter>();
    }
    // Start is called before the first frame update
    void Start()
    {
        procedures = new List<ProcedureBase>();
        ProcedureBase newProc;

        ////simple
        //procedures.Add(new SimpleProcedureTutorial());
        //newProc = new SimpleProcedure();
        //newProc.RestartOnFailure = true;
        //procedures.Add(newProc);
        ////intemediate
        //procedures.Add(new IntermediateProcedureTutorial());
        //newProc = new IntermediateProcedure();
        //newProc.RestartOnFailure = true;
        //procedures.Add(newProc);
        ////advanced
        //procedures.Add(new AdvancedProcedureTutorial());
        //newProc = new AdvancedProcedure();
        //newProc.RestartOnFailure = true;
        //procedures.Add(newProc);

        newProc = new SimpleProcedureTutorial2();
        procedures.Add(newProc);
        newProc = new SimpleProcedure2();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);
        newProc = new IntermediateProcedureTutorial2();
        procedures.Add(newProc);
        newProc = new IntermediateProcedure2();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);
        newProc = new AdvancedProcedureTutorial2();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);
        newProc = new AdvancedProcedure2();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);

        //if (procedures != null && procedures.Count > 0)
        //    startScreen.SetActive(true);
        if (startSequences.Count > 0 && startSequences[0] != null)
        {
            startSequences[0].OnceSequenceFinished += StartSequenceFinished;
            startSequences[0].Begin();
        }else if (procedures != null && procedures.Count > 0)
            startScreen.SetActive(true);
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

    void StartNextProcedure()
    {
        if (procIndex < procedures.Count)
        {
            //advance to next only if beginning, successful, or ResartOnFailure is false
            if (currentProc == null || currentProc.Success || !currentProc.RestartOnFailure)
            {
                currentProc = procedures[procIndex++];
            }
            currentProc.BeginProcedure(this);
        }
        else if (currentProc != null && currentProc.RestartOnFailure && !currentProc.Success)
        {
            currentProc.BeginProcedure(this);
        }
    }

    void FinishedProcedure(bool success, bool restarting)
    {
        //only do special message if either the procedure was successful,
        //or if it was failed and restartOnFailure is not true
        if (procIndex == procedures.Count
            && (success ||
                (!success && !restarting)))
        {
            string message = "";

            if (success)
                message += "Success! ";
            else
                message += "Failed. ";

            message += "Finished all procedures.";
            controls.Notifications.ShowNotification(message);
        }
        else if (!success && restarting)
        {
            if (procIndex - 1 >= 0 &&
                startSequences.Count > procIndex - 1
                && startSequences[procIndex - 1] != null)
            {
                //startSequences[procIndex-1].OnceSequenceFinished += StartSequenceFinished;
                startSequences[procIndex - 1].Begin();
            }
            //startScreen.SetActive(true);
        }
        else
        {
            if (startSequences.Count > procIndex
                && startSequences[procIndex] != null)
            {
                startSequences[procIndex].OnceSequenceFinished += StartSequenceFinished;
                startSequences[procIndex].Begin();
            }
            else
            {
                startScreen.SetActive(true);
            }
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
        StartNextProcedure();
    }
}
