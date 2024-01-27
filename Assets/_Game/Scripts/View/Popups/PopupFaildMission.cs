using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupFaildMission : OSK.Popup
{
    public TextMeshProUGUI currentWaveText;

    public Button backButton;
    public Button retryButton;

    public override void Initialize()
    {
        base.Initialize();

        retryButton.onClick.AddListener(OnRetry);
        backButton.onClick.AddListener(OnBackMenu);
    }

    public void OnRetry()
    {
        SceneManager.LoadScene(1);
    }

    public void OnBackMenu()
    {
        SceneManager.LoadScene(0);
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
