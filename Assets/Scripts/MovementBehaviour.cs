using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 1.0f;


    bool _isGrounded = false;
    protected const float GROUND_CHECK_DISTANCE = 0.2f;
    protected const string GROUND_LAYER = "Ground";

    private Vector3 _desiredMovementDirection = Vector3.zero;
    private Vector3 _desiredLookatPoint = Vector3.zero;
    private GameObject _target;


    private Rigidbody _rigidBody;
    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }

    public Vector3 DesiredLookatPoint
    {
        get { return _desiredLookatPoint; }
        set { _desiredLookatPoint = value; }
    }
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

     void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
        RotateWithInput();
        //check ground collision
        _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down,
            GROUND_CHECK_DISTANCE, LayerMask.GetMask("Ground"));
    }

    private void HandleMovement()
    {
        Debug.Assert(_rigidBody != null, "Rigidbody does not exist");
        if (_rigidBody == null) return;

        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed;

        //maintain vertical velocity as it was otherwise gravity would be stripped out
       // movement.y = _rigidBody.velocity.y;
        _rigidBody.velocity = movement;



    }
    private void RotateWithInput()
    {
        if (_desiredMovementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(DesiredMovementDirection, Vector3.up);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f); //no limit to how much rotation is allowed;

            _rigidBody.MoveRotation(rotation);
        }

    }

}
