using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProcedureBase
{
    private bool running = false;
    private bool finished = false;
    private bool success = false;
    protected bool restartOnFailure = false;
    protected ProcedureController controller;

    public bool Running { get { return running; } }
    public bool Finished { get { return finished; } }
    public bool Success { get { return success; } }
    public bool RestartOnFailure
    {
        get { return restartOnFailure; }
        set { restartOnFailure = value; }
    }

    public virtual void BeginProcedure(ProcedureController cont)
    {
        controller = cont;
        finished = false;
        success = false;
        running = true;
    }

    protected void EndProcedure(bool wasSuccessful)
    {
        success = wasSuccessful;
        finished = true;
        //running = false; //defer stopping to Stop function
    }

    public void Stop()
    {
        running = false;
    }

    //main logic loop
    public abstract void RunUpdate();
}
