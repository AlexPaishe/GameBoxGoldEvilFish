using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterMovementScript)),
    RequireComponent(typeof(MeshRenderer)),
    RequireComponent(typeof(EnemySoundScript))]
public class HealthDaymonScript : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _reward;
    [SerializeField] private int _moneyReward;

    [SerializeField] private GameObject[] _unit;

    private Animator _anima;
    private MoneySystem _money;
    private MeshRenderer[] _meshRender = new MeshRenderer[23];
    private Vector3 _begin;
    private InterfaceScript _interfaces;
    private SpawnerScript _spawn;
    private MonsterMovementScript _monster;
    private PlayerMovementScript _player;
    private EnemySoundScript _sound;

    private bool _isAlive = true;
    private int _maxhealth;

    void Start()
    {
        _money = FindObjectOfType<MoneySystem>();
        _spawn = FindObjectOfType<SpawnerScript>();
        _maxhealth = _currentHealth;
        _interfaces = FindObjectOfType<InterfaceScript>();
        _monster = GetComponent<MonsterMovementScript>();
        _anima = _unit[0].GetComponent<Animator>();
        _player = FindObjectOfType<PlayerMovementScript>();
        for (int i = 0; i < _unit.Length; i++)
        {
            _meshRender[i] = _unit[i].GetComponent<MeshRenderer>();
        }
        _sound = GetComponent<EnemySoundScript>();
    }

    void Update()
    {
        if (_player.gameObject.transform.position.y < transform.position.y - 12)
        {
            _currentHealth = _maxhealth;
            int rand = Random.Range(0, _spawn._enemiesSpawn.Length);
            _begin = _spawn._enemiesSpawn[rand];
            transform.position = _begin;
            this.gameObject.SetActive(false);
        }

        if (_isAlive == true)
        {
            _anima.speed = SpeedBooster.Speed;
        }
        else
        {
            _anima.speed = 1;
        }
    }

    /// <summary>
    /// Нанесение урона врагу
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (_isAlive == true)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _isAlive = false;
                StartCoroutine(Death());
            }
            if (_currentHealth > 0)
            {
                StartCoroutine(Damage());
            }
        }
    }

    /// <summary>
    /// Реализация смерти
    /// </summary>
    /// <returns></returns>
    IEnumerator Death()
    {
        _monster.Stopping(false);
        _interfaces.Score(_reward);
        gameObject.layer = 10;
        _anima.SetBool("Iddle", false);
        _sound.ConditionSound(1);
        for (int i = 0; i < _meshRender.Length; i++)
        {
            for (int j = 0; j < _meshRender[i].materials.Length; j++)
            {
                _meshRender[i].materials[j].EnableKeyword("_EMISSION");
            }
        }
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < _meshRender.Length; i++)
        {
            _meshRender[i].enabled = false;
        }
        _anima.SetBool("Iddle", true);
        _money.Money += _moneyReward;
        yield return new WaitForSeconds(0.2f);
        _isAlive = true;
        _currentHealth = _maxhealth;
        int rand = Random.Range(0, _spawn._enemiesSpawn.Length);
        _begin = _spawn._enemiesSpawn[rand];
        transform.position = _begin;
        gameObject.layer = 0;
        for (int i = 0; i < _meshRender.Length; i++)
        {
            _meshRender[i].enabled = true;
        }
        for (int i = 0; i < _meshRender.Length; i++)
        {
            for (int j = 0; j < _meshRender[i].materials.Length; j++)
            {
                _meshRender[i].materials[j].DisableKeyword("_EMISSION");
            }
        }
        _monster.Stopping(true);
        _sound.ConditionSound(0);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Реализация урона
    /// </summary>
    /// <returns></returns>
    IEnumerator Damage()
    {
        for (int i = 0; i < _meshRender.Length; i++)
        {
            for (int j = 0; j < _meshRender[i].materials.Length; j++)
            {
                _meshRender[i].materials[j].EnableKeyword("_EMISSION");
            }
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < _meshRender.Length; i++)
        {
            for (int j = 0; j < _meshRender[i].materials.Length; j++)
            {
                _meshRender[i].materials[j].DisableKeyword("_EMISSION");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageZone"))
        {
            _isAlive = false;
            StartCoroutine(Death());
        }
    }
}
