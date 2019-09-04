using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PSM
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;

        public bool followX = true;
        public bool followY = true;
        public bool followZ = true;

        private void FixedUpdate()
        {
            Vector3 pos = transform.position;

            if (followX == true)
                pos.x = target.transform.position.x;
            if (followY == true)
                pos.y = target.transform.position.y;
            if (followZ == true)
                pos.z = target.transform.position.z;

            transform.position = pos;
        }
    }
}