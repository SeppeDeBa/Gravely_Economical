using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField]
    private GameObject _spawnTemplate = null;



    public string InteractionPrompt => _prompt;


    //private void Start()
    //{
    //    _spawnTemplate.GetComponent<>
    //}
    public bool Interact(InteractorBehaviour interactor)
    {
        Debug.Log("Interacting with Spawnpoint");
        NPCSpawner.Instance.StartDayTime();

        return true;
    }

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
        //if (Keyboard.current.kKey.wasPressedThisFrame)
        //{
        //    Spawn();
        //}
    }

    public GameObject Spawn()
    {
        GameObject spawnedGO = Instantiate(_spawnTemplate, transform.position, transform.rotation);

        spawnedGO.GetComponent<NPCBehaviour>().SetStartPositionGO(this.gameObject);
        return spawnedGO;
    }
}
