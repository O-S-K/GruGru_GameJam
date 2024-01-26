using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DataChar
{
    [SerializeField] protected int _hp = 5;
    [SerializeField] protected int _damage = 5;


    [SerializeField] protected float _speedMovement;
    [SerializeField] protected float _smoothSpeed = 0.9f;
    [SerializeField] protected float _seachRadiusTarget = 5;

    public int GetHP()
    {
        return _hp;
    }

    public float GetSpeedMovement()
    {
        return _speedMovement;
    }

    public float GetSmoothSpeed()
    {
        return _smoothSpeed;
    }

    public float GetSeachRadiusTarget()
    {
        return _seachRadiusTarget;
    }
}

public class BaseCharacter : Entity
{
    public enum ETypeChar
    {
        Player,
        Creep,
        MiniBoss,
        Boss
    }

    public enum EStateChar
    {
        None,
        Preview,
        Idle,
        Fly,
        Attack,
        KnockBack,
        Die
    }


    public ETypeChar typeChar;
    public EStateChar stateChar;

    public List<Entity> targets = new List<Entity>();

    [SerializeField] protected Duck _duck;
    [SerializeField] protected Baby _baby;


    public DataChar data;


    protected Rigidbody2D _rigidbody2D;
    protected BoxCollider2D _collider2D;

    protected HealthSystem _heath;

    protected Vector2 velocity;
    protected Vector2 direction;


    protected void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
        _heath = GetComponent<HealthSystem>();
    }

    protected virtual void Start()
    {
        _heath.Initialize(data.GetHP());

        _heath.OnHit.AddListener(Hit);
        _heath.OnDeath.AddListener(Die);
    }

    public virtual void InitData()
    {
        IsDie = false;
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Idle()
    {
        if (IsDie)
            return;

        stateChar = EStateChar.Idle;
        _baby.Idle();
        _duck.Idle();
    }

    protected virtual Vector2 GetDirection()
    {
        return direction;
    }

    protected virtual void Fly()
    {
        if (IsDie)
            return;
        stateChar = EStateChar.Fly;
        _duck.Fly();
    }

    protected virtual void Attack(Entity target)
    {
        if (IsDie)
            return;

        stateChar = EStateChar.Attack;
        var attacks = GetComponents<IAttack>();
        foreach (var attack in attacks)
        {
            attack.Attack(target);
        }
    }


    protected virtual void Hit(Vector2 dir, int damageValue, DamagePopup.ETypeDamage typeDamage)
    {
        if (IsDie)
            return;

        _baby.Hit();
        _duck.Hit();

        CreateManager.Instance.CreateTextDamagePopup(transform.position, (int)damageValue, typeDamage);
        //BloodParticleSystemHandler.Instance.SpawnBlood(2, transform.position, -dir);
    }

    public virtual void KnockBack(Vector2 direction, float power)
    {
        _rigidbody2D.AddForce(direction * power, ForceMode2D.Impulse);
    }

    protected virtual void Die()
    {
        if (IsDie)
            return;

        IsDie = true;
        stateChar = EStateChar.Die;
        _baby.Die();
        _duck.Die();
        _rigidbody2D.gravityScale = 3;
        _collider2D.enabled = false;

        Destroy(gameObject, 3);
    }

    public void AddTarget(Entity target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    public void RemoveTarget(Entity target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
        }
    }

    public void FindNearestObject(List<Entity> characters, float searchRadius)
    { 
        // Đối tượng gần mình nhất
        Entity nearestObject = null;
        float nearestDistance = Mathf.Infinity;

        // Kiểm tra từng đối tượng để tìm đối tượng gần nhất
        foreach (var target in characters)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance < nearestDistance && !target.IsDie)
            {
                nearestObject = target;
                nearestDistance = distance;
            }
        }

        // Kiểm tra xem đối tượng gần nhất có nằm trong bán kính tìm kiếm không
        if (nearestObject != null && !nearestObject.IsDie && nearestDistance <= searchRadius)
        {
            Attack(nearestObject);
        }
        else
        {
            // Không có đối tượng nào trong bán kính tìm kiếm
            Debug.Log($"{name} No nearby objects found.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.GetSeachRadiusTarget());
    }
}
