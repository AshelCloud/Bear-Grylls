using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] Vector3 rot;
        [SerializeField] Space space;
        void FixedUpdate()
        {
            transform.Rotate(rot, space);
        }
    }
}
