using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermediateProcedureTutorial : ProcedureBase
{
    List<TriggerResponse>.Enumerator currentTask;
    List<TriggerResponse> tasks;

    //bool tutorial = false;
    bool repeat = true;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        ResetUI();
        if (controller.Controls.intermediateTutorialHelper != null)
        {
            //tutorial = true;
        }
        tasks = new List<TriggerResponse>()
        {
            new IntermediateTask1(),
            new IntermediateTask3(),
            new IntermediateTask2()
        };
        currentTask = tasks.GetEnumerator();
        controller.Controls.SubscribeToAllControls(HandleInput);
        BeginTask();
    }

    void BeginTask()
    {
        if (currentTask.MoveNext())
        {
            currentTask.Current.SetController(controller.Controls);
            currentTask.Current.Begin();
        }
        else
        {
            if (!repeat)
            {
                controller.Controls.UnsubscribeToAllControls(HandleInput);
                EndProcedure(true);
            }
            else
            {
                currentTask = tasks.GetEnumerator();
                repeat = false;
                controller.Controls.intermediateTutorialHelper.WhenRepeating.SetActive(true);
                controller.Controls.intermediateTutorialHelper.RepeatOk.SetActive(true);
                controller.Controls.intermediateTutorialHelper.RepeatOk.GetComponent<Button>().onClick.AddListener(() =>
                {
                    controller.Controls.intermediateTutorialHelper.WhenRepeating.SetActive(false);
                    controller.Controls.intermediateTutorialHelper.RepeatOk.SetActive(false);
                    BeginTask();
                });
            }
        }
    }

    public override void RunUpdate()
    {
        //check if the task was finished
        if (currentTask.Current != null)
        {
            if (currentTask.Current.finished)
            {
                if (currentTask.Current.successful)
                {
                    BeginTask();
                }
                else
                {
                    controller.Controls.UnsubscribeToAllControls(HandleInput);
                    EndProcedure(false);
                }
            }
            //run task's trigger function if it needs to constantly run
            else if (currentTask.Current.constantTrigger)
                currentTask.Current.trigger();
        }
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
        if (currentTask.Current != null && !currentTask.Current.finished && !currentTask.Current.waiting)
            currentTask.Current.currentStep.Current(control);
    }

    //inner classes

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

        public bool waiting = true;

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
            finished = false;
            successful = false;
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
            controller.intermediateTutorialHelper.WhenGreenLitHint.SetActive(true);
            //controller.intermediateTutorialHelper.GreenStepOneHint.SetActive(true);
            controller.intermediateTutorialHelper.GreenOkButton.SetActive(true);
            controller.intermediateTutorialHelper.GreenOkButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                controller.intermediateTutorialHelper.GreenStepOneHint.SetActive(true);
                controller.intermediateTutorialHelper.WhenGreenLitHint.SetActive(false);
                controller.intermediateTutorialHelper.GreenOkButton.SetActive(false);
                waiting = false;
            });
        }

        public void StepOne(BaseControl control)
        {
            if (control == controller.button5)
            {
                controller.intermediateTutorialHelper.GreenStepOneHint.SetActive(false);
                NextStep();
                controller.intermediateTutorialHelper.GreenStepTwoHint.SetActive(true);
            }
        }

        public void StepTwo(BaseControl control)
        {
            if (control == controller.toggle1)
            {
                controller.intermediateTutorialHelper.GreenStepTwoHint.SetActive(false);
                controller.intermediateTutorialHelper.WhenGreenLitHint.SetActive(false);
                NextStep();
            }
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
            controller.intermediateTutorialHelper.WhenRedLitHint.SetActive(true);
            //controller.intermediateTutorialHelper.RedStepOneHint.SetActive(true);
            controller.intermediateTutorialHelper.RedOkButton.SetActive(true);
            controller.intermediateTutorialHelper.RedOkButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                controller.intermediateTutorialHelper.RedStepOneHint.SetActive(true);
                controller.intermediateTutorialHelper.WhenRedLitHint.SetActive(false);
                controller.intermediateTutorialHelper.RedOkButton.SetActive(false);
                waiting = false;
            });
        }

        public void StepOne(BaseControl control)
        {
            if (control == controller.setting1
                && controller.setting1.SelectedOption == 2)
            {
                controller.intermediateTutorialHelper.RedStepOneHint.SetActive(false);
                NextStep();
                controller.intermediateTutorialHelper.RedStepTwoHint.SetActive(true);
            }
        }

        public void StepTwo(BaseControl control)
        {
            if (control == controller.toggle3)
            {
                controller.intermediateTutorialHelper.RedStepTwoHint.SetActive(false);
                controller.intermediateTutorialHelper.WhenRedLitHint.SetActive(false);
                NextStep();
            }
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
            controller.intermediateTutorialHelper.WhenYellowLitHint.SetActive(true);
            //controller.intermediateTutorialHelper.YellowStepOneHint.SetActive(true);
            controller.intermediateTutorialHelper.YellowOkButton.SetActive(true);
            controller.intermediateTutorialHelper.YellowOkButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                controller.intermediateTutorialHelper.YellowStepOneHint.SetActive(true);
                controller.intermediateTutorialHelper.WhenYellowLitHint.SetActive(false);
                controller.intermediateTutorialHelper.YellowOkButton.SetActive(false);
                waiting = false;
            });
        }

        public void StepOne(BaseControl control)
        {
            if (control == controller.button7)
            {
                controller.intermediateTutorialHelper.YellowStepOneHint.SetActive(false);
                NextStep();
                controller.intermediateTutorialHelper.YellowStepTwoHint.SetActive(true);
            }
        }

        public void StepTwo(BaseControl control)
        {
            if (control == controller.button8)
            {
                controller.intermediateTutorialHelper.YellowStepTwoHint.SetActive(false);
                NextStep();
                controller.intermediateTutorialHelper.YellowStepThreeHint.SetActive(true);
            }
        }

        public void StepThree(BaseControl control)
        {
            if (control == controller.button9)
            {
                controller.intermediateTutorialHelper.YellowStepThreeHint.SetActive(false);
                controller.intermediateTutorialHelper.WhenYellowLitHint.SetActive(false);
                NextStep();
            }
        }
    }
}
