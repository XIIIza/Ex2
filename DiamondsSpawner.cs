using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiamondsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gem;
    [SerializeField] private Transform _path;
    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_gem, _points[i].position, Quaternion.identity);

            yield return new WaitForSeconds(2);
        }
    }
}