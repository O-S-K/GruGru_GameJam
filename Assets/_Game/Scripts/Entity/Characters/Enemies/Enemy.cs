using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    protected override void Update()
    {
        base.Update();

        if (GameManger.Instance.stateGame != GameManger.EGameState.Playing)
            return;

        FindNearestObject(targets, data.GetSeachRadiusTarget());
    }

    protected override void Die()
    {
        base.Die();
        GameManger.Instance.RemoveEnemyDie(this);
    }

}
