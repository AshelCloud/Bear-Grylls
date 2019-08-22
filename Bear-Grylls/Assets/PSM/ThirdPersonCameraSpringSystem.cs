using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    [RequireComponent(typeof(ThirdPersonCamera))]
    public class ThirdPersonCameraSpringSystem : MonoBehaviour
    {
        public float DefaultDistance { get { return defaultDistance; } set { defaultDistance = value; } }

        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Header("- Required Setting -")]
        [SerializeField] string targetLayerName;

        [Header("Distance")]
        [Header("- Option -")]
        [SerializeField] float defaultDistance = 40f;
        [SerializeField] bool useMinMaxCameraDistance = false;
        [ConditionalHide("useMinMaxCameraDistance", true)]
        [SerializeField] float minCameraDistance = 1f;
        [ConditionalHide("useMinMaxCameraDistance", true)]
        [SerializeField] float maxCameraDistance = 70f;

        [Header("Zoom")]
        [SerializeField] float zoomSpeed = 2f;
        [SerializeField] float springSpeed = 2f;

        [Header("Debug")]
        [SerializeField] bool drawCapsuleForDebug = true;

        private ThirdPersonCamera thirdPersonCamera;
        private Transform parentOffsetTransform;
        private Transform target;

        private const float radius = 0.2f;
        private const float size = 0.25f;

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void UpdateSpring()
        {
            //target의 키를 고려한 좌표 생성
            Vector3 targetPos = target.position;
            targetPos += target.up.normalized * thirdPersonCamera.PosCorrection.y;


            //두개의 캡슐 캐스트 사용
            int layerMask = (-1) - ((1 << LayerMask.NameToLayer(targetLayerName)));

            Collider[] smallCapsuleOverlapedColl = Physics.OverlapCapsule(transform.position, targetPos, radius, layerMask);

            Vector3 toCameraNomal = (transform.position - targetPos).normalized;
            Collider[] bigCapsuleOverlapedColl = Physics.OverlapCapsule(transform.position + toCameraNomal * size, targetPos, radius, layerMask);


            //캡슐 충돌 상태에 따라 늘리고 줄이기
            Vector3 parentOffsetScale = parentOffsetTransform.localScale;
            if (1 <= smallCapsuleOverlapedColl.Length && 1 <= bigCapsuleOverlapedColl.Length)//두 캡슐에 충돌이 있어 카메라 가 줄어들어야 하는 상황
            {
                Vector3 shrinkedParentOffsetScale = parentOffsetScale;
                shrinkedParentOffsetScale.z = 1;

                parentOffsetTransform.localScale = Vector3.Lerp(parentOffsetScale, shrinkedParentOffsetScale, Time.deltaTime * springSpeed);


                //디버그용 캡슐 그리기
                if (drawCapsuleForDebug)
                {
                    DebugExtension.DebugCapsule(transform.position, targetPos, Color.yellow, radius);
                    DebugExtension.DebugCapsule(transform.position + toCameraNomal * size, targetPos, Color.yellow, radius);
                }
            }
            else if ((1 <= smallCapsuleOverlapedColl.Length) != true &&
                1 <= bigCapsuleOverlapedColl.Length)//큰 캡슐에만 충돌이 있어 카메라가 늘지도 줄지도 않아야 하는 상황
            {
                //디버그용 캡슐 그리기
                if (drawCapsuleForDebug)
                {
                    DebugExtension.DebugCapsule(transform.position, targetPos, Color.red, radius);
                    DebugExtension.DebugCapsule(transform.position + toCameraNomal * size, targetPos, Color.yellow, radius);
                }
            }
            else if ((1 <= smallCapsuleOverlapedColl.Length) != true &&
                (1 <= bigCapsuleOverlapedColl.Length) != true)//두 캡슐에 충돌이 없어 카메라가 늘어나야하는 상황
            {
                Vector3 defaultParentOffsetScale = parentOffsetScale;
                defaultParentOffsetScale.z = -defaultDistance;

                parentOffsetTransform.localScale = Vector3.Lerp(parentOffsetScale, defaultParentOffsetScale, Time.deltaTime * springSpeed);

                //디버그용 캡슐 그리기
                if (drawCapsuleForDebug)
                {
                    DebugExtension.DebugCapsule(transform.position, targetPos, Color.red, radius);
                    DebugExtension.DebugCapsule(transform.position + toCameraNomal * size, targetPos, Color.blue, radius);
                }
            }
        }
        private void DistanceModulate()
        {
            if (useMinMaxCameraDistance != true)
                return;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (0 < scroll && minCameraDistance <= defaultDistance)//휠업 시 줌을 댕겨야함
            {
                defaultDistance -= Time.deltaTime * zoomSpeed;
                if (defaultDistance < minCameraDistance)
                    defaultDistance = minCameraDistance;
            }
            else if (scroll < 0 && defaultDistance <= maxCameraDistance)//휠 다운 시 줌을 밀어야함
            {
                defaultDistance += Time.deltaTime * zoomSpeed;
                if (maxCameraDistance < defaultDistance)
                    defaultDistance = maxCameraDistance;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Start()
        {
            thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            parentOffsetTransform = thirdPersonCamera.ParentOffsetTransform;
            target = thirdPersonCamera.Target;
        }
        protected virtual void Update()
        {
            DistanceModulate();
            UpdateSpring();
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}
