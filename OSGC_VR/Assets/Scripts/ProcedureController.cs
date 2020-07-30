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
        procedures.Add(new SimpleProcedure());
        procedures.Add(new IntermediateProcedure());
        procedures.Add(new AdvancedProcedure());
        startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentProc != null)
        {
            if (currentProc.Finished)
            {
                if (currentProc.Success)
                    Controls.Notifications.ShowNotification("Finished successfully!");
                else
                    Controls.Notifications.ShowNotification("FAILED");

                FinishedProcedure(currentProc.Success);
                currentProc = null;
            }
            else if (currentProc != null)
                currentProc.RunUpdate();
        }
    }

    void StartNextProcedure()
    {
        if (procIndex < procedures.Count)
        {
            currentProc = procedures[procIndex++];
            currentProc.BeginProcedure(this);
        }
        else
        {
            Debug.Log("All procedures finished!");
            currentProc = null;
        }
    }

    void FinishedProcedure(bool success)
    {
        if (procIndex == procedures.Count)
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
