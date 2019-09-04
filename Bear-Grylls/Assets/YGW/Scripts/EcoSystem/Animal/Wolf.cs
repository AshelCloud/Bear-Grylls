using MalbersAnimations;
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
                if (inArea == null)
                {
                    inArea = GetComponentInChildren<DetectArea>();
                }

                return inArea;
            }
        }

        private FoodTrack foodTrack;
        public FoodTrack FoodTrack
        {
            get
            {
                if (foodTrack == null)
                {
                    foodTrack = GetComponentInChildren<FoodTrack>();
                }

                return foodTrack;
            }
        }

        private bool Once = false;
        #endregion

        #region MonoEvents
        private void Start()
        {
            base.StartAgent();

            SetDestination(EcoManager.Instance.GetRandomPosition());
        }

        private void Update()
        {
            base.Updating();

            if (State == STATE.HUNGRY)
            {
                var curFood = FoodTrack.CurFood;
                if (curFood != null)
                {
                    base.SetTarget(curFood.transform);
                }
            }

            if (Head == null) { return; }

            if (IsHead == false && State == STATE.IDLE)
            {
                if (InArea.Detected == true && Once == true)
                {
                    Once = false;

                    SetDestination(EcoManager.Instance.GetRandomPosition());
                }
                else if (InArea.Detected == false)
                {
                    Once = true;
                    SetTarget(Head.transform);
                }
            }

        }
        #endregion

        #region Function
        public void Effect()
        {
            if(base.isActionZone != null)
            {
                if(isActionZone.ID == (int)ActionID.Eat)
                {
                    Condition.Hungry -= 20f;
                }
            }
        }
        #endregion

        #region Coroutine
        #endregion
    }
}