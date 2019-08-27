using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class AimingUseThirdPersonCamera : MonoBehaviour, UsingAnimatorManagerComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Header("- Required Setting -")]
        [SerializeField] ThirdPersonCamera cameraScript;
        [SerializeField] ThirdPersonCameraSpringSystem cameraSpringScript;
        [SerializeField] RigidbodyControllerControllMoveRotateBasedCameraView moveRotateScript;
        [SerializeField] float modelAngle;

        [Header("- Option -")]
        [SerializeField] Vector3 aimingPoint;
        [SerializeField] float zoomDistanceWhenAiming;

        private Vector3 backUpPosCorrection;
        private float backUpDefaultDistance;

        private enum BowState
        {
            idle,
            load


        }
        private BowState bowState = BowState.idle;

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void ForInit(Animator animator)
        {

        }
        public void UpdateAnimator(Animator animator)
        {
            if(bowState == BowState.idle)
            {

            }
            else if (bowState == BowState.load)
            {

            }

        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Coroutine - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {

        }
        protected virtual void Start()
        {

        }
        protected virtual void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                backUpPosCorrection = cameraScript.PosCorrection;
                backUpDefaultDistance = cameraSpringScript.DefaultDistance;

                cameraScript.LookAtTargetForward(modelAngle);
                cameraScript.PosCorrection = aimingPoint;
                cameraSpringScript.DefaultDistance = zoomDistanceWhenAiming;
            }

            if (Input.GetMouseButton(1))
            {
                moveRotateScript.RotateCameraForward();

                //Vector3 TargetPos = transform.position + cameraScript.transform.forward;
                //TargetPos.y = transform.position.y;
                //transform.LookAt(TargetPos);
            }

            if (Input.GetMouseButtonUp(1))
            {
                cameraScript.PosCorrection = backUpPosCorrection;
                cameraSpringScript.DefaultDistance = backUpDefaultDistance;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}