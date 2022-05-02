using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HouseManMove : MonoBehaviour
{
    [SerializeField] private Transform[] _arch;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _speed;
    private bool _isAlive = true;
    private Vector3 _target;
    private int _numberTarget = 0;
    private Bonus _bonus;
    void Start()
    {
        _bonus = FindObjectOfType<Bonus>();
        for(int i = 0; i < _arch.Length; i++)
        {
            _arch[i] = _bonus._archHouseMan[i].transform;
        }
        _target = _arch[_numberTarget].position;
    }

    void Update()
    {
        _agent.speed = _speed * SpeedBooster.Speed;
        if (_isAlive == true)
        {
            if (transform.position.x != _target.x && transform.position.z != _target.z)
            {
                _agent.destination = _target;
            }
            else
            {
                SearchTarget();
            }
        }
        else
        {
            _agent.destination = transform.position;
        }
    }

    /// <summary>
    /// Реализация поиска таргета
    /// </summary>
    private void SearchTarget()
    {
        int rand = 0;
        do
        {
            rand = Random.Range(0, _arch.Length);
        } while (rand == _numberTarget);
        _numberTarget = rand;

        _target = _arch[_numberTarget].position;
    }

    /// <summary>
    /// Остановка движения
    /// </summary>
    /// <param name="isDeath"></param>
    public void Stopping(bool isDeath)
    {
        _isAlive = isDeath;
    }
}
