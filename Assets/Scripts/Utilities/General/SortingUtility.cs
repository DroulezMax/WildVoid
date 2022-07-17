using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.Utility.Math
{
    public static class SortingUtility
    {
        public static RectTransform GetClosest(Transform reference, List<RectTransform> competitors)
        {
            RectTransform closest = null;
            float bestDistance = Mathf.Infinity;
            float foundDistance;

            for (int i = 0; i < competitors.Count; i++)
            {
                foundDistance = Vector3.Distance(reference.position, competitors[i].position);
                if(foundDistance < bestDistance)
                {
                    closest = competitors[i];
                    bestDistance = foundDistance;
                }
            }

            return closest;
        }
    }
}