using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class TestFirst : MonoBehaviour
    {
        private void Awake()
        {
            Type t = Type.GetType("PSM.ItemBranch");
            Item asd = (Item)Activator.CreateInstance(t);

            ItemBranch dsa = new ItemBranch();


            print(asd);

        }
    }
}


