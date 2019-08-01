using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class AnimalSpawn<T> : MonoBehaviour
    {
        #region Variable
        [SerializeField]
        protected GameObject animal;

        [Range(0, 100)]
        [SerializeField]
        private int limitAnimalCount;
        public int LimitAnimalCount
        {
            get
            {
                return limitAnimalCount;
            }
            protected set
            {
                limitAnimalCount = value;
            }
        }

        [SerializeField]
        private int curAnimalCount;
        public int CurAnimalCount
        {
            get
            {
                return curAnimalCount;
            }
            protected set
            {
                curAnimalCount = value;
            }
        }

        private IEnumerator CSpawn;
        private IEnumerator CDelete;
        #endregion

        #region MonoEvents
        public void Awake()
        {
            T isAnimal = animal.GetComponent<T>();

            if (isAnimal == null)
            {
                Debug.LogError("The prefab is not in the right place");
            }
        }

        private void Start()
        {
            CSpawn = Spawn();
            StartCoroutine(CSpawn);
        }

        private void FixedUpdate()
        {
            CurAnimalCount = GetComponentsInChildren<T>().Length;
        }

        private void Update()
        {
            if (LimitAnimalCount < CurAnimalCount)
            {
                if (CSpawn != null)
                {
                    StopCoroutine(CSpawn);
                    CSpawn = null;
                }

                CDelete = Delete();
                StartCoroutine(CDelete);
            }
            else if (CurAnimalCount < LimitAnimalCount)
            {
                if (CSpawn != null)
                {
                    StopCoroutine(CSpawn);
                }

                CSpawn = Spawn();
                StartCoroutine(CSpawn);

                if (CDelete != null)
                {
                    StopCoroutine(CDelete);
                    CDelete = null;
                }
            }
        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        private IEnumerator Spawn()
        {
            while (CurAnimalCount < LimitAnimalCount)
            {
                Instantiate(animal, EcoManager.GetRandomPosition(), Quaternion.identity, transform);

                yield return new WaitForFixedUpdate();
            }

            yield return null;
        }

        private IEnumerator Delete()
        {
            while (LimitAnimalCount < CurAnimalCount)
            {
                var animals = GetComponentsInChildren<T>() as Component[];
                
                Destroy(animals[CurAnimalCount - 1].gameObject);

                yield return new WaitForFixedUpdate();
            }

            yield return null;
        }
        #endregion
    }
}