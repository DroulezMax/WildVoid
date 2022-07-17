using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntValueDictionaryUtility
{
    //Adds value to key if it exists, creates key if not. Returns true if key was created
    public static bool AddAmount<T>(Dictionary<T, int> dictionary, T key, int amount)
    {
        if(dictionary.ContainsKey(key))
        {
            dictionary[key] += amount;
            return false;
        } else
        {
            dictionary.Add(key, amount);
            return true;
        }
    }

    //Removes value to key if it exists,creates key if not. Returns true if key was removed
    //amountForRemoval will remove key if value of key is <= to it
    public static bool RemoveAmount<T>(Dictionary<T, int> dictionary, T key, int amount, int amountForRemoval = 0)
    {
        if(dictionary.ContainsKey(key))
        {
            if((dictionary[key] -= amount) > amountForRemoval)
            {
                return false;
            }

            dictionary.Remove(key);
        }

        return true;
    }

    public static int TotalAmount<T>(Dictionary<T, int> dictionary)
    {
        int result = 0;

        foreach(int amount in dictionary.Values)
        {
            result += amount;
        }

        return result;
    }
}
