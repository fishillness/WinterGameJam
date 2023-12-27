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

    [Header("DEBUG")]
    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [SerializeField] private List<GameObject> activeMaps = new List<GameObject>();

    private int _itemCountInMap = 10;

    static public MapGenerator instance;
    
    private int _defaultItemSpace = 5;
    
    private float _lineOffset = 2.5f;
    
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

    struct MapItem
    {
        public TrackPos trackPos;
        public GameObject obstacle;
        public GameObject item;
        public int itemCount;

        public void SetValues(GameObject obstacle, GameObject item, TrackPos trackPos, int itemCount=0)
        {
            this.obstacle = obstacle;
            this.trackPos = trackPos;
            this.item = item;
            this.itemCount = itemCount;
        }
    }

    private void Awake()
    {
        instance = this;
        _mapSize = _itemCountInMap * _defaultItemSpace;
        maps.Add(MakeMap1());
        maps.Add(MakeMap2());
        maps.Add(MakeMap3());

        maps.Add(MakeMap1());
        maps.Add(MakeMap3());
        maps.Add(MakeMap1());

        maps.Add(MakeMap2());
        maps.Add(MakeMap1());
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
            item.SetValues(null, BoxPrefab, TrackPos.Center, 5);

            switch (i)
            {
                case 2:
                    item.SetValues(SnowmanPrefab, null, TrackPos.Center);
                    break;
                case 3:
                    item.SetValues(SkiPrefab, null, TrackPos.Right);
                    break;
                case 4:
                    item.SetValues(null, TimeIncrementPrefab, TrackPos.Right, 5);
                    break;
                case 5:
                    item.SetValues(null, BoxPrefab, TrackPos.Left, 10);
                    break;
                case 6:
                    item.SetValues(SnowmanPrefab, null, TrackPos.Left);
                    break;
                case 7:
                    item.SetValues(SkiPrefab, null, TrackPos.Right);
                    break;
                case 8:
                    item.SetValues(SkiPrefab, null, TrackPos.Right);
                    break;
                case 9:
                    item.SetValues(null, SpeedIncrementPrefab, TrackPos.Left, 1);
                    break;
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos + _lineOffset, 0.5f, i * _defaultItemSpace);
            CreateItems(item.item, obstaclePos, result, item.itemCount);

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
            item.SetValues(null, SpeedIncrementPrefab, TrackPos.Right, 1);

            switch (i)
            {
                case 2:
                    item.SetValues(null, TimeIncrementPrefab, TrackPos.Left, 2);
                    break;
                case 3:
                    item.SetValues(SnowmanPrefab, null, TrackPos.Left);
                    break;
                case 4:
                    item.SetValues(null, TimeIncrementPrefab, TrackPos.Center, 10);
                    break;
                case 5:
                    item.SetValues(null, BoxPrefab, TrackPos.Right, 5);
                    break;
                case 6:
                    item.SetValues(null, TimeIncrementPrefab, TrackPos.Left);
                    break;
                case 8:
                    item.SetValues(SkiPrefab, null, TrackPos.Right);
                    break;
                case 9:
                    item.SetValues(null, SpeedIncrementPrefab, TrackPos.Left, 1);
                    break;
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos + _lineOffset, 0.5f, i * _defaultItemSpace);
            CreateItems(item.item, obstaclePos, result, item.itemCount);

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
        GameObject result = new GameObject("Map3");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();

        for (int i = 0; i < _itemCountInMap; i++)
        {
            item.SetValues(SkiPrefab, null, TrackPos.Left);

            switch (i)
            {
                case 2:
                    item.SetValues(null, BoxPrefab, TrackPos.Center, 15);
                    break;
                case 3:
                    item.SetValues(SnowmanPrefab, null, TrackPos.Left);
                    break;
                case 4:
                    item.SetValues(null, TimeIncrementPrefab, TrackPos.Right, 10);
                    break;
                case 5:
                    item.SetValues(SnowmanPrefab, null, TrackPos.Center, 5);
                    break;
                case 6:
                    item.SetValues(null, TimeIncrementPrefab, TrackPos.Left);
                    break;
                case 8:
                    item.SetValues(SkiPrefab, null, TrackPos.Right);
                    break;
                case 9:
                    item.SetValues(null, SpeedIncrementPrefab, TrackPos.Left, 1);
                    break;
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos + _lineOffset, 0.5f, i * _defaultItemSpace);
            CreateItems(item.item, obstaclePos, result, item.itemCount);

            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                SetRoadGenerationInSpeedChangingItem(go);
                go.transform.SetParent(result.transform);
            }
        }
        return result;
    }

    private void CreateItems(GameObject item, Vector3 pos, GameObject parentObj, int itemsCount)
    {
        Vector3 coinPos = Vector3.zero;

        if (item == TimeIncrementPrefab)
        {
            for (int i = -itemsCount / 2; i < itemsCount / 2; i++)
            {
                coinPos.y = _coinsHeight;
                coinPos.z = i * ((float)_defaultItemSpace / itemsCount);
                GameObject go = Instantiate(TimeIncrementPrefab, coinPos + pos, Quaternion.identity);                
                SetLevelControllerInTimeChangingItem(go);
                go.transform.SetParent(parentObj.transform);
            }
        }
        else if (item == SpeedIncrementPrefab)
        {
            for (int i = -itemsCount / 2; i < itemsCount / 2; i++)
            {
                coinPos.y = _coinsHeight;
                coinPos.z = i * ((float)_defaultItemSpace / itemsCount);
                GameObject go = Instantiate(SpeedIncrementPrefab, coinPos + pos, Quaternion.identity);
                SetRoadGenerationInSpeedChangingItem(go);
                go.transform.SetParent(parentObj.transform);
            }
        }
        else if (item == BoxPrefab)
        {
            for (int i = -itemsCount / 2; i < itemsCount / 2; i++)
            {
                coinPos.y = _coinsHeight;
                coinPos.z = i * ((float)_defaultItemSpace / itemsCount);
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
