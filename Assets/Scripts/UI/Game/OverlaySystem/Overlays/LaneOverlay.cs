using MDZ.WildVoid.UI.Widgets;
using ModularUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneOverlay : Overlay
{
    [SerializeField] private WidgetGroup widgetGroup;
    [SerializeField] private ModularPanel laneModularPanelPrefab;

    private List<Lane> lanes = new List<Lane>();

    public override void Init()
    {
        base.Init();

        Lane[] lanes = FindObjectsOfType<Lane>();

        ModularPanel panel;

        foreach(Lane lane in lanes)
        {
            panel = Instantiate(laneModularPanelPrefab);

            panel.Init(lane.gameObject);

            widgetGroup.Add(lane.gameObject, panel.transform as RectTransform);
        }

        this.lanes = new List<Lane>(lanes);
    }

    public override void Open()
    {
        base.Open();

        widgetGroup.ShowAllWidgets();

        foreach(Lane lane in lanes)
        {
            lane.SetVisible(true);
        }
    }

    public override void Close()
    {
        base.Close();

        widgetGroup.HideAllWidgets();

        foreach (Lane lane in lanes)
        {
            lane.SetVisible(false);
        }
    }
}
