using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        SpawnWave(0);
    }

    public void StartGame()
    {
        stateGame = EGameState.Playing;
    }

    public void SpawnWave(float delaySpawnWave)
    {
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
        if(player.targets.Count == 0)
        {
            player.targets.Clear();
            UIManager.Instance.ShowCache<PopupUpgrade>();
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
