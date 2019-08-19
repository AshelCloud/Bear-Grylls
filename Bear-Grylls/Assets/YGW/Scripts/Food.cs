using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    [RequireComponent(typeof(ActionZone))]
    [RequireComponent(typeof(SphereCollider))]
    public class Food : MonoBehaviour
    {
        private SphereCollider SphereCollider{ get; set; }
        private void Start()
        {
            SphereCollider = GetComponent<SphereCollider>();

            SphereCollider.isTrigger = true;
        }
    }
}