using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    [CreateAssetMenu(fileName = "ResourceStorageAndPriceModuleFetcher", menuName = "UI/ModularUI/ModuleFetcher/ResourceStorageAndPrice")]
    public class ResourceStorageAndPriceModuleFetcher : UIModuleFetcher
    {
        public override GameObject FetchModule(GameObject target)
        {
            ResourceStorage storage = target.GetComponent<ResourceStorage>();
            Market market = target.GetComponent<Market>();

            if (!storage)
                return null;

            if (!market)
                return null;

            GameObject uiModule = Instantiate(module);

            uiModule.GetComponent<ResourceStorageAndPriceModule>().Init(storage, market);

            return uiModule;
        }
    }
}