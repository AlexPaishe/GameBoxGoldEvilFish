using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour
{
    [SerializeField] private Image _sliderArmor;
    [SerializeField] private MeshRenderer _shild;
    [SerializeField] private float _timer;
    [SerializeField] private HealthPlayer _playerHealth;
    private float _timerVal = 0;
    private Bonus _bonus;
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
                gameObject.layer = _playerHealth.layer;
                _shild.enabled = true;
            }
            else if(_timerVal <= 0)
            {
                gameObject.layer = 10;
                _shild.enabled = false;
            }
            _sliderArmor.fillAmount = _timerVal / _timer;
        }
    }

    private void Start()
    {
        _bonus = FindObjectOfType<Bonus>();
    }

    void Update()
    {
        if(gameObject.layer == _playerHealth.layer)
        {
            TimerVal -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Armor"))
        {
            NewArmor();
            Destroy(other.gameObject);
            _bonus.NewBonus();
        }
    }

    /// <summary>
    /// Реализация установки щита через магазин
    /// </summary>
    public void NewArmor()
    {
        TimerVal = _timer;
    }
}
