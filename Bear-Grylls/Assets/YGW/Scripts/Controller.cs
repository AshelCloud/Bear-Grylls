using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
    }
}
