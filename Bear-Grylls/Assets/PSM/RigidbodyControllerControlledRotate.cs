using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    

    [RequireComponent(typeof(RigidbodyController))]
    public class RigidbodyControllerControlledRotate : MonoBehaviour
    {
        #region Variable
        public float turnSpeed = 3;

        [Header("모델이 transform.rotation.y 값이 0 일 때 왼손좌표계에서 어느 각도를 바라보고 있습니까?")]
        [SerializeField] private float modelAngle;

        private RigidbodyController controller;
        #endregion


        #region Function
        private void Rotate()
        {
            //rigidbody 이동속도 기반
            Vector3 direction = controller.velocity.normalized;
            direction.y = 0;

            //멈춰있을때는 작동하지 않음
            if (direction.magnitude == 0)
                return;

            //각도 구하기
            Vector2 v2Dir = new Vector2(direction.x, direction.z);

            float dgree = Mathf.Asin(-v2Dir.y) * Mathf.Rad2Deg;

            if (v2Dir.x < 0)
                dgree = 180 - dgree;

            float angle = dgree + modelAngle;

            //lerp 각도 적용
            Quaternion AngleToReplace = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, AngleToReplace, turnSpeed * Time.deltaTime);

        }


        #endregion

        #region MonoEvents
        void Awake()
        {
            controller = GetComponent<RigidbodyController>();
        }
        void Update()
        {
            Rotate();
        }
        #endregion
    }
}


