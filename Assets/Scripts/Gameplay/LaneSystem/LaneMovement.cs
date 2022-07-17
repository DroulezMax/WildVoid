using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneMovement : MonoBehaviour
{
    [SerializeField] private Lane currentLane;
    [SerializeField] private PointOfInterest destination;

    [Header("Parameters")]
    [SerializeField] private float moveSpeed = 1;

    public Action<PointOfInterest> OnArrivedAtDestination;

    private bool isMoving = true;

    private void Update()
    {
        if(isMoving)
            MoveTowardsDestination();
    }

    public PointOfInterest GetOtherPOI(PointOfInterest poi)
    {
        if (poi == currentLane.PointA)
            return currentLane.PointB;
        if (poi == currentLane.PointB)
            return currentLane.PointA;

        Debug.LogError("POI passed in argument is not member of lane");
        return null;
    }

    public void GoTo(PointOfInterest destination)
    {
        this.destination = destination;
        StartMoving();
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void Stop()
    {
        isMoving = false;
    }

    private void MoveTowardsDestination()
    {
        transform.LookAt(destination.transform, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, moveSpeed * Time.deltaTime);

        if(transform.position == destination.transform.position)
        {
            Stop();
            OnArrivedAtDestination?.Invoke(destination);
        }
    }
}
