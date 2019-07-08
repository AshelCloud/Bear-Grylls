using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class TigerSpawn : MonoBehaviour
    {
        #region Variable
        [SerializeField]
        private GameObject tiger;
        #endregion

        #region MonoEvents
        private void Awake()
        {
            Tiger isTiger = tiger.GetComponent<Tiger>();

            if (isTiger == null)
            {
                Debug.LogError("This Prefab is not Tiger!");
            }
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        private IEnumerator Spawn()
        {
            Instantiate(tiger, transform);
            //Instantiate(tiger, EcoManager.GetRandomPosition(), Quaternion.identity, transform);

            yield return null;
        }
        #endregion
    }
}