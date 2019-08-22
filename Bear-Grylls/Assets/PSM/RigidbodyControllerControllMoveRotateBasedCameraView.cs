using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

namespace PSM
{
    [RequireComponent(typeof(RigidbodyController))]
    [RequireComponent(typeof(AnimatorManager))]
    public class RigidbodyControllerControllMoveRotateBasedCameraView : MonoBehaviour, UsingAnimatorManagerComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Camera Camera { get { return camera; } set { camera = value; } }


        [Header("- Required Setting -")]
        [SerializeField] Camera camera;
        [ConditionalHide("useFreeView", true)]
        [SerializeField] ThirdPersonCamera thirdPersonCameraScript;

        [Header("모델이 transform.rotation.y 값이 0 일 때 왼손좌표계에서 어느 각도를 바라보고 있습니까?")]
        [SerializeField] private float modelAngle = 90;

        [Header("- Option -")]
        public float moveSpeed = 2;
        public float turnSpeed = 3;



        [Header("Free View")]
        [SerializeField] bool useFreeView = false;


        private RigidbodyController controller;
        private Vector3 moveVec = new Vector3();
        private bool isWalk = false;
        private float timer1 = 0;
        private bool isSetedRotateCameraForward = false;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void Move(Vector3 move, bool direction = true)
        {
            controller.AddForce(moveVec);
        }
        public void LookAt(Vector3 target)
        {
            transform.LookAt(target);
        }
        public void RotateCameraForward()
        {
            isSetedRotateCameraForward = true;
        }

        public void ForInit(Animator animator)
        {
        }
        public void UpdateAnimator(Animator animator)
        {
            Vector3 velocity = controller.velocity;
            velocity.y = 0;

            float rate = velocity.magnitude / controller.maxSpeed;
            float maxSpeed = animator.GetInteger("ConstMaxSpeed");

            animator.SetFloat("Speed", maxSpeed * rate);


            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isLeftWalk", true);
                animator.SetBool("isRightWalk", false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isRightWalk", true);
                animator.SetBool("isLeftWalk", false);
            }
            else
            {
                animator.SetBool("isRightWalk", false);
                animator.SetBool("isLeftWalk", false);
            }
        }

        private void InputMove(Vector3 cameraForward, Vector3 cameraRight)
        {
            moveVec = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                moveVec += cameraForward * moveSpeed;
            else if (Input.GetKey(KeyCode.S))
                moveVec += cameraForward * -moveSpeed;

            if (Input.GetKey(KeyCode.A))
                moveVec += cameraRight * -moveSpeed;
            else if (Input.GetKey(KeyCode.D))
                moveVec += cameraRight * moveSpeed;

            if (moveVec != Vector3.zero)
            {
                controller.AddForce(moveVec);
            }
        }
        private void Rotate(Vector3 cameraForward)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || isSetedRotateCameraForward == true)
            {
            }
            else //멈춰있을때는 작동하지 않음
                return;


            //각도 구하기
            Vector2 v2Dir = new Vector2(cameraForward.x, cameraForward.z);

            float dgree = Mathf.Asin(-v2Dir.y) * Mathf.Rad2Deg;

            if (v2Dir.x < 0)
                dgree = 180 - dgree;

            float angle = dgree + modelAngle;

            //lerp 각도 적용
            Quaternion AngleToReplace = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, AngleToReplace, turnSpeed * Time.deltaTime);

            isSetedRotateCameraForward = false;
        }
        private void FreeViewMove()
        {
            moveVec = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                moveVec += transform.forward * moveSpeed;
            else if (Input.GetKey(KeyCode.S))
                moveVec += transform.forward * -moveSpeed;

            if (Input.GetKey(KeyCode.A))
                moveVec += transform.right * -moveSpeed;
            else if (Input.GetKey(KeyCode.D))
                moveVec += transform.right * moveSpeed;

            if (moveVec != Vector3.zero)
            {
                controller.AddForce(moveVec);
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        void Awake()
        {
            controller = GetComponent<RigidbodyController>();
            controller.useMaxSpeed = true;
        }
        void Update()
        {
            Vector3 cameraForward = camera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = camera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            if(useFreeView == true)
            {
                if(Input.GetMouseButton(2) || 0 < timer1)
                {
                    FreeViewMove();
                    timer1 -= Time.deltaTime;
                }
                else
                {
                    InputMove(cameraForward, cameraRight);
                    Rotate(cameraForward);                 
                }

                if(Input.GetMouseButtonUp(2))
                {
                    thirdPersonCameraScript.LookAtTargetForward(modelAngle);
                    timer1 = 0.1f;
                }
            }
            else
            {
                InputMove(cameraForward, cameraRight);
                Rotate(cameraForward);
            }

        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }

}
