using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _arch;
    [SerializeField] private Material[] _mats;
    [SerializeField] private GameObject[] _bonus;
    public GameObject[] _archHouseMan;

    private int _variation = 0;
    private int _currentVar = 0;

    private Material _matF;
    private Material _matS;

    private void Awake()
    {
        _matF = _mats[0];
        _matS = _mats[1];
        NewBonus();
    }

    #region �������� ������
    /// <summary>
    /// �������� ������, ��������� ������ � ��������� ������ �� �����
    /// </summary>
    public void NewBonus()
    {
        do
        {
            _variation = Random.Range(0, _arch.Length);
        } while (_variation == _currentVar);

        _currentVar = _variation;
        int rand = Random.Range(0, 101);
        if(rand > 30)
        {
            rand = 0;
        }
        else
        {
            rand = Random.Range(1, 5);
        }

        Material[] _offMat = new Material[2] { _matF, _matF };
        Material[] _onMat = new Material[2] {_matF, _matS };
        for (int i = 0; i < _arch.Length; i++)
        {
            if(i == _variation)
            {
                _arch[i].sharedMaterials = _onMat;
                Vector3 bonusPos = _arch[i].transform.position;
                bonusPos.y += 1;
                Instantiate(_bonus[rand], bonusPos, Quaternion.identity);
            }
            else
            {
                _arch[i].sharedMaterials = _offMat;
            }
        }
    }
    #endregion
}
