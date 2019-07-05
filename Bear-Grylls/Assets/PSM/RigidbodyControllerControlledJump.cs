using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    [RequireComponent(typeof(RigidbodyController))]
    public class RigidbodyControllerControlledJump : MonoBehaviour
    {
        #region Variable
        [Tooltip("하위 오브젝트의 JumpState 스크립트를 가져와야 합니다.")]
        [SerializeField] JumpState jumpState;

        [Header("Jump Options")]
        public KeyCode jumpKey = KeyCode.Space;
        public float jumpPower = 10;
        public int jumpableCount = 1;     private int jumpableCountConstant;
        


        private RigidbodyController controller;
        private const float doubleJumpPreventionTime = 0.2f;
        private float doubleJumpPreventionNowTime = 0.2f;
        #endregion


        #region Function
        public void Jump()
        {
            if(Input.GetKeyDown(jumpKey))
            {
                if (jumpState.state == JUMP_STATE.AIR)
                    return;

                if (jumpableCount <= 0)
                    return;

                if (0 < doubleJumpPreventionNowTime)
                {
                    --jumpableCount;
                    StartCoroutine(JumpRoutine());
                }
            }
        }

        private void UpdateJumpableCount()
        {
            if (0 < doubleJumpPreventionNowTime)
            {
                if (jumpState.state == JUMP_STATE.GROUND)
                    jumpableCount = jumpableCountConstant;
            }
        }
        #endregion

        #region Coroutine
        private IEnumerator JumpRoutine()
        {
            controller.AddForce(new Vector3(0, jumpPower, 0));

            while (true)
            {
                if (doubleJumpPreventionNowTime < 0)
                    break;

                doubleJumpPreventionNowTime -= Time.deltaTime;
            }

            doubleJumpPreventionNowTime = doubleJumpPreventionTime;
            yield return null;
        }
        #endregion


        #region MonoEvents
        void Awake()
        {
            controller = GetComponent<RigidbodyController>();
            jumpableCountConstant = jumpableCount;
        }
        void Update()
        {
            UpdateJumpableCount();
            Jump();
        }
        #endregion
    }
}

