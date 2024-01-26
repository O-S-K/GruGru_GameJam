using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.GetComponent<Player>();
        if (p != null)
        {
            if (!p.IsDie)
            {
                p.GetComponent<HealthSystem>().Death();
            }
        }
    }
}
