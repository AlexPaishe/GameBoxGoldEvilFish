using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private Image[] _healthInterfaces;
    [SerializeField] private MeshRenderer _forceField;
    [SerializeField] private AudioSource _playerSound;
    [SerializeField] private AudioClip[] _healthAndDamage;
    [SerializeField] private Image _sliderArmor;
    public LayerMask layer;

    private int _healthMax = 3;
    private int _health;
    private float _timer = 10;

    private Vector3 _begin;
    private FishBezier _head;

    private void Awake()
    {
        _health = _healthMax;
        HealthSearch();
        _head = FindObjectOfType<FishBezier>();
    }

    private void Update()
    {
        EnvironmentDamage();

        if(_timer <= 5 && _timer >= 0)
        {
            _timer -= Time.deltaTime;
            _sliderArmor.fillAmount = _timer / 5f;
        }
    }

    /// <summary>
    /// Реализация отображения здоровья героя на экране
    /// </summary>
    private void HealthSearch()
    {
        for(int i = 0; i < _healthMax; i++)
        {
            if(i < _health)
            {
                _healthInterfaces[i].color = Color.green;
            }
            else
            {
                _healthInterfaces[i].color = Color.red;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.layer == 10)
        {
            if(collision.collider.CompareTag("Enemy"))
            {
                StartCoroutine(PlayerDamage());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == 10)
        {
            if (other.CompareTag("Enemy"))
            {
                StartCoroutine(PlayerDamage());
            }
            else if(other.CompareTag("DamageZone"))
            {
                StartCoroutine(PlayerDamage());
            }
        }
    }

    /// <summary>
    /// Реализация урона герою и его гибель
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerDamage()
    {
        _health--;
        HealthSearch();
        if(_health > 0)
        {
            _playerSound.clip = _healthAndDamage[1];
            _playerSound.Play();
            gameObject.layer = 0;
            _forceField.enabled = true;
            _timer = 5;
            yield return new WaitForSeconds(5);
            gameObject.layer = 10;
            _forceField.enabled = false;
        }
        else
        {
            _head.Go();
        }
    }

    /// <summary>
    /// Реализация получение урона при выезде из экрана
    /// </summary>
    private void EnvironmentDamage()
    {
        if (transform.position.z < -10.3f)
        {
            _begin = Vector3.zero;
            _begin.y = transform.position.y;
            transform.position = _begin;
            if (gameObject.layer == 10)
            {
                StartCoroutine(PlayerDamage());
            }
        }
    }

    /// <summary>
    /// Лечение героя
    /// </summary>
    public void Healing()
    {
        _health++;
        if(_health > _healthMax)
        {
            _health = _healthMax;
        }
        _playerSound.clip = _healthAndDamage[0];
        _playerSound.Play();
        HealthSearch();
    }

    /// <summary>
    /// Реализация проверки на количество жизней
    /// </summary>
    /// <returns></returns>
    public bool NewHealth()
    {
        if(_health == _healthMax)
        {
            return false;
        }
        return true;
    }
}
