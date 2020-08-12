using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IntermediateProcedure : ProcedureBase
{
    TriggerResponse currentTask;

    const int baseTriggersToSucceed = 5;
    int triggersToSucceed;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        triggersToSucceed = baseTriggersToSucceed;
        ResetUI();
        BeginTask();
        controller.Controls.SubscribeToAllControls(HandleInput);
    }

    void BeginTask()
    {
        currentTask = GetRandomTask();
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
        int random = Random.Range(0, 3);

        TriggerResponse task;

        switch (random)
        {
            case 0:
                task = new IntermediateTask1();
                break;
            case 1:
                task = new IntermediateTask2();
                break;
            case 2:
                task = new IntermediateTask3();
                break;
            default:
                task = new IntermediateTask1();
                break;
        }

        return task;
    }

    void ResetUI()
    {
        //lights
        controller.Controls.greenLight.Lit = false;
        controller.Controls.yellowLight.Lit = false;
        controller.Controls.redLight.Lit = false;

        //meters
        controller.Controls.meter1.Value = 0;
        controller.Controls.meter2.Value = 0;
        controller.Controls.meter3.Value = 0;

        //settings
        controller.Controls.setting1.option1.isOn = true;
        controller.Controls.setting2.option1.isOn = true;

        //toggles
        controller.Controls.toggle1.isOn = false;
        controller.Controls.toggle2.isOn = false;
        controller.Controls.toggle3.isOn = false;
        controller.Controls.toggle4.isOn = false;

        //sliders
        controller.Controls.slider1.Value = 0.5f;
        controller.Controls.slider2.Value = 0.5f;
        controller.Controls.slider3.Value = 0.5f;
    }

    public void HandleInput(BaseControl control)
    {
        currentTask.currentStep.Current(control);
    }

    //Inner classes

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
        public IEnumerator<Step> currentStep;

        //uiController for access to ui controls
        protected UIControlCenter controller;

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

        public void SetController(UIControlCenter cont)
        {
            controller = cont;
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
        public IntermediateTask1() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(StepOne);
            responseSteps.Add(StepTwo);
        }

        public void MainTrigger()
        {
            controller.redLight.Lit = false;
            controller.yellowLight.Lit = false;
            controller.greenLight.Lit = true;
        }

        public void StepOne(BaseControl control)
        {
            if (control == controller.button5)
                NextStep();
            else
                finished = true;
        }

        public void StepTwo(BaseControl control)
        {
            if (control == controller.toggle1)
                NextStep();
            else
                finished = true;
        }
    }

    class IntermediateTask2 : TriggerResponse
    {
        public IntermediateTask2() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(StepOne);
            responseSteps.Add(StepTwo);
        }

        public void MainTrigger()
        {
            controller.redLight.Lit = true;
            controller.yellowLight.Lit = false;
            controller.greenLight.Lit = false;
        }

        public void StepOne(BaseControl control)
        {
            if (control == controller.setting1
                && controller.setting1.SelectedOption == 2)
            {
                NextStep();
            }
            else
                finished = true;
        }

        public void StepTwo(BaseControl control)
        {
            if (control == controller.toggle3)
                NextStep();
            else
                finished = true;
        }
    }

    class IntermediateTask3 : TriggerResponse
    {
        public IntermediateTask3() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(StepOne);
            responseSteps.Add(StepTwo);
            responseSteps.Add(StepThree);
        }

        public void MainTrigger()
        {
            controller.redLight.Lit = false;
            controller.yellowLight.Lit = true;
            controller.greenLight.Lit = false;
        }

        public void StepOne(BaseControl control)
        {
            if (control == controller.button7)
                NextStep();
            else
                finished = true;
        }

        public void StepTwo(BaseControl control)
        {
            if (control == controller.button8)
                NextStep();
            else
                finished = true;
        }

        public void StepThree(BaseControl control)
        {
            if (control == controller.button9)
                NextStep();
            else
                finished = true;
        }
    }
}
