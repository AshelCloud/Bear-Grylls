using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardState : MonoBehaviour
{
    [SerializeField]
    private float life;
    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;

            if(life > 0)
            {
                //TO DO
            }
            else
            {
                life = 0;

                Die();
            }
        }
    }
    
    public void GetDamaged(float value)
    {
        Life -= value;
    }

    public void Die()
    {
        //TO DO
    }
}
