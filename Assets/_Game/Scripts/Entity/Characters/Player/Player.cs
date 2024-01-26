using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : BaseCharacter 
{
    protected override void Update()
    {
        base.Update();

        if (GameManger.Instance.stateGame != GameManger.EGameState.Playing)
            return;

        FindNearestObject(targets, data.GetSeachRadiusTarget()); 
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (GameManger.Instance.stateGame == GameManger.EGameState.Pause)
            return;

        if (GameManger.Instance.stateGame == GameManger.EGameState.Completed)
            return;

        if (GameManger.Instance.stateGame == GameManger.EGameState.Faild)
            return;

        GetInput();
        Fly();
    }

    protected void GetInput()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate velocity based on input
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        velocity = inputVector.normalized * data.GetSpeedMovement();
    }

    protected override void Fly()
    {
        // Apply velocity to the rigidbody

        _rigidbody2D.velocity = velocity;

        // Giới hạn vị trí trong hình vuông
        float clampedX = Mathf.Clamp(_rigidbody2D.position.x, -8, 8);
        float clampedY = Mathf.Clamp(_rigidbody2D.position.y, -4.5f, 4.2f);

        transform.position = new Vector2(clampedX, clampedY);

        // Apply inertia to simulate slowing down
        //velocity *= _inertia;
        base.Fly();
    }

    protected override void Die()
    {
        base.Die();

        GameManger.Instance.FaildMission();
        GameManger.Instance.CamMain.ShakeCamera(0.2f, 0.3f, 10, Ease.InOutBack, 0.5f, Ease.OutBack);
    }
}
