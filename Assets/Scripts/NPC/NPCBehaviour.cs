using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : BasicCharacter, IInteractable
{

    [SerializeField] private string _prompt;
    private Vector3 _startPosition;

    [SerializeField] Canvas _shopManagerOwner;

    ShopManager _shopManager = null;
    //[SerializeField]
    //string _familyName;
    //[SerializeField]
    //Color _familyColor;
    [SerializeField] FamilyInfoStruct  _familyInfo = null;
    [SerializeField] private int _coinsPerSuccess = 5;
    [SerializeField] private GameObject _trackingTarget;
    [SerializeField] private GameObject _startPositionObject;
    [SerializeField] private float _distanceToGraveRequired = 2f; //serialized for debugging and manual changing
    [SerializeField] private float _waitingTimeRequired = 4f; //ditto distance to grave
    [SerializeField] private bool _hasReachedGrave = false;
    [SerializeField] private bool _isReturning = false;
    private float _waitingTime = 0f;

    private bool _isStalled = false;
    private float _stallTimer = 0f;
    private const float _stallTimerMax = 3f;



    public string InteractionPrompt => _prompt;


    public bool Interact(InteractorBehaviour interactor)
    {
        Debug.Log("Interacted with npc");
        if (!_isReturning)
        {
        _isStalled = true;
        _movementBehaviour._isPaused = true;
        _movementBehaviour.SetMovementSpeed(0);
        _stallTimer = 0f;

        }
        else
        {
            _movementBehaviour.SetMovementSpeed(10000);
        }
        return true;
        //TODO: ASK WHY I DO NOT HAVE CONTROL OVER NAVMESH
        
    }



    public NPCBehaviour(FamilyInfoStruct familyInfo)
    {
        _familyInfo = familyInfo;
    }

    private void Start()
    {
        NPCSpawner._npcsExist += 1;
       //set shop manager owner
       //_shopManager = _shopManagerOwner.GetComponentInChildren<ShopManager>();
       _shopManager = GameObject.Find("Shop").GetComponentInChildren<ShopManager>();
        Debug.Assert(_shopManager!= null, "shopmanager is not found in NPCBehaviour");


       if (_movementBehaviour == null)
        {

            Debug.Log("mov behaviour is null");
        }
        if (_familyInfo != null)
        {
            //TODO: How to do without changing the material color? Is there instancing?
            _familyInfo = FamilyListScript.GetRandomFamilyInfoStruct(); // familyListScript is a singleton
        }
        else
        {
            Debug.Assert(false, "_famInfo = null");
        }

        _trackingTarget = GraveManager.GetFamilyTargetGameObject(_familyInfo);
        GetComponentInChildren<Renderer>().material.SetColor("_BaseColor",_familyInfo._familyColor);
        _startPosition = transform.position; 
        Debug.Assert(_trackingTarget!= null, "a tracking target of NPC behaviour is set to null");

        Debug.Log( _familyInfo._familySpeed.ToString());//debug to see if number is properly passed
        _movementBehaviour.SetMovementSpeed(_familyInfo._familySpeed); //TODO: ASK WHY MOVSPEED CHANGE IS NOT WORKING
        HandleMovement();
    }

    private void OnDestroy()
    {
        NPCSpawner._npcsExist -= 1;
    }

    private void Update()
    {
        if (!_isStalled)
        {
            if (!_hasReachedGrave)
            {
                HandleMovement();
                CheckIfGraveIsReached();
            }
            else if (!_isReturning)
            {
                HandleWaiting();
            }
            else
            {
                DespawnOnReturning();
            }
        }
        else
        {
            _stallTimer += Time.deltaTime;
            if (_stallTimer > _stallTimerMax + ShopManager._stallTimerIncrease)
            {
                _stallTimer = 0;
                _isStalled = false;
                _movementBehaviour._isPaused = false;
                _movementBehaviour.ResetMovementSpeed();
            }
        }

    }



    void DespawnOnReturning()
    {
        if ((transform.position - _movementBehaviour.Target.transform.position).sqrMagnitude < _distanceToGraveRequired) //TODO: Test more and possibly rename distanceToGrave to distanceToTarget
        {

            Destroy(this.gameObject);
        }
    }

    void CheckIfGraveIsReached()
    {
        if (_movementBehaviour.Target == null)
        {
            Debug.Assert(_movementBehaviour.Target != null, "Target is null for: " + _familyInfo._familyName);
        }
        if ((transform.position - _movementBehaviour.Target.transform.position).sqrMagnitude < _distanceToGraveRequired) 
        {
            _hasReachedGrave = true;

            CorpseInventory targetCorpseInvent = _trackingTarget.GetComponentInParent<CorpseInventory>();

            Debug.Log("Trying to add coins: " + _coinsPerSuccess.ToString()) ;
            if (targetCorpseInvent == null) { Debug.Assert(false, "TrackingTarget does not have a targetFamilyInfo"); } //seperating the if check and assert for extra safety


            else if (_familyInfo._familyName == targetCorpseInvent.GetCorpseName())
            {
                _shopManager.AddCoins(_coinsPerSuccess);
                Debug.Log("Adding bonus coins");
            }
            else
            {
                //_shopManager.AddCoins(-_coinsPerSuccess);
                Debug.Log("Adding negative coins");
            }

            
        }
    }
    void HandleMovement()
    {
        if (_movementBehaviour == null) return;//safety check

        _movementBehaviour.Target = _trackingTarget;
    }

    void HandleWaiting()
    {
        _waitingTime += Time.deltaTime;
        if (_waitingTime >= _waitingTimeRequired)
        {
          
            _isReturning = true;
            _movementBehaviour.Target = _startPositionObject;
        }

    }

    private void OnDrawGizmos()
    {
        if(_trackingTarget)
        {
        Gizmos.color = _familyInfo._familyColor;
        Gizmos.DrawWireSphere(_trackingTarget.transform.position, 2f); //debug draw location to walk 
        }
    }

    public void SetStartPositionGO(GameObject targetGO)
    {
        _startPositionObject = targetGO;
    }
}
