using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Wolf : AnimalAI
    {
        #region Variable
        public bool IsHead { get; set; } = false;
        public GameObject Head { get; set; }

        private DetectArea inArea;
        public DetectArea InArea
        {
            get
            {
                if(inArea == null)
                {
                    inArea = GetComponentInChildren<DetectArea>();
                }

                return inArea;
            }
        }
        #endregion

        #region MonoEvents
        private void Start()
        {
            base.StartAgent();
        }

        private void Update()
        {
            if(IsHead == false)
            {
                if(InArea.Detected == true)
                {
                    SetDestination(EcoManager.Instance.GetRandomPosition());
                }
                else
                {
                    SetTarget(Head.transform);
                }
            }
            base.Updating();
        }
        
        #endregion

        #region Function
        #endregion

        #region Coroutine
        #endregion
    }
}