using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MDZ.WildVoid.Utility.Economy
{
    public static class MarketUtility
    {
        private struct BuySellPrice
        {
            public int buyPrice;
            public int sellPrice;

            public BuySellPrice(int buyPrice, int sellPrice)
            {
                this.buyPrice = buyPrice;
                this.sellPrice = sellPrice;
            }
        }

        public static int GetBuyingCost(Market buyFrom, Resource resource, int amount)
        {
            int value = 0;
            int startingStock = buyFrom.StorageContent.ContainsKey(resource) ? buyFrom.StorageContent[resource] : 0;

            for (int i = 0; i < amount - 1; i++)
            {
                value += buyFrom.GetResourcePrice(resource, startingStock - i);
            }

            return value;
        }

        public static int GetSellingValue(Market sellTo, Resource resource, int amount)
        {
            int value = 0;
            int startingStock = sellTo.StorageContent.ContainsKey(resource) ? sellTo.StorageContent[resource] : 0;

            for (int i = 0; i < amount - 1; i++)
            {
                value += sellTo.GetResourcePrice(resource, startingStock + i);
            }

            return value;
        }

        //Could be improved by calculating price curves for all resources and ordering list of all price curve keys
        public static Dictionary<Resource, int> BestTrade(Market buyFrom, Market sellTo, int maxUnits, int minProfitPerUnit = 1)
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();

            Dictionary<Resource, int> buyFromProjection = buyFrom.StorageContent;
            Dictionary<Resource, int> sellToProjection = sellTo.StorageContent;

            Dictionary<Resource, int> profitsTable = CreateProfitsTable(buyFrom, sellTo);

            KeyValuePair<Resource, int> best;

            for (int i = 0; i < maxUnits - 1; i++)
            {
                if (buyFromProjection.Keys.Count == 0)
                    return result;

                best = profitsTable.OrderByDescending(t => t.Value).First();

                if (best.Value < minProfitPerUnit)
                {
                    return result;
                }

                IntValueDictionaryUtility.AddAmount(result, best.Key, 1);

                MakeTheoricalPurchase(best.Key, buyFrom, sellTo, buyFromProjection, sellToProjection, profitsTable);
            }

            return result;
        }

        private static void MakeTheoricalPurchase(Resource resource, Market buyFrom, Market sellTo, Dictionary<Resource, int> buyFromProjection, Dictionary<Resource, int> sellToProjection, Dictionary<Resource, int> profitsTable)
        {
            IntValueDictionaryUtility.AddAmount(sellToProjection, resource, 1);

            if (IntValueDictionaryUtility.RemoveAmount(buyFromProjection, resource, 1))
            {
                profitsTable.Remove(resource);
            }
            else
            {
                profitsTable[resource] = sellTo.GetResourcePrice(resource, sellToProjection[resource]) - buyFrom.GetResourcePrice(resource, buyFromProjection[resource]);
            }
        }

        private static Dictionary<Resource, int> CreateProfitsTable(Market buyFrom, Market sellTo)
        {
            Dictionary<Resource, int> profitsTable = new Dictionary<Resource, int>();

            foreach (Resource resource in buyFrom.GetAllResourceTypes())
            {
                profitsTable.Add(resource, sellTo.GetResourcePrice(resource) - buyFrom.GetResourcePrice(resource));
            }

            return profitsTable;
        }
    }
}