using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class TestCube : MonoBehaviour
    {
        #region Variable
        [SerializeField] RigidbodyController controller;
        [SerializeField] float speed;

        Vector3 moveVec = new Vector3();
        #endregion


        #region Function
        #endregion

        #region Coroutine
        #endregion


        #region MonoEvents
        void Awake()
        {
        }
        void Start()
        {
        }
        void Update()
        {

            moveVec = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveVec.z += speed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveVec.z -= speed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveVec.x -= speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveVec.x += speed;
            }

            if(moveVec != Vector3.zero)
                controller.SetVelocity(moveVec);

        }
        #endregion
    }
}



