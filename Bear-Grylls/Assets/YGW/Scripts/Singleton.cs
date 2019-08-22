using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private readonly static object padlock = new object();
        private static T instance;

        public static T Instance
        {
            get
            {
                lock (padlock)
                    if (instance == null)
                    {
                        instance = FindObjectOfType(typeof(T)) as T;

                        if (instance == null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString();
                        }
                    }

                return instance;
            }
        }

    }
}