using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] protected Collider2D _collider2D;
    [SerializeField] protected GameObject _vfxImpact;


    protected Entity _entity;
    protected Vector2 direction;


    public Rigidbody2D GetRig()
    {
        return _rigidbody2D;
    }

    public Collider2D GetCollider()
    {
        return _collider2D;
    }


    public void Init(Entity entity, Vector2 direction)
    {
        _entity = entity;
        ID = entity.ID;
        this.direction = direction; 
    }
     

    void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<Entity>();
        if (target != null)
        {
            if (_entity.layerTarget.value == (_entity.layerTarget.value | (1 << target.gameObject.layer)))
            {
                var c = target.GetComponent<BaseCharacter>();
                if(c.typeChar == BaseCharacter.ETypeChar.Player)
                {
                    var p = c.GetComponent<Player>();
                    if (p.ableItemShield)
                    {
                        p.RemoveItemShield();

                    }
                    else
                    {
                        target.GetComponent<HealthSystem>().TakeDamage(transform, -1);
                    }
                }
                else
                {
                    target.GetComponent<HealthSystem>().TakeDamage(transform, -1);
                }
                if (target.IsDie)
                {
                    c.KnockBack(direction, 10000);
                }
                Destroyd(0);
                CreateImpact();
            }
        }
    }

    public void Destroyd(float time)
    { 
        Destroy(gameObject, time);
    }

    private void CreateImpact()
    {
        var vfxImpact = Instantiate(_vfxImpact, transform.position, Quaternion.identity);
        vfxImpact.transform.parent = null;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //vfxImpact.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Destroy(vfxImpact, 3);
    }
}
