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

    private void Start()
    {
        AudioManager.Instance.PlayMusic($"Theme {Random.Range(1, 3)}", 0.3f);
        FindObjectOfType<FadeUI>().FadeOut(1, 0, null);
        player.InitData();
        SpawnWave(1);
        var notif = UIManager.Instance.ShowCache<PopupNotif>();
        notif.ShowText("Move to avoid and defeat other storks", 6);
    }

    public void StartGame()
    {
        stateGame = EGameState.Playing;
    }

    public void SpawnWave(float delaySpawnWave)
    {
        var notif = UIManager.Instance.ShowCache<PopupNotif>();

        if(wave.CurrentWaveIndex == 9)
        {
            AudioManager.Instance.PlayOneShot("warning");
            notif.ShowText($"Waves Boss !!!", 3);
        }
        else
        {
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
        if(wave.CurrentWaveIndex > 9)
        {
            FindObjectOfType<FadeUI>().FadeIn(1,1, () =>
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
