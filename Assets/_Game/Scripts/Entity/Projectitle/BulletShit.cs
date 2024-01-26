using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShit : Projectile
{
    [SerializeField] private float decelerationForce = 10;
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector3.Lerp(_rigidbody2D.velocity, new Vector2(direction.normalized.x, -5f), Time.fixedDeltaTime * decelerationForce);
    }
}
