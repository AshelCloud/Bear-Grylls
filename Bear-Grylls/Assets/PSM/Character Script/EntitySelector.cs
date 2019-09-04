using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PSM
{
    public class EntitySelector : MonoBehaviour, UsingSubColliderComponent
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [SerializeField] string[] tagsOfSelectObject;


        public GameObject SelectedObject { get; private set; } = null;

        private List<GameObject> nearObjects = new List<GameObject>();
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void UpdateSelectedObject()
        {
            if(nearObjects.Count == 0)
            {
                SelectedObject = null;
                return;
            }


            float minDistance = Vector3.Distance(transform.position, nearObjects[0].transform.position);
            SelectedObject = nearObjects[0];

            for (int i = 1; i < nearObjects.Count; ++i)
            {
                float distance = Vector3.Distance(transform.position, nearObjects[i].transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    SelectedObject = nearObjects[i];
                }
            }


            foreach(GameObject each in nearObjects)
            {
                if(each == SelectedObject)
                    SetOutLine(each, true);
                else
                    SetOutLine(each, false);
            }
        }
        private void SetOutLine(GameObject gameObj, bool enableValue)
        {
            cakeslice.Outline outLine = gameObj.GetComponent<cakeslice.Outline>();
            if (outLine == null)
                return;

            outLine.enabled = enableValue;
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void Update()
        {
            UpdateSelectedObject();

            if(SelectedObject != null)
            {
                Renderer renderer = SelectedObject.GetComponent<Renderer>();
                if (renderer != null && renderer.enabled != true)
                {
                    SetOutLine(SelectedObject.gameObject, false);
                    nearObjects.Remove(SelectedObject);
                    SelectedObject = null;
                }
            }
        }
        public void SubColliderTriggerEnter(Collider other)
        {
            foreach (string tag in tagsOfSelectObject)
            {
                Renderer renderer = other.GetComponent<Renderer>();
                if(renderer != null && renderer.enabled != true)
                    return;

                if (other.tag == tag)
                {
                    nearObjects.Add(other.gameObject);
                    break;
                }
            }
        }
        public void SubColliderTriggerExit(Collider other)
        {
            foreach (string tag in tagsOfSelectObject)
            {
                Renderer renderer = other.GetComponent<Renderer>();
                if (renderer != null && renderer.enabled != true)
                    return;

                if (other.tag == tag)
                {
                    nearObjects.Remove(other.gameObject);
                    SetOutLine(other.gameObject, false);
                    break;
                }
            }
        }
        public void SubColliderTriggerStay(Collider other)
        {

        }


        public void SubCollisionEnter(Collision collision) { }
        public void SubCollisionStay(Collision collision) { }
        public void SubCollisionExit(Collision collision) { }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}