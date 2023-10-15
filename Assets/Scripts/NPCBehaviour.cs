using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : BasicCharacter
{
    private Vector3 _startPosition;


    [SerializeField] private GameObject _trackingTarget;
    [SerializeField] private GameObject _startPositionObject;
    [SerializeField] private float _distanceToGraveRequired = 2f; //serialized for debugging and manual changing
    [SerializeField] private float _waitingTimeRequired = 4f; //ditto distance to grave
    [SerializeField] private bool _hasReachedGrave = false;
    [SerializeField] private bool _isReturning = false;
    private float _waitingTime = 0f;

    private void Start()
    {
        _startPosition = transform.position; 
        Debug.Assert(_trackingTarget!= null, "a tracking target of NPC behaviour is set to null");
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
}
