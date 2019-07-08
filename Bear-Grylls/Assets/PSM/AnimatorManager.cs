using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    /// <summary>
    /// AnimatorManager 컴포넌트가 있어야 작동합니다
    /// [RequireComponent(typeof(AnimatorManager))]  를 활용하세요
    /// </summary>
    public interface UsingAnimatorManagerComponent
    {
        /// <summary>
        /// AnimatorManager 컴포넌트에 의해 Start단계에서 한번만 호출 됩니다
        /// </summary>
        /// <param name="animator">AnimatorManager 컴포넌트에 할당된 animator 입니다</param>
        void ForInit(Animator animator);

        /// <summary>
        /// AnimatorManager 컴포넌트에 의해 Update단계에 매번 호출됩니다
        /// </summary>
        /// <param name="animator">AnimatorManager 컴포넌트에 할당된 animator 입니다</param>
        void UpdateAnimator(Animator animator);
    }


    public class AnimatorManager : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [SerializeField] private bool editComponentField = false;
        [ConditionalHide("editComponentField", true)]
        [SerializeField] private Animator animator;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        UsingAnimatorManagerComponent[] usingAnimatorComponents;
        private void Awake()
        {
            if (animator == null) animator = GetComponent<Animator>();
            usingAnimatorComponents = GetComponents<UsingAnimatorManagerComponent>();
        }
        private void Start()
        {
            for (int i = 0; i < usingAnimatorComponents.Length; ++i)
                usingAnimatorComponents[i].ForInit(animator);
        }
        void Update()
        {
            for (int i = 0; i < usingAnimatorComponents.Length; ++i)
                usingAnimatorComponents[i].UpdateAnimator(animator);
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}


