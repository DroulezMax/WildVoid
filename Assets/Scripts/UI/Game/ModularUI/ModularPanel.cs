using MDZ.WildVoid.UI.ModularUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularUI
{
    public class ModularPanel : MonoBehaviour
    {
        [SerializeField] private UIModuleFetcher[] modules;

        public void Init(GameObject target)
        {
            GameObject module;

            for (int i = 0; i < modules.Length; i++)
            {
                module = modules[i].FetchModule(target);

                if(module)
                {
                    module.transform.SetParent(transform);
                }
            }
        }
    }
}

