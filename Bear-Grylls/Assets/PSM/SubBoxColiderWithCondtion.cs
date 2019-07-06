using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PSM
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class SubBoxColiderWithCondtion : SubColliderWithCondition
    {
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected abstract bool OnTriggerEnterCondition(Collider other);
        protected abstract bool OnTriggerStayCondition(Collider other);
        protected abstract bool OnTriggerExitCondition(Collider other);

        protected abstract bool OnCollisionEnterCondition(Collision collision);
        protected abstract bool OnCollisionStayCondition(Collision collision);
        protected abstract bool OnCollisionExitCondition(Collision collision);



        protected override bool OnTriggerEnterCondition_(Collider other)
        {
            return OnTriggerEnterCondition(other);
        }
        protected override bool OnTriggerStayCondition_(Collider other)
        {
            return OnTriggerStayCondition(other);
        }
        protected override bool OnTriggerExitCondition_(Collider other)
        {
            return OnTriggerExitCondition(other);
        }

        protected override bool OnCollisionEnterCondition_(Collision collision)
        {
            return OnCollisionEnterCondition(collision);
        }
        protected override bool OnCollisionExitCondition_(Collision collision)
        {
            return OnCollisionStayCondition(collision); ;
        }
        protected override bool OnCollisionStayCondition_(Collision collision)
        {
            return OnCollisionExitCondition(collision); ;
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}

