using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    [CreateAssetMenu(fileName = "DepositSlotsModuleFetcher", menuName = "UI/ModularUI/ModuleFetcher/DepositSlots")]
    public class DepositSlotsModuleFetcher : UIModuleFetcher
    {
        public override GameObject FetchModule(GameObject target)
        {
            DepositSlots depositSlots = target.GetComponent<DepositSlots>();

            if (!depositSlots)
                return null;

            GameObject uiModule = Instantiate(module);

            uiModule.GetComponent<DepositSlotsModule>().Init(depositSlots);

            return uiModule;
        }
    }
}