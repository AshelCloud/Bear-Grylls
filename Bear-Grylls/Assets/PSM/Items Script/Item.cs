using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom.Compiler;
using UnityEngine.UI;

namespace PSM
{
    public abstract class Item
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public string Name { get { return name; } private set { name = value; } }
        public int Weight { get { return weight; } private set { weight = value; } }
        public Image Image { get { return image; } private set { image = value; } }

        protected string name;
        protected int weight;
        protected Image image;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Item()
        {
            Init();
        }
        public abstract void Init();
        public abstract void Use();
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}