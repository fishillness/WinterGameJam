using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private int _maxRoadCount = 5;

    private List<GameObject> _roads = new List<GameObject>();
    private float _speed = 0;

    void Start()
    {
        ResetLevel();
        StartLevel();
    }


    void Update()
    {
        if (_speed == 0) 
        {
            return;
        }

        foreach (GameObject road in _roads)
        {
            road.transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
        }

        if (_roads[0].transform.position.z < -10)
        {
            Destroy(_roads[0]);
            _roads.RemoveAt(0);
            CreateNextRoad();
        }
    }

    public void ResetLevel()
    {
        _speed = 0;
        
        while (_roads.Count > 0)
        {
            Destroy(_roads[0]);
            _roads.RemoveAt(0);
        }

        for (int i = 0; i < _maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }

    private void StartLevel()
    {
        _speed = _maxSpeed;
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        
        if (_roads.Count > 0)
        {
            pos = _roads[_roads.Count - 1].transform.position + new Vector3(0, 0, 10);
        }

        GameObject go = Instantiate(_roadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        _roads.Add(go);
    }
}
