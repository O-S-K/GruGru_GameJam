using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableImpact : BaseItem
{
    [SerializeField] private GameObject sharkPrefab;

    public override void Action(Player player)
    {
        AudioManager.Instance.musicSource.Pause();
        AudioManager.Instance.PlayOneShot("babyshark", 0.7f);
        player.DancingEnemy(sharkPrefab); 
    }
     

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}
