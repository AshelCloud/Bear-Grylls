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
        /// <summary>
        /// 인벤토리에 아이템을 넣습니다
        /// </summary>
        /// <returns> 성공시 True를 반환, 무게 초과시 아이템 넣길 거부하고 False를 반환 </returns>
        public bool AddItem()
        {
            print("success");

            return true;
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


