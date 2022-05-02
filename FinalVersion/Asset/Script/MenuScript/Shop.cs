using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private MoneySystem _money;
    [SerializeField] private ShootingScript _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private PlayerGetSpeedBooster _speedBooster;
    [SerializeField] private HealthPlayer _health;
    [SerializeField] private Transform _point;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private int _price;
    [SerializeField] private Text _priceText;
    public int Price
    {
        get
        {
            return _price;
        }

        set
        {
            _price = value;
            if(_price > 100)
            {
                _price = 100;
            }
            _priceText.text = $"{_price}";
        }
    }

    /// <summary>
    /// Реализация покупки оружия
    /// </summary>
    /// <param name="weapon"></param>
    public void NewWeapon(int weapon)
    {
        if (_money.Money >= Price)
        {
            _weapon.NewWeapon(weapon);
            _money.Money -= Price;
            Price *= 2;
        }
    }

    /// <summary>
    /// Реализация покупки защиты
    /// </summary>
    public void Armor()
    {
        if (_money.Money >= Price)
        {
            _armor.NewArmor();
            _money.Money -= Price;
            Price *= 2;
        }
    }

    /// <summary>
    /// Реализация покупки ускорителя
    /// </summary>
    public void SpeedBooster()
    {
        if (_money.Money >= Price)
        {
            _speedBooster.NewSpeedBoost();
            _money.Money -= Price;
            Price *= 2;
        }
    }

    /// <summary>
    /// Реализация покупки бомбы
    /// </summary>
    public void NewBomb()
    {
        if (_money.Money >= Price)
        {
            GameObject bomb = Instantiate(_bomb, _point.position, Quaternion.identity);
            Bomb bombVal = bomb.GetComponent<Bomb>();
            bombVal.NewBomb();
            _money.Money -= Price;
            Price *= 2;
        }
    }

    /// <summary>
    /// Реализация покупки аптечки
    /// </summary>
    public void NewHealth()
    {
        if (_health.NewHealth())
        {
            if (_money.Money >= Price)
            {
                _health.Healing();
                _money.Money -= Price;
                Price *= 2;
            }
        }
    }

    private void Awake()
    {
        Price = 10;
    }
}
