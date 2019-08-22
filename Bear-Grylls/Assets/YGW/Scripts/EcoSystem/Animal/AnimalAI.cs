using MalbersAnimations;
using MalbersAnimations.Events;
using MalbersAnimations.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace YGW
{
    [RequireComponent(typeof(Animal))]
    [RequireComponent(typeof(ConditionSystem))]
    public class AnimalAI : MonoBehaviour
    {
        #region Variable
        protected enum STATE
        {
            IDLE,
            HUNGRY,
            HUNT,
            RUN,
        }

        protected enum ActionID
        {
            zero,
            one,
            Eat,

        }


        protected STATE State { get; set; } = STATE.IDLE;

        public bool AutoSpeed = true;
        public float ToTrot = 6f;
        public float ToRun = 8f;

        private Animal animal;

        private NavMeshAgent _agent;
        /// <summary>
        /// The NavMeshAgent
        /// </summary>
        public NavMeshAgent Agent
        {
            get
            {
                if (_agent == null)
                {
                    _agent = GetComponentInChildren<NavMeshAgent>();
                }
                return _agent;
            }

            private set { _agent = value; }
        }
        /// <summary>Stores the Remainin distance to the Target's Position</summary>
        private float RemainingDistance { get; set; }

        private float DefaultStopDistance { get; set; }

        private bool DoingAnAction { get; set; }
        private bool EnterOffMesh { get; set; }

        [SerializeField]
        private float stoppingDistance = 0.6f;
        public float StoppingDistance
        {
            get { return stoppingDistance; }
            private set { stoppingDistance = value; }
        }

        public bool IsWaiting { get; private set; }

        private static Vector3 NullVector { get; } = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        private Vector3 targetPosition = NullVector;
        protected Vector3 TargetLastPosition = NullVector;

        /// <summary>
        /// The Agent is Active and we are on a NavMesh
        /// </summary>
        public bool AgentActive
        {
            get { return Agent.isOnNavMesh && Agent.enabled; }
        }

        private bool Stopped { get; set; } = false;
        /// <summary>
        /// The Target
        /// </summary>
        protected Transform Target { get; set; }


        private bool targetisMoving;
        /// <summary>
        /// is the Target transform moving??
        /// </summary>
        public bool TargetisMoving
        {
            get
            {
                if (Target != null)
                {
                    targetisMoving = (Target.position - TargetLastPosition).magnitude > 0.001f;
                    return targetisMoving;
                }
                targetisMoving = false;
                return targetisMoving;
            }
        }

        protected ConditionSystem condition;
        public ConditionSystem Condition
        {
            get
            {
                if(condition == null)
                {
                    condition = GetComponent<ConditionSystem>();
                }
                return condition;
            }
        }

        protected ActionZone isActionZone;

        //Event Variables
        public Vector3Event OnTargetPositionArrived = new Vector3Event();
        public TransformEvent OnTargetArrived = new TransformEvent();
        public UnityEvent OnActionStart = new UnityEvent();
        public UnityEvent OnActionEnd = new UnityEvent();
        #endregion

        #region MonoEvents
        private void Awake()
        {
            animal = GetComponent<Animal>();
            animal.OnAnimationChange.AddListener(OnAnimationChanged);           //Listen when the Animations changes..
        }

        private void Start()
        {
            StartAgent();
        }

        private void Update()
        {
            Updating();
        }
        #endregion

        #region Function
        /// <summary>
        /// Initialize the Ai Animal Control Values
        /// </summary>
        protected virtual void StartAgent()
        {
            animal = GetComponent<Animal>();
            animal.OnAnimationChange.AddListener(OnAnimationChanged);           //Listen when the Animations changes..

            DoingAnAction = false;

            Agent.updateRotation = false;                                       //The animator will control the rotation and postion.. NOT THE AGENT
            Agent.updatePosition = false;
            DefaultStopDistance = StoppingDistance;                             //Store the Started Stopping Distance
            Agent.stoppingDistance = StoppingDistance;
            SetDestination(EcoManager.Instance.GetRandomPosition());

            //SetTarget(Target);                                                  //Set the first Target
            IsWaiting = false;
        }

        protected virtual void Updating()
        {
            SetState();

            if (Stopped)
            {
                if (TargetisMoving)
                {
                    Stopped = false;
                }
            }
            else if (animal.Swim)                                                         //if the Animal is flying?
            {
                FreeMovement();
            }
            else if (AgentActive)                                               //if we are on a NAV MESH onGround
            {
                if (IsWaiting) return;                                          //If the Animal is Waiting no nothing . .... he is doing something else... wait until he's finish

                if (targetPosition == NullVector)                               //if there's no Position to go to.. Stop the Agent
                {
                    StopAnimal();
                }
                else
                    UpdateAgent();
            }
            
            if (Target)
            {
                if (TargetisMoving) UpdateTargetTransform();

                TargetLastPosition = Target.position;
            }

            Agent.nextPosition = Agent.transform.position;                  //Update the Agent Position to the Transform position

            //Debug
            Debug.DrawLine(transform.position, targetPosition, Color.blue);
        }

        private void SetState()
        {
            if (Condition.Hungry > 30f)
            {
                State = STATE.HUNGRY;
            }

            else
            {
                State = STATE.IDLE;
            }
        }

        protected virtual void SetTarget(Transform target)
        {
            if (target == null)
            {
                StopAnimal();
                return;             //If there's no target Skip the code
            }

            isActionZone = target.GetComponent<ActionZone>();

            this.Target = target;
            targetPosition = target.position;       //Update the Target Position 

            Stopped = false;

            if (!Agent.isOnNavMesh) return;                             //No nothing if we are not on a Nav mesh or the Agent is disabled
            Agent.enabled = true;
            Agent.SetDestination(targetPosition);                       //If there's a position to go to set it as destination
            Agent.isStopped = false;                                    //Start the Agent again
        }

        protected virtual void SetDestination(Vector3 pos)
        {
            targetPosition = pos;       //Update the Target Position 

            Stopped = false;

            if (!Agent.isOnNavMesh) return;                             //No nothing if we are not on a Nav mesh or the Agent is disabled
            Agent.enabled = true;
            Agent.SetDestination(targetPosition);                       //If there's a position to go to set it as destination
            Agent.isStopped = false;                                    //Start the Agent again
        }

        /// <summary>
        /// Movement with no Agent at all
        /// </summary>
        private void FreeMovement()
        {
            if (IsWaiting) return;
            if (Target == null || targetPosition == NullVector) return; //If we have no were to go then Skip the code

            RemainingDistance = Target ? Vector3.Distance(animal.transform.position, Target.position) : 0;

            var Direction = (Target.position - animal.transform.position);

            animal.Move(Direction);

            Debug.DrawRay(animal.transform.position, Direction.normalized, Color.white);

            if (RemainingDistance < StoppingDistance/* && !IsWaiting*/)   //We arrived to our destination
            {
                //CheckNextTarget();
            }
        }

        /// <summary>
        /// Change velocities
        /// </summary>
        private void AutomaticSpeed()
        {
            if (/* RemainingDistance < ToTrot && */ State == STATE.IDLE)         //Set to Walk
            {
                animal.Speed1 = true;
            }
            else     //Set to Run
            {
                animal.Speed3 = true;
            }
        }

        private void OnAnimationChanged(int animTag)
        {
            var isInAction = (animTag == AnimTag.Action);                                 //Check if the Animal is making an Action

            if (isInAction != DoingAnAction)
            {
                DoingAnAction = isInAction;                                               //Update the Current Status of the Action Variable

                if (DoingAnAction)                  //If we started an Action ?
                {
                    OnActionStart.Invoke();
                    IsWaiting = true;               //Set that the animal is doing something
                }
                else
                {
                    OnActionEnd.Invoke();

                    if (!EnterOffMesh)      //if the action was not on an offmeshlink like eat drink..etc
                    {
                        //SetNextTarget();
                    }
                    else
                    {
                        IsWaiting = false;
                    }
                }
            }

            if (animTag == AnimTag.Jump) animal.MovementRight = 0;                                       //Don't rotate if is in the middle of a jump

            if (animTag == AnimTag.Locomotion || animTag == AnimTag.Idle)                                //Activate the Agent when the animal is moving
            {
                if (!Agent.enabled)                     //If the Agent is disabled while on Idle Locomotion or Recovering Enable it 
                {
                    Agent.enabled = true;
                    Agent.ResetPath();
                    EnterOffMesh = false;

                    if (targetPosition != NullVector)                                       //Resume the the path with the new Target Position in case there's one
                    {
                        Agent.SetDestination(targetPosition);
                        Agent.isStopped = false;
                    }
                }
            }
            else   //Disable the Agent whe is not on Locomotion or Idling (for when is falling or swimming)
            {
                if (Agent.enabled)
                {
                    Agent.enabled = false;
                }
            }
        }

        /// <summary>
        /// Updates the Agents using he animation root motion
        /// </summary>
        private void UpdateAgent()
        {
            var Direction = Vector3.zero;                               //Reset the Direction (THIS IS THE DIRECTION VECTOR SENT TO THE ANIMAL)  

            RemainingDistance = Agent.remainingDistance;                    //Store the remaining distance -- but if navMeshAgent is still looking for a path Keep Moving
            RemainingDistance = Agent.remainingDistance <=0 ? float.PositiveInfinity : Agent.remainingDistance;

            if (Agent.pathPending || Mathf.Abs(RemainingDistance) <= 0.1f)      //In Case the remaining Distance is wrong
            {
                RemainingDistance = float.PositiveInfinity;
                UpdateTargetTransform();
            }

            if (RemainingDistance > StoppingDistance)                   //if haven't arrived yet to our destination  
            {
                Direction = Agent.desiredVelocity;
                DoingAnAction = false;
            }
            else  //if we get to our destination                                                          
            {
                OnTargetPositionArrived.Invoke(targetPosition);         //Invoke the Event On Target Position Arrived
                if (Target)
                {
                    OnTargetArrived.Invoke(Target);                 //Invoke the Event On Target Arrived
                }

                targetPosition = NullVector;                            //Reset the TargetPosition
                Agent.isStopped = true;                                 //Stop the Agent

                CheckNextTarget();
            }

            animal.Move(Direction);                                     //Set the Movement to the Animal

            if (AutoSpeed) AutomaticSpeed();                            //Set Automatic Speeds
            CheckOffMeshLinks();                                        //Jump/Fall behaviour 
        }

        private void CheckNextTarget()
        {
            if (isActionZone && !DoingAnAction)                     //If the Target is an Action Zone Start the Action
            {
                animal.Action = true;                               //Activate the Action on the Animal (The ID is Given by the ACTION ZONE)
                animal.Stop();

                if (isActionZone.MoveToExitAction)
                {
                    float time = isActionZone.WaitTime;
                    animal.Invoke("WakeAnimal", time);
                }
            }
            else //if (isWayPoint)                                    //If the Next Target is a Waypoint
            {
                SetNextTarget(Random.Range(0f, 10f), EcoManager.Instance.GetRandomPosition());
            }
        }

        IEnumerator WaitToNextTargetC;

        /// <summary>
        /// Set the next target and wait x time if the next waypoint has wait Time > 0
        /// </summary>
        private void SetNextTarget(float time, Transform target)
        {
            if (WaitToNextTargetC != null) StopCoroutine(WaitToNextTargetC);

            WaitToNextTargetC = WaitToNextTarget(time, target);
            StartCoroutine(WaitToNextTargetC);
        }

        /// <summary>
        /// Set the next position and wait x time if the next waypoint has wait Time > 0
        /// </summary>
        private void SetNextTarget(float time, Vector3 pos)
        {
            if (WaitToNextTargetC != null) StopCoroutine(WaitToNextTargetC);

            WaitToNextTargetC = WaitToNextTarget(time, pos);
            StartCoroutine(WaitToNextTargetC);
        }

        /// <summary>
        /// Use this for Targets that changes their position
        /// </summary>
        public void UpdateTargetTransform()
        {
            if (!Agent.isOnNavMesh) return;         //No nothing if we are not on a Nav mesh or the Agent is disabled
            if (Target == null) return;             //If there's no target Skip the code
            targetPosition = Target.position;       //Update the Target Position 
            Agent.SetDestination(targetPosition);   //If there's a position to go to set it as destination
            if (Agent.isStopped) Agent.isStopped = false;
        }

        /// <summary>
        /// Manage all Off Mesh Links
        /// </summary>
        private void CheckOffMeshLinks()
        {
            if (Agent.isOnOffMeshLink && !EnterOffMesh)                         //Check if the Agent is on a OFF MESH LINK
            {
                EnterOffMesh = true;                                            //Just to avoid entering here again while we are on a OFF MESH LINK
                OffMeshLinkData OMLData = Agent.currentOffMeshLinkData;

                if (OMLData.linkType == OffMeshLinkType.LinkTypeManual)                 //Means that it has a OffMesh Link component
                {
                    OffMeshLink CurrentOML = OMLData.offMeshLink;                       //Check if the OffMeshLink is a Manually placed  Link

                    ActionZone Is_a_OffMeshZone =
                        CurrentOML.GetComponentInParent<ActionZone>();                  //Search if the OFFMESH IS An ACTION ZONE (EXAMPLE CRAWL)

                    if (Is_a_OffMeshZone && !DoingAnAction)                             //if the OffmeshLink is a zone and is not making an action
                    {
                        animal.Action = DoingAnAction = true;                           //Activate the Action on the Animal
                        return;
                    }

                    var DistanceEnd = (transform.position - CurrentOML.endTransform.position).sqrMagnitude;
                    var DistanceStart = (transform.position - CurrentOML.startTransform.position).sqrMagnitude;

                    var NearTransform = DistanceEnd < DistanceStart ? CurrentOML.endTransform : CurrentOML.startTransform;
                    var FarTransform = DistanceEnd > DistanceStart ? CurrentOML.endTransform : CurrentOML.startTransform;
                    StartCoroutine(MalbersTools.AlignTransform_Rotation(transform, NearTransform.rotation, 0.15f)); //Aling the Animal to the Link Position

                    if (CurrentOML.area == 2)
                    {
                        animal.SetJump();                         //if the OffMesh Link is a Jump type
                    }
                }
                else if (OMLData.linkType == OffMeshLinkType.LinkTypeJumpAcross)             //Means that it has a OffMesh Link component
                {
                    animal.SetJump();
                }
            }
        }

        /// <summary>
        /// Stop the Agent on the Animal... also remove the Transform target and the Target Position and Stops the Animal
        /// </summary>
        public void StopAnimal()
        {
            if (Agent && Agent.isOnNavMesh) Agent.isStopped = true;
            targetPosition = NullVector;
            StopAllCoroutines();
            DoingAnAction = false;
            animal.InterruptAction();
            if (animal) animal.Stop();
            IsWaiting = false;
            Stopped = true;
        }
        #endregion

        #region Coroutine
        protected virtual IEnumerator WaitToNextTarget(float time, Vector3 NextPos)
        {
            if (isActionZone && isActionZone.MoveToExitAction) time = 0;    //Do not wait if the Action Zone was a 'Move to Exit" one


            if (time > 0)
            {
                IsWaiting = true;
                animal.Move(Vector3.zero);  //Stop the animal
                yield return new WaitForSeconds(time);
            }

            IsWaiting = false;
            SetDestination(NextPos);

            yield return null;
        }

        protected virtual IEnumerator WaitToNextTarget(float time, Transform NextTarget)
        {
            if (isActionZone && isActionZone.MoveToExitAction) time = 0;    //Do not wait if the Action Zone was a 'Move to Exit" one


            if (time > 0)
            {
                IsWaiting = true;
                animal.Move(Vector3.zero);  //Stop the animal
                yield return new WaitForSeconds(time);
            }

            IsWaiting = false;
            SetTarget(NextTarget);

            yield return null;
        }
        #endregion
    }
}