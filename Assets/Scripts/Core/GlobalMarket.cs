using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMarket : MonoBehaviour
{
    [SerializeField] private ResourceMarketPriceDictionary globalPrices;

    public Dictionary<Resource, MarketPrice> GlobalPrices { get => globalPrices; }

    public int GetPrice(Resource resource, int supply, int demand)
    {
        return globalPrices[resource].GetPrice((float)supply / (float)demand);
    }
}
