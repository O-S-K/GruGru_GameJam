using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieComponent : MonoBehaviour
{
    [SerializeField] private GameObject vfxWater;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.GetComponent<Player>();
        if (p != null)
        {
            if (!p.IsDie)
            {
                p.GetComponent<HealthSystem>().Death();
                p.gameObject.SetActive(false);
                var vfx = Instantiate(vfxWater, p.transform);
                vfx.transform.parent = null;
                vfx.transform.position = p.transform.position + new Vector3(0, -0.2f, 0);
                Destroy(vfx, 1.2f);
                // TODO: play Vfx trigger water
                //p.ResetVel();
                //p.GetRig().gravityScale = 0.5f;
                //p.transform.DOScale(0, 1f).SetEase(Ease.Linear);
            }
        }
    }
}
