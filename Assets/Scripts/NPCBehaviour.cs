using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : BasicCharacter
{
    private Vector3 _startPosition;
    //[SerializeField]
    //string _familyName;
    //[SerializeField]
    //Color _familyColor;
    [SerializeField] Material _materialToColor = null;
    [SerializeField] FamilyInfoStruct  _familyInfo = null;

    [SerializeField] private GameObject _trackingTarget;
    [SerializeField] private GameObject _startPositionObject;
    [SerializeField] private float _distanceToGraveRequired = 2f; //serialized for debugging and manual changing
    [SerializeField] private float _waitingTimeRequired = 4f; //ditto distance to grave
    [SerializeField] private bool _hasReachedGrave = false;
    [SerializeField] private bool _isReturning = false;
    private float _waitingTime = 0f;
    public NPCBehaviour(FamilyInfoStruct familyInfo)
    {
        _familyInfo = familyInfo;
    }

    private void Start()
    {
        if (_familyInfo != null)
        {
            //TODO: How to do without changing the material color? Is there instancing?
            _familyInfo = FamilyListScript.GetRandomFamilyInfoStruct(); // familyListScript is a singleton
        }
        _materialToColor.color = _familyInfo._familyColor;
        _startPosition = transform.position; 
        Debug.Assert(_trackingTarget!= null, "a tracking target of NPC behaviour is set to null");
        _movementBehaviour.SetMovementSpeed(_familyInfo._familySpeed);
    
    }


    private void Update()
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

    }
    void CheckIfGraveIsReached()
    {
        if ((transform.position - _movementBehaviour.Target.transform.position).sqrMagnitude < _distanceToGraveRequired)
        {
            _hasReachedGrave = true;
           
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
        Gizmos.color = _familyInfo._familyColor;
        Gizmos.DrawWireSphere(_trackingTarget.transform.position, 2f); //debug draw location to walk to
    }

}
