using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSM
{
    public class WeatherSystem : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public enum WHEATHER{ Clear, Sunny, Rainy }
        public static WHEATHER Wheather { private set; get; } = WHEATHER.Clear;

        [Header("- Required Setting -")]
        [SerializeField] GameObject sunnyObject;
        [SerializeField] GameObject rainyObject;

        public static UnityEvent sunnyEvent;
        public static UnityEvent raniyEvent;

        //[Header("- Option -")]
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void ChangeWeather()
        {
            float rand = Random.Range(0.0f, 100.0f);
            if (0 <= rand && rand < 25)
            {
                Wheather = WHEATHER.Sunny;
                sunnyEvent.Invoke();
            }
            if (25 <= rand && rand < 50)
            {
                Wheather = WHEATHER.Rainy;
                raniyEvent.Invoke();
            }
            if (50 <= rand && rand < 100)
            {
                Wheather = WHEATHER.Clear;
            }

            switch (Wheather)
            {
                case WHEATHER.Clear:
                    sunnyObject.active = false;
                    rainyObject.active = false;

                    break;
                case WHEATHER.Sunny:
                    sunnyObject.active = true;
                    rainyObject.active = false;

                    break;
                case WHEATHER.Rainy:
                    sunnyObject.active = false;
                    rainyObject.active = true;

                    break;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            raniyEvent = new UnityEvent();
        }
        protected virtual void Start()
        {
            InvokeRepeating("ChangeWeather", 100, 100);
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}


