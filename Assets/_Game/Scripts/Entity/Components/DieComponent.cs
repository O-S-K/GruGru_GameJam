using DG.Tweening;
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
                p.gameObject.SetActive(false);
                // TODO: play Vfx trigger water
                //p.ResetVel();
                //p.GetRig().gravityScale = 0.5f;
                //p.transform.DOScale(0, 1f).SetEase(Ease.Linear);
            }
        }
    }
}
