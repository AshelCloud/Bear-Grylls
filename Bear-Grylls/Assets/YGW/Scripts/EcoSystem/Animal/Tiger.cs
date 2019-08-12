using MalbersAnimations;
using MalbersAnimations.Utilities;
using UnityEngine;

namespace YGW
{
    public class Tiger : AnimalAI
    {
        #region Variable
        private LookAt look;
        public LookAt Look
        {
            get
            {
                if(look == null)
                {
                    look = GetComponent<LookAt>();
                }

                return look;
            }
        }
        #endregion

        #region MonoEvents
        private void Start()
        {
            StartAgent();
        }

        private void Update()
        {
            Deer isDeer = null;

            if (Look.Target != null)
            {
                isDeer = Utils.GetRoot(Look.Target).GetComponent<Deer>();
            }

            if (isDeer != null)
            {
                State = STATE.HUNT;
                SetTarget(Look.Target);
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