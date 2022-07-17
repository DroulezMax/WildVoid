using MDZ.Utility;
using MDZ.Utility.Math;
using MDZ.Utility.UI;
using MDZ.WildVoid.UI.Widgets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MDZ.WildVoid.UI.MobileUI
{
    public class MobileProjector : MonoBehaviour
    {
        [SerializeField] private RectTransform displayEmplacement;
        [SerializeField] private RectTransform oldDisplayEmplacement;

        [Header("AnimatorReferences")]
        [SerializeField] private Animator animator;
        [SerializeField] private string swapDisplayAnimationName = "SwapDisplay";
        [SerializeField] private string expandAnimationName = "Expand";
        [SerializeField] private string retractAnimationName = "Retract";

        private RectTransform projectorTransform;

        private RectTransform oldTarget;
        private WidgetGroup oldTargetGroup;

        private RectTransform target;
        private WidgetGroup targetGroup;

        private Coroutine coroutine;

        private bool isStationary = false;

        private void Awake()
        {
            projectorTransform = transform as RectTransform;
        }
        void Start()
        {
        }

        void Update()
        {
            if (isStationary)
            {
                PlaceAtStationaryPosition();
            }
        }

        public void MoveTo(RectTransform target, WidgetGroup targetGroup, float duration, AnimationCurve curve)
        {
            if(coroutine != null)
            {
                RemoveOldTarget();
                StopCoroutine(coroutine);
            }

            oldTarget = this.target;
            oldTargetGroup = this.targetGroup;

            this.target = target;
            this.targetGroup = targetGroup;

            PlaceInEmplacement();

            if(oldTarget)
                oldTarget.SetParent(oldDisplayEmplacement);

            isStationary = false;

            //Start at time opposite of actual time to transition alphas smoothly?
            StartNormalizedAnimation(swapDisplayAnimationName, duration);
            coroutine = StartCoroutine(MoveToCor(duration, curve));
        }

        public void ExpandTo(RectTransform target, WidgetGroup targetGroup, Vector2 startPosition, float duration, AnimationCurve curve)
        {
            gameObject.SetActive(true);

            if(coroutine != null)
            {
                RemoveOldTarget();
                StopCoroutine(coroutine);
            }
            projectorTransform.position = startPosition;
            projectorTransform.sizeDelta = new Vector2(0, 0);

            this.target = target;
            this.targetGroup = targetGroup;

            PlaceInEmplacement();

            isStationary = false;

            StartNormalizedAnimation(expandAnimationName, duration);
            coroutine = StartCoroutine(MoveToCor(duration, curve));
        }


        public void Retract(float duration, AnimationCurve curve)
        {
            oldTarget = target;
            oldTargetGroup = targetGroup;

            if (oldTarget)
                oldTarget.SetParent(oldDisplayEmplacement);

            isStationary = false;

            StartNormalizedAnimation(retractAnimationName, duration);
            coroutine = StartCoroutine(RetractCor(duration, curve));
        }

        private void StartNormalizedAnimation(string animation, float duration)
        {
            animator.speed = 1 / duration;

            animator.Play(animation, -1, 0);
        }

        private IEnumerator MoveToCor(float duration, AnimationCurve curve)
        {
            float counter = 0;
            float previousCounter = 0;
            float progress;

            PositionedRect startRect;
            PositionedRect endRect;
            PositionedRect lerpedRect;

            while (counter < duration)
            {
                yield return null;

                previousCounter = counter;
                counter += Time.unscaledDeltaTime;

                progress = CurveUtility.GetRelativeProgress(curve, previousCounter / duration, counter / duration);

                startRect = new PositionedRect(projectorTransform.position, projectorTransform.sizeDelta);
                endRect = targetGroup.GetWidgetScreenRect(target);

                lerpedRect = PositionedRect.Lerp(startRect, endRect, progress);

                projectorTransform.position = lerpedRect.center;
                projectorTransform.sizeDelta = lerpedRect.size;
            }

            isStationary = true;
        }

        private IEnumerator RetractCor(float duration, AnimationCurve curve)
        {
            float counter = 0;
            float previousCounter = 0;
            float progress;

            while (counter < duration)
            {
                yield return null;

                previousCounter = counter;
                counter += Time.unscaledDeltaTime;

                progress = CurveUtility.GetRelativeProgress(curve, previousCounter / duration, counter / duration);

                projectorTransform.sizeDelta = Vector2.Lerp(projectorTransform.sizeDelta, Vector2.zero, progress);
            }

            gameObject.SetActive(false);
        }

        private void RemoveOldTarget()
        {
            if (oldTarget && oldTargetGroup)
            {
                oldTargetGroup.Retrieve(oldTarget);

                oldTarget = null;
                oldTargetGroup = null;
            }
        }

        private void PlaceInEmplacement()
        {
            UIUtility.PlaceInContainer(target, displayEmplacement);

            target.gameObject.SetActive(true);
        }
        private void PlaceAtStationaryPosition()
        {
            PositionedRect rect = targetGroup.GetWidgetScreenRect(target);

            projectorTransform.position = rect.center;
            projectorTransform.sizeDelta = rect.size;
        }
    }
}
