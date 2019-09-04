using UnityEngine;
using System;

namespace MalbersAnimations.Utilities
{
    [Serializable]
    public class BoneRotation
    {
        public Transform bone;                                          //The bone
        public Vector3 offset = new Vector3(0, -90, -90);               //The offset for the look At
        [Range(0, 1)]
        public float weight = 1;                                        //the Weight of the look a
        internal Quaternion initialRotation;
    }

    

    public class LookAt : MonoBehaviour, IAnimatorListener       //This is for sending messages from the animator
    {
        [Tooltip("Global LookAt Activation")]
        [SerializeField]
        private bool active = true;                      //For Activating and Deactivating the HeadTrack
        [Tooltip("The Animations allows the LookAt to be enable/disabled")]
        public bool AnimationActive = true;             //Means the Animator has deactivate it

        [Space]
        [Tooltip("What layers the Look At Rays should ignore")]
        public LayerMask Ignore = 4;
        public bool UseCamera;                                                         //Use the camera ForwardDirection instead a Target
        [Tooltip("What point of the Camera it will cast the Ray")]
        public Vector2 CameraCenter = new Vector2(0.5f,0.5f);                          //Use the camera ForwardDirection instead a Target
        public Transform Target;                        //Use a target to look At

        [Space]
        public float LimitAngle = 80f;                  //Max Angle to LookAt
        public float Smoothness = 5f;                   //Smoothness between Enabled and Disable
        public Vector3 UpVector = Vector3.up;


        float currentSmoothness;

        [Space]
        public BoneRotation[] Bones;                    //Bone Chain   

        private Transform cam;                          //Reference for the camera
        protected float angle;                          //Angle created between the transform.Forward and the LookAt Point    
        protected Vector3 direction;
        public bool debug = true;
        bool hasTarget;

        private RaycastHit aimHit;

        public Vector3 Direction
        {
            set { direction = value; }
            get { return direction; }
        }

        //Custom
        [Header("Custom")]
        [SerializeField]
        private float viewDistance = 1f;
        public float ViewDistance 
        {
            get
            {
                return viewDistance;
            }
            set
            {
                viewDistance = value;
            }
        }

        public LayerMask targetMask;
        public LayerMask viewMask;

        /// <summary>
        /// Check if is on the Angle of Aiming
        /// </summary>
        public bool IsAiming
        {
            get { return angle < LimitAngle && Active && AnimationActive && hasTarget; }
        }

        /// <summary>
        /// Last Raycast stored for calculating the Aim
        /// </summary>
        public RaycastHit AimHit
        {
            get { return aimHit; }
            set { aimHit = value; }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        bool AnimatorOnAnimatePhysics;

        void Awake()
        {

            if (Camera.main != null) cam = Camera.main.transform;               //Get the main camera

            var Anim = GetComponent<Animator>();

            AnimatorOnAnimatePhysics = (Anim && Anim.updateMode == AnimatorUpdateMode.AnimatePhysics); //Check if the animator is on Animate Physics

            if (AnimatorOnAnimatePhysics) return;

            foreach (var bone in Bones)                                         //Stores the Initial Rotation
            {
                bone.initialRotation = bone.bone.transform.localRotation;
            }
        }

        void Update()
        {
            FindTargets();
        }

        void LateUpdate()
        {
            foreach (var bone in Bones)                                         //Stores the Initial Rotation
            {
                bone.initialRotation = bone.bone.transform.localRotation;
            }
            LookAtBoneSet();            //Rotate the bones
        }

        /// <summary>
        /// Enable or Disable this script functionality by the Animator
        /// </summary>
        public void EnableLookAt(bool value)
        {
            AnimationActive = value;
        }

        /// <summary>
        /// Rotates the bones to the Look direction (Only works when the animator is set to Update Mode : Normal)
        /// </summary>
        void LookAtBoneSet()
        {
            if (!Target && !cam) return; //If there's no camera and no Target ignore completely

            hasTarget = false;
            if (UseCamera || Target) hasTarget = true;


            angle = Vector3.Angle(transform.forward, Direction);                                                    //Find the angle for the current bone


            currentSmoothness = Mathf.Lerp(currentSmoothness, IsAiming ? 1 : 0, Time.deltaTime * Smoothness);

            if (currentSmoothness > 0.9999f) currentSmoothness = 1;
            if (currentSmoothness < 0.0001f) currentSmoothness = 0;

            for (int i = 0; i < Bones.Length; i++)
            {
                var bone = Bones[i];

                if (!bone.bone) continue;   //if There's no bone skip

                Vector3 dir = transform.forward;


                if (UseCamera && cam)
                {
                    var ScreenCenter = new Vector2(CameraCenter.x * Screen.width, CameraCenter.y * Screen.height);
                    dir = cam.forward;
                    dir = Utilities.MalbersTools.DirectionFromCamera(bone.bone, ScreenCenter, out aimHit, ~Ignore);

                    //aimHit = MalbersTools.RayCastHitToCenter(bone.bone, CameraCenter, ~Ignore);        //Calculate the Direction from the Bone

                    if (aimHit.collider)                                                //if something was hit 
                    {
                        dir = MalbersTools.DirectionTarget(bone.bone.position, aimHit.point);
                    }
                }

                if (Target) dir = MalbersTools.DirectionTarget(bone.bone, Target);

                Direction = Vector3.Lerp(Direction, dir, Time.deltaTime * Smoothness);

                if (currentSmoothness == 0) return;                                         //Skip all next stuffs


                //if (debug && i == Bones.Length - 1)
                //{
                //    Debug.DrawRay(bone.bone.position, Direction * 15, Color.green);
                //}


                var final = Quaternion.LookRotation(Direction, UpVector) * Quaternion.Euler(bone.offset);
                var next = Quaternion.Lerp(bone.bone.rotation, final, bone.weight * currentSmoothness);
                bone.bone.rotation = next;
            }
        }

        private void FindTargets()
        {
            //시야거리 내에 존재하는 모든 컬라이더 받아오기
            Collider[] targets = Physics.OverlapSphere(transform.position, ViewDistance, targetMask);

            if(targets.Length <= 0)
            {
                NoTarget();
            }

            for (int i = 0; i < targets.Length; i++)
            {
                if(targets[i].isTrigger) { continue; }

                bool isMyBody = false;
                Transform target = targets[i].transform;

                for(int j = 0; j < Bones.Length; j++)
                {
                    if(target == Bones[j].bone)
                    {
                        isMyBody = true;
                    }
                }

                if(isMyBody)
                {
                    continue;
                }

                Vector3 dirToTarget = (target.position - transform.position).normalized;

                //_transform.forward와 dirToTarget은 모두 단위벡터이므로 내적값은 두 벡터가 이루는 각의 Cos값과 같다.
                //내적값이 시야각/2의 Cos값보다 크면 시야에 들어온 것이다.
                //if (Vector3.Angle(_transform.forward, dirToTarget) < ViewAngle/2)
                if (Vector3.Dot(transform.forward, dirToTarget) > Mathf.Cos((LimitAngle / 2) * Mathf.Deg2Rad))
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (Physics.Raycast(transform.position, dirToTarget, distToTarget, viewMask))
                    {
                        Debug.DrawLine(transform.position, target.position, Color.red);
                        Target = target;
                    }
                }
            }
        }

        /// <summary>
        /// Set the Target to Null
        /// </summary>
        public virtual void NoTarget()
        {
            Target = null;
        }

        /// <summary>
        /// This is used to listen the Animator asociated to this gameObject
        /// </summary>
        public virtual void OnAnimatorBehaviourMessage(string message, object value)
        {
            this.InvokeWithParams(message, value);
        }

        private void OnValidate()
        {
            CameraCenter = new Vector2(Mathf.Clamp01(CameraCenter.x), Mathf.Clamp01(CameraCenter.y));
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (debug)
            {
                UnityEditor.Handles.color = new Color(0, 1, 0, 0.1f);

                Transform Center = Bones != null && Bones.Length>0 && Bones[Bones.Length - 1] != null ? Bones[Bones.Length - 1].bone : null;
                if (Center != null)
                {
                    UnityEditor.Handles.DrawSolidArc(Center.position, Vector3.up, Quaternion.Euler(0, -LimitAngle, 0) * transform.forward, LimitAngle * 2, viewDistance);
                    UnityEditor.Handles.color = Color.green;
                    UnityEditor.Handles.DrawWireArc(Center.position, Vector3.up, Quaternion.Euler(0, -LimitAngle, 0) * transform.forward, LimitAngle * 2, viewDistance);
                }
            }

            if (Application.isPlaying)
            {

                if (IsAiming)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(aimHit.point, 0.05f);
                }
            }
        }
#endif
    }
}
















