using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.Utility.Display
{
    public enum FloatRounding
    {
        FLOOR,
        CEIL,
        ROUND
    }

    public static class DisplayConventions
    {
        public static string DisplayInt(int value)
        {
            return value.ToString();
        }
        public static string DisplayGrade(float grade)
        {
            return Mathf.Round(grade * 100) + "%";
        }

        public static string DisplayFloatAsInt(float value, FloatRounding rounding)
        {
            switch (rounding)
            {
                case FloatRounding.FLOOR:
                    return Mathf.FloorToInt(value).ToString();

                case FloatRounding.CEIL:
                    return Mathf.CeilToInt(value).ToString();

                case FloatRounding.ROUND:
                    return Mathf.RoundToInt(value).ToString();
            }

            Debug.LogError("Error : Rounding no accounted for");
            return "";
        }

        public static string DisplayLaneName(Lane lane)
        {
            return lane.PointA.DisplayName + " - " + lane.PointB.DisplayName;
        }
    }
}

