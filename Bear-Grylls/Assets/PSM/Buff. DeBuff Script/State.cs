using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public abstract class State : MonoBehaviour
    {
        private List<State> stateStorage = new List<State>();

        public abstract void Init();
        public abstract void Loop(List<State> stateStorage);
        public abstract void End();

        protected void Awake()
        {
            Init();
        }
        protected void OnDestroy()
        {
            End();
        }
    }
}