using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public abstract class CollectSomthing : MonoBehaviour
    {
        #region Variable
        [SerializeField] private Inventory inventory;

        [SerializeField] protected string animationName = "";
        [SerializeField] protected float timeToCollect = 2f;
        private bool crRunning = false;
        #endregion


        #region Function

        /// <summary>
        /// Awake대신 사용하십시요, Awake는 virtual로 구현되어 있습니다
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Update대신 사용하십시요, Update는 virtual로 구현되어 있습니다
        /// </summary>
        protected abstract void Process();

        /// <summary>
        /// 아이템 수집 시작의 조건을 넣어주세요
        /// </summary>
        /// <returns> True반환시 아이템 수집을 시작 </returns>
        protected abstract bool CollectCondition();


        private void UpdateCollect()
        {
            if (CollectCondition() == false)
                return;

            if (crRunning == false)
                StartCoroutine(CollectRoutine());
        }
        #endregion

        #region Coroutine
        private IEnumerator CollectRoutine()
        {
            crRunning = true;

            //아이템 수집 애니메이션
            //-> 아이템 수집 시간동안 방해 받지 않으면 -> 아이템을 인벤토리에 넣는다
            //->아이템 수집 시간동안 방해를 받으면 -> 아이템 수집이 취소된다
            //->아이템 수집 시간동안 방향키를 누르면 -> 내 의지로 취소 할 수가 있다




            yield return null;
            crRunning = false;
        }

        #endregion


        #region MonoEvents
        protected virtual void Awake()
        {
            if(inventory == null)   inventory = GetComponent<Inventory>();
            Init();
        }
        protected virtual void Update()
        {
            UpdateCollect();
            Process();
        }
        #endregion
    }

}
