using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class InventoryItem
    {
        public Item item { get; set; } = null;
        public int number { get; set; } = 0;

        public InventoryItem(Item item, int number)
        {
            this.item = item;
            this.number = number;
        }
    }


    public class Inventory : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public int MaxWeight { get{ return maxWeight; } set { maxWeight = value; } }

        [SerializeField] int maxWeight = 100;

        private List<InventoryItem> itemList = new List<InventoryItem>();

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void AddItem(Item item, int number)
        {
            InventoryItem findInvenItem = itemList.Find((InventoryItem inventoryItem) =>
            {
                if(inventoryItem.item.GetType() == item.GetType())
                {
                    return true;
                }
                return false;
            });

            if(findInvenItem != null)//이미 있는 아이템을 추가 하는 경우
                ++findInvenItem.number;
            else//새로운 종류의 아이템을 추가하는 경우
            {
                itemList.Add(new InventoryItem(item, number));
            }
        }
        public void RemoveItem(Type itemType, int number)
        {
            InventoryItem findInvenItem = itemList.Find((InventoryItem inventoryItem) =>
            {
                if (inventoryItem.item.GetType() == itemType)
                {
                    return true;
                }
                return false;
            });

            findInvenItem.number -= number;
            if (findInvenItem.number <= 0)
                itemList.Remove(findInvenItem);
        }
        public void RemoveItemAll(Type itemType)
        {
            InventoryItem findInvenItem = itemList.Find((InventoryItem inventoryItem) =>
            {
                if (inventoryItem.item.GetType() == itemType)
                {
                    return true;
                }
                return false;
            });

            itemList.Remove(findInvenItem);
        }
        public bool IsAddable(Type itemType)
        {
            return true;
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}


