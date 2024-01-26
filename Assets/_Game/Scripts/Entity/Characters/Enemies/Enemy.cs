using DG.Tweening;
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

        GameManger.Instance.CamMain.ShakeCamera(0.1f, 0.3f, 10, Ease.InOutBack, 0.25f, Ease.OutBack);
        GameManger.Instance.RemoveEnemyDie(this);
    }

}
