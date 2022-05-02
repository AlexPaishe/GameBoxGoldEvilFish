using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _startPos;
    private bool _size = false;
    public bool Size
    {
        get
        {
            return _size;
        }

        set
        {
            _size = value;
            if(_size == false)
            {
                _cam.orthographicSize = 210;
                _cam.transform.position = _startPos;
            }
            else
            {
                _cam.orthographicSize = 100;
            }
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Vector3 pos = _player.position;
            pos.y = _cam.transform.position.y;
            _cam.transform.position = pos;
            if (_size == false)
            {
                Size = true;
            }
        }
        else if(_size == true)
        {
            Size = false;
        }
    }
}
