using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDemand : MonoBehaviour
{
    [SerializeField] private ResourceIntDictionary demand;

    public int GetDemand(Resource resource)
    {
        if(demand.ContainsKey(resource))
            return demand[resource];

        return 0;
    }
}
