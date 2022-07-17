 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    [SerializeField] private float rate = 1;
    [SerializeField] private int batch = 1;

    public float Rate { get => rate; set => rate = value; }

    private float produceCounter = 0;

    public int Produce()
    {
        produceCounter += rate * Time.deltaTime;

        if(produceCounter >= batch)
        {
            //returns the amount produced by batches
            int produced = (Mathf.FloorToInt(produceCounter) / batch) * batch;
            produceCounter -= produceCounter;
            return produced;
        }

        return 0;
    }
}
