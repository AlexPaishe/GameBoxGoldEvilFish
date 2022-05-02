using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioSource _tick;
    [SerializeField] private float _timer;
    [SerializeField] private GameObject[] _obj;
    [SerializeField] private MeshRenderer _mesh;
    private float _timerVal = 40;
    private Bonus _bonus;
    private bool _on = false;
    private InterfaceScript _inter;
    private bool _pause = false;
    public float TimerVal
    {
        get
        {
            return _timerVal;
        }

        set
        {
            _timerVal = value;
            if(_timerVal == _timer)
            {
                _mesh.enabled = false;
                _obj[0].SetActive(true);
                _tick.Play();
            }
            else if(_timerVal <= 0)
            {
                _obj[1].SetActive(true);
            }
        }
    }
    void Start()
    {
        _bonus = FindObjectOfType<Bonus>();
        _inter = FindObjectOfType<InterfaceScript>();
    }

    void Update()
    {
        if(TimerVal <= _timer && TimerVal > 0)
        {
            TimerVal -= Time.deltaTime;
        }

        bool fire = _inter.PauseReturn();
        if (fire == true && _pause == false)
        {
            _tick.Pause();
            _pause = true;
        }
        else if (fire == false && _pause == true)
        {
            _tick.Play();
            _pause = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (_on == false)
            {
                NewBomb();
                _bonus.NewBonus();
            }
        }
    }

    /// <summary>
    /// Реализация взрыва бомбы
    /// </summary>
    public void NewBomb()
    {
        TimerVal = _timer;
        _on = true;
    }
}
