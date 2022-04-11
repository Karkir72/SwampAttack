using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNunber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEnemyesSpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNunber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstatiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNunber + 1)
                AllEnemyesSpawned?.Invoke(); 

            _currentWave = null;
        }
    }

    private void InstatiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNunber);
        _spawned = 0;
    }
}


[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;

}