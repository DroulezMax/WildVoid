using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    public abstract class UIModuleFetcher : ScriptableObject
    {
        [SerializeField] protected GameObject module;
        public abstract GameObject FetchModule(GameObject target);
    }
}