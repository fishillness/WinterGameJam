using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WinterGameJam;

public class MapGenerator : MonoBehaviour, IDependency<RoadGenerator>, IDependency<LevelController>, IDependency<BoxCounter>
{
    [SerializeField] private GameObject SnowmanPrefab;
    [SerializeField] private GameObject SnowdriftPrefab;
    [SerializeField] private GameObject SkiPrefab;
    [SerializeField] private GameObject TimeIncrementPrefab;
    [SerializeField] private GameObject SpeedIncrementPrefab;
    [SerializeField] private GameObject BoxPrefab;

    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [SerializeField] private List<GameObject> activeMaps = new List<GameObject>();

    static public MapGenerator instance;
    
    private int _itemSpace = 15;
    private int _itemCountInMap = 5;
    private float _lineOffset = 2.5f;
    private int _coinsCountInItem = 10;
    private float _coinsHeight = 0.5f;
    private int _mapSize;

    private RoadGenerator roadGenerator;
    public void Construct(RoadGenerator obj) => roadGenerator = obj;
    private LevelController levelController;
    public void Construct(LevelController obj) => levelController = obj;
    private BoxCounter boxCounter;
    public void Construct(BoxCounter obj) => boxCounter = obj;

    enum TrackPos
    {
        Left = -5,
        Center = -2,
        Right = 0
    };

    enum CoinStyle
    {
        Timer,
        Speed,
        Box
    };

    struct MapItem
    {
        public GameObject obstacle;
        public TrackPos trackPos;
        public CoinStyle coinStyle;

        public void SetValues(TrackPos trackPos)
        {
            this.trackPos = trackPos;
        }

        public void SetValues(GameObject obstacle, TrackPos trackPos)
        {
            this.obstacle = obstacle;
            this.trackPos = trackPos;
        }

        public void SetValues(TrackPos trackPos, CoinStyle coinStyle)
        {
            this.trackPos = trackPos;
            this.coinStyle = coinStyle;
        }

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
        _mapSize = _itemCountInMap * _itemSpace;
        maps.Add(MakeMap1());
        maps.Add(MakeMap2());
        maps.Add(MakeMap3());

        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }
    }

    void Update()
    {
        if (RoadGenerator.instance.currentSpeed == 0)
        {
            return;
        }

        foreach (GameObject map in activeMaps)
        {
            map.transform.position -= new Vector3(0, 0, RoadGenerator.instance.currentSpeed * Time.deltaTime);
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
                SetRoadGenerationInSpeedChangingItem(go);
                go.transform.SetParent(result.transform);
            }

        }
        return result;
    }

    private GameObject MakeMap2()
    {
        GameObject result = new GameObject("Map2");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();

        for (int i = 0; i < _itemCountInMap; i++)
        {
            item.SetValues(TrackPos.Right);

            if (i == 2)
            {
                item.SetValues(SnowmanPrefab, TrackPos.Right, CoinStyle.Speed);
            }
            else if (i == 4)
            {
                item.SetValues(SnowdriftPrefab, TrackPos.Center, CoinStyle.Timer);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos + _lineOffset, 0, i * _itemSpace);
            CreateCoins(item.coinStyle, obstaclePos, result);

            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                SetRoadGenerationInSpeedChangingItem(go);
                go.transform.SetParent(result.transform);
            }

        }
        return result;
    }

    private GameObject MakeMap3()
    {
        GameObject result = new GameObject("Map2");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();

        for (int i = 0; i < _itemCountInMap; i++)
        {
            item.SetValues(TrackPos.Right, CoinStyle.Speed);

            if (i == 2)
            {
                item.SetValues(TrackPos.Left, CoinStyle.Timer);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos + _lineOffset, 0, i * _itemSpace);
            CreateCoins(item.coinStyle, obstaclePos, result);

            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                SetRoadGenerationInSpeedChangingItem(go);
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
                SetLevelControllerInTimeChangingItem(go);
                go.transform.SetParent(parentObj.transform);
            }
        }
        else if (style == CoinStyle.Speed)
        {
            for (int i = -_coinsCountInItem / 2; i < _coinsCountInItem / 2; i++)
            {
                // coinPos.y = Mathf.Max(-1/2f * Mathf.Pow(i,2)+3, _coinsHeight);
                coinPos.y = _coinsHeight;
                coinPos.z = i * ((float)_itemSpace / _coinsCountInItem);
                GameObject go = Instantiate(SpeedIncrementPrefab, coinPos + pos, Quaternion.identity);
                SetRoadGenerationInSpeedChangingItem(go);
                go.transform.SetParent(parentObj.transform);
            }
        }
        else if (style == CoinStyle.Box)
        {
            for (int i = -_coinsCountInItem / 2; i < _coinsCountInItem / 2; i++)
            {
                coinPos.y = _coinsHeight;
                coinPos.z = i * ((float)_itemSpace / _coinsCountInItem);
                GameObject go = Instantiate(BoxPrefab, coinPos + pos, Quaternion.identity);
                SetBoxCounterInBox(go);
                go.transform.SetParent(parentObj.transform);
            }
        }
    }

    private void SetRoadGenerationInSpeedChangingItem(GameObject obj)
    {
        SpeedChangingItem_New speedChanging = obj.GetComponent<SpeedChangingItem_New>();
        if (speedChanging)
            speedChanging.SetRoadGenerator(roadGenerator);
    }

    private void SetLevelControllerInTimeChangingItem(GameObject obj)
    {
        TimeChangingItem timeChanging = obj.GetComponent<TimeChangingItem>();
        if (timeChanging)
            timeChanging.SetLevelController(levelController);
    }

    private void SetBoxCounterInBox(GameObject obj)
    {
        Box box = obj.GetComponent<Box>();
        if (box)
            box.SetBoxCounter(boxCounter);
    }
}
