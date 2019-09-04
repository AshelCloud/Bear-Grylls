using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class NaturalCollection : CollectableObject, HavingCollectableActionComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [SerializeField] float reBuildTime = 10;
        [SerializeField] float reBuildTimeAdvanceWhenRanny = 5;
        
        private Renderer renderer;
        private Collider collider;
        private float reBuildTimer;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void ActionCollected()
        {
            renderer.enabled = false;
            collider.enabled = false;
            reBuildTimer = reBuildTime;
        }
        private void Event_WeatherSystem_Raniy()
        {
            if (0 < reBuildTimer)
                reBuildTimer -= reBuildTimeAdvanceWhenRanny;
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Coroutine - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private IEnumerator CollectedRoutine(){yield return null;}
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            base.Awake();
            renderer = GetComponent<Renderer>();
            collider = GetComponent<Collider>();

            reBuildTimer = 0;


        }
        protected virtual void Start()
        {
            WeatherSystem.raniyEvent.AddListener(Event_WeatherSystem_Raniy);
        }
        protected virtual void Update()
        {
            if (0 < reBuildTimer)
                reBuildTimer -= Time.deltaTime;

            if (reBuildTimer < 0)
            {
                renderer.enabled = true;
                collider.enabled = true;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }

}