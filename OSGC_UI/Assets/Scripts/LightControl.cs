using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LightControl : BaseControl
{
    public Sprite LightOffImage;
    public Sprite LightOnImage;

    Image imageBehaviour;

    [SerializeField]
    bool lit = false;

    public bool Lit
    {
        get { return lit; }
        set
        {
            lit = value;

            UpdateImage();
        }
    }

    void Awake()
    {
        _controlType = BaseControlType.Light;
        imageBehaviour = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateImage();
    }

    void UpdateImage()
    {
        if (lit)
        {
            if (LightOnImage != null)
                imageBehaviour.sprite = LightOnImage;
        }
        else
        {
            if (LightOffImage != null)
                imageBehaviour.sprite = LightOffImage;
        }
    }

    public override object ControlValue()
    {
        return null;
    }
}
