using UnityEngine;

public class Enemy2 : Enemy
{
    public float timeRandomMovement = 3;
    private float timeRandom;
    private Vector2 newPos;

    public override void InitData()
    {
        base.InitData();
        timeRandom = timeRandomMovement + Random.Range(-1f, 1f);
    }

    protected override void Fly()
    {
        base.Fly();

        if (GameManger.Instance.stateGame != GameManger.EGameState.Playing)
            return;


        if (Vector3.Distance(transform.position, newPos) > 1)
        {
            _rigidbody2D.velocity = (newPos - _rigidbody2D.position).normalized * data.GetSpeedMovement();
        }
        else
        {
            if (timeRandom <= 0)
            {
                newPos = new Vector2(Random.Range(-8f, 8f), Random.Range(-3.5f, 4f));

            }
            else
            {
                timeRandom -= Time.deltaTime;
            }
        }
    }
}
