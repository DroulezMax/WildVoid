using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MarketPrice", menuName = "Gameplay/Market/MarketPrice")]
public class MarketPrice : ScriptableObject
{
    [SerializeField] private int basePrice = 10;
    [SerializeField] private int minPrice = 1;
    [SerializeField] private int maxPrice = 40;
    [SerializeField] private float maxSDRatio = 4;
    [SerializeField] private float equilibriumWindow = 0.2f;

    [SerializeField] private AnimationCurve highRatioCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private AnimationCurve lowRatioCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField] private AnimationCurve priceCurveVisualization = AnimationCurve.Linear(0, 1, 2, 0);

    //SDRatio is the measure of supply compared to demand, 1 being the value where supply = demand. 
    //if SDRatio = 0, there is no supply, if equal to 0.5 the supply is half the demand
    //if SDRatio = 2, there is twice as much supply as there is demand
    public int GetPrice(float sdRatio)
    {
        if(sdRatio < 1 - equilibriumWindow)
        {
            return Mathf.RoundToInt(Mathf.Lerp(basePrice, maxPrice, lowRatioCurve.Evaluate(Mathf.InverseLerp(0, 1 - equilibriumWindow, sdRatio))));
        } 

        if(sdRatio > 1 + equilibriumWindow)
        {
            return Mathf.RoundToInt(Mathf.Lerp(minPrice, basePrice, highRatioCurve.Evaluate(Mathf.InverseLerp(1 + equilibriumWindow, maxSDRatio, sdRatio))));
        }
        return basePrice;
    }

    private void OnValidate()
    {
        UpdateVisualization();
    }

    private void UpdateVisualization()
    {
        List<Keyframe> finalVisualization = new List<Keyframe>();

        finalVisualization.AddRange(GetLowRatioVisualization());
        finalVisualization.AddRange(GetHighRatioVisualization());

        priceCurveVisualization.keys = finalVisualization.ToArray();
    }

    //TODO : correct keyframes tangents. 

    private Keyframe[] GetLowRatioVisualization()
    {
        Keyframe[] keys = lowRatioCurve.keys;
        float windowRatio = 1 - equilibriumWindow;
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].value = Mathf.Lerp(basePrice, maxPrice, lowRatioCurve.Evaluate(keys[i].time));
            keys[i].time *= windowRatio;
        }

        return keys;
    }

    private Keyframe[] GetHighRatioVisualization()
    {
        Keyframe[] keys = highRatioCurve.keys;
        float windowRatio = maxSDRatio - (1 + equilibriumWindow);
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].value = Mathf.Lerp(minPrice, basePrice, highRatioCurve.Evaluate(keys[i].time));
            keys[i].time = 1 + equilibriumWindow + keys[i].time * windowRatio;
        }

        return keys;
    }
}
