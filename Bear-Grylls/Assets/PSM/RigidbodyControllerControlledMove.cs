using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

namespace PSM
{
    [RequireComponent(typeof(RigidbodyController))]
    [RequireComponent(typeof(AnimatorManager))]
    public class RigidbodyControllerControlledMove : MonoBehaviour, UsingAnimatorManagerComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public float speed;


        private RigidbodyController controller;
        private Vector3 moveVec = new Vector3();
        private bool isWalk = false;

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void Move(Vector3 move, bool direction = true)
        {
            controller.AddForce(moveVec);
        }

        public void MoveInput()
        {
            moveVec = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                moveVec.z += speed;
            else if (Input.GetKey(KeyCode.S))
                moveVec.z -= speed;

            if (Input.GetKey(KeyCode.A))
                moveVec.x -= speed;
            else if (Input.GetKey(KeyCode.D))
                moveVec.x += speed;

            if (moveVec != Vector3.zero)
            {
                controller.AddForce(moveVec);
            }

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
            MoveInput();
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }

}
