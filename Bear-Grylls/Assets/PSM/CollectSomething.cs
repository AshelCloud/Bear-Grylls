using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    [RequireComponent(typeof(EntitySelector))]
    [RequireComponent(typeof(Inventory))]
    public class CollectSomething : InterplayAction
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public string CollectName { get { return collectName; } set { collectName = value; } }

        [Header("- CollectSomthing -")]

        [SerializeField] protected bool editCollectSomethingComponentField = false;
        [ConditionalHide("editCollectSomethingComponentField", true)]
        [SerializeField] protected EntitySelector selector;
        [ConditionalHide("editCollectSomethingComponentField", true)]
        [SerializeField] protected Inventory inventory;

        [SerializeField] protected string collectName = "";//컴포넌트 구분 용 의미 없는 변수입니다
        [SerializeField] protected string[] tagsOfObjectToCollect;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected override Transform SetInterplayTarget()
        {
            if (selector.SelectedObject == null)
                return null;

            return selector.SelectedObject.transform;
        }
        protected override bool InterplayActionCondition()
        {
            if (selector.SelectedObject == null)
                return false;

            bool isTagMatch = false;
            foreach(string each in tagsOfObjectToCollect)
            {
                if(selector.SelectedObject.tag == each)
                {
                    isTagMatch = true;
                    break;
                }
            }
            if (isTagMatch == false)
                return false;

            if (Input.GetKeyDown(KeyCode.F))
                return true;

            return false;
        }
        protected override bool InterplayCancelCondition()
        {
            //방향키를 누르면 취소
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D))
            {
                return true;
            }

            return false;
        }
        protected override void InterplaySuccessCode()
        {
            if (inventory.AddItem() == false)//무게 초과로 아이템 넣기 실패
            {
                //아이템을 바닥에 떨어트림
                print("Fail");
            }
        }
        protected override void InterplayFailCode()
        {
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected override void Init()
        {
            if (selector == null) selector = GetComponent<EntitySelector>();
            if (inventory == null) inventory = GetComponent<Inventory>();
        }
        protected override void Process()
        {

        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}

