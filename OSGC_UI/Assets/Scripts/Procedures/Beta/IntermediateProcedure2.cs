using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateProcedure2 : ProcedureBase
{
    TriggerResponse currentTask;

    const int baseTriggersToSucceed = 5;
    int triggersToSucceed;

    bool usermode = true;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        triggersToSucceed = baseTriggersToSucceed;
        usermode = true;
        ResetUI();
        BeginTask();
        controller.Controls.SubscribeToAllControls(HandleInput);
    }

    public override void Stop()
    {
        if (Running) controller.Controls.UnsubscribeToAllControls(HandleInput);
        base.Stop();
    }

    void BeginTask()
    {
        ResetUI();
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
        usermode = false;
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

        usermode = true;
    }

    public void HandleInput(BaseControl control)
    {
        if (!usermode) return;

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
            SetupUI();
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

        protected virtual void SetupUI() { }
    }

    class IntermediateTask1 : TriggerResponse
    {
        bool usermode = true;
        public IntermediateTask1() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(Handler);
        }

        protected override void SetupUI()
        {
            usermode = false;
            controller.slider1.Value = 0.5f;
            controller.slider2.Value = 0.5f;
            controller.slider3.Value = 0.5f;
            usermode = true;
        }

        public void MainTrigger()
        {
            controller.redLight.Lit = false;
            controller.yellowLight.Lit = false;
            controller.greenLight.Lit = true;
        }

        public void Handler(BaseControl control)
        {
            //ignore if not triggered by user
            if (!usermode) return;

            if (control == controller.button1)
            {
                //ensure toggle are correct
                if (controller.slider1.Value == 1f
                    && controller.slider2.Value == 1f
                    && controller.slider3.Value == 0f)
                    NextStep();
                else
                    finished = true;
            }
            else if (control.ControlType != BaseControlType.Slider)
            {
                //fail because not a correct control
                finished = true;
            }
        }
    }

    class IntermediateTask2 : TriggerResponse
    {
        bool usermode = true;

        public IntermediateTask2() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(Handler);
        }

        protected override void SetupUI()
        {
            usermode = false;
            controller.toggle1.SetValue(true);
            controller.toggle2.SetValue(true);
            controller.toggle3.SetValue(false);
            controller.toggle4.SetValue(false);
            usermode = true;
        }

        int numTogglesOn()
        {
            int numOn = 0;
            if (controller.toggle1.isOn) ++numOn;
            if (controller.toggle2.isOn) ++numOn;
            if (controller.toggle3.isOn) ++numOn;
            if (controller.toggle4.isOn) ++numOn;
            return numOn;
        }

        public void MainTrigger()
        {
            controller.redLight.Lit = false;
            controller.yellowLight.Lit = true;
            controller.greenLight.Lit = false;
        }

        public void Handler(BaseControl control)
        {
            //ignore if not triggered by user
            if (!usermode) return;

            if (control == controller.button2)
            {
                //ensure toggle are correct
                if (controller.toggle1.isOn
                    && !controller.toggle2.isOn
                    && controller.toggle3.isOn
                    && controller.toggle4.isOn)
                    NextStep();
                else
                    finished = true;
            }
            else if (control.ControlType == BaseControlType.Toggle)
            {
                //fail if 4 on at the same time
                if (numTogglesOn() > 3) finished = true;
            }
            else
            {
                //fail because not a correct control
                finished = true;
            }
        }
    }

    class IntermediateTask3 : TriggerResponse
    {
        bool usermode = true;

        bool[] option1toggles;
        bool[] option2toggles;
        bool[] option3toggles;

        public IntermediateTask3() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(Handler);
            option1toggles = new bool[] { true, false, true, true };
            option2toggles = new bool[] { false, false, true, true };
            option3toggles = new bool[] { true, true, true, true };
        }

        protected override void SetupUI()
        {
            usermode = false;
            controller.setting1.option1.isOn = true;
            controller.toggle1.SetValue(option1toggles[0]);
            controller.toggle2.SetValue(option1toggles[1]);
            controller.toggle3.SetValue(option1toggles[2]);
            controller.toggle4.SetValue(option1toggles[3]);
            usermode = true;
        }

        int numTogglesOn(int togglegroup)
        {
            int numOn = 0;
            bool[] group = null;

            if (togglegroup == 0) group = option1toggles;
            else if (togglegroup == 1) group = option2toggles;
            else if (togglegroup == 2) group = option3toggles;

            if (group != null)
            {
                if (group[0]) ++numOn;
                if (group[1]) ++numOn;
                if (group[2]) ++numOn;
                if (group[3]) ++numOn;
            }
            return numOn;
        }

        public void MainTrigger()
        {
            controller.redLight.Lit = true;
            controller.yellowLight.Lit = false;
            controller.greenLight.Lit = false;
        }

        public void Handler(BaseControl control)
        {
            if (!usermode) return;

            if (control == controller.button3)
            {
                if (numTogglesOn(0) == 0
                    && numTogglesOn(1) == 0
                    && numTogglesOn(2) == 0)
                {
                    NextStep();
                }
                else
                    finished = true;
            }
            else if (control == controller.setting1)
                UpdateToggles();
            else if (control.ControlType == BaseControlType.Toggle)
                CheckToggles();
            else
                finished = true;
        }

        void UpdateToggles()
        {
            usermode = false;

            if (controller.setting1.option1.isOn)
            {
                controller.toggle1.SetValue(option1toggles[0]);
                controller.toggle2.SetValue(option1toggles[1]);
                controller.toggle3.SetValue(option1toggles[2]);
                controller.toggle4.SetValue(option1toggles[3]);
            }
            else if (controller.setting1.option2.isOn)
            {
                controller.toggle1.SetValue(option2toggles[0]);
                controller.toggle2.SetValue(option2toggles[1]);
                controller.toggle3.SetValue(option2toggles[2]);
                controller.toggle4.SetValue(option2toggles[3]);
            }
            else
            {
                controller.toggle1.SetValue(option3toggles[0]);
                controller.toggle2.SetValue(option3toggles[1]);
                controller.toggle3.SetValue(option3toggles[2]);
                controller.toggle4.SetValue(option3toggles[3]);
            }

            usermode = true;
        }

        void CheckToggles()
        {
            bool[] currentoptiontoggles;

            if (controller.setting1.option1.isOn) currentoptiontoggles = option1toggles;
            else if (controller.setting1.option2.isOn) currentoptiontoggles = option2toggles;
            else currentoptiontoggles = option3toggles;

            currentoptiontoggles[0] = controller.toggle1.isOn;
            currentoptiontoggles[1] = controller.toggle2.isOn;
            currentoptiontoggles[2] = controller.toggle3.isOn;
            currentoptiontoggles[3] = controller.toggle4.isOn;
        }
    }
}
