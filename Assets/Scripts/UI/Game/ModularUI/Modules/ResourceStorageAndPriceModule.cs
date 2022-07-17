using MDZ.WildVoid.Utility.Economy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    public class ResourceStorageAndPriceModule : MonoBehaviour
    {
        [SerializeField] private ResourceAmountAndPriceBlock resourceAmountAndPriceBlockPrefab;

        private ResourceStorage storage;
        private Market market;
        private Dictionary<Resource, ResourceAmountAndPriceBlock> resourceAmountBlocks = new Dictionary<Resource, ResourceAmountAndPriceBlock>();

        void Start()
        {

        }

        void Update()
        {
            UpdateDisplay();
        }

        public void Init(ResourceStorage resourceStorage, Market market)
        {
            storage = resourceStorage;
            this.market = market;

            storage.OnNewResourceAdded += NewResourceAdded;
            storage.OnResourceTotallyRemoved += ResourceTotallyRemoved;

            foreach (Resource resource in resourceStorage.Content.Keys)
            {
                AddBlock(resource, resourceStorage.Content[resource]);
            }
        }

        public void NewResourceAdded(Resource resource, int amount)
        {
            AddBlock(resource, amount);
        }

        public void ResourceTotallyRemoved(Resource resource, int amount)
        {
            RemoveBlock(resource);
        }

        private void UpdateDisplay()
        {
            foreach (KeyValuePair<Resource, ResourceAmountAndPriceBlock> pair in resourceAmountBlocks)
            {
                pair.Value.UpdateDisplay(storage.Content[pair.Key], market.GetResourcePrice(pair.Key));
            }
        }

        private void AddBlock(Resource resource, int amount)
        {
            if (resourceAmountBlocks.ContainsKey(resource))
                return;

            ResourceAmountAndPriceBlock block = Instantiate(resourceAmountAndPriceBlockPrefab);
            block.transform.SetParent(transform);

            block.Init(resource, amount, CurrencyUtility.GlobalCurrency, market.GetResourcePrice(resource));

            resourceAmountBlocks.Add(resource, block);
        }

        private void RemoveBlock(Resource resource)
        {
            if (!resourceAmountBlocks.ContainsKey(resource))
                return;

            ResourceAmountAndPriceBlock block = resourceAmountBlocks[resource];

            resourceAmountBlocks.Remove(resource);

            Destroy(block.gameObject);

        }
    }
}