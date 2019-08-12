using MalbersAnimations;
using MalbersAnimations.Utilities;
using UnityEngine;

namespace YGW
{
    public class Tiger : AnimalAI
    {
        #region Variable
        private LookAt look;
        public LookAt Look
        {
            get
            {
                if(look == null)
                {
                    look = GetComponent<LookAt>();
                }

                return look;
            }
        }
        #endregion

        #region MonoEvents
        private void Start()
        {
            StartAgent();
        }

        private void Update()
        {
            Deer isDeer = null;

            if (Look.Target != null)
            {
                isDeer = GetRoot(Look.Target).GetComponent<Deer>();
            }

            if (isDeer != null)
            {
                State = STATE.HUNT;
                SetTarget(Look.Target);
            }

            base.Updating();
        }
        #endregion

        #region Function
        private Transform GetRoot(Transform obj)
        {
            Transform root = obj.root;

            var animals = root.GetComponentsInChildren<Animal>();

            for(int i = 0; i < animals.Length; i ++)
            {
                var transforms = animals[i].GetComponentsInChildren<Transform>();

                for(int j = 0; j < transforms.Length; j ++)
                {
                    if(obj == transforms[j])
                    {
                        return animals[i].transform;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Coroutine
        #endregion
    }
}