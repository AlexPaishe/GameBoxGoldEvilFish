using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManBonus : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    private GameObject _bonus;
    private bool _bonusUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bonus") || other.CompareTag("Armor")|| other.CompareTag("SpeedBooster") && _bonusUp == false)
        {
            other.gameObject.transform.parent = _points[0];
            other.gameObject.transform.localPosition = Vector3.zero;
            _bonus = other.gameObject;
            _bonusUp = true;
        }
    }

    /// <summary>
    /// Реализация падения бонуса
    /// </summary>
    public void DeathHouseMan()
    {
        if (_bonus != null)
        {
            _bonus.transform.parent = _points[1];
            _bonus.transform.localPosition = Vector3.zero;
            _bonus.transform.parent = transform.parent;
            _bonus = null;
            _bonusUp = false;
        }
    }
}
