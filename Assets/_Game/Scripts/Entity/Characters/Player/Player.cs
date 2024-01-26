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
        _rigidbody2D.velocity = Vector3.Lerp(_rigidbody2D.velocity, velocity, Time.fixedDeltaTime * data.GetSmoothSpeed());

        // Apply inertia to simulate slowing down
        //velocity *= _inertia;
        base.Fly();
    }
}
