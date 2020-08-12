using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public GameObject notificationArea;
    public Button dismissNotification;
    public Text notificationText;

    // Start is called before the first frame update
    void Awake()
    {
        dismissNotification.onClick.AddListener(DismissNotificationPressed);
    }

    public void ShowNotification(string message)
    {
        notificationText.text = message;
        notificationArea.SetActive(true);
    }

    public void DismissNotificationPressed()
    {
        notificationArea.SetActive(false);
    }
}
