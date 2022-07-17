using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringStringDictionary))]

[CustomPropertyDrawer(typeof(ResourceIntDictionary))]
[CustomPropertyDrawer(typeof(ResourceFloatDictionary))]
[CustomPropertyDrawer(typeof(ResourceDepositResourceHarvesterDictionary))]
[CustomPropertyDrawer(typeof(OverlayTypeOverlayDictionary))]
[CustomPropertyDrawer(typeof(ResourceMarketPriceDictionary))]
[CustomPropertyDrawer(typeof(ResourceDepositBuildingSlotDictionary))]

public class AnySerializableDictionaryPropertyDrawer :
SerializableDictionaryPropertyDrawer
{ }
