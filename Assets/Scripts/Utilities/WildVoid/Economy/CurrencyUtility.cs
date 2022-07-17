using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.Utility.Economy
{
    public static class CurrencyUtility
    {
        private static CurrencyParameters currencyParameters = null;

        public static Resource GlobalCurrency
        {
            get
            {
                if (!currencyParameters)
                {
                    currencyParameters = Object.FindObjectOfType<CurrencyParameters>();

                    if (!currencyParameters)
                    {
                        Debug.LogError("CurrencyUtilities failed to find a CurrencyParameters script in scene to get global currency");
                        return null;
                    }
                }

                return currencyParameters.GlobalCurrency;
            }
        }
    }
}