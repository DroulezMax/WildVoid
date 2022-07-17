using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : Building
{
    [SerializeField] private ResourceConsumer resourceConsumer;

    private ResourceStorage resourceStorage;

    private void Update()
    {
        ConsumeResources();
    }

    //Upgrade by creating a utility for buying consumed resources
    private void ConsumeResources()
    {
        Dictionary<Resource, ConsumptionReport> report = resourceConsumer.Consume();

        Dictionary<Resource, int> feedReport = new Dictionary<Resource, int>();

        foreach(KeyValuePair<Resource, ConsumptionReport> pair in report)
        {
            if(pair.Value.toFeed > 0)
            {
                if(resourceStorage.CanWithdraw(pair.Key, pair.Value.toFeed))
                {
                    resourceStorage.Remove(pair.Key, pair.Value.toFeed);
                    feedReport.Add(pair.Key, pair.Value.toFeed);
                }
            }
        }

        resourceConsumer.Feed(feedReport);
    }

    public override void Setup(PointOfInterest poi)
    {
        base.Setup(poi);

        resourceStorage = poi.GetComponent<ResourceStorage>();
        if (!resourceStorage)
            Debug.LogError("Colony setup error : no ResourceStorage in PointOfInterest");

    }
}
