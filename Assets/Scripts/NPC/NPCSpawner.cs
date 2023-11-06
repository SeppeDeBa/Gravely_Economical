using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class NPCSpawner : MonoBehaviour
{
    #region SINGLETON INSTANCE
    //TODO: Ask if using singleton for this is a bad practice, is there a better way to know where all the graves are?
    //[SerializeField] private static FamilyListScript _familyList; //TODO: Think abt singleton usage 2.0 (check the todo of FamilyListScript.cs
    private static NPCSpawner _instance;

    public static int _currentDay = 0;
    public static int _npcsExist = 0;

    public static NPCSpawner Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<NPCSpawner>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_SpawnManager");
                    _instance = newInstance.AddComponent<NPCSpawner>();
                }
            }
            return _instance;
        }
    }

    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }

    public static bool ApplicationQuitting = false;

    protected virtual void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }
    #endregion



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    public void RegisterSpawnPoint(SpawnPoint spawnPoint)
    {
        if (!_spawnPoints.Contains(spawnPoint))
            _spawnPoints.Add(spawnPoint);
    }
    public void UnRegisterSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Remove(spawnPoint);
    }



    //SPAWNING HAPPENS HERE
    //variables:

    private const float _spawnTimerMax = 4f;
    private float _spawnTimer;

    private const float _dayTimerMax = 30f;
    private float _dayTimer = 0f;

    private bool _daytime = false;
    private bool _daytimeIsOver = false;

    const  float _difficultyScalePerDay = 1.3f;

    [SerializeField] GameObject _directionalLight;
    [SerializeField] GameObject _gateGO;
    private const float _dayTimeRotation = 50f;
    private const float _nightTimeRotation = 170f;
    public void StartDayTime()
    {
        if (!_daytime)
        {
            _gateGO.SetActive(false);
            ++_currentDay;
            _daytime = true;
            _spawnTimer = 0;
            _dayTimer = 0;
            _daytimeIsOver = false;
            _directionalLight.transform.Rotate(-120f, 0f, 0f); //TODO remove magic number
            Debug.Log("StartDayTime is called");
        }
    }




    private void EndDay()
    {

    }

  
    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            SpawnWave();
        }


        //check to end day
        if (_dayTimer > _dayTimerMax && _daytimeIsOver && _npcsExist == 0 && _daytime)
        {
            _gateGO.SetActive(true);
            _daytime = false;
            _directionalLight.transform.Rotate(120f, 0f, 0f);
            Debug.Log("EndDayTime is called");
        }
        // check if dayTime Is over
        else if (_daytime && !_daytimeIsOver && _dayTimer > _dayTimerMax)
        {
            _daytimeIsOver = true;
        }
        //update day Timers if its day
        else if (_daytime && !_daytimeIsOver)
        {
            _dayTimer += Time.deltaTime;
            //creating speed difficulty
            _spawnTimer += Time.deltaTime * Mathf.Pow(_difficultyScalePerDay, _currentDay);
             if (_spawnTimer > _spawnTimerMax)
            {
                SpawnWave();
                _spawnTimer = 0;
            }
        }



        _spawnPoints.RemoveAll(s => s == null);

        while (_spawnPoints.Remove(null)) { }
    }

    public void SpawnWave()
    {
        foreach (SpawnPoint point in _spawnPoints)
        {
            point.Spawn();
        }


        Debug.Log("number of spawnpoints" + _spawnPoints.Count.ToString());
    }


}
