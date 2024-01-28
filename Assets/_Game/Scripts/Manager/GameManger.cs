using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : OSK.SingletonMono<GameManger>
{
    public enum EGameState
    {
        Ready = 0,
        Playing = 1,
        Completed,
        Faild,
        Pause
    }
    public EGameState stateGame;

    public Wave wave;
    public Player player;
    public CameraController CamMain;

    public Color[] colorEnemyCreeps;

    public SpriteRenderer backgroundBlackFade;

    private bool isPause;

    private void Start()
    {
        //AudioManager.Instance.PlayMusic($"Theme {Random.Range(1, 3)}", 0.3f);
        AudioManager.Instance.PlayMusic($"Theme {1}", 0.3f);

        FindObjectOfType<FadeUI>().FadeOut(1, 0, null);
        player.InitData();
        SpawnWave(1);
        var notif = UIManager.Instance.ShowCache<PopupNotif>();
        notif.ShowText("Press arrow buttons to avoid and defeat other storks", 6);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;

            if (isPause)
            {
                Time.timeScale = 0;
                AudioManager.Instance.Pause();
            }
            else
            {
                Time.timeScale = 1;
                AudioManager.Instance.Resume();
            }
        }
    }

    public void StartGame()
    {
        stateGame = EGameState.Playing;
    }

    public void SpawnWave(float delaySpawnWave)
    {
        var notif = UIManager.Instance.ShowCache<PopupNotif>();

        if (wave.CurrentWaveIndex == 9)
        {
            backgroundBlackFade.gameObject.SetActive(true);
            backgroundBlackFade.DOFade(0, 0);
            backgroundBlackFade.DOFade(.5f, 1f);
            AudioManager.Instance.PlayMusic($"Darkness", 0.5f);
            AudioManager.Instance.PlayOneShot("warning");
            notif.ShowText($"Waves Boss !!!", 3);
        }
        else
        {
            backgroundBlackFade.gameObject.SetActive(false);
            notif.ShowText($"Waves {wave.CurrentWaveIndex}/10", 3);
        }
        stateGame = EGameState.Ready;
        StartCoroutine(IESpawnWave(delaySpawnWave));
    }

    private IEnumerator IESpawnWave(float delaySpawnWave)
    {
        yield return new WaitForSeconds(delaySpawnWave);
        wave.SpawnWaves();
    }

    public void RemoveEnemyDie(Enemy enemy)
    {
        player.RemoveTarget(enemy);
        CheckNewWave();
    }

    public void CheckNewWave()
    {
        if (wave.CurrentWaveIndex > 9)
        {
            FindObjectOfType<FadeUI>().FadeIn(1, 2.5f, () =>
            {
                DOVirtual.DelayedCall(2, () =>
                {
                    SceneManager.LoadScene(2);
                });
            });
        }
        else
        {
            if (player.targets.Count == 0)
            {
                player.targets.Clear();
                DOVirtual.DelayedCall(2, () =>
                {
                    UIManager.Instance.ShowCache<PopupUpgrade>();
                });
            }
        }
    }

    public void EndEndGame()
    {
        stateGame = EGameState.Completed;
        player.ResetVel();
    }

    public void FaildMission()
    {
        stateGame = EGameState.Faild;
        DOVirtual.DelayedCall(2, () =>
        {
            UIManager.Instance.ShowCache<PopupFaildMission>();
        });
    }
}
