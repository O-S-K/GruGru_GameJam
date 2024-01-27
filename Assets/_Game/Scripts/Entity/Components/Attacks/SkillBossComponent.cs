using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBossComponent : MonoBehaviour, IAttack
{
    private BaseCharacter entity;
    public Projectile[] projectiles;
    public Transform pointSpawn;

    public float speed = 5;
    public float timeDestroyed = 3;
    public float fireRate = 1;
    public float fireRateDelay;
    private float currentFireRate;

    public float multipleProjectilesAngle = 45f;
    public float amountOfGuns = 1;
    public float scaleBullet = 1;

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
        return fireRate + UnityEngine.Random.Range(-fireRateDelay, fireRateDelay);
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

    public float GetAmountOfGuns()
    {
        return amountOfGuns;
    }

    public Vector3 GetScaleBullet()
    {
        return Vector3.one * UnityEngine.Random.Range(1f, 2f);
    }

    private IEnumerator IECreateBullet(Entity target)
    {
        yield return new WaitForSeconds(0.1f);

        float projectilesAngleSpace = multipleProjectilesAngle;
        float minAngle = -(GetAmountOfGuns() / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngle;
        for (int i = 0; i < GetAmountOfGuns(); i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            //float spread = Random.Range(attackConfig.bulletSprey.x, attackConfig.bulletSprey.y);
            //angle += spread;

            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
            // Add a random angle deviation
            float randomAngle = UnityEngine.Random.Range(-angle, angle);
            Vector3 rotatedDirection = Quaternion.Euler(0, 0, randomAngle) * dirToTarget;

            Vector3 direction = (transform.rotation * Quaternion.Euler(new Vector3(0, 0, angle))) * DegreeToVector2(0);
            var bullet = Instantiate(projectiles[Random.Range(0, projectiles.Length)], pointSpawn.position, Quaternion.identity);

            bullet.Init(this.entity, direction); 
            bullet.transform.localScale = GetScaleBullet();
            bullet.transform.parent = null;
            bullet.GetRig().AddForce(rotatedDirection * speed * 100);
            bullet.Destroyd(timeDestroyed);
        }
    }

    public Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
