using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Baby : Entity
{
    protected Animator _animator;
    protected SpriteRenderer _sprite;
    protected Color _colorInit = Color.white;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void Init()
    {
        _colorInit = Color.white;
        _colorInit = _sprite.color;
    }

    public SpriteRenderer Sprite()
    {
        return _sprite;
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
            _sprite.DOColor(_colorInit, 0.05f);
        });
    }

    public virtual void Die()
    {
        _animator.CrossFadeInFixedTime("BabyDie", 0.1f);
        _sprite.color = Color.gray;
    }

}
