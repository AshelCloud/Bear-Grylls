using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public enum JUMP_STATE
    {
        GROUND,
        AIR
    }

    [RequireComponent(typeof(BoxCollider))]
    public class JumpState : MonoBehaviour
    {
        #region Variable
        public bool use = true;
        public JUMP_STATE state { get; private set; } = JUMP_STATE.GROUND;


        private BoxCollider boxCollider = null;
        #endregion

        #region MonoEvents
        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }
        private void OnTriggerStay(Collider other)
        {
            if (use == false)
                return;

            state = JUMP_STATE.GROUND;
        }
        private void OnTriggerExit(Collider other)
        {
            if (use == false)
                return;

            state = JUMP_STATE.AIR;
        }
        #endregion
    }
}

