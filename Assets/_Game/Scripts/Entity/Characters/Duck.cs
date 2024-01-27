using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Duck : Entity
{
    protected Animator _animator;
    protected SpriteRenderer _sprite;
    protected Tweener spriteTween;
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
        _animator.Play("IdleDuck");
    }

    public virtual void Fly()
    {
        _animator.Play("FlyDuck");
    }

    public virtual void Attack()
    {

    }

    public virtual void Hit()
    {
        if(spriteTween != null) spriteTween.Kill();
        spriteTween = _sprite.DOColor(Color.red, 0.1f).OnComplete(() =>
        {
            spriteTween = _sprite.DOColor(_colorInit, 0.05f);
        });
    }

    public virtual void Die()
    {
        DOTween.KillAll();
        _animator.CrossFadeInFixedTime("DieDuck", 0.1f);
        _sprite.color = Color.gray;
    }
}
