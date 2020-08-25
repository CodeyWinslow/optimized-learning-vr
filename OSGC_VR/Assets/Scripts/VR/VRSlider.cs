using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(LinearMapping))]
public class VRSlider : MonoBehaviour
{
    Slider sliderComp;
    LinearMapping mappingComp;

    float lastVal;

    // Start is called before the first frame update
    void Awake()
    {
        sliderComp = GetComponent<Slider>();
        mappingComp = GetComponent<LinearMapping>();
        //set to -1 to ensure it gets changed on first update
        lastVal = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastVal != mappingComp.value)
        {
            sliderComp.value = mappingComp.value;
            lastVal = mappingComp.value;
        }
    }
}
