using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
   public class CameraAnimatorController : MonoBehaviour
    {
        #region Variable
        private Animator animator;
        private string EventName { get; set; }
        #endregion

        #region MonoEvents
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        #endregion

        #region Function
        public void PlayEvent(string eventName)
        {
            EventName = eventName;
            animator.SetBool(eventName, true);
        }

        public void StopEvent()
        {
            if(EventName == null)
            {
                Debug.LogError("최근에 재생된 이벤트가 없습니다.");
            }
            else
            {
                animator.SetBool(EventName, false);
            }
        }
        #endregion

        #region Coroutine
        #endregion
    }
}