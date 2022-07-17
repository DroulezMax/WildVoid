using MDZ.WildVoid.Utility.Display;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceAmountBlock : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;

    public void Init(Resource resource, int amount)
    {
        icon.sprite = resource.Icon;

        UpdateDisplay(amount);
    }

    public void UpdateDisplay(int amount)
    {
        amountText.text = DisplayConventions.DisplayInt(amount);
    }
}
