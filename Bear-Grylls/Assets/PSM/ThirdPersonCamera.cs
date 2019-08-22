using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Transform Target { get { return target; } set { target = value; } }
        public Transform ParentOffsetTransform { get { return parentOffsetTransform; } set { parentOffsetTransform = value; } }
        public Vector3 OffsetEulerAngles
        {
            get { return offsetEulerAngles; }
            set { offsetEulerAngles = value; tempOffsetEulerAngles = value; isSetOffsetEulerAngles = true; }
        }
        public Vector3 PosCorrection { get { return posCorrection; } set { posCorrection = value; } }

        [Header("- Required Setting -")]
        [SerializeField] Vector3 posCorrection = new Vector3(0, 1.3f, 0.2f);
        [SerializeField] Transform target;

        [Header("- Option -")]
        [SerializeField] bool reverseMouse = false;
        [SerializeField] float turnSpeed = 1f;
        [SerializeField] float cameraLerpSpeed = 10f;

        [SerializeField] float downLimit = -80;
        [SerializeField] float upLimit = 80;

        private Transform parentOffsetTransform;
        private Vector3 offsetEulerAngles;

        private bool isSetOffsetEulerAngles = false;
        private Vector3 tempOffsetEulerAngles;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void LookAtTargetForward(float modelAngle)
        {
            //각도 구하기
            Vector2 v2Dir = new Vector2(target.forward.x, target.forward.z);

            float dgree = Mathf.Asin(-v2Dir.y) * Mathf.Rad2Deg;

            if (v2Dir.x < 0)
                dgree = 180 - dgree;

            float angle = dgree + modelAngle;

            offsetEulerAngles.y = angle;
            OffsetEulerAngles = offsetEulerAngles;
        }

        private void RotationTargetArround()
        {
            if (target == null)
                return;

            Cursor.visible = false;

            float mouseXAxis = Input.GetAxis("Mouse X");
            float mouseYAxis = Input.GetAxis("Mouse Y");

            if (reverseMouse != true)
            {
                offsetEulerAngles.y += mouseXAxis * turnSpeed;

                float tempAngleX = offsetEulerAngles.x;
                tempAngleX -= mouseYAxis * turnSpeed;
                if(downLimit <= tempAngleX && tempAngleX <= upLimit)
                    offsetEulerAngles.x = tempAngleX;
            }
            else
            {
                offsetEulerAngles.y -= mouseXAxis * turnSpeed;

                float tempAngleX = offsetEulerAngles.x;
                tempAngleX += mouseYAxis * turnSpeed;
                if (downLimit <= tempAngleX && tempAngleX <= upLimit)
                    offsetEulerAngles.x = tempAngleX;
            }
        }
        private void ParentOffsetPosUpdate()
        {
            if (target == null)
                return;

            Vector3 targetPos = target.position;
            targetPos += target.up.normalized * posCorrection.y;
            targetPos += target.forward.normalized * posCorrection.z;
            targetPos += target.right.normalized * posCorrection.x;

            parentOffsetTransform.position = targetPos;
        }
        private void WhenUsedOffsetEulerAnglesProperty()
        {
            if (isSetOffsetEulerAngles == true)
            {
                offsetEulerAngles = tempOffsetEulerAngles;
                isSetOffsetEulerAngles = false;
            }
        }
        private void ApplyCameraAngle()
        {
            parentOffsetTransform.rotation = Quaternion.Lerp(parentOffsetTransform.rotation,
                Quaternion.Euler(offsetEulerAngles),
                Time.deltaTime * cameraLerpSpeed);
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            transform.localPosition = new Vector3(0, 0, 0.1f);
            transform.localRotation = new Quaternion();
            transform.localScale = new Vector3(1, 1, 1);

            parentOffsetTransform = transform.parent;
            offsetEulerAngles = parentOffsetTransform.rotation.eulerAngles;

        }
        protected virtual void Update()
        {
            RotationTargetArround();
            ParentOffsetPosUpdate();
            WhenUsedOffsetEulerAnglesProperty();
            ApplyCameraAngle();
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}
