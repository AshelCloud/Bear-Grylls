using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PSM
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyController : MonoBehaviour
    {
        #region Public


        public void AddForce(Vector3 force)
        {
            isMove = true;
            _rigidbody.AddForce(force);
        }
        public void SetVelocity(Vector3 velocity)
        {
            isMove = true;
            _rigidbody.velocity = velocity;
        }
        public void SetVelocityXZ(Vector3 velocity)
        {
            isMove = true;
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }
        public Vector3 GetVelocity()
        {
            return _rigidbody.velocity;
        }

        #endregion


        #region SerializeField
        [SerializeField] Rigidbody _rigidbody;

        #endregion

        #region Private
        private bool controllerStop = true;
        private float _stopPower = 1;

        private bool isMove = false;


        private void ControllerStop()
        {
            if (_useControllerStop)
            {
                if (isMove == false)
                {
                    Vector3 stopVector = Vector3.MoveTowards(_rigidbody.velocity, Vector3.zero, _stopPower * Time.deltaTime);
                    stopVector.y = _rigidbody.velocity.y;

                    _rigidbody.velocity = stopVector;
                }
                isMove = false;
            }
        }
        #endregion


        #region MonoEvents
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            ControllerStop();
        }
        #endregion
    }

}

