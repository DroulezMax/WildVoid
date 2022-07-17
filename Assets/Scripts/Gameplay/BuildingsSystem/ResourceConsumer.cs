using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConsumptionReport
{
    public int toFeed;
    public float consumedFed;
    public float consumedUnfed;

    public ConsumptionReport(int toFeed, float consumedFed, float consumedUnfed)
    {
        this.toFeed = toFeed;
        this.consumedFed = consumedFed;
        this.consumedUnfed = consumedUnfed;
    }
}

public class ResourceConsumer : MonoBehaviour
{
    [SerializeField] ResourceFloatDictionary consumptionRates = new ResourceFloatDictionary();
    [SerializeField] private int batch = 1;

    private Dictionary<Resource, ResourceConsumerCounter> counters = new Dictionary<Resource, ResourceConsumerCounter>();

    private void Start()
    {
        foreach(KeyValuePair<Resource, float> pair in consumptionRates)
        {
            counters.Add(pair.Key, new ResourceConsumerCounter(pair.Value, batch));
        }
    }

    public void Setup(Dictionary<Resource, float> consumptionRates)
    {
        this.consumptionRates.CopyFrom(consumptionRates);
    }

    public Dictionary<Resource, ConsumptionReport> Consume()
    {
        Dictionary<Resource, ConsumptionReport> consumed = new Dictionary<Resource, ConsumptionReport>();

        ConsumptionReport report;

        foreach (KeyValuePair<Resource, ResourceConsumerCounter> pair in counters)
        {
            report = pair.Value.Consume();

            consumed.Add(pair.Key, report);
        }

        return consumed;
    }

    public void Feed(Dictionary<Resource, int> resources)
    {
        foreach(KeyValuePair<Resource, int> pair in resources)
        {
            counters[pair.Key].Feed(pair.Value);
        }
    }

    private class ResourceConsumerCounter
    {
        private float rate = 1;
        private int batch = 1;

        private float stock = 0;
        public ResourceConsumerCounter(float rate = 1, int batch = 1)
        {
            this.rate = rate;
            this.batch = batch;
        }

        public ConsumptionReport Consume()
        {
            float previousStock = stock;
            float consumedFed = rate * Time.deltaTime;
            stock -= consumedFed;

            if (stock <= 0)
            {
                stock = 0;
                return new ConsumptionReport(batch, consumedFed, previousStock);
            }

            return new ConsumptionReport(0, consumedFed, consumedFed);
        }

        public void Feed(int amount)
        {
            stock += amount;
        }
    }
}


