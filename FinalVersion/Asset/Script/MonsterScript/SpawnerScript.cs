using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject prefab;
    public int count;
}
public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemiesOnScene;
    [SerializeField] public Vector3[] _enemiesSpawn;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private float _timer;
    [SerializeField] private float _distanceMin;
    [SerializeField] private float _distanceMax;
    [SerializeField] private float _step;

    private PlayerMovementScript _player;

    private float _currentTime;
    private int _waveMax = 10;
    private int _wave = 0;
    private int _houseMan = 0;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovementScript>();
        for(int i = 0; i < _enemies.Count; i++)
        {
            for(int j = 0; j< _enemies[i].count; j++)
            {
                GameObject enemyObject = Instantiate(_enemies[i].prefab);
                _enemiesOnScene.Add(enemyObject);
                int rand = Random.Range(0, _enemiesSpawn.Length);
                enemyObject.transform.position = _enemiesSpawn[rand];
                enemyObject.SetActive(false);
            }
        }

        _currentTime = _timer;
        Spawn();
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if(_currentTime < 0)
        {
            _timer += _step;
            _currentTime = _timer;
            Spawn();
        }
    }

    /// <summary>
    /// Реализация спавна врагов с увеличением от количества волн
    /// </summary>
    public void Spawn()
    {
        if(_waveMax > _maxEnemies)
        {
            _waveMax = _maxEnemies;
        }

        List<Vector3> spawner = new List<Vector3>();
        for(int i = 0; i < _enemiesSpawn.Length; i++)
        {
            if(Vector3.Distance(_enemiesSpawn[i], _player.transform.position) > _distanceMin &&
                Vector3.Distance(_enemiesSpawn[i], _player.transform.position) < _distanceMax)
            {
                spawner.Add(_enemiesSpawn[i]);
            }
        }

        for (int i = 0; i < _enemiesOnScene.Count; i++)
        {
            if (_wave < _waveMax)
            {
                if(_waveMax > 27 && i < _enemies[0].count  && _waveMax - _wave > 20 && _houseMan < 5)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        int rand = Random.Range(0, spawner.Count);
                        _enemiesOnScene[i].transform.position = spawner[rand];
                        _wave += 19;
                        _houseMan++;
                    }
                }
                else if (_waveMax > 25 && i >= _enemies[0].count && i < _enemies[0].count * 2 && _waveMax - _wave > 6)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        int rand = Random.Range(0, spawner.Count);
                        _enemiesOnScene[i].transform.position = spawner[rand];
                        _wave += 5;
                    }
                }
                else if(_waveMax > 20 && i >= _enemies[0].count * 2 && i < _enemies[0].count * 3 && _waveMax - _wave > 5)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        int rand = Random.Range(0, spawner.Count);
                        _enemiesOnScene[i].transform.position = spawner[rand];
                        _wave += 4;
                    }
                }
                else if (_waveMax > 15 && i >= _enemies[0].count * 3 && i < _enemies[0].count * 4 && _waveMax - _wave > 3)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        int rand = Random.Range(0, spawner.Count);
                        _enemiesOnScene[i].transform.position = spawner[rand];
                        _wave += 2;
                    }
                }
                else if (i >= _enemies[0].count * 4)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        int rand = Random.Range(0, spawner.Count);
                        _enemiesOnScene[i].transform.position = spawner[rand];
                        _wave++;
                    }
                }
            }
        }
        _wave = 0;
        if (_houseMan > 0)
        {
            _waveMax += 4;
        }
        else
        {
            _waveMax += 2;
        }
        _houseMan = 0;
        spawner.Clear();
    }
}
