using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Bear : AnimalAI
    {
        #region Variable
        [SerializeField]
        private SphereCollider detectSphere;
        public SphereCollider DetectSphere
        {
            get
            {
                if (detectSphere == null)
                {
                    detectSphere = GetComponent<SphereCollider>();
                }

                return detectSphere;
            }
        }
        #endregion

        #region MonoEvents
        private void Start()
        {
            AnimalComponent.Tier = 1;
            StartAgent();
        }

        private void Update()
        {
            base.Updating();
        }

        private void OnTriggerStay(Collider other)
        {
            if (CurTarget == null) { return; }

            if (Utils.GetRoot(other.transform) == CurTarget)
            {
                State = STATE.HUNT;
                SetTarget(CurTarget);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (CurTarget == null) { return; }

            if (Utils.GetRoot(other.transform) == CurTarget)
            {
                Target = null;
                CurTarget = null;

                SetTarget(CurTarget);
                SetDestination(EcoManager.Instance.GetRandomPosition());
                State = STATE.IDLE;
            }
        }
        #endregion

        #region Function

        #endregion

        #region Coroutine
        #endregion
    }
}
