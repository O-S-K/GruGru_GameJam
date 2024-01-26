using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDamageable
{
    public bool TakeDamage(Transform target, float damage);
    public void Hit(bool was);
    public bool WasHit();
}

