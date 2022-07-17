using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    ADDITIVE,
    MULTIPLICATIVE_BASE,
    MULTIPLICATIVE_TOTAL
}
public class CompositeFloat
{
    private float baseValue = 0;

    private float finalValue = 0;
    public float Value { get { return finalValue; } }

    public CompositeFloat(float baseValue)
    {
        this.baseValue = baseValue;

        Compute();
    }

    private void Compute()
    {
        //temporary
        finalValue = baseValue;
    }
}
