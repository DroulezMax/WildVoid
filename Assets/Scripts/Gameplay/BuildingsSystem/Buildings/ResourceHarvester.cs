using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHarvester : Building
{
    [Header("Parameters")]
    [SerializeField] private float baseEfficiency = 1;

    [Header("References")]
    [SerializeField] private ResourceProducer producer;

    private ResourceDeposit deposit;
    private ResourceStorage storage;

    private void Start()
    {
        UpdateProducerRate();
    }
    public void Setup(ResourceDeposit deposit, ResourceStorage storage)
    {
        this.deposit = deposit;
        this.storage = storage;

    }

    private void Update()
    {
        int produced = producer.Produce();

        if(produced > 0)
        {
            storage.Add(deposit.Resource, deposit.Extract(produced));
        }
    }

    private void OnValidate()
    {
        if (producer && deposit)
            UpdateProducerRate();
    }

    private void UpdateProducerRate()
    {
        producer.Rate = baseEfficiency * deposit.Grade;
    }
}
