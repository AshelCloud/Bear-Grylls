using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class WolfManager : AnimalSpawn<Wolf>
    {
        #region Variable
        private int packCnt { get; set; } = 0;
        #endregion

        #region MonoEvents
        private new void Update()
        {
            base.Update();
            
            packCnt = (int)Mathf.Ceil(animals.Count / 10f);

            int headCnt = 0;
            foreach(var ani in base.animals)
            {
                headCnt += ani.GetComponent<Wolf>().IsHead ? 1 : 0;
            }

            int cnt = packCnt - headCnt;

            for(int i = 0; i < cnt; i ++)
            {
                int ran = Random.Range(0, base.animals.Count);
                var wolf = base.animals[ran].GetComponent<Wolf>();

                if(wolf.IsHead == false)
                {
                    wolf.IsHead = true;
                }
            }

            GameObject curHead = null;
            for(int i = 0; i < base.animals.Count; i ++)
            {
                var wolf = animals[i].GetComponent<Wolf>();

                if(wolf.IsHead == true && wolf.LoadCnt < Wolf.MaxLoadCnt)
                {
                    curHead = animals[i];
                }
            }

            for(int i = 0; i < base.animals.Count; i ++)
            {
                var wolf = animals[i].GetComponent<Wolf>();

                if(wolf.IsHead == false)
                {
                    wolf.Head = curHead;
                }
            }
        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        #endregion
    }
}