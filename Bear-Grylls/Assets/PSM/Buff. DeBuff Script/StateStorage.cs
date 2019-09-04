using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class StateStorage : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public List<State> List { get; private set; } = new List<State>();
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void AddState(Type stateType)
        {
            if (GetComponent(stateType) == null)
            {
                List.Add(
                    (State)gameObject.AddComponent(stateType)
                    );
            }
        }
        public void RemoveState(Type stateType)
        {
            State state = (State)GetComponent(stateType);
            if (state != null)
            {
                List.Remove(state);
                Destroy(state);
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Update()
        {
            for(int i = 0; i < List.Count; ++i)
            {
                List[i].Loop(List);
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }

}
