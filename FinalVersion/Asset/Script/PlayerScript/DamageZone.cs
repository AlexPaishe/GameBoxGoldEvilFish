using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private Transform _trans;
    [SerializeField] private GameObject _obj;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _radius;

    void Update()
    {
        if(_trans.localScale != _radius)
        {
            _trans.localScale = Vector3.MoveTowards(_trans.localScale, _radius, _speed * Time.deltaTime);
        }
        else
        {
            Destroy(_obj);
        }
    }
}
