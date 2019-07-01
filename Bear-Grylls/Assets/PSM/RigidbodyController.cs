using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PSM
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyController : MonoBehaviour
    {
        #region Variable
        public Vector3 velocity
        {
            get { return rigidbody.velocity; }
        }
        public float stopPower = 1;


        [SerializeField] Rigidbody rigidbody = null;
        [SerializeField] bool useDrag = true;


        private bool useMove = false;
        #endregion


        #region Function
        public void AddForce(Vector3 force)
        {
            useMove = true;

            rigidbody.AddForce(force);
        }
        public void SetVelocity(Vector3 velocity)
        {
            useMove = true;

            rigidbody.velocity = velocity;
        }
        public void SetVelocityY(Vector3 velocity)
        {
            useMove = true;

            float y = velocity.y;
            velocity = rigidbody.velocity;
            velocity.y = y;

            rigidbody.velocity = velocity;
        }
        public void SetVelocityXZ(Vector3 velocity)
        {
            useMove = true;

            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }


        private void Drag()
        {
            if (useMove == false)
            {
                Vector3 stopVector = Vector3.MoveTowards(rigidbody.velocity, Vector3.zero, stopPower * Time.deltaTime);
                stopVector.y = rigidbody.velocity.y;

                rigidbody.velocity = stopVector;
            }
            useMove = false;
        }
        #endregion


        #region MonoEvents
        private void Awake()
        {
            if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
            if (rigidbody == null) Debug.LogError("missing rigidbody");
        }
        private void FixedUpdate()
        {
            if (useDrag) Drag();
        }
        #endregion
    }

}

