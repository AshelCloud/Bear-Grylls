using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PSM
{
    [RequireComponent(typeof(StateStorage))]
    public class DesireSystem : MonoBehaviour, UsingSubColliderComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private StateStorage stateStorage;

        private float hygiene = 0;
        private float thirsty = 0;
        private float temperature = 0;
        private float hungry = 0;
        private float fatigue = 0;

        [SerializeField] float hygieneConstant = 0.15f;
        [SerializeField] float thirstyConstant = 0.15f;
        [SerializeField] float hungryConstant = 0.15f;
        [SerializeField] float fatigueConstant = 0.2f;
        [SerializeField] float temperatureConstant = 0.15f;

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void UpdateHygiene()
        {
            if (hygiene < 100)
                hygiene += Time.deltaTime * hygieneConstant;
            else if (100 < hygiene)
                hygiene = 100;

            if (100 <= hygiene)
                stateStorage.AddState(typeof(StateDisease));
            else
                stateStorage.RemoveState(typeof(StateDisease));
        }
        private void UpdateThirsty()
        {
            if (thirsty < 100)
                thirsty += Time.deltaTime * thirstyConstant;
            else if (100 < hygiene)
                thirsty = 100;

            if (100 <= thirsty)
                stateStorage.AddState(typeof(StateDehydration));
            else
                stateStorage.RemoveState(typeof(StateDehydration));
        }
        private void UpdateTemperature()
        {
            if (WeatherSystem.Wheather == WeatherSystem.WHEATHER.Sunny)
                temperature += Time.deltaTime * temperatureConstant;
            if (WeatherSystem.Wheather == WeatherSystem.WHEATHER.Rainy)
                temperature -= Time.deltaTime * temperatureConstant;


            if (100 <= temperature)
                stateStorage.AddState(typeof(StateFever));
            else
                stateStorage.RemoveState(typeof(StateFever));

            if (temperature <= -100)
                stateStorage.AddState(typeof(StateCold));
            else
                stateStorage.RemoveState(typeof(StateCold));
        }
        private void UpdateHungry()
        {
            if (hungry < 100)
                hungry += Time.deltaTime * hungryConstant;
            else if (100 < hygiene)
                hungry = 100;

            if (100 <= hungry)
                stateStorage.AddState(typeof(StateUndernourished));
            else
                stateStorage.RemoveState(typeof(StateUndernourished));
        }
        private void UpdateFatigue()
        {
            if (fatigue < 100)
                fatigue += Time.deltaTime * fatigueConstant;
            else if (100 < hygiene)
                fatigue = 100;

            if (100 <= fatigue)
                stateStorage.AddState(typeof(StateOverwork));
            else
                stateStorage.RemoveState(typeof(StateOverwork));
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            stateStorage = GetComponent<StateStorage>();
        }
        protected virtual void Update()
        {
            UpdateHygiene();
            UpdateThirsty();
            UpdateTemperature();
            UpdateHungry();
            UpdateFatigue();
        }
        protected void OnTriggerEnter(Collider other)
        {
        }
        private void OnTriggerExit(Collider other)
        {
        }
        public void SubColliderTriggerEnter(Collider other)
        {
        }
        public void SubColliderTriggerExit(Collider other)
        {
        }

        public void SubColliderTriggerStay(Collider other) { }
        public void SubCollisionEnter(Collision collision) { }
        public void SubCollisionStay(Collision collision) { }
        public void SubCollisionExit(Collision collision) { }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}