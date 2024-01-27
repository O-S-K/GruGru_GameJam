using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
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
                MoveToDir(Vector2.zero);
            }
            else
            {
                MoveToDir(p.transform.position);
            }
        }
    }

    private void MoveToDir(Vector2 target)
    {
        if (Vector3.Distance(transform.position, target) > 1)
        {
            _rigidbody2D.velocity = ((Vector2)target - _rigidbody2D.position).normalized * data.GetSmoothSpeed();
        }
    }
}
