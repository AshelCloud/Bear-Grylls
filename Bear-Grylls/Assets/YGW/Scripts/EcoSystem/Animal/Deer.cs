using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Deer : AnimalAI
    {
        #region Variable
        #endregion

        #region MonoEvents
        private void Start()
        {
            StartAgent();
        }

        private void Update()
        {
            Tiger isTiger = null;

            if (Look.Target != null)
            {
                isTiger = Utils.GetRoot(Look.Target).GetComponent<Tiger>();
                if (isTiger != null)
                {
                    State = STATE.RUN;
                }
            }
            else if (AnimalComponent.Damaged == true)
            {
                State = STATE.RUN;
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