using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BulletRockketMissle : Projectile
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    private Entity enemyTarget;
    private Vector2 newPos;

    public override void Init(Entity entity, Vector2 direction)
    {
        base.Init(entity, direction);

        var t = entity.GetComponent<Player>().targets;

        if (t != null)
        {
            enemyTarget = t[Random.Range(0, t.Count)];
            newPos = enemyTarget.transform.position;
            transform.DOMove(newPos, 1).SetEase(Ease.InOutCubic).OnUpdate(UpdatePosEnemy);
            transform.DOLookAt(newPos, 0.1f);
        }
        else
        {
            newPos = Vector2.zero;
            transform.DOMove(newPos, 1).SetEase(Ease.InOutCubic);
            transform.DOLookAt(newPos, 0.1f);
        }
    }

    void UpdatePosEnemy()
    {
        if (enemyTarget != null)
        {
            newPos = enemyTarget.transform.position;
        }
        else
        {
            var t = _entity.GetComponent<Player>().targets;
            if(t != null && t.Count > 0)
            {
                newPos = t[Random.Range(0, t.Count)].transform.position;
            }
            else
            {
                Destroyd(0);
            }
        }
    }

    public override void Destroyd(float time)
    {
        base.Destroyd(time);
        GameManger.Instance.CamMain.ShakeCamera(0.3f, 0.7f, 10, Ease.InOutBack, 0.5f, Ease.OutBack);
    }

}
