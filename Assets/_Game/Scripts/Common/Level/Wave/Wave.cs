using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public Enemy enemyPrefab;  // Prefab của quái vật

    [Range(0, 100)]
    public float spawnWeight = 1f;   // Tỉ lệ spawn của quái vật
    // Các thông tin khác bạn muốn thêm cho mỗi loại quái vật
}

[System.Serializable]
public class WaveData
{
    public EnemySpawnData[] enemySpawnData;  // Mảng chứa thông tin về tỉ lệ spawn cho từng loại quái vật
    public int totalEnemies;                 // Tổng số quái vật trong wave
    public float timeBetweenEnemies = 1f;   // Thời gian giữa các quái trong cùng một wave
    // Các thông tin khác bạn muốn thêm cho mỗi wave
}


public class Wave : MonoBehaviour
{
    public WaveData[] waves;  // Mảng chứa dữ liệu của từng wave

    private List<Enemy> _enemies = new List<Enemy>();
    public List<Enemy> Enemies => _enemies;

    public int CurrentWaveIndex => _currentWaveIndex;
    [SerializeField] private int _currentWaveIndex = 0;


    public void SpawnWaves()
    {
        if (_currentWaveIndex < waves.Length)
        {
            Debug.Log("Spawn Wavesssssssss");
            StartCoroutine(SpawnEnemies(waves[_currentWaveIndex]));
            _currentWaveIndex++;
        }
        else
        {
            GameManger.Instance.EndEndGame();
            Debug.LogError("End Wavesssssssss");
        }
    }

    private IEnumerator SpawnEnemies(WaveData wave)
    {
        _enemies.Clear();
        for (int i = 0; i < wave.totalEnemies; i++)
        {
            // Chọn loại quái vật dựa trên tỉ lệ spawn
            Enemy enemyPrefab = ChooseEnemyPrefab(wave.enemySpawnData);

            // Tạo một quái vật mới sử dụng thông tin từ wave
            Vector2 pos = new Vector3(-20, Random.Range(-4, 4));
            Enemy enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
            enemy.InitData();
            // Có thể cài đặt thông tin về loại quái vật ở đây (nếu cần) 
            GameManger.Instance.player.AddTarget(enemy);
            enemy.AddTarget(GameManger.Instance.player);

            _enemies.Add(enemy);
            yield return new WaitForSeconds(wave.timeBetweenEnemies);
        }

        yield return new WaitForSeconds(0.1f);
        EnemyGotoPoinSpawn();
    }

    private void EnemyGotoPoinSpawn()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            var pos = GetSpawnPosition();
            float dis = Vector3.Distance(_enemies[i].transform.position, pos);
            float time = 5;

            if (_enemies[i].typeChar == BaseCharacter.ETypeChar.Boss)
            {
                _enemies[i].transform.DOMove(Vector3.zero, dis / time);
            }
            else
            {
                _enemies[i].transform.DOMove(GetSpawnPosition(), dis / time);
            }
        }

        CancelInvoke(nameof(StartGame));
        Invoke(nameof(StartGame), 5);
    }

    void StartGame()
    {
        GameManger.Instance.StartGame();
    }


    private Enemy ChooseEnemyPrefab(EnemySpawnData[] enemySpawnData)
    {
        // Tính tổng trọng số của tất cả các loại quái
        float totalWeight = 0f;
        foreach (var data in enemySpawnData)
        {
            totalWeight += data.spawnWeight;
        }

        // Chọn một loại quái ngẫu nhiên dựa trên tỉ lệ spawn
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var data in enemySpawnData)
        {
            cumulativeWeight += data.spawnWeight;
            if (randomValue <= cumulativeWeight)
            {
                return data.enemyPrefab;
            }
        }

        // Nếu có lỗi xảy ra, trả về prefab đầu tiên
        return enemySpawnData[0].enemyPrefab;
    }

    private Vector3 GetSpawnPosition()
    {
        // Cài đặt logic để xác định vị trí spawn của quái vật (ví dụ: ngẫu nhiên trong một khu vực cố định)
        float spawnX = Random.Range(8, -8);
        float spawnY = Random.Range(4, -4);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
