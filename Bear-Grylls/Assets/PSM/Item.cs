using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom.Compiler;

namespace PSM
{
    [System.Serializable]
    public struct Information
    {
        public delegate void A();
        public A a;
    }

    public abstract class Item : MonoBehaviour
    {
        Information i;

        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        void asd()
        {
            i.a = () => 
            {
                //효과
            };
        }

        void FASDF()
        {
            i.a();
        }

        public new string name { get; private set; }
        public string information { get; private set; }
        public int weight { get; private set; }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        /// <summary>
        /// name, information, weight등의 값을 초기화해주세요 Init을 사용하지 않아도 됩니다
        /// </summary>
        public abstract void Init();
        public abstract void Use();
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}