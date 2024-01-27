using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override void InitData()
    {
        base.InitData();
    }

    protected override void Fly()
    {
        base.Fly();

        if (GameManger.Instance.stateGame != GameManger.EGameState.Playing)
            return;

        var p = GameManger.Instance.player;
        if (p != null)
        {
            if (p.IsDie)
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                if (Vector3.Distance(transform.position, p.transform.position) > 1)
                {
                    _rigidbody2D.velocity = ((Vector2)p.transform.position - _rigidbody2D.position).normalized * data.GetSmoothSpeed();
                }
            }
        }

    }
}
