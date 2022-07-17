using MDZ.WildVoid.Utility.Display;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepositSlotBlock : MonoBehaviour
{
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private TMP_Text amountText;

    private DepositSlots depositSlots;
    private ResourceDeposit deposit;

    private void Update()
    {
        UpdateDisplay();
    }

    public void Init(DepositSlots depositSlots, ResourceDeposit deposit)
    {
        this.depositSlots = depositSlots;
        this.deposit = deposit;

        InitDisplay();
    }

    private void UpdateDisplay()
    {
        gradeText.text = DisplayConventions.DisplayGrade(deposit.Grade);
        amountText.text = DisplayConventions.DisplayInt(deposit.Amount);

    }

    private void InitDisplay()
    {
        resourceIcon.sprite = deposit.Resource.Icon;

        UpdateDisplay();
    }
}
