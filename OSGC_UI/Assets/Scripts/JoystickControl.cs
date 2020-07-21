using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class JoystickControl : MonoBehaviour
{
    public delegate void DirectionChangedDelegate(JoystickControl j, Vector2 direction, float magnitude);
    public event DirectionChangedDelegate DirectionChangedEvent;

    Camera mainCam;
    RectTransform position;
    Vector3 originalPosition;

    public float radius = 2f;

    bool attached = false;
    bool released = false;

    void Start()
    {
        position = GetComponent<RectTransform>();
        originalPosition = position.position;
        mainCam = Camera.main;
    }

    public void OnPointerDown(BaseEventData e)
    {
        attached = true;
    }

    void Update()
    {
        if (attached && Input.GetMouseButtonUp(0))
        {
            attached = false;
            released = true;
        }
        if (attached)
        {
            Vector3 newPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = originalPosition.z;
            Vector3 positionDelta = newPosition - originalPosition;
            if (positionDelta.magnitude > radius)
            {
                newPosition = originalPosition + (positionDelta.normalized * radius);
            }
            position.position = newPosition;

            if (DirectionChangedEvent != null)
                DirectionChangedEvent(this, positionDelta.normalized, positionDelta.magnitude);
        }
        else if (released)
        {
            position.position = originalPosition;
            released = false;

            if (DirectionChangedEvent != null)
                DirectionChangedEvent(this, Vector3.zero, 0);
        }
    }
}
