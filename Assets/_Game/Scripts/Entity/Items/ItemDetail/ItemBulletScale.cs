using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBulletScale : BaseItem
{
    public override void Action(Player player)
    {
        AudioManager.Instance.PlayOneShot("Upgrade");
        player.SetScaleBullet();
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}
