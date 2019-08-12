using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class PackArea : MonoBehaviour
    {
        [SerializeField]
        private SphereCollider area;
        private SphereCollider Area
        {
            get
            {
                if (area == null)
                {
                    area = GetComponent<SphereCollider>();
                }
                return area;
            }
        }

        private Wolf wolf;
        private Wolf Wolf
        {
            get
            {
                if(wolf == null)
                {
                    wolf = transform.parent.GetComponent<Wolf>();
                }

                return wolf;
            }
        }

        private void Update()
        {
            Area.enabled = Wolf.IsHead;
        }
    }
}