using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDZ.WildVoid.UI.Widgets
{
    public class WidgetGroupController : MonoBehaviour
    {
        protected List<WidgetGroup> controlledGroups = new List<WidgetGroup>();

        protected Dictionary<RectTransform, WidgetGroup> shownWidgets = new Dictionary<RectTransform, WidgetGroup>();

        protected virtual void Start()
        {
            FindWidgetGroups();
        }

        protected virtual void FindWidgetGroups()
        {
            controlledGroups.AddRange(GetComponentsInChildren<WidgetGroup>());

            foreach (WidgetGroup group in controlledGroups)
            {
                group.Controller = this;
            }
        }

        public virtual void GroupShown(WidgetGroup group, List<RectTransform> widgets)
        {
            foreach (RectTransform widget in widgets)
            {
                shownWidgets.Add(widget, group);
            }
        }

        public virtual void GroupHidden(WidgetGroup group, List<RectTransform> widgets)
        {
            foreach (RectTransform widget in widgets)
            {
                shownWidgets.Remove(widget);
            }
        }
    }
}