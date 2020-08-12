using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderValueUI : MonoBehaviour
{
    Text textComp;
    public SliderControl slider;

    void Awake()
    {
        textComp = GetComponent<Text>();
        slider.SliderChangedEvent += OnSliderValueChanged;
    }

    void Start()
    {
        OnSliderValueChanged(slider);
    }

    public void OnSliderValueChanged(BaseControl s)
    {
        textComp.text = string.Format("{0:F2}", (float)(s.ControlValue()));
    }
}
