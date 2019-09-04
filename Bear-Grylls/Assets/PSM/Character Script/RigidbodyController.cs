using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PSM
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyController : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Vector3 Velocity { get { return rigidbody.velocity; } }
        public bool UseDrag { get { return useDrag; } set { useDrag = value; } }
        public float StopPower { get { return stopPower; } set { stopPower = value; } }
        public bool UseMaxSpeed { get { return useMaxSpeed; } set { useMaxSpeed = value; } }
        public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
        public float DefaultMaxSpeed { get { return defaultMaxSpeed; } }

        [SerializeField] private bool editComponentField = false;
        [ConditionalHide("editComponentField", true)]
        [SerializeField] Rigidbody rigidbody = null;

        [Header("Drag")]
        [SerializeField] bool useDrag = true;
        [ConditionalHide("useDrag", true)]
        [SerializeField] float stopPower = 1;

        [Header("MaxSpeed")]
        [SerializeField] bool useMaxSpeed;
        [ConditionalHide("useMaxSpeed", true)]
        [SerializeField] float maxSpeed = 3;



        private float defaultMaxSpeed = 0;
        private bool useMove = false;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
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
        public void SetMaxSpeedToDefault()
        {
            maxSpeed = defaultMaxSpeed;
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
        private void ClampVelocity()
        {
            Vector3 velocityXZ = rigidbody.velocity;
            velocityXZ.y = 0;
            
            if (maxSpeed < velocityXZ.magnitude)
            {
                velocityXZ = Vector3.ClampMagnitude(velocityXZ, maxSpeed);

                velocityXZ.y = rigidbody.velocity.y;
                rigidbody.velocity = velocityXZ;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void Awake()
        {
            if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
            if (rigidbody == null) Debug.LogError("missing rigidbody");
            defaultMaxSpeed = maxSpeed;
        }
        private void FixedUpdate()
        {
            if (useDrag) Drag();
            if (useMaxSpeed) ClampVelocity();
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }

}

