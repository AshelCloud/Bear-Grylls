using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public interface UsingSubColliderComponent
    {
        void SubColliderTriggerEnter(Collider other);
        void SubColliderTriggerStay(Collider other);
        void SubColliderTriggerExit(Collider other);

        void SubCollisionEnter(Collision collision);
        void SubCollisionStay(Collision collision);
        void SubOnCollisionExit(Collision collision);
    }

    public abstract class SubCollider : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Component usingComponent;
        public bool use = true;

        protected UsingSubColliderComponent usingSubColliderComponent;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            if (usingComponent is UsingSubColliderComponent)
                usingSubColliderComponent = usingComponent as UsingSubColliderComponent;
            else if (usingComponent == null)
                Debug.LogWarning("usingComponent is none");
            else
                Debug.LogError("usingComponent is Not Extends UsingSubColliderComponent");
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (use == false)
                return;

            usingSubColliderComponent.SubColliderTriggerEnter(other);
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            if (use == false)
                return;

            usingSubColliderComponent.SubColliderTriggerStay(other);
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (use == false)
                return;

            usingSubColliderComponent.SubColliderTriggerExit(other);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (use == false)
                return;

            usingSubColliderComponent.SubCollisionEnter(collision);
        }
        protected virtual void OnCollisionStay(Collision collision)
        {
            if (use == false)
                return;

            usingSubColliderComponent.SubCollisionStay(collision);
        }
        protected virtual void OnCollisionExit(Collision collision)
        {
            if (use == false)
                return;

            usingSubColliderComponent.SubOnCollisionExit(collision);
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}