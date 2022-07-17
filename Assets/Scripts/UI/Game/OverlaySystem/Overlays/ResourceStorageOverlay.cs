using MDZ.WildVoid.UI.Widgets;
using ModularUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorageOverlay : Overlay
{
    [SerializeField] private WidgetGroup widgetGroup;
    [SerializeField] private ModularPanel resourceStorageModularPanelPrefab;

    public override void Init()
    {
        base.Init();

        PointOfInterest[] pois = FindObjectsOfType<PointOfInterest>();

        ModularPanel panel;

        foreach (PointOfInterest poi in pois)
        {
            panel = Instantiate(resourceStorageModularPanelPrefab, transform);

            panel.Init(poi.gameObject);

            widgetGroup.Add(poi.gameObject, panel.transform as RectTransform);
        }
    }

    public override void Open()
    {
        base.Open();

        widgetGroup.ShowAllWidgets();
    }

    public override void Close()
    {
        base.Close();

        widgetGroup.HideAllWidgets();
    }
}
