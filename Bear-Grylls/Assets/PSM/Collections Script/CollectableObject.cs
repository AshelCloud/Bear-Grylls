using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PSM
{
    public interface HavingCollectableActionComponent
    {
        void ActionCollected();
    }

    public class DropableItem
    {
        public Type ItemType { get; private set; }
        public float DropChance { get; private set; }
        public int MaximumGain { get; private set; }

        private DropableItem() { }
        public DropableItem(string initString)
        {
            string[] splited = initString.Split(',');

            ItemType = Type.GetType("PSM." + splited[0]);
            DropChance = float.Parse(splited[1]);
            MaximumGain = int.Parse(splited[2]);
        }
        public ItemInfoDroped Drop()
        {
            int successNumber = 1;

            for(int i = 0; i < MaximumGain-1; ++i)
            {
                float rand = UnityEngine.Random.Range(0.0f, 1.01f);
                if (DropChance < rand)
                    ++successNumber;
            }
            
            return new ItemInfoDroped(ItemType, successNumber);
        }
    }
    public class ItemInfoDroped
    {
        public Type ItemType { get; private set; }
        public int ItemNumber { get; private set; }

        private ItemInfoDroped() { }
        public ItemInfoDroped(Type itemType, int itemNumber)
        {
            ItemType = itemType;
            ItemNumber = itemNumber;
        }
    }


    public class CollectableObject : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Type Itemtype { get; private set; }
        public DropableItem[] DropableItem { get { return dropableItem; } private set { dropableItem = value; } }

        [Header("아이템클래스타입(ItemBranch),확률(0.5),최대휙득갯수(20)")]
        [SerializeField] string[] collectableItemInfo;

        private DropableItem[] dropableItem;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public ItemInfoDroped[] DropItem()
        {
            ItemInfoDroped[] itemInfoDroped = new ItemInfoDroped[dropableItem.Length];
            for(int i = 0; i < itemInfoDroped.Length; ++i)
                itemInfoDroped[i] = dropableItem[i].Drop();

            return itemInfoDroped;
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            dropableItem = new DropableItem[collectableItemInfo.Length];
            for(int i = 0; i < dropableItem.Length ;++i)
                dropableItem[i] = new DropableItem(collectableItemInfo[i]);

        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}

