using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSM
{
    public class EntitySelector : MonoBehaviour, UsingSubColliderComponent
    {
        #region Variable
        [SerializeField] string[] tagsOfSelectObject;
        //public GameObject SelectedObject { get; private set; } = null;
        #endregion


        #region Function
        #endregion

        #region Coroutine
        #endregion


        #region MonoEvents
        void Awake()
        {
        }
        void Start()
        {
        }
        void Update()
        {
        }

        public void SubColliderTriggerStay(Collider other)
        {
            foreach(string tag in tagsOfSelectObject)
            {
                if (other.tag == tag)
                {


                    break;
                }
            }
        }

        public void SubColliderTriggerExit(Collider other) { }
        public void SubColliderTriggerEnter(Collider other) { }
        public void SubCollisionEnter(Collision collision) { }
        public void SubCollisionStay(Collision collision) { }
        public void SubOnCollisionExit(Collision collision) { }
        #endregion
    }
}