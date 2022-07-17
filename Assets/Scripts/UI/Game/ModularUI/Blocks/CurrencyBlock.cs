using MDZ.WildVoid.Utility.Display;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyBlock : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;

    public void Init(Resource currency, int amount)
    {
        icon.sprite = currency.Icon;

        UpdateDisplay(amount);
    }

    public void UpdateDisplay(int amount)
    {
        amountText.text = DisplayConventions.DisplayInt(amount);
    }
}
