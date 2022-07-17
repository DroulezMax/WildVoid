using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    [CreateAssetMenu(fileName = "LaneNameModuleFetcher", menuName = "UI/ModularUI/ModuleFetcher/LaneName")]
    public class LaneNameModuleFetcher : UIModuleFetcher
    {
        public override GameObject FetchModule(GameObject target)
        {
            Lane lane = target.GetComponent<Lane>();

            if (!lane)
                return null;

            GameObject uiModule = Instantiate(module);

            uiModule.GetComponent<LaneNameModule>().Init(lane);

            return uiModule;
        }
    }
}