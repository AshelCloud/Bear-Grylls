using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSM
{
    public class TimeSystem : MonoBehaviour
    {
        #region Variable - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static float time = 0.01f;

        [Header("- Required Setting -")]
        [SerializeField] Transform skyDoomShader;
        [SerializeField] Transform dayLight;
        [SerializeField] Transform nightLight;


        [Header("- Option -")]
        [SerializeField] float defaultTime = 0;

        private const float maxTime = 48*60*60;
        private float halfTime;
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        #region MonoEvents - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        protected virtual void Awake()
        {
            time = defaultTime;
            halfTime = maxTime / 2;
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
            }
            else if(halfTime <= time)
            {
                
                float v = 1-((time- halfTime) / halfTime);
                print(v);
                skyDoomShader.GetComponent<Renderer>().material.SetColor("_ColorTop", new Color(v, v, v, 1));
            }

            if (maxTime < time)
            {
                time = 0.01f;
            }
        }
        #endregion - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}