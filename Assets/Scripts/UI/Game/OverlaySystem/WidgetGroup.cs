using MDZ.Utility.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MDZ.WildVoid.UI.Widgets
{
    public class WidgetGroup : MonoBehaviour
    {
        //TODO : allow for different pivot and 8-directions positions (corners)
        [Serializable]
        private enum WidgetPosition
        {
            TOP,
            RIGHT,
            BOTTOM,
            LEFT,
            CENTER
        }

        [SerializeField] private WidgetPosition widgetPosition = WidgetPosition.TOP;

        private Dictionary<RectTransform, GameObject> widgetTargets = new Dictionary<RectTransform, GameObject>();

        public List<RectTransform> Widgets { get { return new List<RectTransform>(widgetTargets.Keys); } }

        private CameraManager cameraManager;

        private WidgetGroupController controller;

        public WidgetGroupController Controller { set => controller = value; }

        void Start()
        {
            if (!(cameraManager = FindObjectOfType<CameraManager>()))
                Debug.LogError("WidgetGroup needs a CameraManager in scene to function");

        }

        // Update is called once per frame
        void Update()
        {
            if (!controller)
                ReplaceWidgets();
        }

        public void Add(GameObject target, RectTransform widget)
        {
            widgetTargets.Add(widget, target);
            //temporary
            widget.gameObject.SetActive(false);
            //Change pivots
        }

        public void ShowAllWidgets()
        {
            if (controller)
            {
                controller.GroupShown(this, Widgets);
            }
            else
            {
                foreach (RectTransform widget in widgetTargets.Keys)
                {
                    widget.gameObject.SetActive(true);
                    enabled = true;
                }
            }
        }

        public void HideAllWidgets()
        {
            if (controller)
            {
                controller.GroupHidden(this, Widgets);
            }
            else
            {
                foreach (RectTransform widget in widgetTargets.Keys)
                {
                    widget.gameObject.SetActive(false);
                    enabled = false;
                }
            }

        }

        public void LendWidget(RectTransform widget)
        {

        }

        public void Retrieve(RectTransform widget)
        {
            widget.SetParent(transform);
            widget.gameObject.SetActive(false);
        }

        private void ReplaceWidgets()
        {
            Camera camera = cameraManager.CurrentCamera;

            foreach (KeyValuePair<RectTransform, GameObject> pair in widgetTargets)
            {
                pair.Key.position = GetScreenPivotPoint(pair.Value);
            }
        }

        public PositionedRect GetWidgetScreenRect(RectTransform widget)
        {
            Vector2 size = new Vector2(LayoutUtility.GetMinWidth(widget), LayoutUtility.GetMinHeight(widget));
            
            return new PositionedRect(
                 GetCenterFromPivot(GetScreenPivotPoint(widgetTargets[widget]), size),
                 size);
        }

        //Find a better way to get renderer / radius of POI or target
        public Vector2 GetScreenPivotPoint(GameObject target)
        {
            return cameraManager.CurrentCamera.WorldToScreenPoint(target.transform.position + cameraManager.CurrentCamera.transform.rotation * GetPositionOffset(target.GetComponentInChildren<Renderer>()));
        }

        private Vector3 GetPositionOffset(Renderer renderer)
        {
            switch (widgetPosition)
            {
                case WidgetPosition.TOP:
                    return new Vector3(0, renderer.bounds.extents.y * 2, 0);

                case WidgetPosition.RIGHT:
                    return new Vector3(renderer.bounds.extents.x * 2, 0, 0);

                case WidgetPosition.BOTTOM:
                    return new Vector3(0, -renderer.bounds.extents.y * 2, 0);

                case WidgetPosition.LEFT:
                    return new Vector3(-renderer.bounds.extents.x * 2, 0, 0);

                case WidgetPosition.CENTER:
                    return Vector3.zero;

                default:
                    return Vector3.zero;
            }
        }

        private Vector2 GetCenterFromPivot(Vector2 pivot, Vector2 size)
        {
            switch (widgetPosition)
            {
                case WidgetPosition.TOP:
                    return pivot + new Vector2(0, size.y / 2);

                case WidgetPosition.RIGHT:
                    return pivot + new Vector2(size.x / 2, 0);

                case WidgetPosition.BOTTOM:
                    return pivot + new Vector2(0, -size.y / 2);

                case WidgetPosition.LEFT:
                    return pivot + new Vector2(-size.x / 2, 0);

                case WidgetPosition.CENTER:
                    return pivot;

                default:
                    return pivot;
            }
        }
    }
}