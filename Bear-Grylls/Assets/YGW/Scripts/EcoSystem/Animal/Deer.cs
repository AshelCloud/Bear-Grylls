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
            AnimalComponent.Tier = 4;
            StartAgent();
        }

        private void Update()
        {
            base.Updating();
        }
        #endregion

        #region Function

        #endregion

        #region Coroutine
        #endregion
    }
}