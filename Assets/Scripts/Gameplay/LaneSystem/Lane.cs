using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private PointOfInterest pointA;
    [SerializeField] private PointOfInterest pointB;

    public PointOfInterest PointA { get => pointA; }
    public PointOfInterest PointB { get => pointB; }

    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisible(bool visible)
    {
        lineRenderer.enabled = visible;
    }

    private void OnValidate()
    {
        if(pointA && pointB)
        {
            transform.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, 0.5f);
            if (lineRenderer)
                lineRenderer.SetPositions(new Vector3[] { pointA.transform.position, pointB.transform.position });
        }
    }
}
