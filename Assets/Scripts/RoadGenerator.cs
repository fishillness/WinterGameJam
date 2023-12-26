using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private int _maxRoadCount = 5;

    [Header("Speed")]
    [SerializeField] private float startingSpeed = 10;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;

    public float currentSpeed = 0;
    static public RoadGenerator instance;
    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;  

    private List<GameObject> _roads = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetLevel();
        StartLevel();
    }


    void Update()
    {
        if (currentSpeed == 0) 
        {
            return;
        }

        foreach (GameObject road in _roads)
        {
            road.transform.position -= new Vector3(0, 0, currentSpeed * Time.deltaTime);
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
        currentSpeed = 0;
        
        while (_roads.Count > 0)
        {
            Destroy(_roads[0]);
            _roads.RemoveAt(0);
        }

        for (int i = 0; i < _maxRoadCount; i++)
        {
            CreateNextRoad();
        }

        MapGenerator.instance.ResetMaps();
    }

    private void StartLevel()
    {
        currentSpeed = startingSpeed;
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

    public void ChangeSpeed(float value)
    {
        currentSpeed += value;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    public void SetStartingSpeed()
    {
        currentSpeed = startingSpeed;
    }
}
