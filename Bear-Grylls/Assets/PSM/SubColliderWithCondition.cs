using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public abstract class SubColliderWithCondition : SubCollider
    {

        #region Function
        protected abstract bool OnTriggerEnterCondition_(Collider other);
        protected abstract bool OnTriggerStayCondition_(Collider other);
        protected abstract bool OnTriggerExitCondition_(Collider other);

        protected abstract bool OnCollisionEnterCondition_(Collision collision);
        protected abstract bool OnCollisionStayCondition_(Collision collision);
        protected abstract bool OnCollisionExitCondition_(Collision collision);
        #endregion

        #region MonoEvents
        protected override void OnTriggerEnter(Collider other)
        {
            if (OnTriggerEnterCondition_(other) == true)
                base.OnTriggerEnter(other);
        }
        protected override void OnTriggerStay(Collider other)
        {
            if (OnTriggerStayCondition_(other) == true)
                base.OnTriggerStay(other);
        }
        protected override void OnTriggerExit(Collider other)
        {
            if (OnTriggerExitCondition_(other) == true)
                base.OnTriggerExit(other);
        }


        protected override void OnCollisionEnter(Collision collision)
        {
            if (OnCollisionEnterCondition_(collision) == true)
                base.OnCollisionEnter(collision);
        }
        protected override void OnCollisionStay(Collision collision)
        {
            if (OnCollisionStayCondition_(collision) == true)
                base.OnCollisionStay(collision);
        }
        protected override void OnCollisionExit(Collision collision)
        {
            if (OnCollisionExitCondition_(collision) == true)
                base.OnCollisionExit(collision);
        }
        #endregion
    }
}