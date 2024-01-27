using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : BaseCharacter
{
    public bool ableItemShield = false;
    private GameObject itemShield;



    protected override void Update()
    {
        base.Update();

        if (GameManger.Instance.stateGame != GameManger.EGameState.Playing)
            return;
        if (IsDie)
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

        if (IsDie)
            return;

        GetInput();
        Fly();
    }

    public void UpgradeFireRate()
    {
        if (_fireRate >= 0.2f)
        {
            _fireRate -= 0.2f;
        }
        else
        {
            _fireRate = 0.2f;
        }
    }

    public void BonusProjectile()
    {
    }

    public void IncreasesHealth()
    {
        _heath.SetMaxHeath((int)_heath.MaxHealth + 1);
    }

    public void AddItemShield(GameObject shield)
    {
        if (itemShield != null)
        {
            Destroy(itemShield);
        }

        itemShield = Instantiate(shield);
        itemShield.transform.parent = transform;
        itemShield.transform.localPosition = Vector3.zero;
        ableItemShield = true;
    }

    public void RemoveItemShield()
    {
        ableItemShield = false;
        Destroy(itemShield);
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
