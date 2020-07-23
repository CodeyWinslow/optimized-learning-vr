using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TriggerResponse
{
    //delegates
    public delegate void Step(BaseControl control);
    public delegate void Trigger();

    //the (current) trigger to be activated
    public Trigger trigger;
    //the list of steps for this trigger response
    protected List<Step> responseSteps;
    //the current step method for processing input
    public List<Step>.Enumerator currentStep;

    //uiController for access to ui controls
    UIControlCenter controller;

    //flags
    public bool finished;
    public bool successful;
    public bool constantTrigger;

    public TriggerResponse()
    {
        finished = false;
        successful = false;
        constantTrigger = false;
        responseSteps = new List<Step>();
    }

    public TriggerResponse(TriggerResponse copy)
    {
        this.controller = copy.controller;
        this.finished = copy.finished;
        this.successful = copy.successful;
        this.constantTrigger = copy.constantTrigger;
        this.trigger = copy.trigger;
        this.responseSteps = copy.responseSteps;
        this.currentStep = copy.currentStep;
    }

    public void SetController(UIControlCenter cont)
    {
        controller = cont;
    }

    public virtual TriggerResponse Copy()
    {
        return new TriggerResponse(this);
    }

    //Begins the trigger-response by activating trigger and getting the first step
    public virtual void Begin()
    {
        currentStep = responseSteps.GetEnumerator();
        trigger();
        NextStep();
    }

    //moves to next step in list and finishes if at the end
    protected void NextStep()
    {
        //Debug.Log("currentStep is null: " + (currentStep is null).ToString());
        if (!currentStep.MoveNext())
        {
            finished = true;
            successful = true;
        }
    }
}

class IntermediateTask1 : TriggerResponse
{
    public IntermediateTask1() : base() {
        trigger = MainTrigger;
        responseSteps.Add(StepOne);
    }
    public IntermediateTask1(IntermediateTask1 copy) : base(copy)
    { }

    public override TriggerResponse Copy()
    {
        return new IntermediateTask1(this);
    }

    public void MainTrigger()
    {
        Debug.Log("Activated trigger!");
    }

    public void StepOne(BaseControl control)
    {
        Debug.Log("Received input! Finishing!");
        NextStep();
    }
}

class IntermediateTask2 : TriggerResponse
{

}

class IntermediateTask3 : TriggerResponse
{

}

public class IntermediateProcedure : ProcedureBase
{
    static TriggerResponse[] triggersResponses =
    {
        new IntermediateTask1()//,
        //new IntermediateTask2(),
        //new IntermediateTask3()
    };

    TriggerResponse currentTask;
    
    int triggersToSucceed = 1;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        BeginTask();
        controller.Controls.SubscribeToAllControls(HandleInput);
    }

    void BeginTask()
    {
        currentTask = GetRandomTask().Copy();
        currentTask.SetController(controller.Controls);
        currentTask.Begin();
    }

    public override void RunUpdate()
    {
        //check if the task was finished
        if (currentTask.finished)
        {
            if (currentTask.successful)
            {
                if (--triggersToSucceed == 0)
                {
                    controller.Controls.UnsubscribeToAllControls(HandleInput);
                    EndProcedure(true);
                }
                else
                    BeginTask();
            }
            else
            {
                controller.Controls.UnsubscribeToAllControls(HandleInput);
                EndProcedure(false);
            }
        }
        //run task's trigger function if it needs to constantly run
        else if (currentTask.constantTrigger)
            currentTask.trigger();
    }

    TriggerResponse GetRandomTask()
    {
        return triggersResponses[Random.Range(0, triggersResponses.Length - 1)];
    }

    public void HandleInput(BaseControl control)
    {
        currentTask.currentStep.Current(control);
    }
}
