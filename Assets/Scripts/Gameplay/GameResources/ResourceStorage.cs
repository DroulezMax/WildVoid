using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private int space = 100;
    public int AvailableSpace { get => infiniteSpace ? int.MaxValue : space - GetUsedSpace(); }

    [SerializeField] private bool infiniteSpace = false;
    [SerializeField] private ResourceIntDictionary content;

    public ResourceIntDictionary Content { get => content; }

    public Action<Resource, int> OnResourceAdded;
    public Action<Resource, int> OnResourceRemoved;

    public Action<Resource, int> OnNewResourceAdded;
    public Action<Resource, int> OnResourceTotallyRemoved;

    public static void Transfer(ResourceStorage from, ResourceStorage to, Dictionary<Resource, int> resources)
    {
        foreach(KeyValuePair<Resource, int> pair in resources)
        {
            Transfer(from, to, pair.Key, pair.Value);
        }
    }

    public static void Transfer(ResourceStorage from, ResourceStorage to, Resource resource, int amount)
    {
        from.Remove(resource, amount);
        to.Add(resource, amount);
    }

    public int GetAmount(Resource resource)
    {
        if (content.ContainsKey(resource))
            return content[resource];

        return 0;
    }

    private int GetUsedSpace()
    {
        int result = 0;
        foreach(int amount in content.Values)
        {
            result += amount;
        }

        return result;
    }

    public void Add(Resource resource, int amount)
    {
        if(IntValueDictionaryUtility.AddAmount(content, resource, amount))
        {
            OnNewResourceAdded?.Invoke(resource, content[resource]);
        } else
        {
            OnResourceAdded?.Invoke(resource, content[resource]);
        }
    }

    public void Add(Dictionary<Resource, int> resources)
    {
        foreach(KeyValuePair<Resource, int> pair in resources)
        {
            Add(pair.Key, pair.Value);
        }
    }

    public void Remove(Resource resource, int amount)
    {
        if(IntValueDictionaryUtility.RemoveAmount(content, resource, amount))
        {
            OnResourceTotallyRemoved?.Invoke(resource, 0);
        } else
        {
            OnResourceRemoved?.Invoke(resource, content[resource]);
        }
    }

    public void Remove(Dictionary<Resource, int> resources)
    {
        foreach(KeyValuePair<Resource, int> pair in resources)
        {
            Remove(pair.Key, pair.Value);
        }
    }

    public bool CanWithdraw(Resource resource, int amount)
    {
        if (content.ContainsKey(resource) && content[resource] >= amount)
            return true;

        return false;
    }
}
