using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceConsumer))]
public class ConstructionSite : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private ResourceIntDictionary totalCost;

    [SerializeField] private ResourceIntDictionary alreadyBuilt;

    [SerializeField] private float totalConstructionTime = 50;
    [SerializeField] private float constructionProgress = 0;


    [Header("References")]
    [SerializeField] private ResourceConsumer resourceConsumer;

    public Action<ConstructionSite> OnConstructionFinished;

    private ResourceStorage resourceStorage;

    private int totalMaterialAmount = 0;

    void Start()
    {
        foreach(Resource resource in totalCost.Keys)
        {
            if (!alreadyBuilt.ContainsKey(resource))
                alreadyBuilt.Add(resource, 0);

        }

        totalMaterialAmount = IntValueDictionaryUtility.TotalAmount(totalCost);

        constructionProgress = (float)IntValueDictionaryUtility.TotalAmount(alreadyBuilt) / (float)totalMaterialAmount;
    }

    void Update()
    {
        Build();

        if (constructionProgress >= totalConstructionTime)
            OnConstructionFinished?.Invoke(this);
    }

    private void Build()
    {
        Dictionary<Resource, ConsumptionReport> report = resourceConsumer.Consume();

        Dictionary<Resource, int> feedReport = new Dictionary<Resource, int>();

        foreach(KeyValuePair<Resource, ConsumptionReport> pair in report)
        {
            if(pair.Value.toFeed > 0)
            {
                if(alreadyBuilt[pair.Key] == totalCost[pair.Key])
                {
                    AddConstructionProgress(pair.Value.consumedUnfed);
                } else
                {
                    int clampedBatch = Mathf.Min(pair.Value.toFeed, totalCost[pair.Key] - alreadyBuilt[pair.Key]);

                    if(resourceStorage.CanWithdraw(pair.Key, clampedBatch))
                    {
                        resourceStorage.Remove(pair.Key, clampedBatch);
                        AddConstructionProgress(pair.Value.consumedFed);
                        alreadyBuilt[pair.Key] += clampedBatch;
                        feedReport.Add(pair.Key, clampedBatch);
                    } else
                    {
                        AddConstructionProgress(pair.Value.consumedUnfed);
                    }
                }
            } else
            {
                AddConstructionProgress(pair.Value.consumedFed);
            }
        }

        resourceConsumer.Feed(feedReport);
    }

    private void AddConstructionProgress(float constructed)
    {
        constructionProgress += constructed / totalMaterialAmount * totalConstructionTime;
    }

    public void Setup(PointOfInterest poi)
    {
        resourceStorage = poi.GetComponent<ResourceStorage>();
        if (!resourceStorage)
            Debug.LogError("ConstructionSite setup error : no ResourceStorage in PointOfInterest");
    }

    private void OnValidate()
    {
        SetupConsumer();
    }

    private void SetupConsumer()
    {
        resourceConsumer.Setup(ComputeConsumptionRates());
    }

    private Dictionary<Resource, float> ComputeConsumptionRates()
    {
        Dictionary<Resource, float> rates = new Dictionary<Resource, float>();

        foreach(KeyValuePair<Resource, int> pair in totalCost)
        {
            rates.Add(pair.Key, pair.Value / totalConstructionTime);
        }

        return rates;
    }
}
