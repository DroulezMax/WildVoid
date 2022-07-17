using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyParameters : MonoBehaviour
{
    [SerializeField] private Resource globalCurrency;

    public Resource GlobalCurrency { get => globalCurrency; }
}
