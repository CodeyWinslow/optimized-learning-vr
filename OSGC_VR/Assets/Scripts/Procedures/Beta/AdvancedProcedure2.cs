﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedProcedure2 : ProcedureBase
{
    //flags
    bool usermode;
    bool stopped = false;

    //systems info
    List<PowerSystem> level1systems;
    List<PowerSystem> level2systems;
    List<PowerSystem> level3systems;

    int current;
    int power;

    const int startingPower = 300;
    const int currentLimit = 100;

    //Setup for beginning procedure
    public override void BeginProcedure(ProcedureController cont)
    {
        base.BeginProcedure(cont);
        usermode = true;
        stopped = false;
        current = 0;
        power = 0;
        SetupSystems();
        ResetUI();
        ContextSwitched();
        controller.Controls.SubscribeToAllControls(Handler);
    }

    //Stops procedure
    public override void Stop()
    {
        if (Running) controller.Controls.UnsubscribeToAllControls(Handler);
        stopped = true;
        ResetUI();
        UnsetUILabels();
        base.Stop();
    }

    //Update function runs every frame
    public override void RunUpdate()
    {
        if (!stopped)
        {
            //update all system timers and the current
            current = 0;
            power = startingPower;
            PowerSystem curSys = GetCurrentSystem();

            bool level1enabled = false;
            foreach (PowerSystem sys in level1systems)
            {
                if (sys.on)
                {
                    bool notWarm = false;
                    if (!sys.WarmedUp)
                    {
                        notWarm = true;
                        sys.warmUpTimer -= Time.deltaTime;
                    }

                    if (sys.WarmedUp)
                    {
                        level1enabled = true;
                        current += sys.runningCurrent;
                        if (notWarm && sys == curSys)
                        {
                            controller.Controls.greenLight.Lit = true;
                            controller.Controls.yellowLight.Lit = false;
                            controller.Controls.redLight.Lit = false;
                        }
                    }
                    else
                        current += sys.initialCurrent;

                    power -= sys.power;
                }
            }
            bool level2enabled = false;
            foreach (PowerSystem sys in level2systems)
            {
                if (sys.on)
                {
                    bool notWarm = false;
                    if (!sys.WarmedUp)
                    {
                        notWarm = true;
                        sys.warmUpTimer -= Time.deltaTime;
                    }

                    if (sys.WarmedUp)
                    {
                        level2enabled = true;
                        current += sys.runningCurrent;
                        if (notWarm && sys == curSys)
                        {
                            controller.Controls.greenLight.Lit = true;
                            controller.Controls.yellowLight.Lit = false;
                            controller.Controls.redLight.Lit = false;
                        }
                    }
                    else
                        current += sys.initialCurrent;

                    power -= sys.power;
                }
            }
            bool level3enabled = false;
            foreach (PowerSystem sys in level3systems)
            {
                if (sys.on)
                {
                    bool notWarm = false;
                    if (!sys.WarmedUp)
                    {
                        notWarm = true;
                        sys.warmUpTimer -= Time.deltaTime;
                    }

                    if (sys.WarmedUp)
                    {
                        level3enabled = true;
                        current += sys.runningCurrent;
                        if (notWarm && sys == curSys)
                        {
                            controller.Controls.greenLight.Lit = true;
                            controller.Controls.yellowLight.Lit = false;
                            controller.Controls.redLight.Lit = false;
                        }
                    }
                    else
                        current += sys.initialCurrent;

                    power -= sys.power;
                }
            }

            //update UI for current
            if (controller.Controls.toggle2.isOn)
                controller.Controls.meter1.Value = power;
            else
                controller.Controls.meter1.Value = current;

            if (level1enabled) { controller.Controls.button1.ToggleButton(true); }
            else { controller.Controls.button1.ToggleButton(false); }

            if (level2enabled) { controller.Controls.button2.ToggleButton(true); }
            else { controller.Controls.button2.ToggleButton(false); }

            if (level3enabled) { controller.Controls.button3.ToggleButton(true); }
            else { controller.Controls.button3.ToggleButton(false); }

            if (current > currentLimit || power < 0)
            {
                controller.Controls.UnsubscribeToAllControls(Handler);
                ResetUI();
                UnsetUILabels();
                EndProcedure(false);
            }
            else if (level1enabled && level2enabled && level3enabled)
            {
                controller.Controls.UnsubscribeToAllControls(Handler);
                ResetUI();
                UnsetUILabels();
                EndProcedure(true);
            }
        }
    }

    //Handler captures usage of controls
    void Handler(BaseControl control)
    {
        if (usermode)
        {
            if (control == controller.Controls.setting1
                || control == controller.Controls.setting2
                || control == controller.Controls.toggle2)
                ContextSwitched();
            else if (control == controller.Controls.toggle1)
            {
                PowerSystem system = GetCurrentSystem();

                system.on = !system.on;

                if (system.on)
                {
                    controller.Controls.yellowLight.Lit = true;
                    controller.Controls.redLight.Lit = false;
                }
                else
                {
                    controller.Controls.redLight.Lit = true;
                    controller.Controls.yellowLight.Lit = false;
                    controller.Controls.greenLight.Lit = false;
                    system.ResetTimer();
                }
            }

            //light up buttons
            if (control == controller.Controls.button1)
            {
                int ii = 0;
                bool found = false;
                for (ii = 0; ii < level1systems.Count && !found; ++ii)
                {
                    if (level1systems[ii].on && level1systems[ii].WarmedUp)
                        found = true;
                }

                if (found)
                {
                    controller.Controls.setting1.option1.isOn = true;

                    switch (--ii)
                    {
                        case 0:
                            controller.Controls.setting2.option1.isOn = true;
                            break;
                        case 1:
                            controller.Controls.setting2.option2.isOn = true;
                            break;
                        case 2:
                            controller.Controls.setting2.option3.isOn = true;
                            break;
                    }

                    ContextSwitched();
                }
            }
            else if (control == controller.Controls.button2)
            {
                int ii = 0;
                bool found = false;
                for (ii = 0; ii < level2systems.Count && !found; ++ii)
                {
                    if (level2systems[ii].on && level2systems[ii].WarmedUp)
                        found = true;
                }

                if (found)
                {
                    controller.Controls.setting1.option2.isOn = true;

                    switch (--ii)
                    {
                        case 0:
                            controller.Controls.setting2.option1.isOn = true;
                            break;
                        case 1:
                            controller.Controls.setting2.option2.isOn = true;
                            break;
                        case 2:
                            controller.Controls.setting2.option3.isOn = true;
                            break;
                    }
                    ContextSwitched();
                }
            }
            else if (control == controller.Controls.button3)
            {
                int ii = 0;
                bool found = false;
                for (ii = 0; ii < level1systems.Count && !found; ++ii)
                {
                    if (level3systems[ii].on && level3systems[ii].WarmedUp)
                        found = true;
                }

                if (found)
                {
                    controller.Controls.setting1.option3.isOn = true;

                    switch (--ii)
                    {
                        case 0:
                            controller.Controls.setting2.option1.isOn = true;
                            break;
                        case 1:
                            controller.Controls.setting2.option2.isOn = true;
                            break;
                        case 2:
                            controller.Controls.setting2.option3.isOn = true;
                            break;
                    }
                    ContextSwitched();
                }
            }
        }
    }

    //reset labels to original
    void UnsetUILabels()
    {
        controller.Controls.setting1.SetLabel();
        controller.Controls.setting2.SetLabel();
        controller.Controls.toggle1.SetLabel();
        controller.Controls.toggle2.SetLabel();
        controller.Controls.meter1.SetLabel();
        controller.Controls.meter2.SetLabel();
        controller.Controls.meter3.SetLabel();
    }

    //Resets UI for procedure
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

        //toggle
        controller.Controls.toggle1.SetValue(false);
        controller.Controls.toggle2.SetValue(false);
        controller.Controls.toggle3.SetValue(false);
        controller.Controls.toggle4.SetValue(false);

        //slider
        controller.Controls.slider1.Value = 0.5f;
        controller.Controls.slider2.Value = 0.5f;
        controller.Controls.slider3.Value = 0.5f;

        //labels
        controller.Controls.setting1.SetLabel("Backup System");
        controller.Controls.setting2.SetLabel("Oxygen");
        controller.Controls.toggle1.SetLabel("On/Off");
        controller.Controls.toggle2.SetLabel("Current");
        controller.Controls.meter1.SetLabel("Total Current");
        controller.Controls.meter2.SetLabel("Initial Current");
        controller.Controls.meter3.SetLabel("Running Current");

        //buttons
        controller.Controls.button1.ToggleButton(false);
        controller.Controls.button2.ToggleButton(false);
        controller.Controls.button3.ToggleButton(false);

        usermode = true;
    }

    //Gets the current system based on what settings are chosen
    PowerSystem GetCurrentSystem()
    {
        List<PowerSystem> currentSystems;

        if (controller.Controls.setting1.option1.isOn) currentSystems = level1systems;
        else if (controller.Controls.setting1.option2.isOn) currentSystems = level2systems;
        else currentSystems = level3systems;

        PowerSystem system;

        if (controller.Controls.setting2.option1.isOn) system = currentSystems[0];
        else if (controller.Controls.setting2.option2.isOn) system = currentSystems[1];
        else system = currentSystems[2];

        return system;
    }

    //Update UI based on settings
    void ContextSwitched()
    {
        usermode = false;

        //get current system
        PowerSystem system = GetCurrentSystem();

        //update UI
        if (system.on)
        {
            controller.Controls.toggle1.SetValue(true);
            if (system.WarmedUp)
            {
                controller.Controls.greenLight.Lit = true;
                controller.Controls.yellowLight.Lit = false;
            }
            else
            {
                controller.Controls.greenLight.Lit = false;
                controller.Controls.yellowLight.Lit = true;
            }
            controller.Controls.redLight.Lit = false;
        }
        else
        {
            controller.Controls.toggle1.SetValue(false);
            controller.Controls.greenLight.Lit = false;
            controller.Controls.yellowLight.Lit = false;
            controller.Controls.redLight.Lit = true;
        }

        if (controller.Controls.toggle2.isOn)
        {
            controller.Controls.meter2.Value = system.power;
            controller.Controls.meter3.Value = 0;
            controller.Controls.toggle2.SetLabel("Power");
            controller.Controls.meter1.SetLabel("Total Power");
            controller.Controls.meter2.SetLabel("System Power");
            controller.Controls.meter3.SetLabel();
        }
        else
        {
            controller.Controls.meter2.Value = system.initialCurrent;
            controller.Controls.meter3.Value = system.runningCurrent;
            controller.Controls.toggle2.SetLabel("Current");
            controller.Controls.meter1.SetLabel("Total Current");
            controller.Controls.meter2.SetLabel("Initial Current");
            controller.Controls.meter3.SetLabel("Running Current");
        }
        controller.Controls.setting2.SetLabel(system.backupName);

        usermode = true;
    }

    // initialize systems semi-randomly
    void SetupSystems()
    {
        int currentBudget = 100;
        int powerBudget = 300;

        //running current cannot exceed 40% budget
        int level1running = UnityEngine.Random.Range(20, 41);
        currentBudget -= level1running;
        //surge cannot exceed 180% of running current
        int level1initial = UnityEngine.Random.Range(level1running, Convert.ToInt32(level1running * 1.8f) + 1);
        int level1power = UnityEngine.Random.Range(80, 121);
        powerBudget -= level1power;

        int level2running = UnityEngine.Random.Range(20, 41);
        currentBudget -= level2running;
        int level2initial = UnityEngine.Random.Range(level2running, Convert.ToInt32(level2running * 1.8f) + 1);
        int level2power = UnityEngine.Random.Range(80, 121);
        powerBudget -= level2power;

        //ensure the final system does not exceed budget
        int level3running = UnityEngine.Random.Range(20, Convert.ToInt32(currentBudget * 0.8f) + 1);
        int level3initial = UnityEngine.Random.Range(level3running, currentBudget + 1);
        int level3power = UnityEngine.Random.Range(Convert.ToInt32(powerBudget * 0.8f) + 1, powerBudget + 1);

        List<int> places = new List<int> { 0, 1, 2 };
        int level1placement = places[UnityEngine.Random.Range(0, 3)];
        places.Remove(level1placement);
        int level2placement = places[UnityEngine.Random.Range(0, 2)];
        places.Remove(level2placement);
        int level3placement = places[0];
        places.Remove(level3placement);

        level1systems = new List<PowerSystem>();
        int randomRunning;
        int randomInitial;
        int randomPower;
        for (int ii = 0; ii < 3; ++ii)
        {
            if (ii == level1placement)
            {
                level1systems.Add(new PowerSystem(level1initial, level1running, 5f, level1power, "Oxygen"));
            }
            else
            {
                randomRunning = UnityEngine.Random.Range(20, 46);
                randomInitial = UnityEngine.Random.Range(randomRunning, Convert.ToInt32(randomRunning * 1.8f));
                randomPower = UnityEngine.Random.Range(90, 131);
                level1systems.Add(new PowerSystem(randomInitial, randomRunning, 5f, randomPower, "Oxygen"));
            }
        }

        level2systems = new List<PowerSystem>();
        for (int ii = 0; ii < 3; ++ii)
        {
            if (ii == level2placement)
            {
                level2systems.Add(new PowerSystem(level2initial, level2running, 5f, level2power, "Food Synthesis"));
            }
            else
            {
                randomRunning = UnityEngine.Random.Range(20, 46);
                randomInitial = UnityEngine.Random.Range(randomRunning, Convert.ToInt32(randomRunning * 1.8f));
                randomPower = UnityEngine.Random.Range(90, 131);
                level2systems.Add(new PowerSystem(randomInitial, randomRunning, 5f, randomPower, "Food Synthesis"));
            }
        }

        level3systems = new List<PowerSystem>();
        for (int ii = 0; ii < 3; ++ii)
        {
            if (ii == level3placement)
            {
                level3systems.Add(new PowerSystem(level3initial, level3running, 5f, level3power, "Climate Control"));
            }
            else
            {
                randomRunning = UnityEngine.Random.Range(20, 46);
                randomInitial = UnityEngine.Random.Range(randomRunning, Convert.ToInt32(randomRunning * 1.8f));
                randomPower = UnityEngine.Random.Range(90, 131);
                level3systems.Add(new PowerSystem(randomInitial, randomRunning, 5f, randomPower, "Climate Control"));
            }
        }
    }

    //class for a system
    class PowerSystem
    {
        public int initialCurrent;
        public int runningCurrent;
        public int power;
        public string backupName;

        public int Current
        {
            get { if (WarmedUp)
                    return runningCurrent;
                else
                    return initialCurrent; }
        }

        public bool WarmedUp
        {
            get {
                if (warmUpTimer <= 0)
                    return true;
                else
                    return false;
            }
        }

        float timeToWarmUp;

        public float warmUpTimer;
        public bool on;

        public PowerSystem(int init, int running, float warmUp, int pow, string name)
        {
            initialCurrent = init;
            runningCurrent = running;
            warmUpTimer = timeToWarmUp = warmUp;
            backupName = name;
            power = pow;
            on = false;
        }

        public void ResetTimer()
        {
            warmUpTimer = timeToWarmUp;
        }
    }
}
