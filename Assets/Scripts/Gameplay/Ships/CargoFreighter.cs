using MDZ.WildVoid.Utility.Economy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoFreighter : MonoBehaviour
{
    [SerializeField] private LaneMovement laneMovement;
    [SerializeField] private ResourceStorage storage;
    // Start is called before the first frame update
    void Start()
    {
        laneMovement.OnArrivedAtDestination += Dock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Dock(PointOfInterest poi)
    {
        PointOfInterest nextDestination = laneMovement.GetOtherPOI(poi);

        Market dockedMarket = poi.GetComponent<Market>();
        Market nextMarket = nextDestination.GetComponent<Market>();

        dockedMarket.Sell(new Dictionary<Resource, int>(storage.Content), storage);

        Dictionary<Resource, int> bestTrade = MarketUtility.BestTrade(dockedMarket, nextMarket, storage.AvailableSpace);

        poi.GetComponent<Market>().Buy(bestTrade, storage);

        laneMovement.GoTo(nextDestination);
    }
}
