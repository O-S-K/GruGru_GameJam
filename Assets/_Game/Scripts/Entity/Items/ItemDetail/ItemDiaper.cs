using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDiaper : BaseItem
{
    public override void Action(Player player)
    {
        player.SetSpriteBullet(icon);
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}