using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProcedureBase
{
    private bool running = false;
    private bool finished = false;
    private bool success = false;
    protected ProcedureController controller;

    public bool Running { get { return running; } }
    public bool Finished { get { return finished; } }
    public bool Success { get { return success; } }

    public virtual void BeginProcedure(ProcedureController cont)
    {
        controller = cont;
        running = true;
    }

    protected void EndProcedure(bool wasSuccessful)
    {
        success = wasSuccessful;
        finished = true;
        running = false;
    }

    //main logic loop
    public abstract void RunUpdate();
}
