using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuildingSlot
{
    public Building building;
    public ConstructionSite constructionSite;

    public BuildingSlot(Building building, ConstructionSite constructionSite)
    {
        this.building = building;
        this.constructionSite = constructionSite;
    }

    public bool ConstructionInProgress { get => constructionSite != null; }
}

[RequireComponent(typeof(PointOfInterest))]
public class BuildingSlots : MonoBehaviour
{
    [NonReorderable]
    [SerializeField] private List<BuildingSlot> slots = new List<BuildingSlot>();

    private void Start()
    {
        SetupBuildings();
    }

    private void OnValidate()
    {
        SetupBuildings();
    }
    private void SetupBuildings()
    {
        PointOfInterest poi = GetComponent<PointOfInterest>();

        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].building)
                slots[i].building.Setup(poi);

            if (slots[i].constructionSite)
                slots[i].constructionSite.Setup(poi);

        }
    }
}
