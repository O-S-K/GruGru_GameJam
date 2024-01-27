using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamagePopup;

public class ItemMilkBottle : BaseItem
{
    public int healthPlus = 3;
    public override void Action(Player player)
    {
        AudioManager.Instance.PlayOneShot("Sfx_HealUp");
        player.GetComponent<HealthSystem>().TakeDamage(transform, healthPlus);
        CreateManager.Instance.CreateTextDamagePopup(transform.position, healthPlus, ETypeDamage.BuffHeal);
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}
