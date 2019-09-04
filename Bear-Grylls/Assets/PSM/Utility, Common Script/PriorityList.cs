using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public struct PriorityData<T>
    {
        public T data;
        public int priority;

        public PriorityData(T data, int priority)
        {
            this.data = data;
            this.priority = priority;
        }
    }

    public class PriorityList<T>
    {
        private List<PriorityData<T>> priorityList = new List<PriorityData<T>>();
        private void Sort()
        {
            priorityList.Sort(delegate (PriorityData<T> a, PriorityData<T> b)
            {
                return a.priority.CompareTo(b.priority);
            });
        }
        public delegate bool parameter(PriorityData<T> data);

        public PriorityData<T> this[int index]
        {
            get
            {
                return priorityList[index];
            }
            set
            {
                priorityList[index] = value;
                Sort();
            }
        }
        public int Count { get { return priorityList.Count; } }
        public void Add(PriorityData<T> data)
        {
            priorityList.Add(data);
            Sort();
        }
        public bool Remove(PriorityData<T> data)
        {
            bool result = priorityList.Remove(data);
            Sort();

            return result;
        }
        public void SetPriority(System.Predicate<PriorityData<T>> match, int priority)
        {
            PriorityData<T> data = priorityList.Find(match);
            data.priority = priority;
        }
    }
}