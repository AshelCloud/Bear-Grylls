using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;
using UnityEngine.AI;
using UnityEngine.Events;

namespace YGW
{
    [RequireComponent(typeof(Animal))]
    public class AnimalAI : MonoBehaviour
    {
        #region Variable
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

        public UnityEvent OnActionStart = new UnityEvent();
        public UnityEvent OnActionEnd = new UnityEvent();

        public bool IsWaiting { get; private set; }
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
            IsWaiting = false;
        }

        /// <summary>
        /// Change velocities
        /// </summary>
        private void AutomaticSpeed()
        {
            if (RemainingDistance < ToTrot)         //Set to Walk
            {
                animal.Speed1 = true;
            }
            else if (RemainingDistance < ToRun)     //Set to Trot
            {
                animal.Speed2 = true;
            }
            else if (RemainingDistance > ToRun)     //Set to Run
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
        #endregion

        #region Coroutine
        #endregion
    }
}