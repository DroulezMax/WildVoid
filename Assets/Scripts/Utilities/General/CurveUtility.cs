using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.Utility.Math
{
    public static class CurveUtility
    {
        public static float GetRelativeProgress(AnimationCurve curve, float startTime, float endTime)
        {
            if (endTime >= 1)
                return 1;
            return (curve.Evaluate(endTime) - curve.Evaluate(startTime)) / Mathf.InverseLerp(curve.keys[curve.keys.Length - 1].value, curve.keys[0].value, curve.Evaluate(startTime));
        }
    }
}