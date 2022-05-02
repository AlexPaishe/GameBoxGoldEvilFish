using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private Text _moneyText;
    private int _money = 0;
    public int Money
    {
        get
        {
            return _money;
        }

        set
        {
            _money = value;
            _moneyText.text = $"Money: {_money}";
        }
    }
}
