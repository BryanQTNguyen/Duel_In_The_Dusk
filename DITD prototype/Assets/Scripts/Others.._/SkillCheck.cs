using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheck : MonoBehaviour
{
    public Slider mSlider;
    public bool slay = false;
    // Start is called before the first frame update
    void Start()
    {
        mSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(mSlider.value < mSlider.maxValue && slay == false)
        {
            mSlider.value += 2;
            slay = false;
        }
        else
        {
            mSlider.value -= 2;
            slay = true;
            
        }

    }
}
