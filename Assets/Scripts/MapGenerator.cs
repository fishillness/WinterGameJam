using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject SnowmanPrefab;
    [SerializeField] private GameObject SnowdriftPrefab;
    [SerializeField] private GameObject SkiPrefab;
    [SerializeField] private GameObject TimeIncrementPrefab;
    [SerializeField] private GameObject SpeedIncrementPrefab;

    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [SerializeField] private List<GameObject> activeMaps = new List<GameObject>();

    static public MapGenerator instance;
    
    private int _itemSpace = 15;
    private int _itemCountInMap = 5;
    private float _lineOffset = 2.5f;
    private int _coinsCountInItem = 10;
    private float _coinsHeight = 0.5f;
    private int _mapSize;

    enum TrackPos
    {
        Left = -1,
        Center = 0,
        Right = 1
    };

    enum CoinStyle
    {
        Timer,
        Speed,
    };

    struct MapItem
    {
        public GameObject obstacle;
        public TrackPos trackPos;
        public CoinStyle coinStyle;

        public void SetValues(GameObject obstacle, TrackPos trackPos, CoinStyle coinStyle)
        {
            this.obstacle = obstacle;
            this.trackPos = trackPos;
            this.coinStyle = coinStyle;
        }
    }


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        _mapSize = _itemCountInMap * _itemSpace;
        maps.Add(MakeMap1());
        maps.Add(MakeMap1());
        maps.Add(MakeMap1());

        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }
    }
    

    void Update()
    {
        if (RoadGenerator.instance.speed == 0)
        {
            return;
        }

        foreach (GameObject map in activeMaps)
        {
            map.transform.position -= new Vector3(0, 0, RoadGenerator.instance.speed * Time.deltaTime);
        }

        if (activeMaps[0].transform.position.z < -_mapSize)
        {
            RemoveFirstActiveMap();
            AddActiveMap();
        }
    }

    public void ResetMaps()
    {
        while (activeMaps.Count > 0)
        {
            RemoveFirstActiveMap();
        }
        AddActiveMap();
        AddActiveMap();
    }

    private void AddActiveMap()
    {
        int r = Random.Range(0, maps.Count);
        GameObject go = maps[r];
        go.SetActive(true);
        foreach (Transform child in go.transform)
        {
            child.gameObject.SetActive(true);
        }
        go.transform.position = activeMaps.Count > 0 ?
            activeMaps[activeMaps.Count - 1].transform.position + Vector3.forward * _mapSize : 
            new Vector3(0, 0, 10);
        
        maps.RemoveAt(r);
        activeMaps.Add(go);
    }

    private void RemoveFirstActiveMap()
    {
        activeMaps[0].SetActive(false);
        maps.Add(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }

    private GameObject MakeMap1()
    {
        GameObject result = new GameObject("Map1");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();

        for (int i = 0; i < _itemCountInMap; i++)
        {
            item.SetValues(null, TrackPos.Center, CoinStyle.Timer);


            if (i == 2)
            {
                item.SetValues(SnowmanPrefab, TrackPos.Left, CoinStyle.Timer);
            }
            else if (i == 3)
            {
                item.SetValues(SnowdriftPrefab, TrackPos.Right, CoinStyle.Speed);
            }
            else if (i == 4)
            {
                item.SetValues(SkiPrefab, TrackPos.Right, CoinStyle.Timer);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos + _lineOffset, 0, i * _itemSpace);
            CreateCoins(item.coinStyle, obstaclePos, result);

            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                go.transform.SetParent(result.transform);
            }

        }
        return result;
    }

    private void CreateCoins(CoinStyle style, Vector3 pos, GameObject parentObj)
    {
        Vector3 coinPos = Vector3.zero;
        if (style == CoinStyle.Timer)
        {
            for (int i = -_coinsCountInItem/2; i < _coinsCountInItem/2; i++)
            {
                coinPos.y = _coinsHeight;
                coinPos.z = i * ((float)_itemSpace / _coinsCountInItem);
                GameObject go = Instantiate(TimeIncrementPrefab, coinPos + pos, Quaternion.identity);
                go.transform.SetParent(parentObj.transform);
            }
        }
        else if (style == CoinStyle.Speed)
        {
            for (int i = -_coinsCountInItem / 2; i < _coinsCountInItem / 2; i++)
            {
                coinPos.y = Mathf.Max(-1/2f * Mathf.Pow(i,2)+3, _coinsHeight);
                coinPos.z = i * ((float)_itemSpace / _coinsCountInItem);
                GameObject go = Instantiate(SpeedIncrementPrefab, coinPos + pos, Quaternion.identity);
                go.transform.SetParent(parentObj.transform);
            }
        }
    }
}
