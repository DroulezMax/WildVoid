using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceIntDictionary : SerializableDictionary<Resource, int> { }

[Serializable]
public class ResourceFloatDictionary : SerializableDictionary<Resource, float> { }

[Serializable]
public class ResourceDepositResourceHarvesterDictionary : SerializableDictionary<ResourceDeposit, ResourceHarvester> { }

[Serializable]
public class OverlayTypeOverlayDictionary : SerializableDictionary<OverlayType, Overlay> { }

[Serializable]
public class ResourceMarketPriceDictionary : SerializableDictionary<Resource, MarketPrice> { }

[Serializable]
public class ResourceDepositBuildingSlotDictionary : SerializableDictionary<ResourceDeposit, BuildingSlot> { }