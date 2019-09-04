using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PSM
{
    public class StateDisease : State, StateComponentBuffOrDeBuff
    {
        private AnimatorManager aniManager;

        public BuffsList GetBuffsList()
        {
            return new BuffsList(0, 0, 0);
        }

        public override void Init()
        {
            aniManager = GetComponent<AnimatorManager>();
            aniManager.BasicControllerOn("HeroInjury");
        }
        public override void Loop(List<State> stateStorage)
        {
        }
        public override void End()
        {
            aniManager.BasicControllerOff("HeroInjury");
        }
    }
}