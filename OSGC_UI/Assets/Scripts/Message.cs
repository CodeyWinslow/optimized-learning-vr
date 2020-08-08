using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public delegate void NextClickHandler(Message msg);
    public event NextClickHandler onClick;

    [SerializeField]
    GameObject message = null;
    [SerializeField]
    GameObject nextButton = null;

    private void Awake()
    {
        if (nextButton != null)
            nextButton.GetComponent<Button>().onClick.AddListener(onButtonClicked);
    }

    public void onButtonClicked()
    {
        onClick(this);
    }

    public void Show()
    {
        if (message != null)
            message.SetActive(true);
        if (nextButton != null)
            nextButton.SetActive(true);
    }

    public void Hide()
    {
        if (message != null)
            message.SetActive(false);
        if (nextButton != null)
            nextButton.SetActive(false);
    }
}
