using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverlayType
{
    BUILDING,
    RESOURCE_STORAGE,
    LANE
}
public class OverlaysManager : MonoBehaviour
{
    [SerializeField] private OverlayTypeOverlayDictionary overlays;

    private Overlay currentOverlay = null;

    public Action<Overlay> OnOverlayOpened;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Overlay overlay in overlays.Values)
        {
            overlay.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OverlaySelected(OverlayType type)
    {
        if(currentOverlay != null)
        {
            currentOverlay.Close();

            if(overlays[type] == currentOverlay)
            {
                currentOverlay = null;
                return;
            }
        }

        currentOverlay = overlays[type];
        currentOverlay.Open();

        OnOverlayOpened?.Invoke(currentOverlay);
    }

    public void OnOverlayBuilding()
    {
        OverlaySelected(OverlayType.BUILDING);
    }

    public void OnOverlayResourceStorage()
    {
        OverlaySelected(OverlayType.RESOURCE_STORAGE);
    }

    public void OnOverlayLane()
    {
        OverlaySelected(OverlayType.LANE);
    }
}
