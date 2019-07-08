using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;
using MalbersAnimations.Utilities;

namespace YGW
{
    /// <summary>
    /// 사용하지 않을 클래스
    /// </summary>
    [RequireComponent(typeof(AnimalAIControl))]
    public class AnimalCustomAI : MonoBehaviour
    {
        #region Variable
        private AnimalAIControl aiControl;

        public GameObject map;
        #endregion

        #region MonoEvents
        private void Awake()
        {
            aiControl = GetComponent<AnimalAIControl>();

            if(map == null)
            {
                map = GameObject.Find("Map");
            }

            SetStartWayPoint();
        }

        private void Start()
        {
        }
        #endregion

        #region Function
        private void SetStartWayPoint()
        {
            List<MWayPoint> wayPoints = new List<MWayPoint>();

            for(int i = 0; i < Random.Range(100, 150); i ++)
            {
                GameObject obj = new GameObject();
                
                obj.name = "WayPoint(" + i.ToString() + ")";

                var wayPoint = obj.AddComponent<MWayPoint>();
                wayPoint.transform.position = GetRandomPosition();

                wayPoints.Add(wayPoint);
            }

            aiControl.SetTarget(wayPoints[Random.Range(0, wayPoints.Count)].transform);

            for(int i = 0; i < wayPoints.Count; i ++)
            {
                List<Transform> transforms = new List<Transform>();

                for(int j = 0; j < wayPoints.Count; j ++)
                {
                    if(i == j) { continue; }

                    transforms.Add(wayPoints[j].transform);
                }

                wayPoints[i].NextTargets = transforms;
            }
        }

        private void SetNextWayPoint(Transform transform)
        {
        }

        private void SetNextWayPoints(List<MWayPoint> wayPoints)
        {
            
        }

        private Vector3 GetRandomPosition()
        {
            RaycastHit hit;

            Vector3 scale = map.transform.lossyScale;
            
            Terrain terrain = map.GetComponent<Terrain>();

            Vector3 pos = new Vector3(Random.Range(map.transform.position.x, map.transform.position.x + terrain.terrainData.size.x), 10000f, Random.Range(map.transform.position.z, map.transform.position.z + terrain.terrainData.size.z));
            
            Ray ray = new Ray(pos, Vector3.down);

            if(Physics.Raycast(ray, out hit, float.MaxValue))
            {
                pos.y = hit.point.y;
            }
            else
            {
                pos.y = Random.Range(map.transform.position.y, map.transform.position.y + terrain.terrainData.size.y);
            }

            return pos;
        }
        #endregion
    }
}