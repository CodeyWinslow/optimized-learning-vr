using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermediateProcedureTutorial2 : ProcedureBase
{
    List<TriggerResponse> tasks;
    List<TriggerResponse>.Enumerator currentTask;

    bool usermode = true;

    bool secondTime = false;
    bool repeatMessage = false;

    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        //triggersToSucceed = baseTriggersToSucceed;
        tasks = new List<TriggerResponse>()
        {
            new IntermediateTask1(),
            new IntermediateTask2(),
            new IntermediateTask3()
        };
        currentTask = tasks.GetEnumerator();
        secondTime = false;

        usermode = true;
        ResetUI();
        BeginTask();
        controller.Controls.SubscribeToAllControls(HandleInput);
    }

    void BeginTask()
    {
        ResetUI();
        if (!currentTask.MoveNext())
        {
            if (secondTime)
            {
                EndProcedure(true);
                return;
            }
            else
            {
                if (repeatMessage)
                {
                    currentTask = tasks.GetEnumerator();
                    secondTime = true;
                    BeginTask();
                }
                else
                {
                    repeatMessage = true;
                    controller.Controls.intermediateTutorialHelper2.RepeatMessage.SetActive(true);
                    controller.Controls.intermediateTutorialHelper2.RepeatMessage.GetComponentInChildren<Button>().onClick.AddListener(()=>
                    {
                        controller.Controls.intermediateTutorialHelper2.RepeatMessage.SetActive(false);
                        BeginTask();
                    });
                }
                return;
            }
        }
        currentTask.Current.SetController(controller.Controls);
        currentTask.Current.Begin();
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

        if (currentTask.Current != null)
            currentTask.Current.currentStep.Current(control);
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
            finished = false;
            successful = false;
            currentStep = responseSteps.GetEnumerator();
            SetupUI();
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

            if (controller.intermediateTutorialHelper2 != null)
            {
                controller.intermediateTutorialHelper2.WhenGreenHint.SetActive(true);
                controller.intermediateTutorialHelper2.WhenGreenButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    controller.intermediateTutorialHelper2.WhenGreenHint.SetActive(false);
                    controller.intermediateTutorialHelper2.GreenHintOne.SetActive(true);
                    SetupUI();
                });
            }
            else usermode = true;
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
                {
                    if (controller.intermediateTutorialHelper2 != null)
                        controller.intermediateTutorialHelper2.GreenHintTwo.SetActive(false);
                    NextStep();
                }
            }
            else if (controller.intermediateTutorialHelper2 != null
                    && control.ControlType == BaseControlType.Slider
                    && controller.slider1.Value == 1f
                    && controller.slider2.Value == 1f
                    && controller.slider3.Value == 0)
            {
                controller.intermediateTutorialHelper2.GreenHintOne.SetActive(false);
                controller.intermediateTutorialHelper2.GreenHintTwo.SetActive(true);
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

            if (controller.intermediateTutorialHelper2 != null)
            {
                controller.intermediateTutorialHelper2.WhenYellowHint.SetActive(true);
                controller.intermediateTutorialHelper2.WhenYellowButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    controller.intermediateTutorialHelper2.WhenYellowHint.SetActive(false);
                    controller.intermediateTutorialHelper2.YellowHintOne.SetActive(true);
                    controller.intermediateTutorialHelper2.YellowHintTwo.SetActive(true);
                    SetupUI();
                });
            }
            else usermode = true;
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
                {
                    if (controller.intermediateTutorialHelper2 != null)
                        controller.intermediateTutorialHelper2.YellowHintThree.SetActive(false);
                    NextStep();
                }
            }
            else if (controller.intermediateTutorialHelper2 != null
                    && control.ControlType == BaseControlType.Toggle
                    && controller.toggle1.isOn
                    && !controller.toggle2.isOn
                    && controller.toggle3.isOn
                    && controller.toggle4.isOn)
            {
                controller.intermediateTutorialHelper2.YellowHintOne.SetActive(false);
                controller.intermediateTutorialHelper2.YellowHintTwo.SetActive(false);
                controller.intermediateTutorialHelper2.YellowHintThree.SetActive(true);
            }
        }
    }

    class IntermediateTask3 : TriggerResponse
    {
        bool usermode = true;

        bool[] option1toggles;
        bool[] option2toggles;
        bool[] option3toggles;

        enum TutState
        {
            FirstToggles,
            SecondToggles,
            FinalToggles
        };

        TutState curState = TutState.FirstToggles;

        public IntermediateTask3() : base()
        {
            trigger = MainTrigger;
            responseSteps.Add(Handler);
            curState = TutState.FirstToggles;
            option1toggles = new bool[] { true, false, true, true };
            option2toggles = new bool[] { false, false, true, true };
            option3toggles = new bool[] { true, true, true, true };
        }

        public override void Begin()
        {
            curState = TutState.FirstToggles;
            option1toggles = new bool[] { true, false, true, true };
            option2toggles = new bool[] { false, false, true, true };
            option3toggles = new bool[] { true, true, true, true };
            base.Begin();
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
            if (controller.toggle1.isOn) ++numOn;
            if (controller.toggle2.isOn) ++numOn;
            if (controller.toggle3.isOn) ++numOn;
            if (controller.toggle4.isOn) ++numOn;
            return numOn;
        }

        public void MainTrigger()
        {
            usermode = false;
            controller.redLight.Lit = true;
            controller.yellowLight.Lit = false;
            controller.greenLight.Lit = false;
            if (controller.intermediateTutorialHelper2 != null)
            {
                controller.intermediateTutorialHelper2.WhenRedHint.SetActive(true);
                controller.intermediateTutorialHelper2.WhenRedButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    controller.intermediateTutorialHelper2.WhenRedHint.SetActive(false);
                    controller.intermediateTutorialHelper2.RedHintOne.SetActive(true);
                    SetupUI();
                });
            }
            else usermode = true;
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
                    if (controller.intermediateTutorialHelper2 != null)
                        controller.intermediateTutorialHelper2.RedHintFive.SetActive(false);
                    NextStep();
                }
            }
            else if (control == controller.setting1)
            {
                if (controller.intermediateTutorialHelper2 != null
                    && curState == TutState.SecondToggles
                    && controller.intermediateTutorialHelper2.RedHintTwo.activeSelf)
                {
                    controller.intermediateTutorialHelper2.RedHintTwo.SetActive(false);
                    controller.intermediateTutorialHelper2.RedHintThree.SetActive(true);
                }
                UpdateToggles();
            }
            else if (control.ControlType == BaseControlType.Toggle)
                CheckToggles();
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

            //tutorial messages
            if (controller.intermediateTutorialHelper2 != null)
            {
                if (curState == TutState.FirstToggles
                    && numTogglesOn(0) == 0)
                {
                    controller.intermediateTutorialHelper2.RedHintOne.SetActive(false);
                    controller.intermediateTutorialHelper2.RedHintTwo.SetActive(true);
                    curState = TutState.SecondToggles;
                }
                else if ((currentoptiontoggles == option2toggles
                    && numTogglesOn(1) == 0)
                    || (currentoptiontoggles == option3toggles
                    && numTogglesOn(2) == 0))
                {
                    if (curState == TutState.SecondToggles)
                    {
                        controller.intermediateTutorialHelper2.RedHintThree.SetActive(false);
                        controller.intermediateTutorialHelper2.RedHintFour.SetActive(true);
                        curState = TutState.FinalToggles;
                    }
                    else if (curState == TutState.FinalToggles)
                    {
                        controller.intermediateTutorialHelper2.RedHintFour.SetActive(false);
                        controller.intermediateTutorialHelper2.RedHintFive.SetActive(true);
                    }
                }
            }
        }
    }
}
