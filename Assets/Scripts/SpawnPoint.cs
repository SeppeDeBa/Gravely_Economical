using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnTemplate = null;

    //private void Start()
    //{
    //    _spawnTemplate.GetComponent<>
    //}

    private void OnEnable()
    {
        NPCSpawner.Instance.RegisterSpawnPoint(this);
    }


    private void OnDisable()
    {
        if (NPCSpawner.Exists)
            NPCSpawner.Instance.UnRegisterSpawnPoint(this);
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Spawn();
        }
    }

    public GameObject Spawn()
    {
        GameObject spawnedGO = Instantiate(_spawnTemplate, transform.position, transform.rotation);

        spawnedGO.GetComponent<NPCBehaviour>().SetStartPositionGO(this.gameObject);
        return spawnedGO;
    }
}
