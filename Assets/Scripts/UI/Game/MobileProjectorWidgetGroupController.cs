using MDZ.Utility.Math;
using MDZ.WildVoid.UI.MobileUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.Widgets
{
    public class MobileProjectorWidgetGroupController : WidgetGroupController
    {
        [SerializeField] private float projectorMoveDuration = 1;
        [SerializeField] private AnimationCurve projectorMoveCurve;

        [Header("References")]
        [SerializeField] private MobileProjector mobileProjectorPrefab;

        private List<MobileProjector> projectorPool = new List<MobileProjector>();

        private Dictionary<RectTransform, MobileProjector> shownTargets = new Dictionary<RectTransform, MobileProjector>();

        private List<RectTransform> justAssignedWidgets = new List<RectTransform>();
        private List<MobileProjector> justUnassignedProjectors = new List<MobileProjector>();

        // Update is called once per frame
        void Update()
        {
            //If more projectors, check distance to widgets, if more widgets check distance to projectors
            int matches = Mathf.Min(justAssignedWidgets.Count, justUnassignedProjectors.Count);
            RectTransform foundWidget;

            for (int i = 0; i < matches; i++)
            {
                foundWidget = SortingUtility.GetClosest(justUnassignedProjectors[justUnassignedProjectors.Count - 1].transform, justAssignedWidgets);
                AssignProjector(foundWidget, justUnassignedProjectors[justUnassignedProjectors.Count - 1], false);
                justAssignedWidgets.Remove(foundWidget);
                justUnassignedProjectors.RemoveAt(justUnassignedProjectors.Count - 1);
            }

            for (int i = justAssignedWidgets.Count - 1; i >= 0; i--)
            {
                if (projectorPool.Count > 0)
                {
                    AssignProjector(justAssignedWidgets[i], projectorPool[0], true);
                    projectorPool.RemoveAt(0);
                }
                else
                {
                    AssignProjector(justAssignedWidgets[i], CreateProjector(), true);
                }

                justAssignedWidgets.RemoveAt(i);
            }

            for (int i = justUnassignedProjectors.Count - 1; i >= 0; i--)
            {
                justUnassignedProjectors[i].Retract(projectorMoveDuration, projectorMoveCurve);
                projectorPool.Add(justUnassignedProjectors[i]);
                justUnassignedProjectors.RemoveAt(i);
            }
        }

        private void LateUpdate()
        {
            //Putting things in the lateupdate causes canvas group of displayemplacement to show at start
        }

        public override void GroupShown(WidgetGroup group, List<RectTransform> widgets)
        {
            base.GroupShown(group, widgets);

            foreach(RectTransform widget in widgets)
            {
                PutWidgetInQueue(widget);
            }
        }

        public override void GroupHidden(WidgetGroup group, List<RectTransform> widgets)
        {
            base.GroupHidden(group, widgets);

            foreach(RectTransform widget in widgets)
            {
                PutProjectorInQueue(shownTargets[widget]);
                shownTargets.Remove(widget);
            }
        }

        private void PutWidgetInQueue(RectTransform widget)
        {
            justAssignedWidgets.Add(widget);
        }

        private void PutProjectorInQueue(MobileProjector projector)
        {
            justUnassignedProjectors.Add(projector);
        }

        private void AssignProjector(RectTransform widget, MobileProjector projector, bool expand)
        {
            shownTargets.Add(widget, projector);
            if(expand)
            {
                projector.ExpandTo(widget, shownWidgets[widget], shownWidgets[widget].GetWidgetScreenRect(widget).center, projectorMoveDuration, projectorMoveCurve);
            } else
            {
                projector.MoveTo(widget, shownWidgets[widget], projectorMoveDuration, projectorMoveCurve);
            }
        }

        private MobileProjector CreateProjector()
        {
            return Instantiate(mobileProjectorPrefab, transform);
        }
    }
}