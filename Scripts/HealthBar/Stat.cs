using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Stat
{
    [SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxValue;

    private float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {           
            this.maxValue = value;
            bar.MaxValue = maxValue;
        }
    }

    [SerializeField]
    private float currentVal;

    public float CurrentVal 
    {
        get 
        {
            return currentVal;
        }

        set
        {
            this.currentVal = Mathf.Clamp(value,0,MaxValue);
            bar.Value = currentVal;
        }

    }

    public void Initialize(int initValue)
    {
        this.MaxValue = initValue;
        this.CurrentVal = initValue;
    }
}
