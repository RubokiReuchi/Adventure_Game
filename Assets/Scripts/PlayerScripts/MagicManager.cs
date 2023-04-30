using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider magicSlider;
    public FloatValue magic;

    // Start is called before the first frame update
    void Start()
    {
        magicSlider.maxValue = magic.initialValue;
        magicSlider.value = magic.currentValue;
    }

    public void UpdateMagic()
    {
        magicSlider.value = magic.currentValue;
        if (magic.currentValue > magicSlider.maxValue)
        {
            magicSlider.value = magicSlider.maxValue;
            magic.currentValue = magicSlider.maxValue;
        }
        else if (magic.currentValue < 0)
        {
            magicSlider.value = 0;
            magic.currentValue = 0;
        }
    }
}
