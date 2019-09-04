using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 toTarget = target.position - transform.position;

        _rigidbody.velocity = toTarget * speed;
    }
}
