using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGetSpeedBooster : MonoBehaviour
{
    [SerializeField] private Material _mat;
    [SerializeField] private float _timer;
    [SerializeField] private Image _sliderTimer;
    private Bonus _bonus;
    private float _matSize = 0;
    private bool _speedBoost = false;
    public bool SpeedBoost
    {
        get
        {
            return _speedBoost;
        }

        set
        {
            _speedBoost = value;
            if(_speedBoost == true)
            {
                SpeedBooster.Speed = 0.3f;
            }
            else
            {
                SpeedBooster.Speed = 1f;
            }
        }
    }
    private float _timerVal = 0;
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
                SpeedBoost = true;
            }
            else if(_timerVal <= 0)
            {
                SpeedBoost = false;
            }
            _sliderTimer.fillAmount = _timerVal / _timer;
        }
    }

    private void Start()
    {
        _bonus = FindObjectOfType<Bonus>();
        _mat.SetFloat("_Size", _matSize);
    }

    void Update()
    {
        if(SpeedBoost == true && _matSize <= 2)
        {
            _matSize += Time.deltaTime;
            _mat.SetFloat("_Size", _matSize);
        }
        else if(SpeedBoost == false && _matSize > 0)
        {
            _matSize -= Time.deltaTime;
            _mat.SetFloat("_Size", _matSize);
        }
        else if(_matSize < 0)
        {
            _matSize = 0;
            _mat.SetFloat("_Size", _matSize);
        }

        if(SpeedBoost == true)
        {
            TimerVal -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpeedBooster"))
        {
            NewSpeedBoost();
            Destroy(other.gameObject);
            _bonus.NewBonus();
        }
    }

    /// <summary>
    /// Реализация установки бустера через магазин
    /// </summary>
    public void NewSpeedBoost()
    {
        TimerVal = _timer;
    }
}
