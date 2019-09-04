using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSM
{
    public interface UsingSubColliderComponent
    {
        void SubColliderTriggerEnter(Collider other);
        void SubColliderTriggerStay(Collider other);
        void SubColliderTriggerExit(Collider other);

        void SubCollisionEnter(Collision collision);
        void SubCollisionStay(Collision collision);
        void SubCollisionExit(Collision collision);
    }

    public abstract class SubCollider : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Component[] usingComponent;
        public bool use = true;

        private UnityEvent Event;

        protected UsingSubColliderComponent[] usingSubColliderComponent;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            usingSubColliderComponent = new UsingSubColliderComponent[usingComponent.Length];

            for(int i = 0; i < usingSubColliderComponent.Length; ++i)
            {
                if (usingComponent[i] is UsingSubColliderComponent)
                {
                    usingSubColliderComponent[i] = usingComponent[i] as UsingSubColliderComponent;
                }
                else if (usingComponent == null)
                    Debug.LogWarning("usingComponent is none");
                else
                    Debug.LogError("usingComponent is Not Extends UsingSubColliderComponent");
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (use == false)
                return;

            for (int i = 0; i < usingSubColliderComponent.Length; ++i)
                usingSubColliderComponent[i].SubColliderTriggerEnter(other);
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            if (use == false)
                return;

            for (int i = 0; i < usingSubColliderComponent.Length; ++i)
                usingSubColliderComponent[i].SubColliderTriggerStay(other);
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (use == false)
                return;

            for (int i = 0; i < usingSubColliderComponent.Length; ++i)
                usingSubColliderComponent[i].SubColliderTriggerExit(other);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (use == false)
                return;

            for (int i = 0; i < usingSubColliderComponent.Length; ++i)
                usingSubColliderComponent[i].SubCollisionEnter(collision);
        }
        protected virtual void OnCollisionStay(Collision collision)
        {
            if (use == false)
                return;

            for (int i = 0; i < usingSubColliderComponent.Length; ++i)
                usingSubColliderComponent[i].SubCollisionStay(collision);
        }
        protected virtual void OnCollisionExit(Collision collision)
        {
            if (use == false)
                return;

            for (int i = 0; i < usingSubColliderComponent.Length; ++i)
                usingSubColliderComponent[i].SubCollisionExit(collision);
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}