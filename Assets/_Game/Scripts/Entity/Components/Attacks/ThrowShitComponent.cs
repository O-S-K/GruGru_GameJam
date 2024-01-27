using System;
using System.Collections;
using UnityEngine;

public class ThrowShitComponent : MonoBehaviour, IAttack
{
    private BaseCharacter entity;
    public Projectile projectile;
    public Transform pointSpawn;

    public float speed = 5;
    public float timeDestroyed = 3;
    public float fireRate = 1;
    public float fireRateDelay;
    private float currentFireRate;

    private void Awake()
    {
        entity = GetComponent<BaseCharacter>();
    }


    private void Start()
    {
        currentFireRate = fireRate;
    }

    private float GetFireRate()
    {
        if (entity.typeChar == BaseCharacter.ETypeChar.Player)
        {
            var p = (Player)entity;
            return fireRate = p.FireRate;
        }
        else
        {
            return fireRate + UnityEngine.Random.Range(-fireRateDelay, fireRateDelay);
        }
    }

    public void Attack(Entity target)
    {
        if (currentFireRate >= GetFireRate())
        {
            currentFireRate = 0;
            entity.Baby.Attack();
            StartCoroutine(IECreateBullet(target));
        }
        else
        {
            currentFireRate += Time.deltaTime;
        }
    }

    private IEnumerator IECreateBullet(Entity target)
    {
        yield return new WaitForSeconds(0.1f);
        var bullet = Instantiate(projectile, pointSpawn.position, Quaternion.identity);
        Vector2 direction = (target.transform.position - transform.position).normalized;

        bullet.Init(this.entity, direction);
        bullet.transform.parent = null;
        bullet.GetRig().AddForce(direction * speed * 100);
        bullet.Destroyd(timeDestroyed);
    }
}
