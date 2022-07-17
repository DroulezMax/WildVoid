using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.Utility.UI
{
    public struct PositionedRect
    {
        public Vector2 center;
        public Vector2 size;

        public PositionedRect(Vector2 center, Vector2 size)
        {
            this.center = center;
            this.size = size;
        }

        //Unclamped Version?
        public static PositionedRect Lerp(PositionedRect start, PositionedRect end, float t)
        {
            return new PositionedRect(
                Vector2.Lerp(start.center, end.center, t),
                Vector2.Lerp(start.size, end.size, t)
            );
        }
    }

    public static class UIUtility
    {
        public static void PlaceInContainer(RectTransform target, RectTransform container)
        {
            target.SetParent(container);
            target.anchorMin = new Vector2(0, 0);
            target.anchorMax = new Vector2(1, 1);
            target.sizeDelta = new Vector2(0, 0);
            target.anchoredPosition = new Vector2(0, 0);
        }
    }
}

