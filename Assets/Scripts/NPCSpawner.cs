using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    #region SINGLETON INSTANCE
    //TODO: Ask if using singleton for this is a bad practice, is there a better way to know where all the graves are?
    //[SerializeField] private static FamilyListScript _familyList; //TODO: Think abt singleton usage 2.0 (check the todo of FamilyListScript.cs
    private static NPCSpawner _instance;

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

    private void Update()
    {
        _spawnPoints.RemoveAll(s => s == null);

        while (_spawnPoints.Remove(null)) { }
    }

    public void SpawnWave()
    {
        foreach (SpawnPoint point in _spawnPoints)
        {
            point.Spawn();
        }
    }


}
