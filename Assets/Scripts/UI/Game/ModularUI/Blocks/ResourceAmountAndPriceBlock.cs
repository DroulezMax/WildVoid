using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAmountAndPriceBlock : MonoBehaviour
{
    [SerializeField] private ResourceAmountBlock resourceAmountBlock;
    [SerializeField] private CurrencyBlock currencyBlock;

    public void Init(Resource resource, int amount, Resource currency, int price)
    {
        resourceAmountBlock.Init(resource, amount);
        currencyBlock.Init(currency, price);
    }

    public void UpdateDisplay(int amount, int price)
    {
        resourceAmountBlock.UpdateDisplay(amount);
        currencyBlock.UpdateDisplay(price);
    }
}
