using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public class EcoManager : MonoBehaviour
    {
        private static Terrain map;
        private static Terrain Map
        {
            get
            {
                if (map == null)
                {
                    map = GameObject.Find("Map").GetComponent<Terrain>();
                }
                return map;
            }
        }

        private void Awake()
        {

        }

        private void Start()
        {

        }

        public static Vector3 GetRandomPosition()
        {
            RaycastHit hit;

            Vector3 scale = Map.transform.lossyScale;
            
            Vector3 mapPos = Map.transform.position;

            Vector3 pos = new Vector3(Random.Range(mapPos.x, mapPos.x + Map.terrainData.size.x),
                                    10000f, Random.Range(mapPos.z, mapPos.z + Map.terrainData.size.z));

            Ray ray = new Ray(pos, Vector3.down);

            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                pos.y = hit.point.y;
            }
            else
            {
                pos.y = Random.Range(Map.transform.position.y, Map.transform.position.y + Map.terrainData.size.y);
            }

            return pos;
        }
    }
}