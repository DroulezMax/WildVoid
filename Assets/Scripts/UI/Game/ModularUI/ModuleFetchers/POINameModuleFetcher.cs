using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    [CreateAssetMenu(fileName = "POINameModuleFetcher", menuName = "UI/ModularUI/ModuleFetcher/POIName")]
    public class POINameModuleFetcher : UIModuleFetcher
    {
        public override GameObject FetchModule(GameObject target)
        {
            PointOfInterest poi = target.GetComponent<PointOfInterest>();

            if (!poi)
                return null;

            GameObject uiModule = Instantiate(module);

            uiModule.GetComponent<POINameModule>().Init(poi);

            return uiModule;
        }
    }
}