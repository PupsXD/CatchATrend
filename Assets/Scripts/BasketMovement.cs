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
        Vector2 mousePosition = GetInputPosition();
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
        Vector2 mousePosition = GetInputPosition();
        Vector2 basketToMouse = mousePosition - _rb.position;

        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended && Input.GetTouch(0).phase != TouchPhase.Canceled))
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
    
    private Vector2 GetInputPosition()
    {
        Vector2 inputPosition;

        if (Input.touchCount > 0)
        {
            // Используем сенсорный ввод на мобильных устройствах
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            // Используем позицию мыши на десктопных устройствах
            inputPosition = Input.mousePosition;
        }

        // Убеждаемся, что координаты находятся в пределах экрана
        inputPosition.x = Mathf.Clamp(inputPosition.x, 0, Screen.width);
        inputPosition.y = Mathf.Clamp(inputPosition.y, 0, Screen.height);

        // Преобразуем экранные координаты в мировые
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, -Camera.main.transform.position.z));
        return new Vector2(worldPosition.x, worldPosition.y);
    }
}
