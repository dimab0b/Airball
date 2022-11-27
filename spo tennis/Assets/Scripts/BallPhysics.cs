using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallPhysics : NetworkBehaviour
{
    Rigidbody2D rb;
    Vector3 _velocity;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _velocity = rb.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = _velocity.magnitude;
        var direction = Vector3.Reflect(_velocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f); 
    }
}
