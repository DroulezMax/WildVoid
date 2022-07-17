using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MDZ.WildVoid.Utility.Economy;

public class Market : MonoBehaviour
{
    [SerializeField] private ResourceIntDictionary prices = new ResourceIntDictionary();
    [SerializeField] private ResourceStorage storage;
    [SerializeField] private ResourceDemand demand;

    public Dictionary<Resource, int> StorageContent { get => new Dictionary<Resource, int>(storage.Content); }

    private GlobalMarket globalMarket;


    private void Start()
    {
        if (!(globalMarket = FindObjectOfType<GlobalMarket>()))
            Debug.LogError("Market needs a GlobalMarket in scene to function");

        foreach(Resource resource in globalMarket.GlobalPrices.Keys)
        {
            prices.Add(resource, 0);
            UpdatePrice(resource);
        }

        storage.OnResourceAdded += ResourceAddedInStorage;
        storage.OnResourceRemoved += ResourceRemovedInStorage;
    }

    public int Buy(Dictionary<Resource, int> shoppingList, ResourceStorage buyerStorage)
    {
        int value = 0;

        foreach(KeyValuePair<Resource, int> pair in shoppingList)
        {
            value += MarketUtility.GetBuyingCost(this, pair.Key, pair.Value);
        }

        ResourceStorage.Transfer(storage, buyerStorage, shoppingList);

        return value;
    }

    public int Buy(Resource resource, int amount, ResourceStorage buyerStorage)
    {
        int value = MarketUtility.GetBuyingCost(this, resource, amount);

        ResourceStorage.Transfer(storage, buyerStorage, resource, amount);

        return value;
    }

    public int Sell(Dictionary<Resource, int> sellingList, ResourceStorage sellerStorage)
    {
        int value = 0;

        foreach (KeyValuePair<Resource, int> pair in sellingList)
        {
            value += MarketUtility.GetSellingValue(this, pair.Key, pair.Value);
        }

        ResourceStorage.Transfer(sellerStorage, storage, sellingList);

        return value;
    }

    public int Sell(Resource resource, int amount, ResourceStorage sellerStorage)
    {
        int value = MarketUtility.GetSellingValue(this, resource, amount);

        ResourceStorage.Transfer(sellerStorage, storage, resource, amount);

        return value;
    }

    public int GetResourcePrice(Resource resource)
    {
        return prices[resource];
    }

    public int GetResourcePrice(Resource resource, int theoricalAmount)
    {
        return globalMarket.GetPrice(resource, theoricalAmount, demand.GetDemand(resource));
    }

    public Resource[] GetAllResourceTypes()
    {
        return storage.Content.Keys.ToArray();
    }

    private void UpdatePrice(Resource resource)
    {
        prices[resource] = globalMarket.GetPrice(resource, storage.GetAmount(resource), demand.GetDemand(resource));
    }

    private void UpdatePrice(Resource resource, int amount)
    {
        prices[resource] = globalMarket.GetPrice(resource, amount, demand.GetDemand(resource));
    }

    private void ResourceAddedInStorage(Resource resource, int amount)
    {
        UpdatePrice(resource, amount);
    }

    private void ResourceRemovedInStorage(Resource resource, int amount)
    {
        UpdatePrice(resource, amount);
    }

    private void OnDestroy()
    {
        storage.OnResourceAdded -= ResourceAddedInStorage;
        storage.OnResourceRemoved -= ResourceRemovedInStorage;
    }
}
