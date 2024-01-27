using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonus : Enemy
{
    private float _timeLoopColor = 0.1f;
    private bool _setColor = false; 

    protected override void Update()
    {
        if(_timeLoopColor <= 0)
        {
            _timeLoopColor = 0.1f;
            _setColor = !_setColor;
            _duck.Sprite().color = _setColor ? Color.white : Color.magenta;
        }
        else
        {
            _timeLoopColor -= Time.deltaTime;
        }
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
