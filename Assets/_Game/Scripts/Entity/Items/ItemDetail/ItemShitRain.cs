using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShitRain : BaseItem
{
    public Projectile itemRaintPrefab;
    public override void Action(Player player)
    {
        for (int i = 0; i < 100; i++)
        {
            var itemBullet = Instantiate(itemRaintPrefab);
            itemBullet.transform.parent = null;
            itemBullet.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(8, 20), 0);
            itemBullet.Init(player, Vector3.down);
            itemBullet.GetRig().gravityScale = Random.Range(1f, 2f);
        }

        //GameManger.Instance.CamMain.ShakeCamera()
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}