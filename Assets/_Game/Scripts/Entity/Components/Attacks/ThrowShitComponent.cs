using System;
using System.Collections;
using UnityEngine;

public class ThrowShitComponent : MonoBehaviour, IAttack
{
    private BaseCharacter entity;
    public Projectile projectileShit;
    public Transform pointSpawn;

    public float speed = 5;
    public float timeDestroyed = 3;
    public float fireRate = 1;
    public float fireRateDelay;
    private float currentFireRate;

    private float multipleProjectilesAngle = 45f;
    private float amountOfGuns = 1;
    private float scaleBullet = 1;

    public Sprite[] iconProjectiles;

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
            AudioManager.Instance.PlayOneShot("Throw", 1, 0, UnityEngine.Random.Range(0.8f, 1.2f));
        }
        else
        {
            currentFireRate += Time.deltaTime;
        }
    }

    public float GetAmountOfGuns()
    {
        if (entity.typeChar == BaseCharacter.ETypeChar.Player)
        {
            var p = (Player)entity;
            return amountOfGuns = p.AmountBulletOfGun;
        }
        else
        {
            return amountOfGuns;
        }
    }

    public Vector3 GetScaleBullet()
    {
        if (entity.typeChar == BaseCharacter.ETypeChar.Player)
        {
            var p = (Player)entity;
            return Vector3.one * (p.ableItemScaleBullet ? 5 : 1);
        }
        else
        {
            return Vector3.one;
        }
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
           
            var bullet = Instantiate(projectileShit, pointSpawn.position, Quaternion.identity);

            bullet.Init(this.entity, direction);

            if (entity.typeChar == BaseCharacter.ETypeChar.Player)
            {
                Vector3 scaleInit = new Vector3(0.4f, 0.4f, 1f);
                var p = (Player)entity;
                if (p.ableChangeSprite)
                {
                    bullet.Sprite.transform.localScale = scaleInit * 2;
                    bullet.ChangeSprite(p.spriteBullet);
                }
                else
                {
                    bullet.Sprite.transform.localScale = scaleInit;
                    if(iconProjectiles.Length > 0)
                    {
                        bullet.ChangeSprite(iconProjectiles[UnityEngine.Random.Range(0, iconProjectiles.Length)]);
                    }
                }
            }

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
