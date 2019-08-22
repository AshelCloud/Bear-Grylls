using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    [RequireComponent(typeof(BoxCollider))]
    public class AnimalSpawn<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Variable
        [SerializeField]
        protected GameObject animal;

        [SerializeField]
        protected List<GameObject> animals;

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
        private BoxCollider boxCollider;
        public BoxCollider BoxCollider
        {
            get
            {
                if (boxCollider == null)
                {
                    boxCollider = GetComponent<BoxCollider>();
                }
                return boxCollider;
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

        protected void Update()
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
        private Vector3 RandomPositionInArea()
        {
            Vector3 minVector = new Vector3(transform.position.x - BoxCollider.size.x, BoxCollider.size.y / 2, transform.position.z - BoxCollider.size.z);
            Vector3 maxVector = new Vector3(transform.position.x + BoxCollider.size.x, BoxCollider.size.y / 2, transform.position.z + BoxCollider.size.z);

            Vector3 random = new Vector3(Random.Range(minVector.x, maxVector.x), Random.Range(minVector.y, maxVector.y), Random.Range(minVector.z, maxVector.z));

            RaycastHit hit;
            Ray ray = new Ray(random, Vector3.down);

            if (EcoManager.Instance.MapCollider.Raycast(ray, out hit, Mathf.Infinity))

            {
                random.y = hit.transform.position.y;
            }

            return random;
        }
        #endregion

        #region Coroutine
        private IEnumerator Spawn()
        {
            while (CurAnimalCount < LimitAnimalCount)
            {
                var ani = Instantiate(animal, RandomPositionInArea(), Quaternion.identity, transform);

                animals.Add(ani);

                yield return new WaitForFixedUpdate();
            }

            yield return null;
        }

        private IEnumerator Delete()
        {
            while (LimitAnimalCount < CurAnimalCount)
            {
                var anis = GetComponentsInChildren<T>() as Component[];

                animals.Remove(anis[CurAnimalCount - 1].gameObject);
                Destroy(anis[CurAnimalCount - 1].gameObject);

                yield return new WaitForFixedUpdate();
            }

            yield return null;
        }
        #endregion

    }
}