using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class DetectArea : MonoBehaviour
    {
        public bool Detected { get; private set; } = false;

        [SerializeField]
        private CapsuleCollider spine;
        public CapsuleCollider Spine
        {
            get
            {
                if (spine == null)
                {
                    spine = GetComponent<CapsuleCollider>();
                }

                return spine;
            }

            private set
            {
                spine = value;
            }
        }

        private void Start()
        {
            Spine = GetComponent<CapsuleCollider>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Animal") == false)
            {
                return;
            }

            var isWolf = Utils.GetRoot(other.transform).GetComponent<Wolf>();
            if(isWolf != null)
            {
                if (other.GetComponent<PackArea>() != null && isWolf.IsHead == true)
                {
                    Detected = true;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Animal") == false)
            {
                return;
            }

            var isWolf = Utils.GetRoot(other.transform).GetComponent<Wolf>();
            if (isWolf != null)
            {
                if (other.GetComponent<PackArea>() != null && isWolf.IsHead == true)
                {
                    Detected = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Animal") == false)
            {
                return;
            }

            var isWolf = Utils.GetRoot(other.transform).GetComponent<Wolf>();
            if (isWolf != null)
            {
                if (other.GetComponent<PackArea>() != null && isWolf.IsHead == true)
                {
                    Detected = false;
                }
            }
        }
    }
}