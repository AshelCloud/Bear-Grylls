using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public interface StateComponentBuffOrDeBuff
    {
        BuffsList GetBuffsList();
    }
    public interface ApplyedComponentBuffOrDeBuff
    {
        void ApplyBuff(BuffsList buffsList);
    }
    public struct BuffsList
    {
        public float buffedSpeed;
        public float buffedAttackDamage;
        public float buffedBeAttackedDamage;

        public BuffsList(float buffedSpeed, float buffedAttackDamage, float buffedBeAttackedDamage, RuntimeAnimatorController changedAnimation = null)
        {
            this.buffedSpeed = buffedSpeed;
            this.buffedAttackDamage = buffedAttackDamage;
            this.buffedBeAttackedDamage = buffedBeAttackedDamage;
        }

        public static BuffsList operator +(BuffsList buffsList1, BuffsList buffsList2)
        {
            buffsList1.buffedSpeed += buffsList2.buffedSpeed;
            buffsList1.buffedAttackDamage += buffsList2.buffedAttackDamage;
            buffsList1.buffedBeAttackedDamage += buffsList2.buffedBeAttackedDamage;

            return buffsList1;
        }
    }


    [RequireComponent(typeof(StateStorage))]
    public class BuffAndDeBuff : MonoBehaviour, UsingAnimatorManagerComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private List<StateComponentBuffOrDeBuff> states = new List<StateComponentBuffOrDeBuff>();
        private ApplyedComponentBuffOrDeBuff[] applyedComponents;

        private AnimatorManager aniManager;
        private StateStorage stateStorage = null;
        private int prevBuffCountInStateList = 0;


        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void ForInit(Animator animator, AnimatorManager aniManager)
        {
            this.aniManager = aniManager;
        }
        public void UpdateAnimator(Animator animator, AnimatorManager aniManager)
        {
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            stateStorage = GetComponent<StateStorage>();
            applyedComponents = GetComponents<ApplyedComponentBuffOrDeBuff>();
        }
        protected virtual void Update()
        {
            List<State> states = stateStorage.List.FindAll((State s) =>
            {
                if (s is StateComponentBuffOrDeBuff)
                    return true;
                else
                    return false;
            });

            if(prevBuffCountInStateList != states.Count)
            {
                BuffsList mainBuffsList = new BuffsList(0, 0, 0);

                foreach (State s in states)
                {
                    BuffsList buffsList = ((StateComponentBuffOrDeBuff)s).GetBuffsList();
                    mainBuffsList += buffsList;
                }

                foreach(ApplyedComponentBuffOrDeBuff c in applyedComponents)
                {
                    c.ApplyBuff(mainBuffsList);
                }

                prevBuffCountInStateList = states.Count;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}