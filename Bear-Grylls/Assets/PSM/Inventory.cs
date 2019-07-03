using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class InventoryItem
    {
        public Item item { get; set; } = null;
        public int number { get; set; } = 0;
    }


    public class Inventory : MonoBehaviour
    {
        #region Variable
        private List<InventoryItem> itemList = new List<InventoryItem>();
        #endregion


        #region Function
        public void AddItem()
        {

        }
        public void RemoveItem()
        {

        }
        public Item TakeOutItem(string itemName)
        {
            return null;
        }
        #endregion
    }
}


