using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSM
{
    public class TestFirst : MonoBehaviour
    {

        private void Start()
        {

            TestSecound.unityEvent.AddListener(asd);
        }

        private void Update()
        {
        }

        void asd()
        {
            transform.Translate(new Vector3(0, 0, 0.1f));
            print(123);
        }
    }
}


