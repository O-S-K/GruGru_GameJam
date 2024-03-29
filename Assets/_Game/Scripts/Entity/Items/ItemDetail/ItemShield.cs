using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : BaseItem
{
    public GameObject shieldBlockPrefab;
    public override void Action(Player player)
    {
        AudioManager.Instance.PlayOneShot("WeaponLevelUp");
        player.AddItemShield(shieldBlockPrefab);
    }

    public override void DestroyItem()
    {
        Destroy(gameObject);
    }

    public override void OnEnable()
    {
    }
}
