using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIControlCenter))]
public class ProcedureController : MonoBehaviour
{
    UIControlCenter controls;
    List<ProcedureBase> procedures;
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

        //simple
        procedures.Add(new SimpleProcedureTutorial());
        newProc = new SimpleProcedure();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);
        //intemediate
        procedures.Add(new IntermediateProcedureTutorial());
        newProc = new IntermediateProcedure();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);
        //advanced
        procedures.Add(new AdvancedProcedureTutorial());
        newProc = new AdvancedProcedure();
        newProc.RestartOnFailure = true;
        procedures.Add(newProc);

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
        else
        {
            startScreen.SetActive(true);
        }
    }

    public void StartScreenPressed()
    {
        startScreen.SetActive(false);
        controls.Notifications.DismissNotificationPressed();
        StartNextProcedure();
    }
}
