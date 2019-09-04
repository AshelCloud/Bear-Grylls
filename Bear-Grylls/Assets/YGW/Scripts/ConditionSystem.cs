using System.Collections;
using UnityEngine;

namespace YGW
{
    public class ConditionSystem : MonoBehaviour
    {
        #region Variable
        [SerializeField]
        private bool isHuman = false;
        public bool IsHuman 
        {
            get { return isHuman; }
            set { isHuman = value; }
        }
        
        [Range(0f, 100f)]
        [SerializeField]
        private float hygiene = 100f;
        public float Hygiene
        {
            get { return hygiene; }
            private set { hygiene = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        private float thirst = 0f;
        public float Thirst
        {
            get { return thirst; }
            private set { thirst = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float temperature = 0f;
        public float Temperature
        {
            get { return temperature; }
            private set { temperature = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float hungry = 0f;
        public float Hungry
        {
            get { return hungry; }
            set { hungry = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float coldness = 0f;
        public float Coldness
        {
            get { return coldness; }
            private set { coldness = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float fatigue = 0f;
        public float Fatigue
        {
            get { return fatigue; }
            private set { fatigue = value; }
        }

        [SerializeField]
        private bool _disease = false;
        public bool disease
        {
            get { return _disease; }
            private set { _disease = value; }
        }

        [SerializeField]
        private bool _dehydration = false;
        public bool dehydration
        {
            get { return _dehydration; }
            private set { _dehydration = value; }
        }

        [SerializeField]
        private bool _fever = false;
        public bool fever
        {
            get { return _fever; }
            private set { _fever = value; }
        }

        [SerializeField]
        private bool _malnutrition = false;
        public bool malnutrition
        {
            get { return _malnutrition; }
            private set { _malnutrition = value; }
        }

        [SerializeField]
        private bool _cold = false;
        public bool cold
        {
            get { return _cold; }
            private set { _cold = value; }
        }

        [SerializeField]
        private bool _strain = false;
        public bool strain
        {
            get { return _strain; }
            private set { _strain = value; }
        }

        #endregion

        #region MonoEvent
        private void Start()
        {
            StartCoroutine(HygieneDecrease());
            StartCoroutine(ThirstIncrease());
            StartCoroutine(HungryIncrease());
            StartCoroutine(FatigueDecrease());
        }

        private void Update()
        {
            if(hygiene <= 0f) { disease = true; }
            else { dehydration = false; }

            if (thirst >= 100f) { dehydration = true; }
            else { dehydration = false; }

            if(temperature >= 100f) { fever = true; }
            else { fever = false; }

            if(hungry >= 100f) { malnutrition = true; }
            else { malnutrition = false; }

            if(coldness >= 100f) { cold = true; }
            else { cold = false; }

            if(fatigue >= 100f) { strain = true; }
            else { strain = false; }
        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        private IEnumerator HygieneDecrease()
        {
            while(true)
            {
                if(hygiene > 0f)
                {
                    hygiene -= 0.15f;
                }
                else { hygiene = 0f; }

                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator ThirstIncrease()
        {
            while(true)
            {
                if(thirst < 100f)
                {
                    thirst += 0.15f;
                }
                else { thirst = 100f; }

                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator HungryIncrease()
        {
            while(true)
            {
                if(hungry < 100f)
                {
                    hungry += 0.15f;
                }
                else { hungry = 100f; }

                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator FatigueDecrease()
        {
            while (true)
            {
                if (fatigue < 100f)
                {
                    fatigue += 0.2f;
                }
                else { fatigue = 100f; }

                yield return new WaitForSeconds(1f);
            }
        }
        #endregion
    }
}