using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Deer : AnimalAI
    {
        #region Variable
        bool Once = false;
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
            }
            else
            {
                Once = false;
                State = STATE.IDLE;
            }

            if (isTiger != null && Once == false)
            {
                Once = true;
                State = STATE.RUN;
                SetDestination(EcoManager.Instance.GetRandomPosition());
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