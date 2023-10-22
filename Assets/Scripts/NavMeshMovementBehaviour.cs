using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovementBehaviour : MovementBehaviour
{
    protected NavMeshAgent _navMeshAgent;
    protected Vector3 _spawnPoint;
    protected Vector3 _previousTargetPosition = Vector3.zero; 

    protected override void Awake()
    {
        base.Awake();
        _navMeshAgent= GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;
        _previousTargetPosition = transform.position;
        _spawnPoint = transform.position;
    }

    const float MOVEMENT_EPSILON = 0.25f;


    private void Update()
    {
        if (_navMeshAgent.speed != _movementSpeed)
        {
            _navMeshAgent.speed = _movementSpeed;
        }

    }

    protected override void HandleMovement()
    {
        if (_target == null)
        {
            _navMeshAgent.isStopped = true;
            return;
        }

        //if target moves, we recalculate. safety check
        if ((_target.transform.position - _previousTargetPosition).sqrMagnitude > MOVEMENT_EPSILON )
        {
            _navMeshAgent.SetDestination(_target.transform.position );
            _navMeshAgent.isStopped = false;
            _previousTargetPosition = _target.transform.position;
        }
     
    }


}
