using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGW
{
    public sealed class EcoManager : Singleton<EcoManager>
    {
        [SerializeField]
        private Terrain map;
        public Terrain Map
        {
            get
            {
                if (map == null)
                {
                    map = Terrain.activeTerrain;
                }
                return map;
            }
        }

        public TerrainCollider MapCollider
        {
            get
            {
                return Map.GetComponent<TerrainCollider>();
            }
        }

        public Vector3 GetRandomPosition()
        {
            Vector3 scale = Map.transform.lossyScale;

            Vector3 mapPos = Map.transform.position;
            Vector3 pos = new Vector3(Random.Range(mapPos.x, mapPos.x + Map.terrainData.size.x),
                                    1000f, Random.Range(mapPos.z, mapPos.z + Map.terrainData.size.z));

            return ToTerrainPosition(pos);
        }

        public Vector3 ToTerrainPosition(Vector3 vector)
        {
            RaycastHit hit;
            Ray ray = new Ray(vector, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Terrain")))
            {
                vector.y = GetTerrainHeightAtPoint(hit.point);
            }

            return vector;
        }

        public float GetTerrainHeightAtPoint(Vector3 position)
        {
            return Map.SampleHeight(position);
        }
    }
}