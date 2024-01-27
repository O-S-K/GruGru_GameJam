using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : BaseCharacter
{
    public bool ableItemShield = false;
    private GameObject itemShield;

    public bool ableItemScaleBullet = false;
    private float _scaleBullet = 1;

    public bool ableChangeSprite = false;
    public Sprite spriteBullet;
    public Sprite spriteBulletShitDefault;

    public override void InitData()
    {
        base.InitData();
        ableItemShield = false;
        ableItemScaleBullet = false;
        RemoveSpriteBullet();
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
        if(_amountBulletOfGun <= 5)
        {
            _amountBulletOfGun++;
        }
        else
        {
            _amountBulletOfGun = 5;
        }
    }

    public void IncreasesHealth()
    {
        _heath.RestoreFullHP();

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

    public void SetScaleBullet()
    {
        ableItemScaleBullet = true;
        Invoke(nameof(RemoveScaleBullet), 8);
    }

    public void RemoveScaleBullet()
    {
        ableItemScaleBullet = false;
    }

    public void SetSpriteBullet(Sprite sprite)
    {
        ableChangeSprite = true;
        spriteBullet = sprite;

        Invoke(nameof(RemoveSpriteBullet), 8);
    }


    public void RemoveSpriteBullet()
    {
        ableChangeSprite = false;
        spriteBullet = spriteBulletShitDefault;
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
