using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class Utils : MonoBehaviour
    {
        public static Transform GetRoot(Transform obj)
        {
            Transform root = obj.root;

            var animals = root.GetComponentsInChildren<Animal>();

            for (int i = 0; i < animals.Length; i++)
            {
                var transforms = animals[i].GetComponentsInChildren<Transform>();

                for (int j = 0; j < transforms.Length; j++)
                {
                    if (obj == transforms[j])
                    {
                        return animals[i].transform;
                    }
                }
            }

            return null;
        }
    }
}