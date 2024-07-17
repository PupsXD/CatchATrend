using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject colliderParent;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float deadZone = 0.1f;
    [SerializeField] private float movementDeadZone = 0.1f;
    private Rigidbody2D _rb;
    
    private float _targetRotation = 0f;
    private float maxRotation = 45f;
    private float rotationSmoothing = 5f;
    private Vector2 _velocity = Vector2.zero;

    private Collider2D _mouseTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mouseTrigger = colliderParent.GetComponent<Collider2D>();
    }
    
    private void Update()
    {
        Move();
        RotateBasket();
        
    
    }
    
    private void Move()
    {
        Vector2 mousePosition = GetMousePosition();
        Vector2 currentPosition = _rb.position;
        Vector2 direction = mousePosition - currentPosition;

        if (Input.GetMouseButton(0) && direction.magnitude > movementDeadZone)
        {
            Vector2 targetVelocity = direction.normalized * speed;
            _rb.velocity = Vector2.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, 0.1f);
        }
        else
        {
            _rb.velocity = Vector2.SmoothDamp(_rb.velocity, Vector2.zero, ref _velocity, 0.1f);
        }

        colliderParent.transform.position = transform.position;
    }

    private void RotateBasket()
    {
        Vector2 mousePosition = GetMousePosition();
        Vector2 basketToMouse = mousePosition - _rb.position;

        if (Input.GetMouseButton(0))
        {
            float angle = Vector2.SignedAngle(Vector2.up, basketToMouse);
            
            float normalizedDistance = Mathf.Clamp01((Mathf.Abs(basketToMouse.x) - deadZone) / (1f - deadZone));
            float smoothAngle = Mathf.Sign(angle) * normalizedDistance * maxRotationAngle;
            
            _targetRotation = -smoothAngle;
        }
        else
        {
            _targetRotation = 0f;
        }

        float newRotation = Mathf.MoveTowards(_rb.rotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(newRotation);
    }
    
    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z -= mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
