using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRocketMissle : BaseItem
{
    public BulletRockketMissle bulletRocketPrefab;
    public override void Action(Player player)
    {
        for (int i = 0; i < 3; i++)
        {
            var bullet = Instantiate(bulletRocketPrefab, player.transform.position, Quaternion.identity);
            bullet.transform.parent = null;
            bullet.Init(player, player.direction);
        }
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}