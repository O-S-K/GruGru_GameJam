using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonus : Enemy
{

    protected override void Update()
    {
    }

    protected override void Idle()
    {
        if (IsDie)
            return;

        stateChar = EStateChar.Idle;
        _duck.Idle();
    }


    protected override void Attack(Entity target)
    {
    }


    protected override void Hit(Vector2 dir, int damageValue, DamagePopup.ETypeDamage typeDamage)
    {
        if (IsDie)
            return;

        _duck.Hit();
        CreateManager.Instance.CreateTextDamagePopup(transform.position, (int)damageValue, typeDamage);
    }

    protected override void Die()
    {
        base.Die();

        var itemDrop = CreateManager.Instance.CreateItemEnemyDrop();
        itemDrop.transform.position = transform.position;
        itemDrop.transform.parent = null;
    }
}
