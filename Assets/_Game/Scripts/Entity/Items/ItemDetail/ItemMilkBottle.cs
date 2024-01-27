using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMilkBottle : BaseItem
{
    public override void Action(Player player)
    {
        player.GetComponent<HealthSystem>().TakeDamage(transform, 1);
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}
