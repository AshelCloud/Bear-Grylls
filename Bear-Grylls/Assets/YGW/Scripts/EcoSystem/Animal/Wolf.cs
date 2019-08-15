using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Wolf : AnimalAI
    {
        #region Variable
        [SerializeField]
        private bool isHead = false;
        public bool IsHead 
        {
            get
            {
                return isHead;
            }
            set
            {
                isHead = value;
            }
        }
        [SerializeField]
        private GameObject head;
        public GameObject Head 
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }
        public int LoadCnt { get; set; } = 0;
        public const int MaxLoadCnt = 10;

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
            base.Updating();

            if(Head == null) { return; }

            if(IsHead == false)
            {
                if(InArea.Detected == false)
                {
                    SetTarget(Head.transform);
                }
            }

        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        #endregion
    }
}