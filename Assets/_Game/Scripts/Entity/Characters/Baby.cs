using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Baby : Entity
{
    protected Animator _animator;
    protected SpriteRenderer _sprite;


    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }


    public virtual void Idle()
    {
        _animator.Play("BabyIdle");
    }

 
    public virtual void Attack()
    {
        _animator.Play("BabyAttack");
    }

    public virtual void Hit()
    {
        _sprite.DOColor(Color.red, 0.1f).OnComplete(() => 
        {
            _sprite.DOColor(Color.white, 0.05f);
        });
    }

    public virtual void Die()
    {
        _animator.CrossFadeInFixedTime("BabyDie", 0.1f);
        _sprite.color = Color.gray;
    }

}
