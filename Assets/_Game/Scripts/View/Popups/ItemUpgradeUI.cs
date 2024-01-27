using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeUI : MonoBehaviour
{
    public EAbilityType abilityType;


    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectItem);
    }

    private void SelectItem()
    {
        switch (abilityType)
        {
            case EAbilityType.FireRate:
                GameManger.Instance.player.UpgradeFireRate();
                break;
            case EAbilityType.BonusProjectile:
                GameManger.Instance.player.BonusProjectile();
                break;
            case EAbilityType.IncreasesHealth:
                GameManger.Instance.player.IncreasesHealth(); 
                break;
        }

        AudioManager.Instance.PlayOneShot("ShowReward");
        UIManager.Instance.GetPopup<PopupUpgrade>().Hide();
    }
}
