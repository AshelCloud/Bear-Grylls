using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class ParameterParser
    {
        #region Variable
        protected Dictionary<string, int> DataInt { get; } = new Dictionary<string, int>();
        protected Dictionary<string, float> DataFloat { get; } = new Dictionary<string, float>();
        protected Dictionary<string, bool> DataBool { get; } = new Dictionary<string, bool>();
        protected List<string> DataTrigger { get; } = new List<string>();
        #endregion


        #region Function
        public ParameterParser(string[] parameters)
        {
            foreach (string parameter in parameters)
            {
                string[] parameterSplited = parameter.Split(',');

                //파싱
                switch (parameterSplited[1])
                {
                    case "int":
                    case "Int":
                        try
                        {
                            DataInt.Add(
                                parameterSplited[0],
                                int.Parse(parameterSplited[2])
                                );
                        }
                        catch { Debug.LogError("잘못된 파라미터입니다"); }
                        break;

                    case "float":
                    case "Float":
                        try
                        {
                            DataFloat.Add(
                                   parameterSplited[0],
                                   float.Parse(parameterSplited[2])
                                   );
                        }
                        catch { Debug.LogError("잘못된 파라미터입니다"); }
                        break;

                    case "bool":
                    case "Bool":
                        try
                        {
                            DataBool.Add(
                                   parameterSplited[0],
                                   bool.Parse(parameterSplited[2])
                                   );
                        }
                        catch { Debug.LogError("잘못된 파라미터입니다"); }
                        break;

                    case "trigger":
                    case "Trigger":
                        try
                        {
                            DataTrigger.Add(
                                   parameterSplited[0]
                                   );
                        }
                        catch { Debug.LogError("잘못된 파라미터입니다"); }
                        break;

                    default:
                        Debug.LogError("잘못된 파라미터입니다");
                        break;
                }
            }
        }

        protected ParameterParser() { }
        #endregion
    }
    public class ParameterParserWithUnityCustomUtility : ParameterParser
    {
        #region Function
        public void SetAnimatorVariable(Animator animator)
        {
            foreach (KeyValuePair<string, int> each in DataInt)
                animator.SetInteger(each.Key, each.Value);
            foreach (KeyValuePair<string, float> each in DataFloat)
                animator.SetFloat(each.Key, each.Value);
            foreach (KeyValuePair<string, bool> each in DataBool)
                animator.SetBool(each.Key, each.Value);
            foreach (string each in DataTrigger)
                animator.SetTrigger(each);
        }
        public ParameterParserWithUnityCustomUtility(string[] parameters) : base(parameters){ }

        protected ParameterParserWithUnityCustomUtility() { }
        #endregion
    }




    [RequireComponent(typeof(Inventory))]
    public abstract class CollectSomthing : MonoBehaviour, UsingAnimatorManagerComponent
    {
        #region Variable
        [Header("애니메이션 설정")]
        [SerializeField] protected float timeToCollect = 2f;
        [Header("수집 시작 시 Animator 변수 설정 / \"애니메이터변수이름,자료형,값\" 형식으로 입력합니다   ")]
        [SerializeField] private string[] animatorSetupAtStartOfCollect;

        [Header("수집 종료 시 Animator 변수 설정 / \"애니메이터변수이름,자료형,값\" 형식으로 입력합니다   ")]
        [SerializeField] private string[] animatorSetupAtExitOfCollect;

        [SerializeField] protected bool editComponentField = false;
        [ConditionalHide("editComponentField", true)]
        [SerializeField] protected Inventory inventory;

        private Transform collectTarget = null;

        private ParameterParserWithUnityCustomUtility animatorSetupUtilityAtStartOfCollect;
        private ParameterParserWithUnityCustomUtility animatorSetupUtilityAtEndOfCollect;

        private bool animationSwitchAtStartOfCollect = false;
        private bool animationSwitchAtEndOfCollect = false;

        private bool crRunning = false;
        #endregion

        #region Function
        public void CancelToCollect()
        {
            crRunning = false;
        }

        public void ForInit(Animator animator)
        {
        }

        public void UpdateAnimator(Animator animator)
        {
            if(animationSwitchAtStartOfCollect == true)
            {
                animatorSetupUtilityAtStartOfCollect.SetAnimatorVariable(animator);
                animationSwitchAtStartOfCollect = false;
            }

            if (animationSwitchAtEndOfCollect == true)
            {
                animatorSetupUtilityAtEndOfCollect.SetAnimatorVariable(animator);
                animationSwitchAtEndOfCollect = false;
            }
        }

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

        protected abstract Transform SetCollectTarget();

        private void UpdateCollect()
        {
            collectTarget = SetCollectTarget();

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

            //정지
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
                rigidbody.velocity = Vector3.zero;


            //수집애니메이션 재생
            Vector3 collectTargetPos = collectTarget.transform.position;
            collectTargetPos.y = transform.position.y;
            transform.LookAt(collectTargetPos);

            animationSwitchAtStartOfCollect = true;

            bool success = false;//sucess = true 일 경우 후에 인벤토리에 수집한 아이템을 넣을지 말지 판단함
            float _timeToCollect = timeToCollect;
            while(true)
            {
                //방향키를 누르면 취소
                if (Input.GetKey(KeyCode.W) ||
                    Input.GetKey(KeyCode.S) ||
                    Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.D))

                {
                    success = false;
                    break;
                }

                //->아이템 수집 시간동안 방해를 받으면 취소
                // public void CancelToCollect 호출 시 crRunning = false 됨
                if (crRunning == false)
                {
                    success = false;
                    break;
                }

                if (_timeToCollect < 0)
                {
                    success = true;
                    break;
                }


                _timeToCollect -= Time.deltaTime;
                yield return null;
            }

            //수집애니메이션 종료 //기본애니메이션으로 돌림
            animationSwitchAtEndOfCollect = true;

            //-> 아이템 수집 시간동안 방해 받지 않으면 -> 아이템을 인벤토리에 넣는다
            if (success)
            {
                if(inventory.AddItem() == false)//무게 초과로 아이템 넣기 실패
                {
                    //아이템을 바닥에 떨어트림
                print("Fail");

                }
            }

            yield return null;
            crRunning = false;
        }
        #endregion

        #region MonoEvents
        protected virtual void Awake()
        {
            animatorSetupUtilityAtStartOfCollect = new ParameterParserWithUnityCustomUtility(animatorSetupAtStartOfCollect);
            animatorSetupUtilityAtEndOfCollect = new ParameterParserWithUnityCustomUtility(animatorSetupAtExitOfCollect);
            if (inventory == null)   inventory = GetComponent<Inventory>();
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
