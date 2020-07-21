using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        imageBehaviour = GetComponent<Image>();

        UpdateImage();
    }

    void UpdateImage()
    {
        if (lit)
            imageBehaviour.sprite = LightOnImage;
        else
            imageBehaviour.sprite = LightOffImage;
    }
}
