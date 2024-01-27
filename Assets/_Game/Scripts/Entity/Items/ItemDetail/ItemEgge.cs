using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEgge : BaseItem
{
    [SerializeField] private GameObject dogePrefab;

    public override void Action(Player player)
    {
        var doge= Instantiate(dogePrefab);
        doge.transform.parent = null;
        doge.transform.position = new Vector3(12, 0,0);
        player.BlockAttackEnemy();

        AudioManager.Instance.musicSource.Pause();
        AudioManager.Instance.PlayOneShot("doge");
    }

    public override void DestroyItem()
    {
    }

    public override void OnEnable()
    {
    }
}