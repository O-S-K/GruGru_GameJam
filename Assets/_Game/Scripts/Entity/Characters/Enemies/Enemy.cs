using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public void SetBlockMove()
    {

    }


    protected Color GetRandomColor()
    {
        // Generate random values for red, green, and blue components
        float randomRed = Random.Range(0f, 1f);
        float randomGreen = Random.Range(0f, 1f);
        float randomBlue = Random.Range(0f, 1f);

        // Create and return the random color
        Color randomColor = new Color(randomRed, randomGreen, randomBlue);

        return randomColor;
    }

    public override void InitData()
    {

        var colorCreep = GameManger.Instance.colorEnemyCreeps;
        var newColor = colorCreep[Random.Range(0, colorCreep.Length)];

        if (typeChar != ETypeChar.Player && typeChar != ETypeChar.Boss)
        {
            if (_duck != null) _duck.Sprite().color = newColor;
            if (_baby != null) _baby.Sprite().color = GetRandomColor();
        }

        base.InitData();
    }
    protected override void Update()
    {
        base.Update();

        if (GameManger.Instance.stateGame != GameManger.EGameState.Playing)
            return;

        if (IsDie)
            return;

        FindNearestObject(targets, data.GetSeachRadiusTarget());
    }

    protected override void Die()
    {
        base.Die();
        //AudioManager.Instance.PlayOneShot($"Headgib-{Random.Range(1, 5)}", 0.25f, 0);
         AudioManager.Instance.PlayOneShot($"PlayerDie", 1, 0, Random.Range(0.9f, 1.1f));

        GameManger.Instance.CamMain.ShakeCamera(0.1f, 0.3f, 10, Ease.InOutBack, 0.25f, Ease.OutBack);
        GameManger.Instance.RemoveEnemyDie(this);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (IsDie)
            return;
        Fly();
    }

    protected override void Fly()
    {
        base.Fly();
    }

}
