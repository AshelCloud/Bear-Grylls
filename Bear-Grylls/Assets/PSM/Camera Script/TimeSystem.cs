using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class TimeSystem : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        [Header("- Required Setting -")]
        [SerializeField] Transform skyDoomShader;
        [SerializeField] Transform dayLight;
        [SerializeField] Transform nightLight;


        [Header("- Option -")]
        [SerializeField] float defaultTime = 0;



        public float time = 0.01f;
        private const float maxTime = 10;
        private float halfTime;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Function - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region Coroutine - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            time = defaultTime;
            halfTime = maxTime / 2;
        }
        protected virtual void Start()
        {

        }
        protected virtual void Update()
        {
            time += Time.deltaTime;


               



            Vector3 lightAngle = dayLight.rotation.eulerAngles;
            lightAngle.x = 360 * (time / maxTime) - 90;
            dayLight.rotation = Quaternion.Euler(lightAngle.x, 0, 0);
            nightLight.rotation = Quaternion.Euler(lightAngle.x+180, 0, 0);


            
            if(time < halfTime)
            {
                float v = (time / halfTime);
                skyDoomShader.GetComponent<Renderer>().material.SetColor("_ColorTop", new Color(v, v, v, 1));
                print(v);
            }
            else if(halfTime <= time)
            {
                
                float v = 1-((time- halfTime) / halfTime);
                print(v);
                skyDoomShader.GetComponent<Renderer>().material.SetColor("_ColorTop", new Color(v, v, v, 1));
            }

            //0시  밤이여야함 color 0000 밤
            //12시 낮이여야함 color 1111 낮
            //24시 밤이여야함 color 0000 밤

            //1111 낮
            //0000 밤



            if (maxTime < time)
            {
                time = 0.01f;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}