﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleControl))]
public class ToggleUIScript : MonoBehaviour
{
    public Image ToggleOffImage;
    public Image ToggleOnImage;

    ToggleControl tControl;

    // Start is called before the first frame update
    void Awake()
    {
        tControl = GetComponent<ToggleControl>();
        tControl.ToggleChangedEvent += CheckToggle;
    }

    void Start()
    {
        CheckToggle(tControl);
    }

    public void CheckToggle(BaseControl t)
    {
        if ((bool)(t.ControlValue()))
        {
            ToggleOffImage.enabled = false;
            ToggleOnImage.enabled =true;
        }
        else
        {
            ToggleOffImage.enabled =true;
            ToggleOnImage.enabled = false;
        }

    }
}
