using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour
{

    [SerializeField] private Resource resource = null;
    public Resource Resource { get { return resource; } set { resource = value; } }

    [SerializeField] private int amount = 100;
    public int Amount { get { return amount; } set { amount = value; } }

    [Range(0, 1)]
    [SerializeField] private float grade = 0.5f;
    public float Grade { get { return grade; } set { grade = value; } }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Extract(int desiredAmount)
    {
        int extracted;
        if(amount > desiredAmount)
        {
            extracted = desiredAmount;
            amount -= extracted;
        } else
        {
            extracted = amount;
            amount = 0;
        }
        return extracted;
    }
}
