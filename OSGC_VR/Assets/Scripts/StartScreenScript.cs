using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{
    public UIControlCenter Controls;

    public void StartButtonPressed()
    {
        Controls.EnableAllColliders();
        gameObject.SetActive(false);
    }

    public void ShowScreen()
    {
        Controls.DisableAllColliders();
        gameObject.SetActive(true);
    }
}
