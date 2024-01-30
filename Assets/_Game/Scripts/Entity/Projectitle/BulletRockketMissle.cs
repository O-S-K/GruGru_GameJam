using DG.Tweening;
using UnityEngine;

public class BulletRockketMissle : Projectile
{
    public float speed = 5f;
    private Entity enemyTarget;
    public GameObject vfxExplosion;

    public override void Init(Entity entity, Vector2 direction)
    {
        base.Init(entity, direction);

        var targets = entity.GetComponent<Player>().targets;
        if (targets != null)
        {
            enemyTarget = entity.GetComponent<Player>().targets[Random.Range(0, targets.Count)];
        }
    }


    private void Update()
    {
        if (enemyTarget != null)
        {
            if (enemyTarget.IsDie)
            {
                Destroyd(0);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position, speed * Time.deltaTime);
                transform.LookAt(enemyTarget.transform.position);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
        }
    }

    public override void Destroyd(float time)
    {
        base.Destroyd(time);

        var vfx = Instantiate(vfxExplosion, transform.position, Quaternion.identity);
        vfx.transform.parent = null;
        Destroy(vfx, 1);

        AudioManager.Instance.PlayOneShot("frag1_explosion", 0.5f, 0, Random.Range(0.8f, 1.2f));
        GameManger.Instance.CamMain.ShakeCamera(0.3f, 0.7f, 10, Ease.InOutBack, 0.5f, Ease.OutBack);
    }

}
