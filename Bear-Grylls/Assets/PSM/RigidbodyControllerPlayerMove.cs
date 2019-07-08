using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

namespace PSM
{
    [RequireComponent(typeof(RigidbodyController))]
    public class RigidbodyControllerPlayerMove : MonoBehaviour, ICharacterMove
    {
        #region Variable
        [Header("Speed")]
        public float speed;


        private RigidbodyController controller;
        private Vector3 moveVec = new Vector3();
        #endregion


        #region Function
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
                Move(moveVec);
        }
        #endregion

        #region MonoEvents
        void Awake()
        {
            controller = GetComponent<RigidbodyController>();
            controller.useMaxSpeed = true;
        }
        void Update()
        {
            MoveInput();
        }
        #endregion
    }

}
