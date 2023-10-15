using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnTemplate = null;


    private void OnEnable()
    {
        NPCSpawner.Instance.RegisterSpawnPoint(this);
    }


    private void OnDisable()
    {
        if (NPCSpawner.Exists)
            NPCSpawner.Instance.UnRegisterSpawnPoint(this);
    }


    public GameObject Spawn()
    {
        return Instantiate(_spawnTemplate, transform.position, transform.rotation);
    }
}
