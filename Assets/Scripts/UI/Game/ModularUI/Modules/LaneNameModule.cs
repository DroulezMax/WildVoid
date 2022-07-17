using MDZ.WildVoid.Utility.Display;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MDZ.WildVoid.UI.ModularUI
{
    public class LaneNameModule : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;

        private Lane lane;

        public void Init(Lane lane)
        {
            this.lane = lane;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            nameText.text = DisplayConventions.DisplayLaneName(lane);
        }
    }
}