using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupFaildMission : OSK.Popup
{
    public TextMeshProUGUI currentWaveText;

    public Button backButton;
    public Button retryButton;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Show(object[] inData, PopupClosed callback)
    {
        base.Show(inData, callback);
        currentWaveText.text = $"Waves {GameManger.Instance.wave.CurrentWaveIndex}-{10}";
    }

    public override void Hide()
    {
        base.Hide();
    }
}
