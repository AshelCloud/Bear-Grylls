using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    [RequireComponent(typeof(SphereCollider))]
    public class FoodTrack : MonoBehaviour
    {
        private SphereCollider SphereCollider { get; set; }

        [SerializeField]
        private GameObject food;
        public GameObject CurFood
        {
            get { return food; }
        }

        private void Start()
        {
            SphereCollider = GetComponent<SphereCollider>();

            SphereCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Food>() != null)
            {
                food = other.gameObject;
            }
        }
    }
}