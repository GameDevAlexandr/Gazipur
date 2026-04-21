using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _crouchSpeed = 2.5f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _gravity = 20f;

    [Header("Crouch Settings")]
    [SerializeField] private float _crouchHeight = 0.5f;
    [SerializeField] private float _standingHeight = 1f;
    [SerializeField] private float _crouchTransitionSpeed = 8f;

    [Header("Camera")]
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private float _cameraHeightNormal = 0.8f;
    [SerializeField] private float _cameraHeightCrouch = 0.4f;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckDistance = 0.2f;

    private CharacterController _controller;
    private Vector3 _velocity = Vector3.zero;
    private float _currentSpeed;
    private float _xRotation = 0f;
    private bool _isCrouching = false;
    private bool _wantsToCrouch = false;
    private float _currentCameraHeight;
    private bool _hasJumped = false;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        _standingHeight = _controller.height;
        _controller.height = _standingHeight;
        _currentCameraHeight = _cameraHeightNormal;
        UpdateCameraPosition();
    }

    void Update()
    {
        HandleMouseLook();
        HandleCrouchInput();
        HandleCrouch();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        UpdateCameraPosition();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        if (_playerCamera != null)
        {
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }

    bool IsGrounded()
    {
        float rayLength = (_controller.height / 2) + _groundCheckDistance;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle <= _controller.slopeLimit)
                return true;
        }
        return false;
    }

    bool CanStandUp()
    {
        float checkDistance = _standingHeight - _controller.height;
        if (checkDistance <= 0.05f) return true;

        Vector3 checkStart = transform.position + Vector3.up * _controller.height;
        return !Physics.Raycast(checkStart, Vector3.up, checkDistance);
    }

    void HandleCrouchInput()
    {
        _wantsToCrouch = Input.GetKey(KeyCode.LeftControl);
    }

    void HandleCrouch()
    {
        float targetHeight;
        float targetCameraHeight;

        if (_wantsToCrouch)
        {
            targetHeight = _crouchHeight;
            targetCameraHeight = _cameraHeightCrouch;
            _isCrouching = true;
        }
        else
        {
            if (CanStandUp())
            {
                targetHeight = _standingHeight;
                targetCameraHeight = _cameraHeightNormal;
                _isCrouching = false;
            }
            else
            {
                targetHeight = _crouchHeight;
                targetCameraHeight = _cameraHeightCrouch;
                _isCrouching = true;
            }
        }

        float newHeight = Mathf.Lerp(_controller.height, targetHeight, _crouchTransitionSpeed * Time.deltaTime);
        _controller.height = newHeight;

        _currentCameraHeight = Mathf.Lerp(_currentCameraHeight, targetCameraHeight, _crouchTransitionSpeed * Time.deltaTime);

        AdjustPositionToGround();
    }

    void AdjustPositionToGround()
    {
        RaycastHit hit;
        float checkDistance = _controller.height / 2 + 0.1f;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance))
        {
            Vector3 newPos = transform.position;
            float targetY = hit.point.y + (_controller.height / 2);

            if (Mathf.Abs(newPos.y - targetY) > 0.01f)
            {
                newPos.y = targetY;
                transform.position = newPos;
            }
        }
    }

    void UpdateCameraPosition()
    {
        //if (_playerCamera == null) return;

        //Vector3 cameraPos = _playerCamera.localPosition;
        //cameraPos.y = _currentCameraHeight;
        //_playerCamera.localPosition = cameraPos;
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (_isCrouching)
            _currentSpeed = _crouchSpeed;
        else if (Input.GetKey(KeyCode.LeftShift))
            _currentSpeed = _runSpeed;
        else
            _currentSpeed = _walkSpeed;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        Vector3 movement = moveDirection * _currentSpeed * Time.deltaTime;
        _controller.Move(movement);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && !_isCrouching && IsGrounded() && !_hasJumped)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * 2f * _gravity);
            _hasJumped = true;
        }

        if (IsGrounded() && _velocity.y <= 0)
        {
            _hasJumped = false;
        }
    }

    void ApplyGravity()
    {
        _velocity.y -= _gravity * Time.deltaTime;
        Vector3 verticalMove = new Vector3(0, _velocity.y, 0) * Time.deltaTime;
        _controller.Move(verticalMove);
    }
}