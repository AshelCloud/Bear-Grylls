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
        public ParameterParserWithUnityCustomUtility(string[] parameters) : base(parameters) { }

        protected ParameterParserWithUnityCustomUtility() { }
        #endregion
    }

    public abstract class InterplayAction : MonoBehaviour, UsingAnimatorManagerComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Header("- InterplayAction -")]
        [SerializeField] protected float animationPlaybackTimeLimit = 4f;
        [Header("상호작용 시작 시 Animator 변수 설정 / \"애니메이터변수이름,자료형,값\" 형식으로 입력합니다   ")]
        [SerializeField] private string[] animatorVarialbeSetupAtStart;

        [Header("상호작용 종료 시 Animator 변수 설정 / \"애니메이터변수이름,자료형,값\" 형식으로 입력합니다   ")]
        [SerializeField] private string[] animatorVarialbeSetupAtEnd;



        private Transform interplayTarget = null;

        private ParameterParserWithUnityCustomUtility animatorSetupUtilityAtStart;
        private ParameterParserWithUnityCustomUtility animatorSetupUtilityAtEnd;

        private bool animationSwitchAtStart = false;
        private bool animationSwitchAtEnd = false;

        private bool crRunning = false;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void Cancel()
        {
            crRunning = false;
        }

        //UsingAnimatorManagerComponent Interface 구현
        public void ForInit(Animator animator, AnimatorManager aniManager)
        {
        }
        public void UpdateAnimator(Animator animator, AnimatorManager aniManager)
        {
            if (animationSwitchAtStart == true)
            {
                animatorSetupUtilityAtStart.SetAnimatorVariable(animator);
                animationSwitchAtStart = false;
            }

            if (animationSwitchAtEnd == true)
            {
                animatorSetupUtilityAtEnd.SetAnimatorVariable(animator);
                animationSwitchAtEnd = false;
            }
        }


        protected abstract void Init();// Awake대신 사용하십시요, Awake는 virtual로 구현되어 있습니다
        protected abstract void Process();// Update대신 사용하십시요, Update는 virtual로 구현되어 있습니다
        protected abstract Transform SetInterplayTarget();//transform반환시 타겟 설정
        protected abstract bool InterplayActionCondition();// True반환시 상호작용 시작
        protected abstract bool InterplayCancelCondition();// True반환시 상호작용 취소
        protected abstract void InterplaySuccessCode();//상호작용 성공시 실행되는 코드
        protected abstract void InterplayFailCode();//상호작용 성공시 실행되는 코드

        private void UpdateInterplayAction()
        {
            interplayTarget = SetInterplayTarget();

            if (InterplayActionCondition() == false)
                return;

            if (crRunning == false)
                StartCoroutine(InterplayRoutine());
        }

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Coroutine - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual IEnumerator InterplayRoutine()
        {
            crRunning = true;

            //정지
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
                rigidbody.velocity = Vector3.zero;

            //Target을 바라봄
            Vector3 TargetPos = interplayTarget.transform.position;
            TargetPos.y = transform.position.y;
            transform.LookAt(TargetPos);

            //애니메이션 재생
            animationSwitchAtStart = true;

            //타이머
            bool success = false;//sucess = true 일 경우 후에 InterplaySuccessCode실행
            float animationPlaybackTime = animationPlaybackTimeLimit;
            while (true)
            {
                //취소 조건 달성 시 상호작용 실패
                if (InterplayCancelCondition() == true)
                {
                    success = false;
                    break;
                }

                //외부에서 취소 시킬 경우
                if (crRunning == false)
                {
                    success = false;
                    break;
                }

                //타이머 조건 확인
                if (animationPlaybackTime < 0)
                {
                    success = true;
                    break;
                }

                animationPlaybackTime -= Time.deltaTime;
                yield return null;
            }

            //상호작용 애니메이션 종료
            animationSwitchAtEnd = true;

            //상호작용 결과
            if (success)
                InterplaySuccessCode();
            else
                InterplayFailCode();

            yield return null;
            crRunning = false;
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            animatorSetupUtilityAtStart = new ParameterParserWithUnityCustomUtility(animatorVarialbeSetupAtStart);
            animatorSetupUtilityAtEnd = new ParameterParserWithUnityCustomUtility(animatorVarialbeSetupAtEnd);
            Init();
        }
        protected virtual void Update()
        {
            UpdateInterplayAction();
            Process();
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}
