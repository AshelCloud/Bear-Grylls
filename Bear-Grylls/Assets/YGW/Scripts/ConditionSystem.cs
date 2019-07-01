using System.Collections;
using UnityEngine;

namespace YGW
{
    public class ConditionSystem : MonoBehaviour
    {
        #region Conditions Variable
        [Range(0f, 100f)]
        [SerializeField]
        private float _hygiene = 100f;
        public float hygiene
        {
            get { return _hygiene; }
            private set { _hygiene = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        private float _thirst = 0f;
        public float thirst
        {
            get { return _thirst; }
            private set { _thirst = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float _temperature = 0f;
        public float temperature
        {
            get { return _temperature; }
            private set { _temperature = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float _hungry = 0f;
        public float hungry
        {
            get { return _hungry; }
            private set { _hungry = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float _coldness = 0f;
        public float coldness
        {
            get { return _coldness; }
            private set { _coldness = value; }
        }

        [Range(0f, 100f)]
        [SerializeField]
        public float _fatigue = 0f;
        public float fatigue
        {
            get { return _fatigue; }
            private set { _fatigue = value; }
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

            if (disease)
            {
                Debug.Log("Disease");
                //TO DO
            }

            if(dehydration)
            {
                Debug.Log("Dehydration");
                //TO DO
            }

            if (fever)
            {
                Debug.Log("Fever");
                //TO DO
            }

            if (malnutrition)
            {
                Debug.Log("Malnutrition");
                //TO DO
            }

            if (cold)
            {
                Debug.Log("Cold");
                //TO DO
            }

            if (strain)
            {
                Debug.Log("Strain");
                //TO DO
            }
        }

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