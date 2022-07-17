using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    public class POINameModule : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;

        private PointOfInterest pointOfInterest;

        public void Init(PointOfInterest poi)
        {
            pointOfInterest = poi;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            nameText.text = pointOfInterest.DisplayName;
        }
    }
}