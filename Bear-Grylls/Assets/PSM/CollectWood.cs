using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class CollectWood : CollectSomthing
    {
        protected override void Init()
        {

        }

        protected override void Process()
        {

        }

        protected override bool CollectCondition()
        {
            if(Input.GetKeyDown(KeyCode.F))
                return true;


            return false;
        }
    }
}

