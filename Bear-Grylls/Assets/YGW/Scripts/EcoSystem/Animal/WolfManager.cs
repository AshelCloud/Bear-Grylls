using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class WolfManager : AnimalSpawn<Wolf>
    {
        #region Variable
        private int packNum { get; set; } = 0;
        #endregion

        #region MonoEvents
        private new void Update()
        {
            base.Update();
            
            packNum = (int)Mathf.Ceil(animals.Count / 10);
        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        #endregion
    }
}