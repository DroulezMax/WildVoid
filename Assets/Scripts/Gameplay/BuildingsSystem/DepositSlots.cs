using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointOfInterest))]
[RequireComponent(typeof(ResourceStorage))]
public class DepositSlots : MonoBehaviour
{
    [SerializeField] private ResourceDepositBuildingSlotDictionary slots;

    public ResourceDepositBuildingSlotDictionary Slots { get => slots; }

    private void Start()
    {
        SetupHarvesters();
        SetupConstructionEnds();
    }

    private void OnValidate()
    {
        SetupHarvesters();
    }

    private void SetupHarvesters()
    {
        ResourceStorage storage = GetComponent<ResourceStorage>();

        foreach (KeyValuePair<ResourceDeposit, BuildingSlot> pair in slots)
        {
            if(pair.Value.building)
                ((ResourceHarvester)pair.Value.building).Setup(pair.Key, storage);

            if (pair.Value.constructionSite)
            {
                pair.Value.constructionSite.Setup(GetComponent<PointOfInterest>());
            }
        }
    }

    private void SetupConstructionEnds()
    {
        foreach(BuildingSlot slot in slots.Values)
        {
            if (slot.constructionSite)
                slot.constructionSite.OnConstructionFinished += ConstructionFinished;
        }
    }

    private void ConstructionFinished(ConstructionSite site)
    {
        foreach(BuildingSlot slot in slots.Values)
        {
            if(slot.constructionSite == site)
            {
                slot.building.gameObject.SetActive(true);
                site.OnConstructionFinished -= ConstructionFinished;
                Destroy(site.gameObject);
            }
        }
    }
}
