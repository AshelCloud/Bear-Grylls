using MalbersAnimations;
using MalbersAnimations.Utilities;
using UnityEngine;

namespace YGW
{
    public class Tiger : AnimalAI
    {
        #region Variable
        #endregion

        #region MonoEvents
        private void Start()
        {
            StartAgent();
        }

        private void Update()
        {
            Deer isDeer = null;

            if (Look.Target != null)
            {
                isDeer = Utils.GetRoot(Look.Target).GetComponent<Deer>();

                if (isDeer != null)
                {
                    State = STATE.HUNT;
                    SetTarget(Look.Target);

                    if (Vector3.Distance(Look.Target.transform.position, transform.position) < AttackDistance)
                    {
                        if(isDeer.GetComponent<Animal>().Death)
                        {
                            AnimalComponent.SetAction(2);
                        }
                        else if(AttackTime >= AttackRate)
                        {
                            AttackTime = 0f;
                            AnimalComponent.SetAttack(1);
                        }
                    }
                }
            }

            base.Updating();
        }
        #endregion

        #region Function
        #endregion

        #region Coroutine
        #endregion
    }
}