using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class CollectWood : CollectSomthing
    {
        #region Variable

        [ConditionalHide("editComponentField", true)]
        [SerializeField] private EntitySelector selector;
        #endregion


        #region Function
        protected override Transform SetCollectTarget()
        {
            if (selector.SelectedObject == null)
                return null;

            return selector.SelectedObject.transform;
        }
        protected override bool CollectCondition()
        {
            if (selector.SelectedObject == null)
                return false;

            if (Input.GetKeyDown(KeyCode.F))
                return true;


            return false;
        }
        #endregion


        #region MonoEvents
        protected override void Init()
        {
            if(selector == null) selector = GetComponent<EntitySelector>();
        }
        protected override void Process()
        {

        }
        #endregion
    }
}

